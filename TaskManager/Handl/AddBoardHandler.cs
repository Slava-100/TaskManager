using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TaskManager.Handl
{
    public class AddBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Nazad":
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
                                    new[]
                                    {
                                         new InlineKeyboardButton("Nazad") {CallbackData = "Nazad"},
                                    }
                               });
            DataStorage.GetInstance().AddBoard(userServise.Id);
            userServise.TgClient.SendTextMessageAsync(userServise.Id, "Доска создана - поздравляем! Теперь вы её администратор!", replyMarkup: keyboard);
        }
    }
}
