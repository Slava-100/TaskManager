using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handl
{
    public class MainMenuHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "AddBoard":
                            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userService.SetHandler(new StartHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "JoinTheBoard":
                            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userService.SetHandler(new StartHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "WorkWithBoard":
                            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userService.SetHandler(new StartHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            SendBaseMenu(userService);
                            break;
                    }
                    break;
                default:
                    SendBaseMenu(userService);
                    break;
            }
        }

        private void SendBaseMenu(UserService userService)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                                new[]
                                    {
                                    new[]{new InlineKeyboardButton("Создать новую доску") {CallbackData = "AddBoard"} },
                                    new[]{new InlineKeyboardButton("Присоединиться к доске") {CallbackData="JoinTheBoard"}},
                                    new[]{new InlineKeyboardButton("Работать с доской") {CallbackData="WorkWithBoard"}},
                                });

            userService.TgClient.SendTextMessageAsync(userService.Id, "Главное меню", replyMarkup: keyboard);
        }
    }
}
