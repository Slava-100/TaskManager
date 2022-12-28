﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Handl;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.Handler
{
    public class ChangeStatusOfIssueHandler : IHandler
    {

        private int _numberIssue;

        public ChangeStatusOfIssueHandler(int numberIssue)
        {
            _numberIssue = numberIssue;
        }
        public async void HandleUpdateHandler(Update update, UserService userService)
        {
            List<Issue> issues = userService.ClientUserService.GetAllIssuesInBoardByBoard();

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "MoveToBacklog":
                            userService.ClientUserService.MoveIssueFromInProgressToBacklog(_numberIssue);
                            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Теперь с задача с номером: {issues.FirstOrDefault(i => i.NumberIssue == _numberIssue).NumberIssue} находится в статусе Backlog в общем списке задач доски.");
                            break;
                        case "MoveToReview":
                            userService.ClientUserService.MoveIssueFromInProgressToReview(_numberIssue);
                            await userService.TgClient.SendTextMessageAsync(userService.Id, $"Теперь с задача с номером: {issues.FirstOrDefault(i => i.NumberIssue == _numberIssue).NumberIssue} находится в статусе Review в списке выполненных вами задач.");
                            break;
                        case "BackToShowIssue":
                            userService.SetHandler(new ShowIssueHandler());
                            userService.HandleUpdate(update);
                            break;
                        default:
                            await userService.TgClient.SendTextMessageAsync(userService.Id, $"{issues.FirstOrDefault(i => i.NumberIssue == _numberIssue)}", replyMarkup: Button(userService));
                            break;
                    }
                    break;
                default:
                    await userService.TgClient.SendTextMessageAsync(userService.Id, $"{issues.FirstOrDefault(i => i.NumberIssue == _numberIssue)}", replyMarkup: Button(userService));
                    break;
            }
        }

        private InlineKeyboardMarkup Button(UserService userService)
        {
            InlineKeyboardMarkup keyboard;
            keyboard = new InlineKeyboardMarkup(
    new[]
    {
                        new[]
                        {
                            new InlineKeyboardButton("Перенести в Backlog ") {CallbackData = "MoveToBacklog"},
                            new InlineKeyboardButton("Перенести в Review") {CallbackData="MoveToReview"}
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") {CallbackData = "BackToShowIssue"}
                        }
    });

            return keyboard;
        }
    }
}


