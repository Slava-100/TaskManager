﻿using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class DeleteClientHandler : IHandler
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
                            if (userService.ClientUserService.GetRole() == "Админ")
                            {
                                userService.ClientUserService.GetActiveBoard().IDAdmin.Remove(userService.Id);
                            }
                            else
                            {
                                userService.ClientUserService.GetActiveBoard().IDMembers.Remove(userService.Id);
                            }

                            userService.ClientUserService.BoardsForUser.Remove(userService.ClientUserService.GetActiveBoard().NumberBoard);
                            userService.SetHandler(new MainMenuHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    DeleteClient(update, userService);
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(ClientService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, "Введите id участника", replyMarkup: ButtonBack());
        }

        private InlineKeyboardMarkup ButtonBack()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "BackToShowMembers" };
            return keyboard;
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

        private void DeleteClient(Update update, ClientService userService)
        {
            string text = update.Message.Text;
            long numberClient;

            if (long.TryParse(text, out numberClient))
            {
                if (DataStorage.GetInstance().Clients.ContainsKey(numberClient))
                {
                    if (userService.ClientUserService.GetActiveBoard().IDAdmin.Contains(numberClient) == true && numberClient != userService.Id)
                    {
                        if (numberClient == userService.ClientUserService.GetActiveBoard().OwnerBoard)
                        {
                            userService.TgClient.SendTextMessageAsync(userService.Id, $"Ты не можешь удалить владельца доски!");
                        }
                        else
                        {
                            userService.ClientUserService.GetActiveBoard().IDAdmin.Remove(numberClient);

                            if (ClientServicesCollection.GetInstance()._userService.ContainsKey(numberClient) && userService.ClientUserService.GetActiveBoard() == ClientServicesCollection.GetInstance()._userService[numberClient].ClientUserService.GetActiveBoard())
                            {
                                ClientServicesCollection.GetInstance()._userService[numberClient].SetHandler(new StartHandler());
                                ClientServicesCollection.GetInstance()._userService[numberClient].HandleUpdate(update);
                            }

                            userService.TgClient.SendTextMessageAsync(userService.Id, $"Участник удален");
                            DataStorage.GetInstance().Clients[numberClient].BoardsForUser.Remove(userService.ClientUserService.GetActiveBoard().NumberBoard);
                        }

                        userService.SetHandler(new ShowMembersHandler());
                        userService.HandleUpdate(update);
                    }
                    else if (userService.ClientUserService.GetActiveBoard().IDMembers.Contains(numberClient) == true && numberClient != userService.Id)
                    {
                        userService.ClientUserService.GetActiveBoard().IDMembers.Remove(numberClient);

                        if (ClientServicesCollection.GetInstance()._userService.ContainsKey(numberClient) && userService.ClientUserService.GetActiveBoard() == ClientServicesCollection.GetInstance()._userService[numberClient].ClientUserService.GetActiveBoard())
                        {
                            ClientServicesCollection.GetInstance()._userService[numberClient].SetHandler(new StartHandler());
                            ClientServicesCollection.GetInstance()._userService[numberClient].HandleUpdate(update);
                        }

                        userService.TgClient.SendTextMessageAsync(userService.Id, $"Участник удален");
                        DataStorage.GetInstance().Clients[numberClient].BoardsForUser.Remove(userService.ClientUserService.GetActiveBoard().NumberBoard);
                        userService.SetHandler(new ShowMembersHandler());
                        userService.HandleUpdate(update);
                    }
                    else if (numberClient == userService.Id)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, $"Хотите выйти из доски?", replyMarkup: YesOrNo());
                    }
                    else
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, $"В этой доске такого пользователя не существует!");
                        userService.SetHandler(new ShowMembersHandler());
                        userService.HandleUpdate(update);
                    }

                    DataStorage.GetInstance().RewriteFileForClients();
                    DataStorage.GetInstance().RewriteFileForBoards();
                }
                else
                {
                    userService.TgClient.SendTextMessageAsync(userService.Id, "Пользователя с таким Id в хранилище не существует!");
                    userService.SetHandler(new ShowMembersHandler());
                    userService.HandleUpdate(update);
                }
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, $"значение id участника - число");
                userService.SetHandler(new ShowMembersHandler());
                userService.HandleUpdate(update);
            }
        }
    }
}
