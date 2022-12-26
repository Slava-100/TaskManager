using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TaskManager
{
    public class TelegramService
    {
        private Dictionary<long, UserService> _userService = new Dictionary<long, UserService>();

        private DataStorage _dataStorage = DataStorage.GetInstance();

        private ITelegramBotClient _bot;

        public TelegramService()
        {
            string token = @"5919984451:AAHv_KcJuWKeNTdrxXY1P80y31Cbu2PqSl8";

            //это наш рабочий токен
            //string token = @"5934008674:AAGx_6xThM933nF22Dxk6VdRUxrBAX03NSk";

            //токен юриного бота
            //            string token = @"5731544843:AAG8oa7weu6AdvGYgK4rByEW-qPvqcm0nYQ";

            _bot = new TelegramBotClient(token);
            Console.WriteLine("Запущен бот " + _bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
                ThrowPendingUpdates = true
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
                            if (!_userService.ContainsKey(chatId))
                            {
                                _userService.Add(chatId, new UserService(chatId, update.Message.Chat.FirstName, _bot));
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
                _userService[chatId].HandleUpdate(update);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}
