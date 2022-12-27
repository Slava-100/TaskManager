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
                        case "Back0":
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
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер доски, попробуйте ещё раз", replyMarkup: DefaultButton());
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
                                userService.TgClient.SendTextMessageAsync(userService.Id, "Ты не являешься участником данной доски!", replyMarkup: DefaultButton());
                            }
                        }
                        else
                        {
                            userService.TgClient.SendTextMessageAsync(userService.Id, "Такой доски не существует!", replyMarkup: DefaultButton());
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
                //userService.TgClient.SendTextMessageAsync(userService.Id, $"{GetAllBoards(userService)} \nВведите номер доски для работы с ней!", replyMarkup: ButtonBack());
                userService.TgClient.SendTextMessageAsync(userService.Id, $"{GetBoardsClient(userService)} \nВведите номер доски для работы с ней!", replyMarkup: ButtonBack());
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

        private string WhriteBoardsAdmin(UserService userService)
        {
            List<Board> boardsForAdmin = userService.ClientUserService.GetAllBoardsAdmins();
            string s = "Список всех досок c правами Администратора:\n";
            if (boardsForAdmin.Count > 0)
            {
                foreach (var board in boardsForAdmin)
                {
                    s += board.ToString() + "\n";
                }
            }
            else
            {
                s += "Досок c правами Администратора у Вас нет(";
            }

            return s;
        }

        private string WhriteBoardsMember(UserService userService)
        {
            List<Board> boardsForAdmin = userService.ClientUserService.GetAllBoardsMembers();
            string s = "Список всех досок c правами обычного пользователя:\n";
            if (boardsForAdmin.Count > 0)
            {
                foreach (var board in boardsForAdmin)
                {
                    s += board.ToString() + "\n";
                }
            }
            else
            {
                s += "Досок c правами пользователя у Вас нет(\n";
            }

            return s;
        }

        private string GetBoardsClient(UserService userService)
        {
            return $"{WhriteBoardsAdmin(userService)}\n{WhriteBoardsMember(userService)}";
        }

        private InlineKeyboardMarkup DefaultButton()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "default" };
            return keyboard;
        }

        private InlineKeyboardMarkup ButtonBack()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back0" };
            return keyboard;
        }
    }
}
