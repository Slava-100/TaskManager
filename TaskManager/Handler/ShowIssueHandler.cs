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
                        default:
                            await userService.TgClient.SendTextMessageAsync(userService.Id, "Для изменения статуса задачи вам необходимо ввести номер задачи.");
                            break;
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                                            && int.TryParse(update.Message.Text, out var numberIssue)
                                            && issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.SetHandler(new ChangeStatusOfIssueHandler(numberIssue));
                        userService.HandleUpdate(update);
                        break;
                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение задния", replyMarkup: GetBackButton());
                    }
                    else if (!issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели номер задания, которое отсутствует в текущей доске.", replyMarkup: GetBackButton());
                    }
                    else if (update.Message.Text == null)
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания , попробуйте ещё раз", replyMarkup: GetBackButton());
                    }
                    break;
            }
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToAllTask" };
        }
    }
}



