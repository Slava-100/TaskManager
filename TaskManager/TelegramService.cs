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
            string token = @"5815242340:AAHJwcZ5QaUSWotzJpmAylXcRVEZsdGGBXc";

            _bot = new TelegramBotClient(token);
            Console.WriteLine("Запущен бот " + _bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[] { UpdateType.Message, UpdateType.CallbackQuery },
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
                        else if (!_userService.ContainsKey(chatId) && text != "/start")
                        {
                            _bot.SendTextMessageAsync(chatId, "Для начала работы с ботом введите /start");
                            chatId = -1;
                        }
                    }
                    break;
                case UpdateType.CallbackQuery:
                    if (!_userService.ContainsKey(update.CallbackQuery.Message.Chat.Id))
                    {
                        _bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Для начала работы с ботом введите /start");
                    }
                    else
                    {
                        chatId = update.CallbackQuery.Message.Chat.Id;
                    }
                    break;
            }
            

            if (chatId != -1)
            {
                _userService[chatId].HandleUpdate(update);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            foreach (var userService in _userService)
            {
                _bot.SendTextMessageAsync(userService.Key, $"Я СЛОМАЛСЯ! \n{exception}");
            }
        }
    }
}

