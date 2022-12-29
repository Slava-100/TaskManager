using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ShowsFreeIssuesInBoardHandler : IHandler
    {

        public async void HandleUpdateHandler(Update update, ClientService userService)
        {

            List<Issue> freeIssues = userService.ClientUserService.GetIssuesFreeInBoardForClientByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "BackToAllTask":
                            userService.SetHandler(new ShowAllIssueHandler());
                            userService.HandleUpdate(update);
                            break;
                        case "ShowFreeIssues":
                            await userService.TgClient.SendTextMessageAsync(userService.Id, "Введите номер задачи, которую вы хотите взять к исполнению.");
                            break;
                    }
                    break;

                case UpdateType.Message:
                    if (update.Message.Text != null
                        && int.TryParse(update.Message.Text, out var numberIssue)
                        && freeIssues.Any(crntIssue => crntIssue.NumberIssue == numberIssue))
                    {
                        if (userService.ClientUserService.AttachIssueToClient(numberIssue))
                        {
                            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Теперь задача с номером {numberIssue} находится в вашем исполнении.", replyMarkup: GetBackButton());
                        }
                        else
                        {
                            await userService.TgClient.SendTextMessageAsync(userService.Id, "У вас может быть только 1 задача в исполнении.", replyMarkup: GetBackButton());
                        }

                    }
                    else if (!int.TryParse(update.Message.Text, out numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение номера задания", replyMarkup: GetBackButton());
                    }
                    else if (!freeIssues.Any(crntBoard => crntBoard.NumberIssue == numberIssue))
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Задания с введенным вами номером нет в текущей доске.", replyMarkup: GetBackButton());
                        AsksToEnterIssueNumber(userService);
                    }
                    else if (update.Message.Text == null)
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы не ввели номер задания, попробуйте ещё раз", replyMarkup: GetBackButton());
                    }
                    break;
                default:
                    AsksToEnterIssueNumber(userService);
                    break;
            }
        }

        private async void AsksToEnterIssueNumber(ClientService userService)
        {
            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Введите номер задания, которое хотите взять к исполнению.\n" +
                $"Если задания, которое вы хотите взять в исполнение нет в списке, нажмите кнопку \"Назад\".", replyMarkup: GetBackButton());
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToAllTask" };
        }
    }
}
