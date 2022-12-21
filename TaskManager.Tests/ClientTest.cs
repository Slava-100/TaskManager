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
            _dataStorage = DataStorage.GetInstance();
            _dataStorage.PathFileForBoards = _pathBoards;
            _dataStorage.PathFileForClient = _pathClient;
            _dataStorage.Boards = new Dictionary<int, Board>();
            _dataStorage.Clients = new Dictionary<long, Client>();
            _dataStorage.UpdateNextNumberBoard();
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(Dictionary<int, Board> baseBoards, Board board, Dictionary<long, Client> baseClients, Client client, Dictionary<int, Board> expectedBoards, int idAttachIssue, Dictionary<long, Client> expectedClients)
        {
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;
            _dataStorage.RewriteFileForBoards();
            _dataStorage.RewriteFileForClients();

            client.SetActiveBoard(board.NumberBoard);
                        client.AttachIssueToClient(idAttachIssue);
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
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_pathClient);
            File.Delete(_pathBoards);
        }
    }
}

