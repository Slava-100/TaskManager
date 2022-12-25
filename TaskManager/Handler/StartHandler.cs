using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class StartHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "StartWork":
                            userServise.TgClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
                            userServise.SetHandler(new MainMenuHandler());
                            userServise.HandleUpdate(update);
                            break;
                        default:
                            SendStart(userServise);
                            break;
                    }
                    break;
                default:
                    SendStart(userServise);
                    break;
            }
        }

        private void SendStart(UserService userServise)
        {
            //InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            //                    new[]
            //                        {
            //                        new[]{
            //                            new InlineKeyboardButton("Начать работу") {CallbackData = "StartWork"},
            //                        }
            //                    });

            InlineKeyboardMarkup keyboard = new InlineKeyboardButton("Начать работу") { CallbackData = "StartWork" };

            if (userServise.ClientUserService == null)
            {
                DataStorage dataStorage = DataStorage.GetInstance();
                dataStorage.Clients.Add(userServise.Id, new Client(userServise.Id, userServise.Name));
                userServise.ClientUserService = dataStorage.Clients[userServise.Id];
                dataStorage.RewriteFileForClients();
                userServise.TgClient.SendTextMessageAsync(userServise.Id, "Добрый день! Меня зовут Паша - я менеджер задач. " +
                                "\tЯ могу создавать для Вас доски или присоединять уже к существующим." +
                                "\tВ доске Вы можете создавать или удалять задачи и контролировать процесс их выполнения или брать на себя их выполнение" +
                                "\tВы можете добавлять своих коллег, чтобы они могли принимать участие в выполнении задач Вашей доски." +
                                "\tВы готовы начать работу?", replyMarkup: keyboard);
            }
            else
            {
                userServise.TgClient.SendTextMessageAsync(userServise.Id, "Добрый день! С возвращением, готовы приступить к работе? ", replyMarkup: keyboard);
            }
        }
    }
}
