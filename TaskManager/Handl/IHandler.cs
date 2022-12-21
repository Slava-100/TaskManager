using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TaskManager.Handl
{
    public interface IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userServise);
    }
}
