﻿using System.Text.Json;
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
        public void AttachIssueToClientTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, Board board, Issue attachIssue, long idUser, Dictionary<int, Board> expectedBoards, Dictionary<long, Client> expectedClients)
        {
            AdminUser adminUser = new AdminUser();
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;
            Dictionary<long, Client> actualClients = _dataStorage.Clients;

            adminUser.AttachIssueToClient(board, attachIssue, idUser);

            actualBoards.Should().BeEquivalentTo(expectedBoards);
            actualClients.Should().BeEquivalentTo(expectedClients);
        }

        [TestCaseSource(typeof(AdminUserTestCaseSource), nameof(AdminUserTestCaseSource.GetAllIssuesInBoardByIdUserTestCaseSource))]
        public void GetAllIssuesInBoardByIdUserTest(long idUser, Board board, List<Issue> expectedIssues)
        {
            AdminUser adminUser = new AdminUser();
            List<Issue> actualIssues = adminUser.GetAllIssuesAbountIdUser(idUser, board);

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TestCaseSource(typeof(AdminUserTestCaseSource), nameof(AdminUserTestCaseSource.GetIssuessDoneInBoardByIdUserTestCaseSource))]
        public void GetIssuesDoneInBoardByIdUserTest(long idUser, Board board, List<Issue> expectedIssues)
        {
            AdminUser adminUser = new AdminUser();
            List<Issue> actualIssues = adminUser.GetIssuesDoneForUser(idUser, board);

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
