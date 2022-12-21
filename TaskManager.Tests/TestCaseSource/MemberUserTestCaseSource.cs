using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class MemberUserTestCaseSource
    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue10 = new Issue(10, "10");
            Issue attachIssue = new Issue(20, "20");
            Client admin = new Client(50, "50");
            Board board = new Board(100, 50);
            board.Issues.Add(issue10);
            board.Issues.Add(attachIssue);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client client = new Client(40, "40");
            Dictionary<long, Client> baseClients = new Dictionary<long, Client>
            {
                { 50, admin },
                { 40, client}
            };

            Issue expIssue10 = new Issue(10, "10");
            Issue expIttachIssue = new Issue(20, "20");
            Board expBoard = new Board(100, 50);
            Client expAdmin = new Client(50, "50");
            Client expClient = new Client(40, "40");
            expIttachIssue.IdUser = expClient.IDUser;
            expBoard.Issues.Add(expIssue10);
            expBoard.Issues.Add(expIttachIssue);
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            long idUser = expClient.IDUser;
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                 { 50, expAdmin },
                { 40, expClient}
            };

            yield return new object[] { baseBoards, baseClients, board, attachIssue, idUser, expectedBoards, expectedClients };
        }
    }
}
