using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class DeleteBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back1":
                            userService.SetHandler(new BoardHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "Yes":
                            DeleteBoard(userService);
                            userService.SetHandler(new WorkWithBoardHandler());
                            break;
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:

                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id,"Ты точно хочешь удалить эту доску?", replyMarkup: Button());
        }

        private void DeleteBoard(UserService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            dataStorage.DeleteActiveBoard(userService.ClientUserService.GetActiveBoard(), userService.Id);
        }

        private InlineKeyboardMarkup Button()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Да") {CallbackData = "Yes"},
                        new InlineKeyboardButton("нет") {CallbackData="Back1"},
                    },
                    new[]
                    {
                        new InlineKeyboardButton("Назад") {CallbackData = "Back1"},
                    }
                });

            return keyboard;
        }
    }
}
