using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class ClientTestCaseSource

    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            // 1. проверка на добавление задания к Админу, при всех выполненных условиях

            Issue issue1 = new Issue(1, "1");
            Issue attachIssue = new Issue(2, "2");
            Board board = new Board(10, 5);
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
            Client expClient = new Client(5, "5");
            Board expBoard = new Board(10, 5);
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
            board = new Board(22, 22);
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
            expIssue10.Status = Enums.IssueStatus.Backlog;
            Client expAdmin = new Client(22, "22");
            expBoard = new Board(22, 22);
            expBoard.Issues.Add(issue10);
            expBoard.Issues.Add(attachIssue);
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
            board = new Board(103, 53);
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
            expBoard = new Board(103, 53);
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
            board = new Board(224, 224);
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
            expBoard = new Board(224, 224);
            expBoard.Issues.Add(issue10);
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
            board = new Board(105, 55);
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
            expBoard = new Board(105, 55);
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
            board = new Board(226, 226);
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
            expBoard = new Board(226, 226);
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
            board = new Board(1057, 557);
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
            expBoard = new Board(1057, 557);
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
            board = new Board(2248, 2248);
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
            expBoard = new Board(2248, 2248);
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

            //8. проверка на не добавление задания к человеку, если он не является админом или мембером

            issue1 = new Issue(18, "18");
            attachIssue = new Issue(28, "28");
            board = new Board(108, 58);
            board.Issues.Add(issue1);
            board.Issues.Add(attachIssue);
            idAttachIssue = 28;
            baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client clientNope = new Client(666, "666");
            client = new Client(58, "58");
            client.BoardsForUser.Add(108);
            board.IDAdmin.Add(58);
            baseClients = new Dictionary<long, Client>
            {
                { 58, client }
            };

            expIssue1 = new Issue(18, "18");
            expAttachIssue = new Issue(28, "28");
            expClient = new Client(58, "58");
            expBoard = new Board(108, 58);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expAttachIssue);
            expClient.BoardsForUser.Add(108);
            expBoard.IDAdmin.Add(58);
            expAttachIssue.IdUser = expClient.IDUser;
            expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            expectedClients = new Dictionary<long, Client>
             {
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
            Board baseBoard = new Board(55, 55);
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
            Board expBoard = new Board(55, 55);
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
            baseBoard = new Board(999, 999);
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
            expBoard = new Board(999, 999);
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
            //otherClient = new Client(2, "2");
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
            baseBoard = new Board(55, 55);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.Issues.Add(issue4);
            baseBoard.Issues.Add(issue5);
            baseBoard.IDMembers.Add(2);

            expClient = new Client(55, "55");
           // expOtherClient = new Client(2, "2");
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
            expBoard = new Board(55, 55);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expBoard.Issues.Add(expIssue4);
            expBoard.Issues.Add(expIssue5);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(2);
            expectedIssues = new List<Issue> { expIssue3, expIssue4, expIssue1, expIssue5, expIssue2 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };
        }

        public static IEnumerable GetIssuesInProgressInBoardByBoardTestCaseSource()
        {
            //1. Проверка для Админа, где на него записаны только 1 задача с InProgress, 2 задача на него - но с другим статусом, 3 задача на другом участнике

            Client baseClient = new Client(55, "55");
            Client otherClient = new Client(2, "2");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.InProgress;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = baseClient.IDUser;
            issue2.Status = Enums.IssueStatus.Review;
            Issue issue3 = new Issue(3, "3");
            issue3.IdUser = otherClient.IDUser;
            issue3.Status = Enums.IssueStatus.InProgress;
            Board baseBoard = new Board(55, 55);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issue3);
            baseBoard.IDMembers.Add(2);

            Client expClient = new Client(55, "55");
            Client expOtherClient = new Client(2, "2");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expClient.IDUser;
            expIssue1.Status = Enums.IssueStatus.InProgress;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expClient.IDUser;
            expIssue2.Status = Enums.IssueStatus.Review;
            Issue expIssue3 = new Issue(3, "3");
            expIssue3.IdUser = expOtherClient.IDUser;
            expIssue3.Status = Enums.IssueStatus.InProgress;
            Board expBoard = new Board(55, 55);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(55);
            expBoard.IDMembers.Add(2);
            List<Issue> expectedIssues = new List<Issue> { expIssue1 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };

            //2. Проверка для Мембера, где на него записаны только 3 задача со статусом InProgress, 1 тоже на него - но с дургим статусом, 2 задача на другом участнике

            Client admin = new Client(9992, "9992");
            baseClient = new Client(5502, "5502");
            otherClient = new Client(22, "22");
            issue1 = new Issue(12, "12");
            issue1.IdUser = baseClient.IDUser;
            issue1.Status = Enums.IssueStatus.Done;
            issue2 = new Issue(22, "22");
            issue2.IdUser = admin.IDUser;
            issue2.Status = Enums.IssueStatus.InProgress;
            issue3 = new Issue(32, "32");
            issue3.IdUser = baseClient.IDUser;
            issue3.Status = Enums.IssueStatus.InProgress;
            baseBoard = new Board(9992, 9992);
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
            expIssue1.Status = Enums.IssueStatus.Done;
            expIssue2 = new Issue(22, "22");
            expIssue2.IdUser = admin.IDUser;
            expIssue2.Status = Enums.IssueStatus.InProgress;
            expIssue3 = new Issue(32, "32");
            expIssue3.IdUser = expClient.IDUser;
            expIssue3.Status = Enums.IssueStatus.InProgress;
            expBoard = new Board(9992, 9992);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.Issues.Add(expIssue3);
            expClient.BoardsForUser.Add(552);
            expBoard.IDMembers.Add(5502);
            expBoard.IDMembers.Add(22);
            expectedIssues = new List<Issue> { expIssue3 };

            yield return new Object[] { baseBoard, baseClient, expectedIssues };
        }
    }
}


