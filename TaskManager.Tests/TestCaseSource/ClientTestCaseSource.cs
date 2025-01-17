﻿using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class ClientTestCaseSource

    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            // 1. проверка на добавление задания к Админу, при всех выполненных условиях

            Issue issue1 = new Issue(1, "1");
            Issue attachIssue = new Issue(2, "2");
            Board board = new Board(10, 5, "boardName");
            board.Issues.Add(issue1);
            board.Issues.Add(attachIssue);
            int idAttachIssue = 2;
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client client = new Client(5, "5");
            client.BoardsForUser.Add(10);
            board.IDAdmin.Add(5);
            Dictionary<long, Client> baseClients = new Dictionary<long, Client>
            {
                { 5, client }
            };

            Issue expIssue1 = new Issue(1, "1");
            Issue expAttachIssue = new Issue(2, "2");
            expAttachIssue.Status = Enums.IssueStatus.InProgress;
            Client expClient = new Client(5, "5");
            Board expBoard = new Board(10, 5, "boardName");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expAttachIssue);
            expClient.BoardsForUser.Add(10);
            expBoard.IDAdmin.Add(5);
            expAttachIssue.IdUser = expClient.IDUser;
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                { 5, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };


            // 2. проверка на добавление задания к Мемберу, при всех выполненных условиях

            Issue issue10 = new Issue(10, "10");
            attachIssue = new Issue(20, "20");
            Client admin = new Client(22, "22");
            board = new Board(22, 22, "boardName");
            board.Issues.Add(issue10);
            board.Issues.Add(attachIssue);
            idAttachIssue = 20;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            client = new Client(50, "50");

            client.BoardsForUser.Add(22);
            board.IDMembers.Add(50);
            baseClients = new Dictionary<long, Client>
            {
                { 22, admin},
                { 50, client }
            };

            Issue expIssue10 = new Issue(10, "10");
            expAttachIssue = new Issue(20, "20");
            expAttachIssue.Status = Enums.IssueStatus.InProgress;
            //expIssue10.Status = Enums.IssueStatus.Backlog;
            Client expAdmin = new Client(22, "22");
            expBoard = new Board(22, 22, "boardName");
            expBoard.Issues.Add(expIssue10);
            expBoard.Issues.Add(expAttachIssue);
            expClient = new Client(50, "50");
            expClient.BoardsForUser.Add(22);
            expBoard.IDMembers.Add(50);
            expAttachIssue.IdUser = expClient.IDUser;

            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 22, expAdmin},
                { 50, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 3. проверка на недобавление задания к Админу, если такого задания нет в доске

            issue1 = new Issue(13, "13");
            board = new Board(103, 53, "boardName");
            board.Issues.Add(issue1);
            idAttachIssue = 23;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            client = new Client(53, "53");
            client.BoardsForUser.Add(103);
            board.IDAdmin.Add(53);
            baseClients = new Dictionary<long, Client>
            {
                { 53, client }
            };

            expIssue1 = new Issue(13, "13");
            expClient = new Client(53, "53");
            expBoard = new Board(103, 53, "boardName");
            expBoard.Issues.Add(expIssue1);
            expClient.BoardsForUser.Add(103);
            expBoard.IDAdmin.Add(53);
            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 53, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 4. проверка на недобавление задания к Мемберу, если такого задания нет в доске

            issue10 = new Issue(104, "104");
            admin = new Client(224, "224");
            board = new Board(224, 224, "boardName");
            board.Issues.Add(issue10);
            idAttachIssue = 204;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            client = new Client(504, "504");

            client.BoardsForUser.Add(224);
            board.IDMembers.Add(504);
            baseClients = new Dictionary<long, Client>
            {
                { 224, admin},
                { 504, client }
            };

            expIssue10 = new Issue(104, "104");
            expAdmin = new Client(224, "224");
            expBoard = new Board(224, 224, "boardName");
            expBoard.Issues.Add(expIssue10);
            expClient = new Client(504, "504");
            expClient.BoardsForUser.Add(224);
            expBoard.IDMembers.Add(504);

            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 224, expAdmin},
                { 504, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 5. проверка на недобавление задания к Админу, если у него уже есть другие задания в InProgress

            issue1 = new Issue(15, "15");
            issue1.Status = Enums.IssueStatus.InProgress;
            client = new Client(55, "55");
            issue1.IdUser = client.IDUser;
            attachIssue = new Issue(25, "25");
            attachIssue.Status = Enums.IssueStatus.InProgress;
            board = new Board(105, 55, "boardName");
            board.Issues.Add(issue1);
            board.Issues.Add(attachIssue);
            idAttachIssue = 25;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            client.BoardsForUser.Add(105);
            board.IDAdmin.Add(55);
            baseClients = new Dictionary<long, Client>
            {
                { 55, client }
            };

            expIssue1 = new Issue(15, "15");
            expIssue1.Status = Enums.IssueStatus.InProgress;
            expClient = new Client(55, "55");
            expIssue1.IdUser = expClient.IDUser;
            expAttachIssue = new Issue(25, "25");
            expAttachIssue.Status = Enums.IssueStatus.InProgress;
            expBoard = new Board(105, 55, "boardName");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expAttachIssue);
            expClient.BoardsForUser.Add(105);
            expBoard.IDAdmin.Add(55);
            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 55, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 6. проверка на недобавление задания к Мемберу, если у него уже есть другие задания в InProgress

            client = new Client(506, "506");
            issue10 = new Issue(106, "106");
            issue10.Status = Enums.IssueStatus.InProgress;
            issue10.IdUser = client.IDUser;
            attachIssue = new Issue(206, "206");
            attachIssue.Status = Enums.IssueStatus.InProgress;
            admin = new Client(226, "226");
            board = new Board(226, 226, "boardName");
            board.Issues.Add(issue10);
            board.Issues.Add(attachIssue);
            idAttachIssue = 206;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };

            client.BoardsForUser.Add(226);
            board.IDMembers.Add(506);
            baseClients = new Dictionary<long, Client>
            {
                { 226, admin},
                { 506, client }
            };

            expClient = new Client(506, "506");
            expIssue10 = new Issue(106, "106");
            expIssue10.Status = Enums.IssueStatus.InProgress;
            expAttachIssue = new Issue(206, "206");
            expAttachIssue.Status = Enums.IssueStatus.InProgress;
            expAdmin = new Client(226, "226");
            expBoard = new Board(226, 226, "boardName");
            expBoard.Issues.Add(issue10);
             expBoard.Issues.Add(expAttachIssue);
            expClient.BoardsForUser.Add(226);
            expBoard.IDMembers.Add(506);

            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 226, expAdmin},
                { 506, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 7. проверка на недобавление задания к Админу, если задание находится  в InProgress у другого участника

            client = new Client(557, "557");
            Client otherClient = new Client(1, "1");
            //issue1.IdUser = client.IDUser;
            attachIssue = new Issue(257, "257");
            attachIssue.Status = Enums.IssueStatus.InProgress;
            attachIssue.IdUser = otherClient.IDUser;
            board = new Board(1057, 557, "boardName");
            board.Issues.Add(attachIssue);
            idAttachIssue = 257;
            baseBoards = new Dictionary<int, Board>
            {
                {board.NumberBoard, board }
            };
            client.BoardsForUser.Add(1057);
            board.IDAdmin.Add(557);
            baseClients = new Dictionary<long, Client>
            {
                { 557, client },
                { 1, otherClient}
            };

            expClient = new Client(557, "557");
            Client expOtherClient = new Client(1, "1");
            expAttachIssue = new Issue(257, "257");
            expAttachIssue.IdUser = expOtherClient.IDUser;
            expAttachIssue.Status = Enums.IssueStatus.InProgress;
            expBoard = new Board(1057, 557, "boardName");
            expBoard.Issues.Add(expAttachIssue);
            expClient.BoardsForUser.Add(1057);
            expBoard.IDAdmin.Add(557);
            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 557, expClient },
                { 1, expOtherClient}
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            // 8. проверка на недобавление задания к Мемберу, если задание находится  в InProgress у другого участника

            issue10 = new Issue(1048, "1048");
            admin = new Client(2248, "2248");
            issue10.Status = Enums.IssueStatus.InProgress;
            issue10.IdUser = admin.IDUser;
            board = new Board(2248, 2248, "boardName");
            board.Issues.Add(issue10);
            idAttachIssue = 1048;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            client = new Client(5048, "5048");

            client.BoardsForUser.Add(2248);
            board.IDMembers.Add(5048);
            baseClients = new Dictionary<long, Client>
            {
                { 2248, admin},
                { 5048, client }
            };

            expIssue10 = new Issue(1048, "1048");
            expAdmin = new Client(2248, "2248");
            expClient = new Client(5048, "5048");
            expIssue10.Status = Enums.IssueStatus.InProgress;
            expIssue10.IdUser = expAdmin.IDUser;
            expBoard = new Board(2248, 2248, "boardName");
            expBoard.Issues.Add(issue10);
            expClient.BoardsForUser.Add(2248);
            expBoard.IDMembers.Add(5048);

            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 2248, expAdmin},
                { 5048, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            //9. проверка на не добавление задания к человеку, если он не является админом или мембером

            issue1 = new Issue(18, "18");
            attachIssue = new Issue(28, "28");
            board = new Board(108, 58, "boardName");
            board.Issues.Add(issue1);
            board.Issues.Add(attachIssue);
            idAttachIssue = 28;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client clientNope = new Client(58, "58");
            client = new Client(666, "666");
            clientNope.BoardsForUser.Add(108);
            board.IDAdmin.Add(58);
            baseClients = new Dictionary<long, Client>
            {
                { 666, client },
                { 58, clientNope }
            };

            expIssue1 = new Issue(18, "18");
            expAttachIssue = new Issue(28, "28");
            expClient = new Client(58, "58");
            expBoard = new Board(108, 58, "boardName");
            expClient.BoardsForUser.Add(108);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expAttachIssue);
            expBoard.IDAdmin.Add(58);
            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
                {666, new Client(666, "666") },
                { 58, expClient }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

        }

        public static IEnumerable GetAllIssuesInBoardByBoardTestCaseSource()
        {
            //1. Проверка для Админа, где на него записаны только 1 и 2 задачи, 3 задача на другом участнике

            Client baseClient = new Client(55, "55");
            Client otherClient = new Client(2, "2");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = baseClient.IDUser;
            Issue issue3 = new Issue(3, "3");
            issue3.IdUser = otherClient.IDUser;
            Board baseBoard = new Board(55, 55, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.IDMembers.Add(2);

            Client expClient = new Client(55, "55");
            Client expOtherClient = new Client(2, "2");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expClient.IDUser;
            Issue expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expOtherClient.IDUser;
            Board expBoard = new Board(55, 55, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(2);
            List<Issue> expectedIssues = new List<Issue> { expIssue1, expIssue2 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };

            //2. Проверка для Мембера, где на него записаны только 1 и 3 задачи, 2 задача на другом участнике

            Client admin = new Client(999, "999");
            baseClient = new Client(550, "550");
            otherClient = new Client(2, "2");
            issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            issue2 = new Issue(2, "2");
            issue2.IdUser = admin.IDUser;
            issue3 = new Issue(3, "3");
            issue3.IdUser = baseClient.IDUser;
            baseBoard = new Board(999, 999, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.IDMembers.Add(550);
            baseBoard.IDMembers.Add(2);

            Client expAdmin = new Client(999, "999");
            expClient = new Client(550, "550");
            expOtherClient = new Client(2, "2");
            expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = admin.IDUser;
            expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expClient.IDUser;
            expBoard = new Board(999, 999, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(550);
            expBoard.IDMembers.Add(2);
            expectedIssues = new List<Issue> { expIssue1, expIssue3 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };

            //3. Проверка для Админа, где на него записаны 5 задач, которые должны отсортироваться

            baseClient = new Client(55, "55");
            issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.InProgress;
            issue2 = new Issue(2, "2");
            issue2.IdUser = baseClient.IDUser;
            issue2.Status = Enums.IssueStatus.Done;
            issue3 = new Issue(3, "3");
            issue3.IdUser = baseClient.IDUser;
            issue3.Status = Enums.IssueStatus.UserStory;
            Issue issue4 = new Issue(4, "4");
            issue4.IdUser = baseClient.IDUser;
            issue4.Status = Enums.IssueStatus.Backlog;
            Issue issue5 = new Issue(5, "5");
            issue5.Status = Enums.IssueStatus.Review;
            issue5.IdUser = baseClient.IDUser;
            baseBoard = new Board(55, 55, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.Issues.Add(issue4);
            baseBoard.Issues.Add(issue5);
            baseBoard.IDMembers.Add(2);

            expClient = new Client(55, "55");
            expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expClient.IDUser;
            expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expClient.IDUser;
            expIssue1.Status = Enums.IssueStatus.InProgress;
            expIssue2.Status = Enums.IssueStatus.Done;
            expIssue3.Status = Enums.IssueStatus.UserStory;
            Issue expIssue4 = new Issue(4, "4");
            expIssue4.IdUser = expClient.IDUser;
            expIssue4.Status = Enums.IssueStatus.Backlog;
            Issue expIssue5 = new Issue(5, "5");
            expIssue5.Status = Enums.IssueStatus.Review;
            expIssue5.IdUser = expClient.IDUser;
            expBoard = new Board(55, 55, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expBoard.Issues.Add(expIssue4);
            expBoard.Issues.Add(expIssue5);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(2);
            expectedIssues = new List<Issue> { expIssue3, expIssue4, expIssue1, expIssue5, expIssue2 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };

            //4. Проверка для Мембера, где на нем находятся 5 задач, которые должны быть отсортированы

            admin = new Client(999, "999");
            baseClient = new Client(550, "550");
            //otherClient = new Client(2, "2");
            issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            issue2 = new Issue(2, "2");
            issue2.IdUser = baseClient.IDUser;
            issue3 = new Issue(3, "3");
            issue3.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.Backlog;
            issue2.Status = Enums.IssueStatus.UserStory;
            issue3.Status = Enums.IssueStatus.InProgress;
            issue4 = new Issue(4, "4");
            issue4.IdUser = baseClient.IDUser;
            issue5 = new Issue(5, "5");
            issue5.IdUser = baseClient.IDUser;
            issue4.Status = Enums.IssueStatus.Done;
            issue5.Status = Enums.IssueStatus.Review;
            baseBoard = new Board(999, 999, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.Issues.Add(issue4);
            baseBoard.Issues.Add(issue5);
            baseBoard.IDMembers.Add(550);
            //baseBoard.IDMembers.Add(2);

            expAdmin = new Client(999, "999");
            expClient = new Client(550, "550");
            //expOtherClient = new Client(2, "2");
            expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expClient.IDUser;
            expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expClient.IDUser;
            expIssue1.Status = Enums.IssueStatus.Backlog;
            expIssue2.Status = Enums.IssueStatus.UserStory;
            expIssue3.Status = Enums.IssueStatus.InProgress;
            expIssue4 = new Issue(4, "4");
            expIssue4.IdUser = baseClient.IDUser;
            expIssue5 = new Issue(5, "5");
            expIssue5.IdUser = baseClient.IDUser;
            expIssue4.Status = Enums.IssueStatus.Done;
            expIssue5.Status = Enums.IssueStatus.Review;
            expBoard = new Board(999, 999, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expBoard.Issues.Add(expIssue4);
            expBoard.Issues.Add(expIssue5);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(550);
            //expBoard.IDMembers.Add(2);
            expectedIssues = new List<Issue> { expIssue2, expIssue1, expIssue3, expIssue5, expIssue4 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };
        }

        public static IEnumerable GetIssuesDoneInBoardByBoardTestCaseSource()
        {
            //1. Проверка для Админа, где на него записаны только 1 задача с Done, 2 задача на него - но с другим статусом, 3 задача на другом участнике

            Client baseClient = new Client(55, "55");
            Client otherClient = new Client(2, "2");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.Done;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = baseClient.IDUser;
            issue2.Status = Enums.IssueStatus.Review;
            Issue issue3 = new Issue(3, "3");
            issue3.IdUser = otherClient.IDUser;
            issue3.Status = Enums.IssueStatus.Done;
            Board baseBoard = new Board(55, 55, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.IDMembers.Add(2);

            Client expClient = new Client(55, "55");
            Client expOtherClient = new Client(2, "2");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            expIssue1.Status = Enums.IssueStatus.Done;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expClient.IDUser;
            expIssue2.Status = Enums.IssueStatus.Review;
            Issue expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expOtherClient.IDUser;
            expIssue3.Status = Enums.IssueStatus.Done;
            Board expBoard = new Board(55, 55, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(2);
            List<Issue> expectedIssues = new List<Issue> { expIssue1 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };

            //2. Проверка для Мембера, где на него записаны только 3 задача со статусом Done, 1 тоже на него - но с дургим статусом, 2 задача на другом участнике

            Client admin = new Client(9992, "9992");
            baseClient = new Client(5502, "5502");
            otherClient = new Client(22, "22");
            issue1 = new Issue(12, "12");
            issue1.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.Review;
            issue2 = new Issue(22, "22");
            issue2.IdUser = admin.IDUser;
            issue2.Status = Enums.IssueStatus.Done;
            issue3 = new Issue(32, "32");
            issue3.IdUser = baseClient.IDUser;
            issue3.Status = Enums.IssueStatus.Done;
            baseBoard = new Board(9992, 9992, "boardMy");
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.IDMembers.Add(5502);
            baseBoard.IDMembers.Add(22);

            Client expAdmin = new Client(9992, "9992");
            expClient = new Client(5502, "5502");
            expOtherClient = new Client(22, "22");
            expIssue1 = new Issue(12, "12");
            expIssue1.IdUser = expClient.IDUser;
            expIssue1.Status = Enums.IssueStatus.Review;
            expIssue2 = new Issue(22, "22");
            expIssue2.IdUser = admin.IDUser;
            expIssue2.Status = Enums.IssueStatus.Done;
            expIssue3 = new Issue(32, "32");
            expIssue3.IdUser = expClient.IDUser;
            expIssue3.Status = Enums.IssueStatus.Done;
            expBoard = new Board(9992, 9992, "boardMy");
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(552);
            expBoard.IDMembers.Add(5502);
            expBoard.IDMembers.Add(22);
            expectedIssues = new List<Issue> { expIssue3 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };
        }

        public static IEnumerable GetAllBoardsByNumbersOfBoardTestCaseSource()
        {
            //1. Проверка, если запрашиваем доски для Админа

            Client client = new Client(10, "10");
            Board board1 = new Board(1, 10, "boardMy");
            Board board2 = new Board(2, 10, "boardMy");
            List<int> baseBoardsForUser = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard
            };
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                {board1.NumberBoard, board1 },
                {board2.NumberBoard, board2 }
            };
            client.BoardsForUser = baseBoardsForUser;

            Client expClient = new Client(10, "10");
            Board expBoard1 = new Board(1, 10, "boardMy");
            Board expBoard2 = new Board(2, 10, "boardMy");
            Dictionary<int, Board> expBaseBoards = new Dictionary<int, Board>
            {
                {expBoard1.NumberBoard, expBoard1 },
                {expBoard2.NumberBoard, expBoard2 }
            };
            List<int> expBoardsForUser = new List<int>
            {
            expBoard1.NumberBoard,
            expBoard2.NumberBoard
            };
            client.BoardsForUser = expBoardsForUser;
            List<Board> expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { client, baseBoards, baseBoardsForUser, expectedBoards };

            //2. Проверка, если запрашиваем доски для Мембера

            Client admin = new Client(10, "10");
            client = new Client(100, "100");
            board1 = new Board(10, 10, "nameOfBoard");
            board1.IDMembers.Add(100);
            board2 = new Board(20, 10, "nameOfBoard");
            board2.IDMembers.Add(100);
            baseBoardsForUser = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard
            };
            baseBoards = new Dictionary<int, Board>
            {
                {board1.NumberBoard, board1 },
                {board2.NumberBoard, board2 }
            };
            client.BoardsForUser = baseBoardsForUser;

            Client expAdmin = new Client(10, "10");
            expClient = new Client(100, "100");
            expBoard1 = new Board(10, 10, "nameOfBoard");
            expBoard1.IDMembers.Add(100);
            expBoard2 = new Board(20, 10, "nameOfBoard");
            expBoard2.IDMembers.Add(100);
            expBaseBoards = new Dictionary<int, Board>
            {
                {expBoard1.NumberBoard, expBoard1 },
                {expBoard2.NumberBoard, expBoard2 }
            };
            expBoardsForUser = new List<int>
            {
            expBoard1.NumberBoard,
            expBoard2.NumberBoard
            };
            client.BoardsForUser = expBoardsForUser;
            expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { client, baseBoards, baseBoardsForUser, expectedBoards };

            //3. Проверка, если запрашиваем доски для Админа, где часть досок на другом участнике

            client = new Client(103, "103");
            board1 = new Board(13, 103, "nameOfBoard");
            board2 = new Board(23, 103, "nameOfBoard");
            Board boardOther = new Board(66, 66, "nameOfBoard");
            baseBoardsForUser = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard
            };
            baseBoards = new Dictionary<int, Board>
            {
                {board1.NumberBoard, board1 },
                {board2.NumberBoard, board2 },
                { boardOther.NumberBoard, boardOther}
            };
            client.BoardsForUser = baseBoardsForUser;

            expClient = new Client(103, "103");
            expBoard1 = new Board(13, 103, "nameOfBoard");
            expBoard2 = new Board(23, 103, "nameOfBoard");
            Board expBoardOther = new Board(66, 66, "nameOfBoard");
            expBaseBoards = new Dictionary<int, Board>
            {
                {expBoard1.NumberBoard, expBoard1 },
                {expBoard2.NumberBoard, expBoard2 },
                { expBoardOther.NumberBoard , expBoardOther}
            };
            expBoardsForUser = new List<int>
            {
            expBoard1.NumberBoard,
            expBoard2.NumberBoard
            };
            client.BoardsForUser = expBoardsForUser;
            expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { client, baseBoards, baseBoardsForUser, expectedBoards };

            //4. Проверка, если запрашиваем доски для Мембера, где часть досок на другом участнике

            admin = new Client(104, "104");
            client = new Client(1004, "1004");
            board1 = new Board(104, 104, "nameOfBoard");
            board1.IDMembers.Add(1004);
            board2 = new Board(204, 104, "nameOfBoard");
            board2.IDMembers.Add(1004);
            boardOther = new Board(99, 99, "nameOfBoard");
            baseBoardsForUser = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard
            };
            baseBoards = new Dictionary<int, Board>
            {
                {board1.NumberBoard, board1 },
                {board2.NumberBoard, board2 },
                { boardOther.NumberBoard, boardOther}
                        };
            client.BoardsForUser = baseBoardsForUser;

            expBoard1 = new Board(104, 104, "nameOfBoard");
            expBoard1.IDMembers.Add(1004);
            expBoard2 = new Board(204, 104, "nameOfBoard");
            expBoard2.IDMembers.Add(1004);
            expBoardsForUser = new List<int>
            {
            expBoard1.NumberBoard,
            expBoard2.NumberBoard
            };
            client.BoardsForUser = expBoardsForUser;
            expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { client, baseBoards, baseBoardsForUser, expectedBoards };

            //5. Показать все доски у участника, в которых он и Админ, и Мембер

            client = new Client(1045, "1045");
            board1 = new Board(1045, 1045, "nameOfBoard");
            board2 = new Board(2045, 1045, "nameOfBoard");
            boardOther = new Board(995, 995, "nameOfBoard");
            boardOther.IDMembers.Add(1045);
            baseBoardsForUser = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            boardOther.NumberBoard
            };
            baseBoards = new Dictionary<int, Board>
            {
                {board1.NumberBoard, board1 },
                {board2.NumberBoard, board2 },
                { boardOther.NumberBoard, boardOther}
                        };
            client.BoardsForUser = baseBoardsForUser;

            expClient = new Client(1045, "1045");
            expBoard1 = new Board(1045, 1045, "nameOfBoard");
            expBoard2 = new Board(2045, 1045, "nameOfBoard");
            Board expboardOther = new Board(995, 995, "nameOfBoard");
            expboardOther.IDMembers.Add(1045);
            expBoardsForUser = new List<int>
            {
            expBoard1.NumberBoard,
            expBoard2.NumberBoard,
            expboardOther.NumberBoard
            };
            expClient.BoardsForUser = expBoardsForUser;
            expectedBoards = new List<Board> { expBoard1, expBoard2, expboardOther };

            yield return new Object[] { client, baseBoards, baseBoardsForUser, expectedBoards };
        }

        public static IEnumerable GetAllBoardsAdminsTestCaseSource()
        {
            //1. Проверяем метод, где Админ одной из досок заправшивает список своих админских досок.

            Client client = new Client(10, "10");
            Board board1 = new Board(10, 10, "boardMy");
            Board board2 = new Board(20, 20, "boardMy");
            board2.IDAdmin.Add(10);
            Board board3 = new Board(30, 30, "boardMy");
            board3.IDMembers.Add(10);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
                { board3.NumberBoard, board3 }
            };
            List<int> baseListBoards = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            board3.NumberBoard
            };

            Client expClient = new Client(10, "10");
            Board expBoard1 = new Board(10, 10, "boardMy");
            Board expBoard2 = new Board(20, 20, "boardMy");
            expBoard2.IDAdmin.Add(10);
            Board expBoard3 = new Board(30, 30, "boardMy");
            expBoard3.IDMembers.Add(10);
            List<Board> expectedBoards = new List<Board>
            {
            expBoard1,
            expBoard2
            };

            yield return new Object[] { baseBoards, client, baseListBoards, expectedBoards };

            //2. Проверяем метод, где Мембер одной из досок заправшивает список своих админских досок(которых нет).

            client = new Client(102, "102");
            board1 = new Board(2, 2, "boardMy");
            board1.IDMembers.Add(102);
            board2 = new Board(202, 202, "boardMy");
            board2.IDMembers.Add(102);
            board3 = new Board(302, 302, "boardMy");
            board3.IDMembers.Add(102);
            baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
                { board3.NumberBoard, board3 }
            };
            baseListBoards = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            board3.NumberBoard
            };

            expClient = new Client(102, "102");
            expBoard1 = new Board(2, 2, "boardMy");
            expBoard1.IDMembers.Add(102);
            expBoard2 = new Board(202, 202, "boardMy");
            expBoard2.IDMembers.Add(102);
            expBoard3 = new Board(302, 302, "boardMy");
            expBoard3.IDMembers.Add(102);
            expectedBoards = new List<Board>();

            yield return new Object[] { baseBoards, client, baseListBoards, expectedBoards };
        }

        public static IEnumerable GetAllBoardsMembersTestCaseSource()
        {
            //1. Проверяем метод, где Админ одной из досок заправшивает список своих мемборских досок.

            Client client = new Client(10, "10");
            Board board1 = new Board(10, 10, "boardJoy");
            Board board2 = new Board(20, 20, "boardJoy");
            board2.IDAdmin.Add(10);
            Board board3 = new Board(30, 30, "boardJoy");
            board3.IDMembers.Add(10);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
                { board3.NumberBoard, board3 }
            };
            List<int> baseListBoards = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            board3.NumberBoard
            };

            Client expClient = new Client(10, "10");
            Board expBoard1 = new Board(10, 10, "boardJoy");
            Board expBoard2 = new Board(20, 20, "boardJoy");
            expBoard2.IDAdmin.Add(10);
            Board expBoard3 = new Board(30, 30, "boardJoy");
            expBoard3.IDMembers.Add(10);
            List<Board> expectedBoards = new List<Board>
            {
            expBoard3
            };

            yield return new Object[] { baseBoards, client, baseListBoards, expectedBoards };

            //2. Проверяем метод, где Админ одной из досок заправшивает список своих мемборских досок, которых нет.

            client = new Client(102, "102");
            board1 = new Board(19, 19, "boardJoy");
            board1.IDAdmin.Add(102);
            board2 = new Board(202, 202, "boardJoy");
            board2.IDAdmin.Add(102);
            board3 = new Board(302, 302, "boardJoy");
            board3.IDAdmin.Add(102);
            baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
                { board3.NumberBoard, board3 }
            };
            baseListBoards = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            board3.NumberBoard
            };

            expClient = new Client(102, "102");
            expBoard1 = new Board(19, 19, "boardJoy");
            expBoard1.IDAdmin.Add(102);
            expBoard2 = new Board(202, 202, "boardJoy");
            expBoard2.IDAdmin.Add(102);
            expBoard3 = new Board(302, 302, "boardJoy");
            expBoard3.IDMembers.Add(102);
            expectedBoards = new List<Board>();

            yield return new Object[] { baseBoards, client, baseListBoards, expectedBoards };

            //3. Проверяем метод, где Мембер одной из досок заправшивает список своих мемборских

            client = new Client(1023, "1023");
            board1 = new Board(23, 23, "boardJoy");
            board1.IDMembers.Add(1023);
            board2 = new Board(2023, 2023, "boardJoy");
            board2.IDAdmin.Add(1023);
            board3 = new Board(3023, 3023, "boardJoy");
            board3.IDMembers.Add(1023);
            baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
                { board3.NumberBoard, board3 }
            };
            baseListBoards = new List<int>
            {
            board1.NumberBoard,
            board2.NumberBoard,
            board3.NumberBoard
            };

            expClient = new Client(1023, "1023");
            expBoard1 = new Board(23, 23, "boardJoy");
            expBoard1.IDMembers.Add(1023);
            expBoard2 = new Board(2023, 2023, "boardJoy");
            expBoard2.IDAdmin.Add(1023);
            expBoard3 = new Board(3023, 3023, "boardJoy");
            expBoard3.IDMembers.Add(1023);
            expectedBoards = new List<Board>
            {
            board1,
            board3
            };

            yield return new Object[] { baseBoards, client, baseListBoards, expectedBoards };
        }

        public static IEnumerable ChangeRoleFromMemberToAdminTestCaseSource()
        {
            //1. Проверяем, когда Админ может поменять роль у Мембера

            Board activeBoard = new Board(11, 11, "boardJoy");
            Client client = new Client(1, "1");
            Client member = new Client(10, "10");
            Client member2 = new Client(20, "20");
            activeBoard.IDAdmin.Add(1);
            activeBoard.IDMembers.Add(10);
            activeBoard.IDMembers.Add(20);
            long idMemeber = 10;
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            Board expActiveBoard = new Board(11, 11, "boardJoy");
            Client expClient = new Client(1, "1");
            Client expMember = new Client(10, "10");
            Client expMember2 = new Client(20, "20");
            expActiveBoard.IDAdmin.Add(1);
            expActiveBoard.IDAdmin.Add(10);
            expActiveBoard.IDMembers.Add(20);
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idMemeber, expectedBoards };

            //2. Проверяем, что Мембер не может поменять роль у Мембера

            activeBoard = new Board(12, 1, "boardJoy");
            client = new Client(12, "12");
            member = new Client(102, "102");
            member2 = new Client(202, "202");
            activeBoard.IDMembers.Add(12);
            activeBoard.IDMembers.Add(102);
            activeBoard.IDMembers.Add(202);
            idMemeber = 102;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            expActiveBoard = new Board(12, 1, "boardJoy");
            expClient = new Client(12, "12");
            expMember = new Client(102, "102");
            expMember2 = new Client(202, "202");
            expActiveBoard.IDMembers.Add(12);
            expActiveBoard.IDMembers.Add(102);
            expActiveBoard.IDMembers.Add(202);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idMemeber, expectedBoards };

            //3. Проверяем, когда Админ пытается поменять роль у Мембера, которого нет в этой доске

            activeBoard = new Board(13, 13, "boardJoy");
            Board otherBoard = new Board(50, 50, "boardJoy");
            Client otherClient = new Client(49, "49");
            client = new Client(13, "13");
            member = new Client(103, "103");
            member2 = new Client(203, "203");
            activeBoard.IDMembers.Add(103);
            activeBoard.IDMembers.Add(203);
            otherBoard.IDMembers.Add(49);
            idMemeber = 49;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard },
                {otherBoard.NumberBoard, otherBoard }
            };

            expActiveBoard = new Board(13, 13, "boardJoy");
            Board expOtherBoard = new Board(50, 50, "boardJoy");
            Client expOtherClient = new Client(49, "49");
            expClient = new Client(13, "13");
            expMember = new Client(103, "103");
            expMember2 = new Client(203, "203");
            expActiveBoard.IDMembers.Add(103);
            expActiveBoard.IDMembers.Add(203);
            expOtherBoard.IDMembers.Add(49);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard },
                {expOtherBoard.NumberBoard, expOtherBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idMemeber, expectedBoards };

            //4. Проверяем, когда Админ пытается поменять роль у другого Админа (хотя метод рассчитан на изменение статуса Мембера на Админа)

            activeBoard = new Board(134, 134, "boardJoy");
            client = new Client(134, "134");
            member = new Client(1034, "1034");
            member2 = new Client(2034, "2034");
            activeBoard.IDMembers.Add(1034);
            activeBoard.IDAdmin.Add(2034);
            activeBoard.IDMembers.Add(494);
            idMemeber = 2034;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            expActiveBoard = new Board(134, 134, "boardJoy");
            expClient = new Client(134, "134");
            expMember = new Client(1034, "1034");
            expMember2 = new Client(2034, "2034");
            expActiveBoard.IDMembers.Add(1034);
            expActiveBoard.IDAdmin.Add(2034);
            expActiveBoard.IDMembers.Add(494);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idMemeber, expectedBoards };
        }

        public static IEnumerable ChangeRoleFromAdminToMemberTestCaseSource()
        {
            //1. Проверяем, когда Админ может поменять роль у другого Админа

            Board activeBoard = new Board(111, 111, "boardTwo");
            Client client = new Client(111, "111");
            Client admin = new Client(11, "11");
            Client member = new Client(101, "101");
            Client member2 = new Client(201, "201");
            activeBoard.IDAdmin.Add(11);
            activeBoard.IDMembers.Add(101);
            activeBoard.IDMembers.Add(201);
            long idAdmin = 11;
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            Board expActiveBoard = new Board(111, 111, "boardTwo");
            Client expAdmin = new Client(11, "11");
            Client expClient = new Client(111, "111");
            Client expMember = new Client(101, "101");
            Client expMember2 = new Client(201, "201");
            expActiveBoard.IDMembers.Add(101);
            expActiveBoard.IDMembers.Add(201);
            expActiveBoard.IDMembers.Add(11);
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idAdmin, expectedBoards };

            //2. Проверяем, что Админ не может пенести Мембера (т.к. метод направлен на изменение статуса из Админа в Мембера)

            activeBoard = new Board(1112, 1112, "boardTwo");
            client = new Client(1112, "1112");
            admin = new Client(112, "112");
            member = new Client(1012, "1012");
            member2 = new Client(2012, "2012");
            activeBoard.IDAdmin.Add(112);
            activeBoard.IDMembers.Add(1012);
            activeBoard.IDMembers.Add(2012);
            idAdmin = 2012;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            expActiveBoard = new Board(1112, 1112, "boardTwo");
            expAdmin = new Client(112, "112");
            expClient = new Client(1112, "1112");
            expMember = new Client(1012, "1012");
            expMember2 = new Client(2012, "2012");
            expActiveBoard.IDAdmin.Add(112);
            expActiveBoard.IDMembers.Add(1012);
            expActiveBoard.IDMembers.Add(2012);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idAdmin, expectedBoards };

            //3. Проверяем, что Мембер не может пенести из Админа другого Мембера

            activeBoard = new Board(11123, 11123, "boardTwo");
            client = new Client(77, "77");
            activeBoard.IDAdmin.Add(1123);
            activeBoard.IDMembers.Add(77);
            activeBoard.IDMembers.Add(10123);
            activeBoard.IDMembers.Add(20123);
            idAdmin = 1123;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard }
            };

            expActiveBoard = new Board(11123, 11123, "boardTwo");
            expActiveBoard.IDAdmin.Add(1123);
            expActiveBoard.IDMembers.Add(77);
            expActiveBoard.IDMembers.Add(10123);
            expActiveBoard.IDMembers.Add(20123);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idAdmin, expectedBoards };

            //4. Проверяем, когда Админ пытается изменить статус у др. Админа, который не состоит в текущей доске

            activeBoard = new Board(11124, 11124, "boardTwo");
            Board otherBoard = new Board(13, 13, "boardTwo");
            client = new Client(11124, "11124");
            activeBoard.IDAdmin.Add(1124);
            activeBoard.IDMembers.Add(10124);
            activeBoard.IDMembers.Add(20124);
            idAdmin = 13;
            baseBoards = new Dictionary<int, Board>
            {
                {activeBoard.NumberBoard, activeBoard },
                {otherBoard.NumberBoard, otherBoard }
            };

            expActiveBoard = new Board(11124, 11124, "boardTwo");
            Board expOtherBoard = new Board(13, 13, "boardTwo");
            expActiveBoard.IDAdmin.Add(1124);
            expActiveBoard.IDMembers.Add(10124);
            expActiveBoard.IDMembers.Add(20124);
            expectedBoards = new Dictionary<int, Board>
            {
                {expActiveBoard.NumberBoard, expActiveBoard },
                {expOtherBoard.NumberBoard, expOtherBoard }
            };

            yield return new Object[] { baseBoards, client, activeBoard, idAdmin, expectedBoards };
        }

        public static IEnumerable GetAllBoardsToWhichYouCanJoinTestCaseSource()
        {
            //1. Получаем 3 и 4 доски, в которых наш клиент не состоит

            Client client = new Client(1, "1");
            Board boardA = new Board(1, 1, "nameOfBoard");
            Board boardB = new Board(2, 2, "nameOfBoard");
            boardB.IDMembers.Add(1);
            Board boardC = new Board(3, 3, "nameOfBoard");
            Board boardD = new Board(4, 4, "nameOfBoard");
            List<int> baseNumberBoardsForUser = new List<int> { 1, 2 };
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                {boardA.NumberBoard, boardA },
                {boardB.NumberBoard, boardB },
                {boardC.NumberBoard, boardC },
                {boardD.NumberBoard, boardD },
            };

            Board expBoardC = new Board(3, 3, "nameOfBoard");
            Board expBoardD = new Board(4, 4, "nameOfBoard");
            List<Board> expectedBoards = new List<Board> { expBoardC, expBoardD };

            yield return new Object[] { baseNumberBoardsForUser, baseBoards, client, expectedBoards };

            //2. Получаем пустой лист, т.к. клиент состоит во всех имеющихся досках

            client = new Client(12, "12");
            boardA = new Board(12, 12, "nameOfBoard");
            boardB = new Board(22, 22, "nameOfBoard");
            boardB.IDMembers.Add(11);
            boardC = new Board(33, 33, "nameOfBoard");
            boardC.IDMembers.Add(11);
            boardD = new Board(44, 44, "nameOfBoard");
            boardD.IDAdmin.Add(11);
            baseNumberBoardsForUser = new List<int> { 12, 22, 33, 44 };
            baseBoards = new Dictionary<int, Board>
            {
                {boardA.NumberBoard, boardA },
                {boardB.NumberBoard, boardB },
                {boardC.NumberBoard, boardC },
                {boardD.NumberBoard, boardD },
            };

            expectedBoards = new List<Board>();

            yield return new Object[] { baseNumberBoardsForUser, baseBoards, client, expectedBoards };
        }
    }
}

