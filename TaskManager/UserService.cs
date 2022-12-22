using System.Reflection.Metadata;
using Telegram.Bot.Types;
using Telegram.Bot;
using TaskManager.Handl;

namespace TaskManager
{
    public class UserService
    {
        public ITelegramBotClient TgClient { get; set; }
        private IHandler _handler;
        public long Id { get; set; }
        public string Name { get; set; }

        public UserService(long id, string name, ITelegramBotClient client)
        {
            TgClient = client;
            Id = id;
            Name = name;
            _handler = new StartHandler();
        }

        public void SetHandler(IHandler handler)
        {
            _handler = handler;
        }

        public void HandleUpdate(Update update)
        {
            _handler.HandleUpdateHandler(update, this);
        }
    }
}
