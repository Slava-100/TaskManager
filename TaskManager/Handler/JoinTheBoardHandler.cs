using System.Linq;
using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TaskManager.Handler
{
    public class JoinTheBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise)
        {
                       List<Board> boards = userServise.ClientUserService.GetAllBoardsToWhichYouCanJoin();

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberBoard)
                        && boards.Any(crntBoard => crntBoard.NumberBoard == numberBoard))
                    {
                        userServise.SetHandler(new AddClientInBoardHandler(numberBoard));
                        AsksToEnterKeyOfBoard(userServise);
                    }
                    else if (!int.TryParse(update.Message.Text, out numberBoard))
                    {
                        userServise.TgClient.SendTextMessageAsync(userServise.Id, "Вам необходимо ввести числовое значение номера доски");
                    }
                    else if (!boards.Any(crntBoard => crntBoard.NumberBoard == numberBoard))
                    {
                        userServise.TgClient.SendTextMessageAsync(userServise.Id, "Вы ввели номер доски, к которой не можете присоединиться");
                        ShowsAllBoards(userServise);
                        AsksToEnterBoardNumber(userServise);
                    }
                    else if (update.Message.Text == null)
                    {
                        userServise.TgClient.SendTextMessageAsync(userServise.Id, "Вы не ввели номер доски, попробуйте ещё раз");
                        AsksToEnterBoardNumber(userServise);
                    }
                    break;
                case UpdateType.CallbackQuery:
                    if (update.CallbackQuery.Data == "Back")
                    {
                        userServise.SetHandler(new MainMenuHandler());
                        userServise.HandleUpdate(update);
                    }
                    else
                    {
                        ShowsAllBoards(userServise);
                        AsksToEnterBoardNumber(userServise);
                    }
                    break;
                default:
                    AsksToEnterBoardNumber(userServise);
                    break;
            }
        }

        private async void ShowsAllBoards(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список досок, к которым вы можете присоединиться: \n" +
                $" {GetAllBoardsToWhichYouCanJoin(userService)}");
        }

        public string GetAllBoardsToWhichYouCanJoin(UserService userServise)
        {
            List<Board> boards = userServise.ClientUserService.GetAllBoardsToWhichYouCanJoin();
            if (boards.Count > 0)
            {
                string result = $"{boards[0].NumberBoard}";
                for (int i = 1; i < boards.Count; i++)
                {
                    result = $"{result}, {boards[i].NumberBoard}";
                }
                return result;
            }
            else
            {
                return "Доски, к которым вы можете присоединиться отсутствуют";
            }
        }

        private async void AsksToEnterBoardNumber(UserService userServise)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
            await userServise.TgClient.SendTextMessageAsync(userServise.Id, $"Введите номер доски, к которой хотите присоединиться.\n" +
                $"Если хотите отменить присоединение, нажмите кнопку \"Назад\".", replyMarkup: keyboard);
        }

        private async void ClearButtons(UserService userServise, Update update)
        {
            await userServise.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
        }

        private async void AsksToEnterKeyOfBoard(UserService userServise)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
            await userServise.TgClient.SendTextMessageAsync(userServise.Id, $"Введите ключ от доски, к которой хотите присоединиться.\n" +
                $"Если хотите отменить присоединение, нажмите кнопку \"Назад\".", replyMarkup: keyboard);
        }
    }
}
