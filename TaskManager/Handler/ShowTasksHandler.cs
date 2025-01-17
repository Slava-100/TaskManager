﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ShowTasksHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "AddTask":
                            userService.SetHandler(new AddIssueHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "SelectTask":
                            userService.SetHandler(new SelectTaskHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "DeleteTask":
                            userService.SetHandler(new DeleteIssueHandler(new ShowTasksHandler()));
                            userService.HandleUpdate(update);
                            break;
                        case "BackToShowAllTasks":
                            userService.SetHandler(new ShowAllIssueHandler());
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
            var spisokIssues = userService.ClientUserService.GetActiveBoard().Issues;
            string s = "";
            if (spisokIssues.Count != 0)
            {
                foreach (Issue i in spisokIssues)
                {
                    s += $"Номер задачи: {i.NumberIssue} \nОписание задачи: \n{i.Description}\nСтатус задачи:\n {i.Status}\n";
                }
                userService.TgClient.SendTextMessageAsync(userService.Id, s, replyMarkup: ButtonListIsNotEmpty(userService));
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, "Задач нет", replyMarkup: ButtonListIsEmpty(userService));
            }
        }

        private InlineKeyboardMarkup ButtonListIsEmpty(ClientService userService)
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
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToShowAllTasks"},
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
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToShowAllTasks"},
                        }
                    });
            }

            return keyboard;
        }

        private InlineKeyboardMarkup ButtonListIsNotEmpty(ClientService userService)
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
                            new InlineKeyboardButton("Выбрать задачу") {CallbackData="SelectTask"},
                            new InlineKeyboardButton("Удалить задачу") {CallbackData="DeleteTask"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToShowAllTasks"},
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
                                new InlineKeyboardButton("Назад") {CallbackData = "BackToShowAllTasks"},
                            }
                        });
            }

            return keyboard;
        }
    }
}
