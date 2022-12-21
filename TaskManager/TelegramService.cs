using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TaskManager
{
    public class TelegramService
    {
        private ITelegramBotClient _bot;

        private DataStorage _dataStorage = DataStorage.GetInstance();

        public TelegramService()
        {
            string token = @"5934008674:AAGx_6xThM933nF22Dxk6VdRUxrBAX03NSk";
            _bot = new TelegramBotClient(token);

            Console.WriteLine("Запущен бот " + _bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long chatId = -1;
            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message is not null)
                    {
                        string text = update.Message.Text.ToLower();
                        chatId = update.Message.Chat.Id;
                        string fullNameClient = $"{update.Message.Chat.FirstName} {update.Message.Chat.LastName}";

                        if (text == "/start")
                        {
                            if (!_dataStorage.Clients.ContainsKey(chatId))
                            {
                                _dataStorage.Clients.Add(chatId, new Client(chatId, fullNameClient,_bot));
                                _dataStorage.RewriteFileForClients();
                            }
                        }
                    }
                    break;
                case UpdateType.CallbackQuery:
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    break;
            }

            //if (chatId == -1)
            //{
            //    _dataStorage.Clients[chatId]
            //}

            #region
            //string name = update.Message.Chat.FirstName;
            //long id = update.Message.Chat.Id;

            //Console.WriteLine(name + " " + id);

            //if (update.Message.Text is not null)
            //{
            //    switch (update.Message.Text)
            //    {
            //        case "/start" or "/Start":
            //            bool flag = DataStorage.GetInstance().Clients.ContainsKey(update.Message.Chat.Id);
            //            if (!flag)
            //            {
            //                _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет. Меня зовут {_bot.GetMeAsync().Result.FirstName}. Я предоставляю удобную командную работу над общим проектом," +
            //                    $" а именно создание доски в которую можно добавлять, удалять задачи, брать задачи на выполнение, менять их статус... Начнём работу? (Да/нет)"+"/Да"+"/нет");
            //            }
            //            else
            //            {
            //                _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет, рад тебя видеть снова!");
            //            }
            //            break;

            //        case "Да":
            //            _bot.SendTextMessageAsync(update.Message.Chat.Id, "\n 1 - Присоединиться к существующей доске \n 2 - Создать доску");
            //            break;

            //        case "1":
            //            _bot.SendTextMessageAsync(update.Message.Chat.Id, "");
            //            break;

            //        default:
            //            _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Я не понимаю тебя!");
            //            break;
            //    }
            #endregion
        }
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}
