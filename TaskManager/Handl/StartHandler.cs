using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TaskManager.Handl
{
    public class StartHandler : IHandler
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
                            userServise.SetHandler(new AddBoardHandler());
                            userServise.HandleUpdate(update);
                            break;
                        case "JoinTheBoard":
                            userServise.SetHandler(new AddBoardHandler());
                            userServise.HandleUpdate(update);
                            break;
                        case "ContinueWorking":
                            userServise.SetHandler(new AddBoardHandler());
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
                                    new[]{
                                        new InlineKeyboardButton("Создать новую доску") {CallbackData = "AddBoard"},
                                        new InlineKeyboardButton("Присоединиться к существующей доске") {CallbackData="JoinTheBoard"},
                                        new InlineKeyboardButton("Продолжить работу со своей доской") {CallbackData="ContinueWorking"}
                                    }
                                });

            userServise.TgClient.SendTextMessageAsync(userServise.Id, "Привет! Ты в главном меню управления досками!!!", replyMarkup: keyboard);
        }
    }
}
