using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handlers
{
    public class StartHandler : IHandler
    {
        private TelegramService telegramService = TelegramService.GetInstance();

        private void SendStartMenu(Client client)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]{ new InlineKeyboardButton("Добавить доску") { CallbackData = "AddBoard" } },
                    new[]{ new InlineKeyboardButton("Подключиться к доске") {CallbackData = "ConnectToBoard"} },
                    new[]{ new InlineKeyboardButton("Посмотреть доску") { CallbackData = "ViewBoard" } }
                });



            telegramService.Bot.SendTextMessageAsync(client.IDUser, "Здравствйте!Начните работу с досками!", replyMarkup: keyboard);
        }

        public void HandlerUpdate(Update update, Client client)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "AddBoard":
                            telegramService.Bot.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id,
                                                                 update.CallbackQuery.Message.MessageId,
                                                                 update.CallbackQuery.Message.Text,
                                                                 replyMarkup: null);
                            client.SetHendler(new AddBoardHandler());
                            client.HandlerUpdate(update);
                            break;
                        case "ConnectToBoard":
                            client.SetHendler(new ConnectToBoard());
                            client.HandlerUpdate(update);
                            break;
                        case "ViewBoard":
                            client.SetHendler(new ViewBoard());
                            client.HandlerUpdate(update);
                            break;
                    }
                    break;
                default:
                    SendStartMenu(client);
                    break;

            }
        }
    }
}
