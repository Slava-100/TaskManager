using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ShowAllIssueHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, ClientService userService)
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
                            userService.SetHandler(new ShowIssueHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "ShowDoneAndReviewMyIssues":
                            ShowsDoneAndReviewIssuesForMember(userService);
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

        private void SubmitsQuestion(ClientService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, $"Доска №{userService.ClientUserService.GetActiveBoard().NumberBoard} (Твоя роль: {userService.ClientUserService.GetRole()})", replyMarkup: Button(userService));
        }

        private InlineKeyboardMarkup Button(ClientService userService)
        {
            InlineKeyboardMarkup keyboard;
            keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Задачи доски") {CallbackData = "ShowTasks"},
                        new InlineKeyboardButton("Мои задачи") {CallbackData="ShowAllMyIssues"},
                        new InlineKeyboardButton("Мои выполненные задачи") {CallbackData="ShowDoneAndReviewMyIssues"},
                        new InlineKeyboardButton("Свободные задачи") {CallbackData="ShowFreeIssues"}
                    },
                    new[]
                     {
                        new InlineKeyboardButton("Назад") {CallbackData = "BackToBoard"}
                     }
                });

            return keyboard;
        }

        private async void ShowsDoneAndReviewIssuesForMember(ClientService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех выполненных вами задач текущей доски: \n" +
                $" {GetIssuesReviewAndDoneInBoard(userService)}", replyMarkup: GetBackButton());
        }

        private string GetIssuesReviewAndDoneInBoard(ClientService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetIssuesReviewAndDoneForClientInBoard();
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

        private async void ShowsFreeIssuesForMember(ClientService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех свободных задач текущей доски, которые вы можете взять в исполнение: \n" +
                $" {GetIssuesFreeInBoard(userService)}", replyMarkup: GetBackButton());
        }

        private string GetIssuesFreeInBoard(ClientService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetIssuesFreeInBoardForClientByBoard();
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
