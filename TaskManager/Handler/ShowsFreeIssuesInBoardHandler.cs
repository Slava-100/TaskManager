using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ShowsFreeIssuesInBoardHandler : IHandler
    {

        public async void HandleUpdateHandler(Update update, UserService userService)
        {

            List<Issue> freeIssues = userService.ClientUserService.GetIssuesFreeInBoardByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await userService.TgClient.SendTextMessageAsync(userService.Id, "Введите номер задачи, которую вы хотите взять к исполнению.");

                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberIssue)
                        && freeIssues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.ClientUserService.AttachIssueToClient(numberIssue);
                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задания", replyMarkup: GetBackButton());
                    }
                    else if (!freeIssues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Задания с введенным вами номером нет в текущей доске.", replyMarkup: GetBackButton());
                        AsksToEnterIssueNumber(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания, попробуйте ещё раз", replyMarkup: GetBackButton());
                    }
                    break;
                default:
                    AsksToEnterIssueNumber(userService);
                    break;
            }
        }

        private async void AsksToEnterIssueNumber(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер задания, которое хотите взять к исполнению.\n" +
                $"Если задания, которое вы хотите взять в исполнение нет в списке, нажмите кнопку \"Назад\".", replyMarkup: GetBackButton());
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}
