using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public static class AdminUserTestCaseSource
    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue1 = new Issue(1, "1");
            Issue attachIssue = new Issue(2, "2");
            Board board = new Board(10, 5);
            board.Issues.Add(issue1);
            board.Issues.Add(attachIssue);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client client = new Client(5, "5");
            //long idUser = client.IDUser;
            Dictionary<long, Client> baseClients = new Dictionary<long, Client>
            {
                { 5, client }
            };

            Issue expIssue1 = new Issue(1, "1");
            Issue expIttachIssue = new Issue(2, "2");
            Board expBoard = new Board(10, 5);
            Client expClient = new Client(5, "5");
            expIttachIssue.IdUser = expClient.IDUser;
            long idUser = expClient.IDUser;
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIttachIssue);

            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                { 5, expClient }
            };

            yield return new object[] { baseBoards, baseClients, board, attachIssue, idUser, expectedBoards, expectedClients };
        }

        public static IEnumerable GetAllIssuesInBoardByIdUserTestCaseSource()
        {
            Client admin = new Client(70, "70");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = admin.IDUser;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = admin.IDUser;
            Board board = new Board(70, 70);
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            long idUser = 70;

            Client expAdmin = new Client(70, "70");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expAdmin.IDUser;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expAdmin.IDUser;
            Board expBoard = new Board(70, 70);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            List<Issue> expectedIssues = new List<Issue> { expIssue1, expIssue2 };

            yield return new Object[] { idUser, board, expectedIssues };
        }

        public static IEnumerable GetIssuessDoneInBoardByIdUserTestCaseSource()
        {
            Client admin = new Client(70, "70");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = admin.IDUser;
            issue1.Status = Enums.IssueStatus.Done;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = admin.IDUser;
            issue2.Status = Enums.IssueStatus.UserStory;
            Board board = new Board(70, 70);
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            long idUser = 70;

            Client expAdmin = new Client(70, "70");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = expAdmin.IDUser;
            expIssue1.Status = Enums.IssueStatus.Done;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = expAdmin.IDUser;
            expIssue2.Status = Enums.IssueStatus.UserStory;
            Board expBoard = new Board(70, 70);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            List<Issue> expectedIssues = new List<Issue> { expIssue1 };

            yield return new Object[] { idUser, board, expectedIssues };
        }
    }
}

