using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Handl;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;

namespace TaskManager.Handler
{
    public class WorkWithBoardHandler : IHandler
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
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text is null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер доски, попробуйте ещё раз", replyMarkup: ButtonBack());
                    }
                    else
                    {
                        string text = update.Message.Text;
                        int number = Convert.ToInt32(text);
                        if (dataStorage.Boards.ContainsKey(number)) 
                        {
                            if (userService.ClientUserService.BoardsForUser.Contains(number))
                            {
                                userService.ClientUserService.SetActiveBoard(number);
                                userService.SetHandler(new BoardHandler());
                                userService.HandleUpdate(update);
                            }
                            else
                            {
                                userService.TgClient.SendTextMessageAsync(userService.Id, "Ты не являешься участником данной доски!", replyMarkup: ButtonBack());
                            }
                        }
                        else
                        {
                            userService.TgClient.SendTextMessageAsync(userService.Id, "Такой доски не существует!", replyMarkup: ButtonBack());
                        }
                    }
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            if (GetAllBoards(userService) != "Список всех досок:\n")
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, $"{GetAllBoards(userService)} \nВведите номер доски для работы с ней!", replyMarkup: ButtonBack());
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, "Досок не существует!", replyMarkup: ButtonBack());
            }
        }

        private string GetAllBoards(UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            string s = "Список всех досок:\n";
            foreach (KeyValuePair<int, Board> i in dataStorage.Boards)
            {
                s = s + $"Название доски: {i.Value.NameBoard} Номер доски: {i.Value.NumberBoard} \n";
            }

            return s;
        }

        private InlineKeyboardMarkup ButtonBack()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
            return keyboard;
        }
    }
}
