using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TaskManager
{
    public class TelegramService
    {
        ITelegramBotClient _bot;

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
            string name = update.Message.Chat.FirstName;
            long id = update.Message.Chat.Id;
            Console.WriteLine(name + " " + id);
            
            if (update.Message.Text is not null)
            {
                if (update.Message.Text.ToLower() == "/start")
                {
                    bool flag = DataStorage.GetInstance().Users.ContainsKey(update.Message.Chat.Id);
                    if (!flag) 
                    {
                        _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет. Меня зовут {_bot.GetMeAsync().Result.FirstName}. Я предоставляю удобную командную работу над общим проектом," +
                            $" а именно создание доски в которую можно добавлять, удалять задачи, брать задачи на выполнение, менять их статус... Начнём работу?");
                    }
                    else
                    {
                        _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет, рад тебя видеть снова!");
                    }
                }
                else
                {
                    _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Я не понимаю тебя!");
                }
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}
