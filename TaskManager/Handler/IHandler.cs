using Telegram.Bot.Types;

namespace TaskManager.Handler
{
    public interface IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise);
    }
}