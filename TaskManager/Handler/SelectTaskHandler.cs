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
    public class SelectTaskHandler : IHandler
    {
        private int _numberIssue;
        public async void HandleUpdateHandler(Update update, ClientService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoard();
            List<Issue> issuesReview = userService.ClientUserService.GetAllIssuesReviewForAllClientsInBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "SelectTask":
                            if (userService.ClientUserService.GetRole() == "Админ")
                            {
                                await userService.TgClient.SendTextMessageAsync(userService.Id, "Введите номер задачи со статусом Review, которую хотите перенести в статус Done", replyMarkup: GetBackButton());
                            }
                            break;
                        case "BackToShowTasks":
                            userService.SetHandler(new ShowAllIssueHandler());
                            userService.HandleUpdate(update);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out _numberIssue)
                        && issuesReview.Any(crntIssue => crntIssue.NumberIssue == _numberIssue))
                    {
                        MoveIssueFromReviewToDone(userService);
                        userService.TgClient.SendTextMessageAsync(userService.Id, $"Вы перенесли задачу с номером {_numberIssue} в статус Done", replyMarkup: GetBackButton());
                        userService.SetHandler(new ShowTasksHandler());
                        userService.HandleUpdate(update);

                    }
                    else if (!int.TryParse(update.Message.Text, out _numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задачи", replyMarkup: GetBackButton());
                        userService.SetHandler(new ShowTasksHandler());
                        userService.HandleUpdate(update);
                    }
                    else if (!issuesReview.Any(crntIssue => crntIssue.NumberIssue == _numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер задачи, которой нет в списке со статусом Review", replyMarkup: GetBackButton());
                        userService.SetHandler(new ShowTasksHandler());
                        userService.HandleUpdate(update);
                    }
                    else if (update.Message.Text == null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задачи, попробуйте ещё раз", replyMarkup: GetBackButton());
                        userService.SetHandler(new ShowTasksHandler());
                        userService.HandleUpdate(update);
                    }
                    break;
            }
        }

        private bool MoveIssueFromReviewToDone(ClientService userService)
        {
            userService.ClientUserService.MoveIssueFromReviewToDone(_numberIssue);
            return true;
        }

        private InlineKeyboardMarkup ButtonList(ClientService userService)
        {
            InlineKeyboardMarkup keyboard;
            if (userService.ClientUserService.GetRole() == "Админ")
            {
                keyboard = new InlineKeyboardMarkup(
                    new[]
                        {
                        new[]
                        {
                            new InlineKeyboardButton("Все задачи") {CallbackData = "AllIssues"},
                            new InlineKeyboardButton("Свободные задачи") {CallbackData="FreeIssues"},
                            new InlineKeyboardButton("Выполненные задачи") {CallbackData="DoneIssuesForMemeber"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back"},
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
                            new InlineKeyboardButton("Все задачи") {CallbackData = "AllIssues"},
                            new InlineKeyboardButton("Свободные задачи") {CallbackData="FreeIssues"},
                            new InlineKeyboardButton("Все выполненные задачи") {CallbackData="DoneIssuesForAdmin"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back"},
                        }
                    });
            }

            return keyboard;
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToShowTasks" };
        }
    }
}


