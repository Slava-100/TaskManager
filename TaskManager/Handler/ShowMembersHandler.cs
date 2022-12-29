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
    public class ShowMembersHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "AddClient":
                            userService.SetHandler(new AddClientHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "DeleteClient":
                            userService.SetHandler(new DeleteClientHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "IncreaseLevelRights":
                            userService.SetHandler(new IncreaseLevelRightsHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "ExitFromBoard":
                            userService.SetHandler(new ExitFromBoardHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "BackToBoard":
                            userService.SetHandler(new BoardHandler());
                            userService.HandleUpdate(update);
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
            string s = "Администраторы:\n";

            foreach (long i in userService.ClientUserService.GetActiveBoard().IDAdmin)
            {
                s += $"Id участника: {i} Имя участника: {DataStorage.GetInstance().Clients[i].NameUser}\n";    
            }

            s += "Участники:\n";

            foreach (long i in userService.ClientUserService.GetActiveBoard().IDMembers)
            {
                s += $"Id участника: {i} Имя участника: {DataStorage.GetInstance().Clients[i].NameUser}\n";
            }

            userService.TgClient.SendTextMessageAsync(userService.Id, s, replyMarkup: Button(userService));
        }


        private InlineKeyboardMarkup Button(ClientService userService)
        {
            InlineKeyboardMarkup keyboard;
            if (userService.ClientUserService.GetRole() == "Админ")
            {
                keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("Добавить участника") {CallbackData = "AddClient"},
                            new InlineKeyboardButton("Удалить участника") {CallbackData = "DeleteClient"},
                            new InlineKeyboardButton("Повысить уровень прав участника") {CallbackData = "IncreaseLevelRights"},
                            new InlineKeyboardButton("Выйти из доски") {CallbackData = "ExitFromBoard"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToBoard"}
                        }
                    });
            }
            else
            {
                keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("Выйти из доски") {CallbackData = "ExitFromBoard"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToBoard"}
                        }
                    });
            }

            return keyboard;
        }
    }
}
