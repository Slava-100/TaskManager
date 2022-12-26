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
    public class ShowTasksHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "":
                            userService.SetHandler(new ShowTasksHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "1":
                            userService.SetHandler(new ShowMembersHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "2":
                            userService.SetHandler(new DeleteBoardHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "3":
                            userService.SetHandler(new WorkWithBoardHandler());
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

        private void SubmitsQuestion(UserService userService)
        {
            var spisokIssues = userService.ClientUserService.GetActiveBoard().Issues;
            string s = "";
            if (spisokIssues.Count != 0)
            {
                foreach (Issue i in spisokIssues)
                {
                    s += $"Номер задачи: {i.NumberIssue} \nОписание задачи: \n{i.Description}\n";
                }
                userService.TgClient.SendTextMessageAsync(userService.Id, s, replyMarkup: ButtonListIsNotEmpty(userService));
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, "Задач нет", replyMarkup: ButtonListIsEmpty(userService));
            }
        }

        private InlineKeyboardMarkup ButtonListIsEmpty(UserService userService)
        {
            InlineKeyboardMarkup keyboard;
            if (userService.ClientUserService.GetRole() == "Админ")
            {
                keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("Добавить задачу") {CallbackData = "AddTask"},
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back3"},
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
                            new InlineKeyboardButton("Назад") {CallbackData = "Back3"},
                        }
                    });
            }

            return keyboard;    
        }

        private InlineKeyboardMarkup ButtonListIsNotEmpty(UserService userService)
        {
            InlineKeyboardMarkup keyboard;
            if (userService.ClientUserService.GetRole() == "Админ")
            {
                keyboard = new InlineKeyboardMarkup(
                    new[]
                        {
                        new[]
                        {
                            new InlineKeyboardButton("Добавить задачу") {CallbackData = "AddTask"},
                            new InlineKeyboardButton("ВЫбрать задачу") {CallbackData="SelectTask"},
                            new InlineKeyboardButton("Удалить задачу") {CallbackData="DeleteTask"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back3"},
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
                            new InlineKeyboardButton("ВЫбрать задачу") {CallbackData="SelectTask"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back3"},
                        }
                    });
            }

            return keyboard;
        }
    }
}
