using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class WorkWithBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToMainMenu":
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
                        int number;
                        if (int.TryParse(text, out number))
                        {
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
                        else
                        {
                            userService.TgClient.SendTextMessageAsync(userService.Id, "Введи числовое значение номера доски!");
                        }
                    }
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(ClientService userService)
        {
            if (GetAllBoards(userService) != "Список всех досок:\n")
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, $"{GetBoardsClient(userService)} \nВведите номер доски для работы с ней!", replyMarkup: ButtonBack());
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, "Досок не существует!", replyMarkup: ButtonBack());
            }
        }

        private string GetAllBoards(ClientService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            string s = "Список всех досок:\n";
            foreach (KeyValuePair<int, Board> i in dataStorage.Boards)
            {
                s = s + $"Название доски: {i.Value.NameBoard} Номер доски: {i.Value.NumberBoard} \n";
            }

            return s;
        }

        private string WhriteBoardsAdmin(ClientService userService)
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

        private string WhriteBoardsMember(ClientService userService)
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

        private string GetBoardsClient(ClientService userService)
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
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "BackToMainMenu" };
            return keyboard;
        }
    }
}
