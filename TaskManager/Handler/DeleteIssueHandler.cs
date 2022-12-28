using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class DeleteIssueHandler : IHandler
    {
        private IHandler _handler;

        public DeleteIssueHandler(IHandler handler)
        {
            _handler = handler;
        }

        public async void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService._activeBoard.Issues;
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToIssues":
                            userService.SetHandler(_handler);
                            userService.HandleUpdate(update);
                            break;
                        default:
                            AskIssueNumber(userService);
                            break;
                    }
                    break;
                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberIssue)
                        && issues.Exists(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        userService.ClientUserService.RemoveIssue(numberIssue);
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Задача удалена");
                        userService.SetHandler(_handler);
                        userService.HandleUpdate(update);
                    }
                    else
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Ошибка : Некорректный ввод");
                        AskIssueNumber(userService);
                    }
                    //else if (!int.TryParse(update.Message.Text, out numberIssue))
                    //{
                    //    userService.TgClient.SendTextMessageAsync(userService.Id, "Ошибка : Вам необходимо ввести числовое значение номера задания");
                    //    AskIssueNumber(userService);
                    //}
                    //else if (!issues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
                    //{
                    //    userService.TgClient.SendTextMessageAsync(userService.Id, "Ошибка : Задания с введенным вами номером нет в текущей доске.");
                    //    AskIssueNumber(userService);
                    //}
                    //else if (update.Message.Text == null)
                    //{
                    //    userService.TgClient.SendTextMessageAsync(userService.Id, "Ошибка : Вы не ввели номер задания.");
                    //    AskIssueNumber(userService);
                    //}
                    break;
                default:
                    AskIssueNumber(userService);
                    break;
            }
        }


        private async void AskIssueNumber(UserService userService)
        {
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер задания, которое хотите удалить.\n" +
                $"Для отмены, нажмите кнопку \"Вернуться к задачам\".", replyMarkup: GetBackButton());
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Вернуться к задачам") { CallbackData = "BackToIssues" };
        }
    }
}
