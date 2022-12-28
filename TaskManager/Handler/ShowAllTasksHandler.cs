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
    public class ShowAllTasksHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "ShowTasks":
                            userService.SetHandler(new ShowTasksHandler());
                            userService.HandleUpdate(update);
                            break;


                        case "ShowAllMyIssues":
                            ShowsAllIssuesInBoard(userService);

                            userService.SetHandler(new ShowIssueHandler());
                            userService.HandleUpdate(update);
                            break;


                        case "ShowDoneMyIssues":
                            ShowsDoneIssuesForMember(userService);

                            userService.SetHandler(new ShowsDoneIssuesHandler());
                            userService.HandleUpdate(update);
                            break;

                        case "ShowFreeIssues":
                            ShowsFreeIssuesForMember(userService);

                            userService.SetHandler(new ShowsFreeIssuesInBoardHandler());
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

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, $"Доска №{userService.ClientUserService.GetActiveBoard().NumberBoard} (Твоя роль: {userService.ClientUserService.GetRole()})", replyMarkup: Button(userService));
        }

        private InlineKeyboardMarkup Button(UserService userService)
        {
            InlineKeyboardMarkup keyboard;
            keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Задачи доски") {CallbackData = "ShowTasks"},
                        new InlineKeyboardButton("Мои задачи") {CallbackData="ShowAllMyIssues"},
                        new InlineKeyboardButton("Мои выполненные задачи") {CallbackData="ShowDoneMyIssues"},
                        new InlineKeyboardButton("Свободные задачи") {CallbackData="ShowFreeIssues"}
                    },
                    new[]
                     {
                        new InlineKeyboardButton("Назад") {CallbackData = "BackToBoard"}
                     }
                });

            return keyboard;
        }

        private async void ShowsAllIssuesInBoard(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех ваших задач в текущей доске: \n" +
                $" {GetAllIssuesInBoard(userService)}", replyMarkup: GetBackButton());
        }

        private string GetAllIssuesInBoard(UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();
            if (issues.Count > 0)
            {
                string result = $"{issues[0]}";
                for (int i = 1; i < issues.Count; i++)
                {
                    result = $"{result}\n{issues[i]}";
                }
                return result;
            }
            else
            {
                return "Ваш список заданий в текущей доске пуст.";
            }
        }

        private async void ShowsDoneIssuesForMember(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех выполненных вами задач текущей доски: \n" +
                $" {GetIssuesDoneInBoard(userService)}", replyMarkup: GetBackButton());
        }

        private string GetIssuesDoneInBoard(UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetIssuesDoneInBoardByBoard();
            if (issues.Count > 0)
            {
                string result = $"{issues[0]}";
                for (int i = 1; i < issues.Count; i++)
                {
                    result = $"{result}\n{issues[i]}";
                }
                return result;
            }
            else
            {
                return "Список выполненных вами заданий в текущей доске пуст.";
            }
        }

        private async void ShowsFreeIssuesForMember(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех свободных задач текущей доски, которые вы можете взять в исполнение: \n" +
                $" {GetIssuesFreeInBoard(userService)}", replyMarkup: GetBackButton());
        }

        private string GetIssuesFreeInBoard(UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetIssuesFreeInBoardByBoard();
            if (issues.Count > 0)
            {
                string result = $"{issues[0]}";
                for (int i = 1; i < issues.Count; i++)
                {
                    result = $"{result}\n{issues[i]}";
                }
                return result;
            }
            else
            {
                return "Список свободных заданий в текущей доске пуст.";
            }
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToAllTask" };
        }
    }
}
