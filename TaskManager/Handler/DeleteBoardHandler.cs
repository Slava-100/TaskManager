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
    public class DeleteBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back":
                            userService.SetHandler(new MainMenuHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "Да":
                            //DeleteBoard();
                            userService.SetHandler(new WorkWithBoardHandler());
                            break;
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:

                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id,"Ты точно хочешь удалить эту доску?", replyMarkup: Button());
        }

        private InlineKeyboardMarkup Button()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Да") {CallbackData = "ShowTasks"},
                        new InlineKeyboardButton("нет") {CallbackData="Back"},
                    },
                    new[]
                    {
                        new InlineKeyboardButton("Назад") {CallbackData = "Back"},
                    }
                });

            return keyboard;
        }
    }
}
