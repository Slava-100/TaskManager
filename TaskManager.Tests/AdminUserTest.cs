using System.Text.Json;
using TaskManager;
using TaskManager.Tests.TestCaseSource;
using FluentAssertions;


namespace TaskManager.Tests
{
    public class AdminUserTest
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

        [TestCaseSource(typeof(AdminUserTestCaseSource), nameof(AdminUserTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, Client client, Dictionary<int, Board> expectedBoards, int idAttachIssue, Dictionary<long, Client> expectedClients)
        {

            // (Board board, Issue issue, long IDUser)
            using (StreamWriter sw = new StreamWriter(_pathBoards))
            {
                string jsn = JsonSerializer.Serialize(baseBoards);
                sw.WriteLine(jsn);
            }
            using (StreamWriter sw = new StreamWriter(_pathClient))
            {
                string jsn = JsonSerializer.Serialize(baseClients);
                sw.WriteLine(jsn);
            }
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;
            client.SetActiveBoard(10);
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

        //(Board board, Issue issue, long IDUser)

        [TearDown]
        public void Teardown()
        {
            File.Delete(@"C:\Users\Кристина\Desktop\MakeUPro\Коды\Tests\AdminUserTest.txt");
        }
    }
}

