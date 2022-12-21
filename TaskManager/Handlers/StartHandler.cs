using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using TaskManager.Enums;

namespace TaskManager.Handlers
{
    public class StartHandler:IHandler
    {
        Buttons buttons;
        private void SendFisrtStartMenu(Client client)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new InlineKeyboardButton("Добавить доску") {CallbackData = "ViewBoard" },
                    new InlineKeyboardButton("Подключиться к доске") {CallbackData = "ConnectToBoard"},
                    new InlineKeyboardButton("Посмотреть доску") {CallbackData = "ViewBoard"},
                });
            client.TgmClient.SendTextMessageAsync(client.IDUser, "Здравствйте!Начните работу с досками!", replyMarkup: keyboard);
        }

        public void HandlerUpdate(Update update, Client client)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "ViewBoard":
                            client.TgmClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id,
                                                                 update.CallbackQuery.Message.MessageId,
                                                                 update.CallbackQuery.Message.Text,
                                                                 replyMarkup: null);
                            client.SetHendler(new Me)

                    }
                    break;
            }
        }
    }
}
