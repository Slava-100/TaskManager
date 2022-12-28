﻿using System;
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
    public class IncreaseLevelRightsHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back8":
                            userService.SetHandler(new ShowMembersHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    IncreaseLevelRights(update, userService);
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, "Введите id участника", replyMarkup: ButtonBack());
        }

        private InlineKeyboardMarkup ButtonBack()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back8" };
            return keyboard;
        }

        private void IncreaseLevelRights(Update update, UserService userService) 
        {
            string textMessage = update.Message.Text;
            long numberClient = Convert.ToInt64(textMessage);

            if (DataStorage.GetInstance().Clients.ContainsKey(numberClient))
            {
                if (userService.ClientUserService.GetActiveBoard().IDAdmin.Contains(numberClient) == true && numberClient != userService.Id)
                {
                    userService.TgClient.SendTextMessageAsync(userService.Id, $"Он и так уже администратор! (выше прав нет)");
                }
                else if(userService.ClientUserService.GetActiveBoard().IDMembers.Contains(numberClient) == true)
                {
                    userService.ClientUserService.GetActiveBoard().IDAdmin.Add(numberClient);
                    userService.ClientUserService.GetActiveBoard().IDMembers.Remove(numberClient);

                    userService.TgClient.SendTextMessageAsync(userService.Id, $"Права участника (имя: {DataStorage.GetInstance().Clients[numberClient].NameUser}) повышены!");
                }else if (numberClient == userService.Id)
                {
                    userService.TgClient.SendTextMessageAsync(userService.Id, $"Вы не можете повысить себе права! (Вы и так админ)");
                }
                else
                {
                    userService.TgClient.SendTextMessageAsync(userService.Id, $"В этой доске такого пользователя не существует!");
                }

                DataStorage.GetInstance().RewriteFileForClients();
                DataStorage.GetInstance().RewriteFileForBoards();
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, $"Пользователя с таким Id в хранилище не существует!");
            }

            userService.SetHandler(new ShowMembersHandler());
            userService.HandleUpdate(update);
        }
    }
}