﻿using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ExitFromBoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToShowMembers":
                            userService.SetHandler(new ShowMembersHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "Yes":
                            Exit(update, userService);
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
            userService.TgClient.SendTextMessageAsync(userService.Id, "Вы точно хотите выйти из доски!", replyMarkup: YesOrNo());
        }

        private InlineKeyboardMarkup YesOrNo()
        {
            InlineKeyboardMarkup keyboard;
            keyboard = new InlineKeyboardMarkup(
                new[]
                {
                        new[]
                        {
                            new InlineKeyboardButton("Да") {CallbackData = "Yes"},
                            new InlineKeyboardButton("Нет") {CallbackData = "BackToShowMembers"},
                        },
                });

            return keyboard;
        }

        private void Exit(Update update, ClientService userService)
        {
            if (userService.ClientUserService.GetRole() == "Админ")
            {
                userService.ClientUserService.GetActiveBoard().OwnerBoard = 0;
                userService.ClientUserService.GetActiveBoard().IDAdmin.Remove(userService.Id);
            }
            else
            {
                userService.ClientUserService.GetActiveBoard().IDMembers.Remove(userService.Id);
            }

            userService.ClientUserService.BoardsForUser.Remove(userService.ClientUserService.GetActiveBoard().NumberBoard);
            userService.ClientUserService.ActiveBoard = null;
            DataStorage.GetInstance().RewriteFileForBoards();
            DataStorage.GetInstance().RewriteFileForClients();
            userService.SetHandler(new MainMenuHandler());
            userService.HandleUpdate(update);
        }
    }
}
