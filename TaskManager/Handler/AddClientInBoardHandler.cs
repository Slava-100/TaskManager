using TaskManager.Handl;
using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class AddClientInBoardHandler : IHandler
    {
        DataStorage _dataStorage = DataStorage.GetInstance();
        private int _numberBoard;

        public AddClientInBoardHandler(int numberBoard)
        {
            _numberBoard = numberBoard;
        }

        public async void HandleUpdateHandler(Update update, UserService userServise)
        {

            Dictionary<int, Board> _boards = _dataStorage.Boards;

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (long.TryParse(update.Message.Text, out var keyBoard))
                    {
                        if (keyBoard == _boards[_numberBoard].Key)
                        {
                            userServise.SetHandler(new MainMenuHandler());
                            _boards[_numberBoard].IDMembers.Add(userServise.Id);
                            _dataStorage.Clients[userServise.Id].BoardsForUser.Add(_numberBoard);
                            await userServise.TgClient.SendTextMessageAsync(userServise.Id, $"Поздравляем! Вы присоеденились к доске  с номером {_numberBoard}");
                        }
                        else
                        {
                            await userServise.TgClient.SendTextMessageAsync(userServise.Id, "Вы ввели неверный ключ", replyMarkup: GetBackButton());
                        }
                    }
                    else
                    {
                        userServise.TgClient.SendTextMessageAsync(userServise.Id, "Вам необходимо ввести числовое значение ключа", replyMarkup: GetBackButton());
                    }
                    break;
                case UpdateType.CallbackQuery:
                    if (update.CallbackQuery.Data == "Back")
                    {
                        userServise.SetHandler(new JoinTheBoardHandler());
                        userServise.HandleUpdate(update);
                    }
                    break;
                default:
                    userServise.SetHandler(new MainMenuHandler());
                    break;
            }
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "Back" };
        }
    }
}
