using TaskManager.Handl;
using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class DeleteIssueHandler : IHandler
    {
        public async void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    if (userService.ClientUserService.GetRole() == "Админ")
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Введите номер задачи, которую хотите удалить.");
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberIssue)
                        && issues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.ClientUserService.RemoveIssue(numberIssue);
                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задания", replyMarkup: GetBackButton());
                    }
                    else if (!issues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
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
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер задания, которое хотите удалить.\n" +
                $"Если вы не хотите удалять задание, нажмите кнопку \"Назад\".", replyMarkup: GetBackButton());
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}
