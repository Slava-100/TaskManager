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

        public async void HandleUpdateHandler(Update update, UserService userService)
        {

            Dictionary<int, Board> _boards = _dataStorage.Boards;

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (long.TryParse(update.Message.Text, out var keyBoard))
                    {
                        if (keyBoard == _boards[_numberBoard].Key)
                        {
                            userService.ClientUserService.AddNewUserByKey(_numberBoard, keyBoard);
                            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Поздравляем! Вы присоеденились к доске  с номером {_numberBoard}");
                            userService.SetHandler(new MainMenuHandler());
                            userService.HandleUpdate(update);
                        }
                        else
                        {
                            await userService.TgClient.SendTextMessageAsync(userService.Id, "Вы ввели неверный ключ", replyMarkup: GetBackButton());
                        }
                    }
                    else
                    {
                        await userService.TgClient.SendTextMessageAsync(userService.Id, "Вам необходимо ввести числовое значение ключа", replyMarkup: GetBackButton());
                    }
                    break;
                case UpdateType.CallbackQuery:
                    if (update.CallbackQuery.Data == "BackToJoinTheBoard")
                    {
                        userService.SetHandler(new JoinTheBoardHandler());
                        userService.HandleUpdate(update);
                    }
                    break;
                default:
                    userService.SetHandler(new MainMenuHandler());
                    break;
            }
        }

        private InlineKeyboardMarkup GetBackButton()
        {
            return new InlineKeyboardButton("Назад") { CallbackData = "BackToJoinTheBoard" };
        }
    }
}
