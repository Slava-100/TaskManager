using FluentAssertions;
using System.Text.Json;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
    public class MemberUserTest
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

        [TestCaseSource(typeof(MemberUserTestCaseSource), nameof(MemberUserTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, Board board, Issue attachIssue, long idUser, Dictionary<int, Board> expectedBoards, Dictionary<long, Client> expectedClients)
        {
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;

            MemberUser memberUser = new MemberUser();
            memberUser.AttachIssueToClient(board, attachIssue, idUser);

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

        [TestCaseSource(typeof(MemberUserTestCaseSource), nameof(MemberUserTestCaseSource.GetAllIssuesInBoardByIdUserTestCaseSource))]
        public void GetAllIssuesInBoardByIdUserTest(long idUser, Board board, List<Issue> expectedIssues)
        {
            MemberUser memeberUser = new MemberUser();
            List<Issue> actualIssues = memeberUser.GetAllIssuesInBoardByIdUser(idUser, board);

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TestCaseSource(typeof(MemberUserTestCaseSource), nameof(MemberUserTestCaseSource.GetIssuesInProgressInBoardByIdUserTestCaseSource))]
        public void GetIssuesInProgressInBoardByIdUserTest(long idUser, Board board, List<Issue> expectedIssues)
        {
            MemberUser memeberUser = new MemberUser();
            List<Issue> actualIssues = memeberUser.GetIssuesInProgressInBoardByIdUser(idUser, board);

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_pathBoards);
            File.Delete(_pathClient);
        }
    }
}
