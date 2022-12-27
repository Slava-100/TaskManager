//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace TaskManager.Handler
//{
//    public class SelectTaskHandler : IHandler
//    {
//        public async void HandleUpdateHandler(Update update, UserService userService)
//        {

//            switch (update.Type)
//            {
//                case UpdateType.CallbackQuery:
//                    switch (update.CallbackQuery.Data)
//                    {
//                        case "SelectTask":
//                            await userService.TgClient.SendTextMessageAsync(userService.Id, "Введите номер задачи.", replyMarkup: GetBackButton());
//                            userService.SetHandler(new ShowIssueHandler());
//                            userService.HandleUpdate(update);
//                            break;
//                            break;

//                        case "AllIssues":
//                            ShowsAllIssuesInBoard(userService);

//                            userService.SetHandler(new ShowIssueHandler());
//                            userService.HandleUpdate(update);
//                            break;

//                        case "FreeIssues":
//                            ShowsFreeIssuesForMember(userService);

//                            userService.SetHandler(new ShowsFreeIssuesInBoardHandler());
//                            userService.HandleUpdate(update);
//                            break;

//                        case "DoneIssuesForMemeber":
//                            ShowsDoneIssuesForMember(userService);

//                            userService.SetHandler(new DeleteBoardHandler());
//                            userService.HandleUpdate(update);
//                            break;

//                        case "DoneIssuesForAdmin":
//                            userService.SetHandler(new DeleteBoardHandler());
//                            userService.HandleUpdate(update);
//                            break;
//                        case "Back":
//                            userService.SetHandler(new BoardHandler());
//                            userService.HandleUpdate(update);
//                            break;
//                        default:
//                            //SubmitsQuestion(userService);
//                            break;
//                    }
//                    break;
//                default:
//                    //SubmitsQuestion(userService);
//                    break;
//            }
//        }

//        private InlineKeyboardMarkup ButtonList(UserService userService)
//        {
//            InlineKeyboardMarkup keyboard;
//            if (userService.ClientUserService.GetRole() == "Участник")
//            {
//                keyboard = new InlineKeyboardMarkup(
//                    new[]
//                        {
//                        new[]
//                        {
//                            new InlineKeyboardButton("Все задачи") {CallbackData = "AllIssues"},
//                            new InlineKeyboardButton("Свободные задачи") {CallbackData="FreeIssues"},
//                            new InlineKeyboardButton("Выполненные задачи") {CallbackData="DoneIssuesForMemeber"}
//                        },
//                        new[]
//                        {
//                            new InlineKeyboardButton("Назад") {CallbackData = "Back"},
//                        }
//                    });
//            }
//            else
//            {
//                keyboard = new InlineKeyboardMarkup(
//                    new[]
//                        {
//                        new[]
//                        {
//                            new InlineKeyboardButton("Все задачи") {CallbackData = "AllIssues"},
//                            new InlineKeyboardButton("Свободные задачи") {CallbackData="FreeIssues"},
//                            new InlineKeyboardButton("Все выполненные задачи") {CallbackData="DoneIssuesForAdmin"}
//                        },
//                        new[]
//                        {
//                            new InlineKeyboardButton("Назад") {CallbackData = "Back"},
//                        }
//                    });
//            }

//            return keyboard;
//        }

//        private async void ShowsAllIssuesInBoard(UserService userService)
//        {
//            await userService.TgClient.SendTextMessageAsync
//                (userService.Id, $"Перед вами список всех задач текущей доски: \n" +
//                $" {GetAllIssuesInBoard(userService)}", replyMarkup: null);
//        }

//        private string GetAllIssuesInBoard(UserService userService)
//        {
//            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();
//            if (issues.Count > 0)
//            {
//                string result = $"{issues[0]}";
//                for (int i = 1; i < issues.Count; i++)
//                {
//                    result = $"{result}\n{issues[i]}";
//                }
//                return result;
//            }
//            else
//            {
//                return "Список заданий в текущей доске пуст.";
//            }
//        }

//        private async void ShowsDoneIssuesForMember(UserService userService)
//        {
//            await userService.TgClient.SendTextMessageAsync
//                (userService.Id, $"Перед вами список всех выполненных вами задач текущей доски: \n" +
//                $" {GetIssuesDoneInBoard(userService)}", replyMarkup: null);
//        }

//        private string GetIssuesDoneInBoard(UserService userService)
//        {
//            List<Issue> issues = userService.ClientUserService.GetIssuesDoneInBoardByBoard();
//            if (issues.Count > 0)
//            {
//                string result = $"{issues[0]}";
//                for (int i = 1; i < issues.Count; i++)
//                {
//                    result = $"{result}\n{issues[i]}";
//                }
//                return result;
//            }
//            else
//            {
//                return "Список выполненных заданий в текущей доске пуст.";
//            }
//        }

//        private async void ShowsFreeIssuesForMember(UserService userService)
//        {
//            await userService.TgClient.SendTextMessageAsync
//                (userService.Id, $"Перед вами список всех свободных задач текущей доски, которые вы можете взять в исполнение: \n" +
//                $" {GetIssuesFreeInBoard(userService)}", replyMarkup: null);
//        }

//        private string GetIssuesFreeInBoard(UserService userService)
//        {
//            List<Issue> issues = userService.ClientUserService.GetIssuesFreeInBoardByBoard();
//            if (issues.Count > 0)
//            {
//                string result = $"{issues[0]}";
//                for (int i = 1; i < issues.Count; i++)
//                {
//                    result = $"{result}\n{issues[i]}";
//                }
//                return result;
//            }
//            else
//            {
//                return "Список свободных заданий в текущей доске пуст.";
//            }
//        }

//        private InlineKeyboardMarkup GetBackButton()
//        {
//            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
//        }
//    }
//}





////        public void HandleUpdateHandler(Update update, UserService userService)
////{
////    throw new NotImplementedException();
////}

