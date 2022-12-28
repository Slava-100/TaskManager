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
        public async void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();



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
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var _numberIssue)
                        && issues.Any(crntIssue => crntIssue.NumberIssue == _numberIssue))
                    {
                        MoveIssueFromReviewToDone(userService);
                        userService.TgClient.SendTextMessageAsync(userService.Id, $"Вы перенесли задачу с номером {_numberIssue} в статус Done", replyMarkup: GetBackButton());
                    }
                    else if (!int.TryParse(update.Message.Text, out _numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задачи", replyMarkup: GetBackButton());
                    }
                    else if (!issues.Any(crntIssue => crntIssue.NumberIssue == _numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер задачи, которой нет в списке", replyMarkup: GetBackButton());
                        //ShowsAllBoards(userService);
                        //AsksToEnterBoardNumber(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задачи, попробуйте ещё раз", replyMarkup: GetBackButton());
                    }
                    break;

            }
        }

        public bool MoveIssueFromReviewToDone(int IdIssue)
        {
            //var issue = _activeBoard.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);

            //_userRole.MoveIssueFromReviewToDone(_activeBoard, issue);
            return true;
        }


        private bool MoveIssueFromReviewToDone(UserService userService)
        {
            userService.ClientUserService.MoveIssueFromReviewToDone(_numberIssue);
            return true;
        }



        //            case "AllIssues":
        //                ShowsAllIssuesInBoard(userService);

        //                userService.SetHandler(new ShowIssueHandler());
        //                userService.HandleUpdate(update);
        //                break;

        //            case "FreeIssues":
        //                ShowsFreeIssuesForMember(userService);

        //                userService.SetHandler(new ShowsFreeIssuesInBoardHandler());
        //                userService.HandleUpdate(update);
        //                break;

        //            case "DoneIssuesForMemeber":
        //                ShowsDoneIssuesForMember(userService);

        //                userService.SetHandler(new DeleteBoardHandler());
        //                userService.HandleUpdate(update);
        //                break;

        //            case "DoneIssuesForAdmin":
        //                userService.SetHandler(new DeleteBoardHandler());
        //                userService.HandleUpdate(update);
        //                break;
        //            case "Back":
        //                userService.SetHandler(new BoardHandler());
        //                userService.HandleUpdate(update);
        //                break;
        //            default:
        //                //SubmitsQuestion(userService);
        //                break;
        //        }
        //        break;
        //    default:
        //        //SubmitsQuestion(userService);
        //        break;
        //}


















        private InlineKeyboardMarkup ButtonList(UserService userService)
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

        private async void ShowsAllIssuesInBoard(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех задач текущей доски: \n" +
                $" {GetAllIssuesInBoard(userService)}", replyMarkup: null);
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
                return "Список заданий в текущей доске пуст.";
            }
        }

        private async void ShowsDoneIssuesForMember(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех выполненных вами задач текущей доски: \n" +
                $" {GetIssuesDoneInBoard(userService)}", replyMarkup: null);
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
                return "Список выполненных заданий в текущей доске пуст.";
            }
        }

        private async void ShowsFreeIssuesForMember(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync
                (userService.Id, $"Перед вами список всех свободных задач текущей доски, которые вы можете взять в исполнение: \n" +
                $" {GetIssuesFreeInBoard(userService)}", replyMarkup: null);
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
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}





//        public void HandleUpdateHandler(Update update, UserService userService)
//{
//    throw new NotImplementedException();
//}

