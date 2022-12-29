using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskManager
{
    public class ClientService
    {
        private IHandler _handler;

        public ITelegramBotClient TgClient { get; set; }

        public string AccName { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public Client ClientUserService { get; set; }

        public ClientService(long id, string name, ITelegramBotClient client)
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

        public IHandler GetHandler()
        {
            return _handler;
        }

        public void HandleUpdate(Update update)
        {
            _handler.HandleUpdateHandler(update, this);
        }
    }
}
