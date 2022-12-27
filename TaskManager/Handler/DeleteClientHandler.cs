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
    public class DeleteClientHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back7":
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

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, "Введите id участника", replyMarkup: ButtonBack());
        }

        private InlineKeyboardMarkup ButtonBack()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back7" };
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
                            new InlineKeyboardButton("Нет") {CallbackData = "Back7"},
                        },
                    });
           
            return keyboard;
        }

        private void DeleteClient(Update update, UserService userService)
        {
            string textMessage = update.Message.Text;
            long numberClient = Convert.ToInt64(textMessage);

            if (DataStorage.GetInstance().Clients.ContainsKey(numberClient))
            {
                if (userService.ClientUserService.GetActiveBoard().IDAdmin.Contains(numberClient) == true && numberClient != userService.Id)
                {
                    userService.ClientUserService.GetActiveBoard().IDAdmin.Remove(numberClient);
                    userService.TgClient.SendTextMessageAsync(userService.Id, $"Участник удален");
                    DataStorage.GetInstance().Clients[numberClient].BoardsForUser.Remove(userService.ClientUserService.GetActiveBoard().NumberBoard);
                    userService.SetHandler(new ShowMembersHandler());
                    userService.HandleUpdate(update);
                }
                else if (userService.ClientUserService.GetActiveBoard().IDMembers.Contains(numberClient) == true && numberClient != userService.Id)
                {
                    userService.ClientUserService.GetActiveBoard().IDMembers.Remove(numberClient);
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
    }
}
