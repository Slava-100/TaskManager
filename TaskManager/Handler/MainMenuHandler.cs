using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handl
{
    public class MainMenuHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "AddBoard":
                            userServise.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userServise.SetHandler(new StartHandler());
                            userServise.HandleUpdate(update);
                            break;
                        case "JoinTheBoard":
                            userServise.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userServise.SetHandler(new JoinTheBoardHandler());
                            userServise.HandleUpdate(update);
                            break;
                        case "WorkWithBoard":
                            userServise.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userServise.SetHandler(new StartHandler());
                            userServise.HandleUpdate(update);
                            break;
                        default:
                            SendBaseMenu(userServise);
                            break;
                    }
                    break;
                default:
                    SendBaseMenu(userServise);
                    break;
            }
        }

        private void SendBaseMenu(UserService userServise)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                                new[]
                                    {
                                    new[]{new InlineKeyboardButton("Создать новую доску") {CallbackData = "AddBoard"} },
                                    new[]{new InlineKeyboardButton("Присоединиться к доске") {CallbackData="JoinTheBoard"}},
                                    new[]{new InlineKeyboardButton("Работать с доской") {CallbackData="WorkWithBoard"}},
                                });

            userServise.TgClient.SendTextMessageAsync(userServise.Id, "Главное меню", replyMarkup: keyboard);
        }
    }
}
