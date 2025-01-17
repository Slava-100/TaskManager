﻿using FluentAssertions;
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
            client.SetActiveBoard(board.NumberBoard);
            client.AttachIssueToClient(idAttachIssue);
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;
            Dictionary<long, Client> actualClients = _dataStorage.Clients;

            actualBoards.Should().BeEquivalentTo(expectedBoards);
            actualClients.Should().BeEquivalentTo(expectedClients);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetAllIssuesInBoardByBoardTestCaseSource))]
        public void GetAllIssuesInBoardByBoardTest(Board baseBoard, Client baseClient, List<Issue> expectedIssues)
        {
            _dataStorage.Boards.Add(baseBoard.NumberBoard, baseBoard);
            _dataStorage.Clients.Add(baseClient.IDUser, baseClient);
            baseClient.SetActiveBoard(baseBoard.NumberBoard);
            List<Issue> actualIssues = baseClient.GetAllIssuesInBoardForClientByBoard();

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetIssuesDoneInBoardByBoardTestCaseSource))]
        public void GetIssuesDoneInBoardByBoardTest(Board baseBoard, Client baseClient, List<Issue> expectedIssues)
        {
            _dataStorage.Boards.Add(baseBoard.NumberBoard, baseBoard);
            _dataStorage.Clients.Add(baseClient.IDUser, baseClient);
            baseClient.SetActiveBoard(baseBoard.NumberBoard);
            List<Issue> actualIssues = baseClient.GetIssuesDoneInBoardForClientByBoard();

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetAllBoardsByNumbersOfBoardTestCaseSource))]
        public void GetAllBoardsByNumbersOfBoardTest(Client client, Dictionary<int, Board> baseBoards, List<int> baseBoardsForUser, List<Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;
            List<Board> actualBoards = client.GetAllBoardsByNumbersOfBoard();

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetAllBoardsAdminsTestCaseSource))]
        public void GetAllBoardsAdminsTest(Dictionary<int, Board> baseBoards, Client client, List<int> baseListBoards, List<Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;
            client.BoardsForUser = baseListBoards;

            List<Board> actualBoards = client.GetAllBoardsAdmins();

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetAllBoardsMembersTestCaseSource))]
        public void GetAllBoardsMembersTest(Dictionary<int, Board> baseBoards, Client client, List<int> baseListBoards, List<Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;
            client.BoardsForUser = baseListBoards;

            List<Board> actualBoards = client.GetAllBoardsMembers();

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.ChangeRoleFromMemberToAdminTestCaseSource))]
        public void ChangeRoleFromMemberToAdminTest(Dictionary<int, Board>  baseBoards, Client client, Board activeBoard, long idMemeber, Dictionary<int, Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;

            client.SetActiveBoard(activeBoard.NumberBoard);
            client.ChangeRoleFromMemberToAdmin(idMemeber);

            Dictionary<int, Board> actualBoards;
            actualBoards = _dataStorage.Boards;

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.ChangeRoleFromAdminToMemberTestCaseSource))]
        public void ChangeRoleFromAdminToMemberTest(Dictionary<int, Board> baseBoards, Client client, Board activeBoard, long idAdmin, Dictionary<int, Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;

            client.SetActiveBoard(activeBoard.NumberBoard);
            client.ChangeRoleFromAdminToMember(idAdmin);

            Dictionary<int, Board> actualBoards;
            actualBoards = _dataStorage.Boards;

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(ClientTestCaseSource), nameof(ClientTestCaseSource.GetAllBoardsToWhichYouCanJoinTestCaseSource))]
        public void GetAllBoardsToWhichYouCanJoinTest(List<int> baseNumberBoardsForUser, Dictionary<int, Board> baseBoards, Client client, List<Board> expectedBoards )
        {
            _dataStorage.Boards = baseBoards;
            client.BoardsForUser = baseNumberBoardsForUser;
            List<Board> actualBoards = client.GetAllBoardsToWhichYouCanJoin();

            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_pathClient);
            File.Delete(_pathBoards);
        }
    }
}

