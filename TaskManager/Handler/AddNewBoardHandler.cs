using TaskManager.Handl;
using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class AddNewBoardHandler:IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message.Text is String)
                    {
                        CreateNewBoard(update, userService);
                    }
                    else
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели название доски, попробуйте ещё раз");
                        SubmitsQuestion(userService);
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
                        SubmitsQuestion(userService);
                    }
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }
        private void SubmitsQuestion(UserService userService)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
            userService.TgClient.SendTextMessageAsync(userService.Id, "Введите название новой доски!" +
                "(чтобы отменить создание, нажмите кнопку \"Назад\"", replyMarkup:keyboard);
        }

        private void CreateNewBoard(Update update, UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            int NumberNewBoard = userService.ClientUserService.AddBoard(update.Message.Text);
            Board newBoard = dataStorage.Boards[NumberNewBoard];
            userService.TgClient.SendTextMessageAsync(userService.Id, newBoard.ToString() + $" Ключ: {newBoard.Key}");
            userService.SetHandler(new MainMenuHandler());
            userService.HandleUpdate(update);
        }

        private void ClearButtons(UserService userService, Update update)
        {
            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
        }
    }
}
