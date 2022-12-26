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
        public void HandleUpdateHandler(Update update, UserService userService)
        {
                       List<Board> boards = userService.ClientUserService.GetAllBoardsToWhichYouCanJoin();

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberBoard)
                        && boards.Any(crntBoard => crntBoard.NumberBoard == numberBoard))
                    {
                        userService.SetHandler(new AddClientInBoardHandler(numberBoard));
                        AsksToEnterKeyOfBoard(userService);
                    }
                    else if (!int.TryParse(update.Message.Text, out numberBoard))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера доски");
                    }
                    else if (!boards.Any(crntBoard => crntBoard.NumberBoard == numberBoard))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер доски, к которой не можете присоединиться");
                        ShowsAllBoards(userService);
                        AsksToEnterBoardNumber(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер доски, попробуйте ещё раз");
                        AsksToEnterBoardNumber(userService);
                    }
                    break;
                case UpdateType.CallbackQuery:
                    if (update.CallbackQuery.Data == "Back")
                    {
                        userService.SetHandler(new MainMenuHandler());
                        userService.HandleUpdate(update);
                    }
                    else
                    {
                        ShowsAllBoards(userService);
                    }
                    break;
                default:
                    AsksToEnterBoardNumber(userService);
                    break;
            }
        }

        private async void ShowsAllBoards(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список досок, к которым вы можете присоединиться: \n" +
                $" {GetAllBoardsToWhichYouCanJoin(userService)}", replyMarkup: GetBackButton());
        }

        public string GetAllBoardsToWhichYouCanJoin(UserService userService)
        {
            List<Board> boards = userService.ClientUserService.GetAllBoardsToWhichYouCanJoin();
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

        private async void AsksToEnterBoardNumber(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер доски, к которой хотите присоединиться.\n" +
                $"Если хотите отменить присоединение, нажмите кнопку \"Назад\".", replyMarkup: GetBackButton());
        }

        private async void ClearButtons(UserService userService, Update update)
        {
            await userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
        }

        private async void AsksToEnterKeyOfBoard(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите ключ от доски, к которой хотите присоединиться.\n" +
                $"Если хотите отменить присоединение, нажмите кнопку \"Назад\".", replyMarkup: GetBackButton());
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}
