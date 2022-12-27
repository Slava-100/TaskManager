using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TaskManager.Handler
{
    public class ShowsDoneIssuesHandler : IHandler
    {
        public async void HandleUpdateHandler(Update update, UserService userService)
        {

            List<Issue> doneIssues = userService.ClientUserService.GetIssuesDoneInBoardByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "Back":
                            userService.SetHandler(new ShowAllTasksHandler());
                            userService.HandleUpdate(update);
                            break;
                    }
                    break;

                    //case UpdateType.Message:
                    //    if (update.Message.Text != null
                    //        && int.TryParse(update.Message.Text, out var numberIssue)
                    //        && doneIssues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    //    {
                    //        userService.ClientUserService.AttachIssueToClient(numberIssue);
                    //    }
                    //    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    //    {
                    //        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задания", replyMarkup: GetBackButton());
                    //    }
                    //    else if (!doneIssues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
                    //    {
                    //        userService.TgClient.SendTextMessageAsync(userService.Id, "Задания с введенным вами номером нет в текущей доске.", replyMarkup: GetBackButton());
                    //        AsksToEnterIssueNumber(userService);
                    //    }
                    //    else if (update.Message.Text == null)
                    //    {
                    //        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания, попробуйте ещё раз", replyMarkup: GetBackButton());
                    //    }
                    //    break;
                    //default:
                    //    AsksToEnterIssueNumber(userService);
                    //    break;
            }
        }
    }
}

