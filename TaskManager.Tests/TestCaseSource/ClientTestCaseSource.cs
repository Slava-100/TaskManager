using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class ClientTestCaseSource

    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue1 = new Issue(1, "1");
            Issue attachIssue = new Issue(2, "2");
            attachIssue.Status = Enums.IssueStatus.Backlog;
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
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                { 5, client }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

            Issue issue10 = new Issue(10, "10");
            attachIssue = new Issue(20, "20");
            attachIssue.Status = Enums.IssueStatus.Backlog;
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
            expectedBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            expectedClients = new Dictionary<long, Client>
             {
                { 22, admin},
                { 50, client }
            };

            yield return new object[] { baseBoards, board, baseClients, client, expectedBoards, idAttachIssue, expectedClients };

        }
    }
}

