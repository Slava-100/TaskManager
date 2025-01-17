﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class DeleteBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToBoard":
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
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(ClientService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, "Ты точно хочешь удалить эту доску?", replyMarkup: Button());
        }

        private void DeleteBoard(ClientService userService)
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
                        new InlineKeyboardButton("нет") {CallbackData="BackToBoard"},
                    },
                    new[]
                    {
                        new InlineKeyboardButton("Назад") {CallbackData = "BackToBoard"},
                    }
                });

            return keyboard;
        }
    }
}
