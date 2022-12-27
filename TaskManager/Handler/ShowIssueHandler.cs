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
    public class ShowIssueHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();

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
                case UpdateType.Message:
                    if (update.Message.Text != null
                                            && int.TryParse(update.Message.Text, out var numberIssue)
                                            && issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, $"{issues[numberIssue]}", replyMarkup: GetBackButton());
                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение задния", replyMarkup: GetBackButton());
                    }
                    else if (!issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер задания, которое отсутствует в текущей доске.", replyMarkup: GetBackButton());
                        //ShowsAllBoards(userService);
                        //AsksToEnterBoardNumber(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания , попробуйте ещё раз", replyMarkup: GetBackButton());
                    }
                    break;
            }
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}


//        switch (update.Type)
//        {
//            case UpdateType.CallbackQuery:
//                switch (update.CallbackQuery.Data)
//                {
//                    case "DeleteIssue":
//                        userService.SetHandler(new DeleteIssueHandler());
//                        userService.HandleUpdate(update);
//                        break;
//                    case "Back":
//                        userService.SetHandler(new SelectTaskHandler());
//                        userService.HandleUpdate(update);
//                        break;
//                    default:
//                        //SubmitsQuestion(userService);
//                        break;
//                }
//                break;
//            default:
//                //SubmitsQuestion(userService);
//                break;
//        }
//    }

//    private InlineKeyboardMarkup ButtonList(UserService userService)
//    {
//        InlineKeyboardMarkup keyboard;
//        if (userService.ClientUserService.GetRole() == "Участник")
//        {
//            keyboard = new InlineKeyboardButton("Назад") { CallbackData = "Back" };
//        }
//        else
//        {
//            keyboard = new InlineKeyboardMarkup(
//                new[]
//                    {
//                    new[]
//                    {
//                        new InlineKeyboardButton("Удалить задачу") {CallbackData = "DeleteIssue"},
//                    },
//                    new[]
//                    {
//                        new InlineKeyboardButton("Назад") {CallbackData = "Back"},
//                    }
//                });
//        }

//        return keyboard;
//    }
//}

