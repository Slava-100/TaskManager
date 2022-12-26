using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class StartHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "StartWork":
                            userService.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userService.SetHandler(new MainMenuHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            SendStart(userService);
                            break;
                    }
                    break;
                default:
                    SendStart(userService);
                    break;
            }
        }

        private void SendStart(UserService userService)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                                new[]
                                    {
                                    new[]{
                                        new InlineKeyboardButton("Начать работу") {CallbackData = "StartWork"},
                                    }
                                });

            if (userService.ClientUserService == null)
            {
                DataStorage dataStorage = DataStorage.GetInstance();
                dataStorage.Clients.Add(userService.Id, new Client(userService.Id, userService.Name));
                userService.ClientUserService = dataStorage.Clients[userService.Id];
                dataStorage.RewriteFileForClients();
                userService.TgClient.SendTextMessageAsync(userService.Id, "Добрый день! Меня зовут Паша - я менеджер задач. " +
                                "\tЯ могу создавать для Вас доски или присоединять уже к существующим." +
                                "\tВ доске Вы можете создавать или удалять задачи и контролировать процесс их выполнения или брать на себя их выполнение" +
                                "\tВы можете добавлять своих коллег, чтобы они могли принимать участие в выполнении задач Вашей доски." +
                                "\tВы готовы начать работу?", replyMarkup: keyboard);
            }
            else
            {
                userService.TgClient.SendTextMessageAsync(userService.Id, "Добрый день! С возвращением, готовы приступить к работе? ", replyMarkup: keyboard);
            }
        }
    }
}
