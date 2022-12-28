using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TaskManager.Enums;

namespace TaskManager.Handler
{
    public class ShowIssueHandler : IHandler
    {
        public async void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardForClientByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToAllTask":
                            userService.SetHandler(new ShowAllTasksHandler());
                            userService.HandleUpdate(update);
                            break;
                        //default:
                        //    await userService.TgClient.SendTextMessageAsync(userService.Id, "Для изменения статуса задачи вам необходимо ввести номер задачи.");
                        //    break;
                        default:
                            ShowsAllIssuesInBoard(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                                            && int.TryParse(update.Message.Text, out var numberIssue)
                                            && issues.Exists(crntIssue => crntIssue.NumberIssue == numberIssue &&
                                            crntIssue.Status == IssueStatus.InProgress))

                    {
                        userService.SetHandler(new ChangeStatusOfIssueHandler(numberIssue));
                        userService.HandleUpdate(update);
                        break;
                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение задния");
                        ShowsAllIssuesInBoard(userService);
                    }
                    else if (!issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер задания, которое отсутствует в текущей доске.");
                        ShowsAllIssuesInBoard(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания , попробуйте ещё раз");
                        ShowsAllIssuesInBoard(userService);
                    }
                    else
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Это не Ваша задача");
                        ShowsAllIssuesInBoard(userService);
                    }
                    break;
                default:
                    ShowsAllIssuesInBoard(userService);
                    break;
            }
        }



        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToAllTask" };
        }

        //private async void ShowsFreeIssuesForMember(UserService userService)
        //{
        //    await userService.TgClient.SendTextMessageAsync
        //        (userService.Id, $"Перед вами список всех свободных задач текущей доски, которые вы можете взять в исполнение:\n" +
        //        $" {GetIssuesFreeInBoard(userService)}\n" +
        //        $"\"Для изменения статуса задачи вам необходимо ввести номер задачи.", replyMarkup: GetBackButton());
        //}

        //private string GetIssuesFreeInBoard(UserService userService)
        //{
        //    List<Issue> issues = userService.ClientUserService.GetIssuesFreeInBoardForClientByBoard();
        //    if (issues.Count > 0)
        //    {
        //        string result = $"{issues[0]}";
        //        for (int i = 1; i < issues.Count; i++)
        //        {
        //            result = $"{result}\n{issues[i]}";
        //        }
        //        return result;
        //    }
        //    else
        //    {
        //        return "Список свободных заданий в текущей доске пуст.";
        //    }
        //}

        private async void ShowsAllIssuesInBoard(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех ваших задач в исполнении в текущей доске: \n" +
                $" {GetAllIssuesInProgressForClientInBoard(userService)}\n" +
                $"Для изменения статуса задачи вам необходимо ввести номер задачи.", replyMarkup: GetBackButton());
        }

        private string GetAllIssuesInProgressForClientInBoard(UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetIssuesInProgressForClientInBoard();
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
    }
}



