using TaskManager.Handl;
using TaskManager.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class AddTaskHandler : IHandler
    {
        public void AskWhriteIssueDescription(UserService userService)
        {
            InlineKeyboardMarkup keyboard=new InlineKeyboardButton("Меню задач") { CallbackData = "ShowTasksHandler" };
            userService.TgClient.SendTextMessageAsync(userService.Id, "Создание задачи:" +
                "\nНапишите, пожалуйста, описание задачи!" +
                "\nЧтобы вернуться в меню задач,нажмите \"меню задач\"",replyMarkup:keyboard);
        }

        

        public void HandleUpdateHandler(Update update, UserService userService)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    bool result = userService.ClientUserService.AddNewIssue(update.Message.Text);
                    if (result)
                    {
                        Issue newIssue = userService.ClientUserService._activeBoard.Issues.Last();
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Задача создана:\n\n" + newIssue.ToString());
                        userService.SetHandler(new ShowTasksHandler());
                        userService.HandleUpdate(update);
                    }
                    else
                    {
                        userService.TgClient.SendTextMessageAsync(userService.Id, "Задача не создана, попробуйте ещё раз!");
                        AskWhriteIssueDescription(userService);
                    }
                    break; 
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "ShowTasksHandler":
                            userService.SetHandler(new ShowTasksHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            AskWhriteIssueDescription(userService);
                            break;
                    }
                    break;
                default:
                    AskWhriteIssueDescription(userService);
                    break;
            }
        }
    }
}