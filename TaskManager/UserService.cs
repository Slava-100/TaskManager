using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskManager
{
    public class UserService
    {
        public ITelegramBotClient TgClient { get; set; }

        private IHandler _handler;

        public string AccName { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public Client ClientUserService { get; set; }

        public UserService(long id, string name, ITelegramBotClient client)
        {
            TgClient = client;
            Id = id;
            Name = name;
            _handler = new StartHandler();
            if (DataStorage.GetInstance().Clients.ContainsKey(id))
            {
                ClientUserService = DataStorage.GetInstance().Clients[id];
            }
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
