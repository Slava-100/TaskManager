using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Nodes;
using TaskManager;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
    public class ClientTest
    {
        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(List<Issue> baseIssues, Dictionary<long, Client> clients, Client client, Board board, Issue attachIssue, List<Issue> expectedIssues)
        {
            DataStorage.GetInstance().PathFileForClient = @"C:\Users\Кристина\Desktop\MakeUPro\Коды\Tests\ClientTest.txt";
            string path = DataStorage.GetInstance().PathFileForClient;
            DataStorage.GetInstance().Boards = new Dictionary<int, Board> { { board.NumberBoard, board } };
            board.Issues = baseIssues;
            DataStorage.GetInstance().Clients = clients;
            clients.Add(client.IDUser, client);
            using (StreamWriter sw = new StreamWriter(path))
            {
                string jsn = JsonSerializer.Serialize(baseIssues);
                sw.WriteLine(jsn);
            }
            client.AttachIssueToClient(attachIssue);
            List<Issue> actualIssues;
            using (StreamReader sr = new StreamReader(path))
            {
                string jsn = sr.ReadLine();
                actualIssues = JsonSerializer.Deserialize<List<Issue>>(jsn);
            }

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(@"C:\Users\Кристина\Desktop\MakeUPro\Коды\Tests\ClientTest.txt");
        }
    }
}

//public void AttachIssueToClient(Issue issue)
//{
//    if (BoardsForUser.Contains(issue.NumberIssue) && SelectRole())
//    {
//        _userRole.AttachIssueToClient(_activeBoard, issue, IDUser);
//        _dataStorage.RewriteFileForBoards();
//    }
//}