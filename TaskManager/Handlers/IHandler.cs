using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TaskManager.Handlers
{
    public interface IHandler
    {
        public void HandlerUpdate(Update update, Client client);
    }
}
