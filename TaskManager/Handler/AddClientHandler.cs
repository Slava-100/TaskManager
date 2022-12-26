using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        case "Back6":
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
            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back6" };
            return keyboard;
        }

        private void AddClient(Update update, UserService userService)
        {
            string textMessage = update.Message.Text;
            long numberClient = Convert.ToInt64(textMessage);

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
                    userService.ClientUserService.BoardsForUser.Add(userService.ClientUserService.GetActiveBoard().NumberBoard);
                    DataStorage.GetInstance().RewriteFileForClients();
                    DataStorage.GetInstance().RewriteFileForBoards();

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
    }
}
