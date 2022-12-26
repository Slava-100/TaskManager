using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Handl;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TaskManager.Handler
{
    public class BoardHandler : IHandler
    {
        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        
                    }
                    break;
                default:
                    SubmitsQuestion(userService);
                    break;
            }
        }

        private void SubmitsQuestion(UserService userService)
        {
            userService.TgClient.SendTextMessageAsync(userService.Id, $"Доска №{userService.ClientUserService.GetActiveBoard().NumberBoard} (Твоя роль: {userService.ClientUserService.GetRole()})");
        }
    }
}
