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
    }
}

