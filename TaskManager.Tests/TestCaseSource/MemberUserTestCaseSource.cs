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
            long idUser = client.IDUser;
            Dictionary<long, Client> baseClients = new Dictionary<long, Client>
            {
                { 50, admin },
                { 40, client}
            };
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                 { 50, admin },
                { 40, client}
            };

            yield return new object[] { baseBoards, baseClients, board, attachIssue, idUser, expectedBoards, expectedClients };
        }
    }
}
