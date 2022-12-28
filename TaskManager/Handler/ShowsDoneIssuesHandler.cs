//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types;
//using Telegram.Bot;
//using System.Runtime.CompilerServices;
//using Telegram.Bot.Types.InlineQueryResults;

//namespace TaskManager.Handler
//{
//    public class ShowsDoneIssuesHandler : IHandler
//    {
//        public async void HandleUpdateHandler(Update update, UserService userService)
//        {

//            List<Issue> doneIssues = userService.ClientUserService.GetIssuesDoneInBoardByBoard();

//            switch (update.Type)
//            {
//                case UpdateType.CallbackQuery:
//                    switch (update.CallbackQuery.Data)
//                    {
//                        case "BackToAllTask":
//                            userService.SetHandler(new ShowAllTasksHandler());
//                            userService.HandleUpdate(update);
//                            break;
//                        default:

//                            SendIssueList(userService);
//                            break;
//                    }
//                    break;
//            }
//        }


//        private List<Issue> GetIssuesDoneAndReviewInBoardByBoard(UserService userService)
//        {
//            //await userService.TgClient.SendTextMessageAsync(userService.Id, "Перед вами список всех ваших задач, которые находятся в статусе Review и Done");
//            List<Issue> issueReview = userService.ClientUserService.GetIssuesReviewInBoard();
//            List<Issue> issueDone = userService.ClientUserService.GetIssuesDoneInBoardByBoard();
//            List<Issue> result = new List<Issue>();
//            result.AddRange(issueReview);
//            result.AddRange(issueDone);
//            return result;

//            //string resultR = "";
//            //string resultD = "";
//            //if (issueReview.Count > 0)
//            //{
//            //    resultR = $"{issueReview[0]}";
//            //    for (int i = 1; i < issueReview.Count; i++)
//            //    {
//            //        resultR = $"{resultR}\n{issueReview[i]}";
//            //    }
//            //}
//            //else
//            //{
//            //    //await userService.TgClient.SendTextMessageAsync(userService.Id, "Задания в статусе Review отсутствуют");
//            //}
//            //if (issueDone.Count > 0)
//            //{
//            //    resultD = $"{issueDone[0]}";
//            //    for (int i = 1; i < issueDone.Count; i++)
//            //    {
//            //        resultD = $"{resultD}\n{issueDone[i]}";
//            //    }
//            //}
//            //else
//            //{
//            //    //await userService.TgClient.SendTextMessageAsync(userService.Id, "Задания в статусе Done отсутствуют");
//            //}
//            //return $"{resultR}\n{resultD}";
//        }


//        private async void SendIssueList(UserService userService)
//        {
//            var issues = GetIssuesDoneAndReviewInBoardByBoard(userService);
//            if (issues.Count==0)
//            {
//                await userService.TgClient.SendTextMessageAsync(userService.Id, "Задания в статусе Review/Done отсутствуют");
//            }
//            else
//            {
//                string resultIssues = "";
//                if (issues.Count > 0)
//                {
//                    resultIssues = $"{issues[0]}";
//                    for (int i = 1; i < issues.Count; i++)
//                    {
//                        resultIssues = resultIssues + issues[i];
//                    }
//                }
//                await userService.TgClient.SendTextMessageAsync(userService.Id, resultIssues);
//            }
//        }



//        //case UpdateType.Message:
//        //    if (update.Message.Text != null
//        //        && int.TryParse(update.Message.Text, out var numberIssue)
//        //        && doneIssues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
//        //    {
//        //        userService.ClientUserService.AttachIssueToClient(numberIssue);
//        //    }
//        //    else if (!int.TryParse(update.Message.Text, out numberIssue))
//        //    {
//        //        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задания", replyMarkup: GetBackButton());
//        //    }
//        //    else if (!doneIssues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
//        //    {
//        //        userService.TgClient.SendTextMessageAsync(userService.Id, "Задания с введенным вами номером нет в текущей доске.", replyMarkup: GetBackButton());
//        //        AsksToEnterIssueNumber(userService);
//        //    }
//        //    else if (update.Message.Text == null)
//        //    {
//        //        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания, попробуйте ещё раз", replyMarkup: GetBackButton());
//        //    }
//        //    break;
//        //default:
//        //    AsksToEnterIssueNumber(userService);
//        //    break;
//    }
//}



