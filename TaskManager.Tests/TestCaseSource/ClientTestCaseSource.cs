using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class ClientTestCaseSource

    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue1 = new Issue();
            Issue issue2 = new Issue(2, "2");
            Issue attachIssue = new Issue(10, "10");
            List<Issue> baseIssues = new List<Issue> { issue1, issue2, attachIssue };
            Client client = new Client(100, "100");
            Client client1 = new Client(1, "1");
            Dictionary<long, Client> clients = new Dictionary<long, Client>
            {
                { client1.IDUser, client1}
            };
            Board board = new Board(50, 50);
            List<Issue> expectedIssues = new List<Issue> { issue1, issue2, attachIssue };

            yield return new Object[] { baseIssues, clients, client, board, attachIssue, expectedIssues };

            Issue issue10 = new Issue(10, "10");
            Issue issue20 = new Issue(20, "20");
            attachIssue = new Issue(30, "30");
            baseIssues = new List<Issue> { issue10, issue20, attachIssue };
            client = new Client(110, "110");
            clients = new Dictionary<long, Client>();
            board = new Board(500, 500);
            expectedIssues = new List<Issue> { issue10, issue20, attachIssue };

            yield return new Object[] { baseIssues, clients, client, board, attachIssue, expectedIssues };
        }
    }
}

