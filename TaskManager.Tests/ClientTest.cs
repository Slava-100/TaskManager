using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Nodes;
using TaskManager;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
    public class ClientTest
    {
        private string _pathBoards;
        private string _pathClient;

        private DataStorage _dataStorage;

        [SetUp]

        public void SetUp()
        {
            _pathBoards = @".\TestBoards.txt";
            _pathClient = @".\TestClient.txt";
            
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, Client client, Dictionary<int, Board> expectedBoards, Issue attachIssue, Dictionary<long, Client> expectedClients)
        {
            //using (StreamWriter sw = new StreamWriter(_pathBoards))
            //{
            //    string jsn = JsonSerializer.Serialize(baseBoards);
            //    sw.WriteLine(jsn);
            //}
            //using (StreamWriter sw = new StreamWriter(_pathClient))
            //{
            //    string jsn = JsonSerializer.Serialize(baseClients);
            //    sw.WriteLine(jsn);
            //}
            _dataStorage = DataStorage.GetInstance();
            _dataStorage.PathFileForBoards = _pathBoards;
            _dataStorage.PathFileForClient = _pathClient;
            _dataStorage.Boards = baseBoards;

            client.SetActiveBoard(10);
            _dataStorage.Clients = baseClients;
            client.AttachIssueToClient(attachIssue);
            Dictionary<int, Board> actualBoards;
            Dictionary<long, Client> actualClients;
            using (StreamReader sr = new StreamReader(_pathBoards))
            {
                string jsn = sr.ReadLine();
                actualBoards = JsonSerializer.Deserialize<Dictionary<int, Board>>(jsn);
            }
            using (StreamReader sr = new StreamReader(_pathClient))
            {
                string jsn = sr.ReadLine();
                actualClients = JsonSerializer.Deserialize<Dictionary<long, Client>>(jsn);
            }
            actualBoards.Should().BeEquivalentTo(expectedBoards);
            actualClients.Should().BeEquivalentTo(expectedClients);

            //string path = @".\PathFileForClientTest.txt";
            //using (StreamWriter sw = new StreamWriter(path))
            //{
            //    string jsn = JsonSerializer.Serialize(baseIssues);
            //    sw.WriteLine(jsn);
            //}
            //DataStorage.GetInstance().PathFileForClient = path;
            ////DataStorage.GetInstance().Boards = new Dictionary<int, Board> { { board.NumberBoard, board } };
            //board.Issues = baseIssues;
            //DataStorage.GetInstance().Clients = clients;
            //clients.Add(client.IDUser, client);
            //client.AttachIssueToClient(attachIssue);
            //List<Issue> actualIssues;
            //using (StreamReader sr = new StreamReader(path))
            //{
            //    string jsn = sr.ReadLine();
            //    actualIssues = JsonSerializer.Deserialize<List<Issue>>(jsn);
            //}

            //actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_pathClient);
            File.Delete(_pathBoards);
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