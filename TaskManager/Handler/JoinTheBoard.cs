using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class JoinTheBoard : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back":
                            userService.SetHandler(new MainMenuHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            SubmitsQuestion(userService, update);
                            break;
                    }
                    break;
                default:
                    SubmitsQuestion(userService, update);
                    break;
            }
        }


        private void SubmitsQuestion(UserService userService, Update update)
        {
            if (GetAllBoards(userService).Length != 0)
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, $"{GetAllBoards(userService)} Введите номер доски");
            }
            else
            {
                InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
                userService.TgClient.SendTextMessageAsync(userService.Id, "Досок не существует!", replyMarkup: keyboard);
            }
        }

        private string GetAllBoards(UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            string s = "";
            foreach (KeyValuePair<int, Board> i in dataStorage.Boards)
            {
                s = s + $"Название доски: {i.Value.NameBoard} Номер доски: {i.Value.NameBoard} \n";
            }

            return s;
        }
    }
}
