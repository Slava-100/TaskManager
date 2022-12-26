using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class BoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {

                    }
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, $"Доска №{userService.ClientUserService.GetActiveBoard().NumberBoard} (Твоя роль: {userService.ClientUserService.GetRole()})", replyMarkup: Button());
        }

        private InlineKeyboardMarkup Button()
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                   {
                        new[]
                        {
                            new InlineKeyboardButton("Показать задачи") {CallbackData = "ShowTasks"},
                            new InlineKeyboardButton("Показать участников") {CallbackData="ShowMembers"},
                            new InlineKeyboardButton("Удалить доску") {CallbackData="DeleteBoard"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "Back"},
                        }
                   });

            return keyboard;
        }
    }
}
