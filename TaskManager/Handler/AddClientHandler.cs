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
    public class AddClientHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
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
                        default:
                            SubmitsQuestion(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    AddClient(update, userService);
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
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "BackToShowMembers" };
            return keyboard;
        }

        private void AddClient(Update update, UserService userService)
        {
            string text = update.Message.Text;
            long numberClient;

            if (long.TryParse(text, out numberClient))
            {
                if (DataStorage.GetInstance().Clients.ContainsKey(numberClient))
                {
                    if (userService.ClientUserService.GetActiveBoard().IDAdmin.Contains(numberClient) == true || userService.ClientUserService.GetActiveBoard().IDMembers.Contains(numberClient) == true)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Данный участник уже является пользователем данной доски");
                        userService.SetHandler(new ShowMembersHandler());
                        userService.HandleUpdate(update);
                    }
                    else
                    {
                        userService.ClientUserService.GetActiveBoard().IDMembers.Add(numberClient);
                        DataStorage.GetInstance().Clients[numberClient].BoardsForUser.Add(userService.ClientUserService.GetActiveBoard().NumberBoard);
                        DataStorage.GetInstance().RewriteFileForClients();
                        DataStorage.GetInstance().RewriteFileForBoards();

                        //if (CollectionUserServices.GetInstance()._userService[numberClient].GetHandler() is WorkWithBoardHandler)
                        //{
                        //    CollectionUserServices.GetInstance()._userService[numberClient].SetHandler(new MainMenuHandler());
                        //    CollectionUserServices.GetInstance()._userService[numberClient].HandleUpdate(update);
                        //}

                        userService.TgClient.SendTextMessageAsync(userService.Id, $"Участник (Имя: {DataStorage.GetInstance().Clients[numberClient].NameUser}) добавлен!");
                        userService.SetHandler(new ShowMembersHandler());
                        userService.HandleUpdate(update);
                    }
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
                userService.TgClient.SendTextMessageAsync(userService.Id, "значение id участника - число!");
                SubmitsQuestion(userService);
            }
        }
    }
}
