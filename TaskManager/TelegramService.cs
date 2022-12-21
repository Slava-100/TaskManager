using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TaskManager
{
    public class TelegramService
    {
        private ITelegramBotClient _bot;

        private Dictionary<long, UserService> _UserServices;
        DataStorage _dataStorage = DataStorage.GetInstance();
        public TelegramService()
        {
            _UserServices = new Dictionary<long, UserService>();

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
                    if (update.Message.Text is not null)
                    {
                        string text = update.Message.Text.ToLower();
                        chatId = update.Message.Chat.Id;

                        if (text == "/start")
                        {
                            if (!_dataStorage.Clients.ContainsKey(chatId))
                            {
                                _UserServices.Add(chatId, new UserService(chatId, update.Message.Chat.FirstName, _bot));
                            }
                        }
                    }
                    break;
                case UpdateType.CallbackQuery:
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    break;
            }

            if (chatId != -1)
            {
                _UserServices[chatId].HandleUpdate(update);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}
