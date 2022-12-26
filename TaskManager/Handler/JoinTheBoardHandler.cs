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
            //DataStorage dataStorage = DataStorage.GetInstance();
            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message.Text is String)
                    {
                        //ShowsAllBoards(userService);
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер доски, попробуйте ещё раз");
                        AsksToEnterBoardNumber(userService);
                    }
                    else
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "3");
                        //ShowsAllBoards(userService);
                        //AsksToEnterBoardNumber(userService);
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
                        AsksToEnterBoardNumber(userService);
                    }
                    break;
                default:
                    AsksToEnterBoardNumber(userService);
                    break;
            }
        }

        private void ShowsAllBoards(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список досок, к которым вы можете присоединиться: \n" +
                $" {GetAllBoardsToWhichYouCanJoin(userService)}");
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


        private void AsksToEnterBoardNumber(UserService userService)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
            userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер доски, к которой хотите присоединиться.\n" +
                $"Если хотите отменить присоединение, нажмите кнопку", replyMarkup: keyboard);
        }


        private void ClearButtons(UserService userService, Update update)
        {
            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
        }
    }
}
