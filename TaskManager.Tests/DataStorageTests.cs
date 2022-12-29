using FluentAssertions;
using System.Text.Json;
using TaskManager.Tests.TestCaseSource;
using static TaskManager.Tests.TestCaseSource.DataStorageTestCaseSource;

namespace TaskManager.Tests
{
    public class DataStorageTests
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

        [Test]
        public void AddNewUserByKeyTest_WhenBoardDoesNotExistWithId()
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            int numberBoard = 1;
            long idMember = 12;
            int keyBoard = 1;
            string nameMember = "";

            bool expectedFlag = false;
            bool actualFlag = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

            Assert.AreEqual(expectedFlag, actualFlag);
        }

        [Test]
        public void AddNewUserByKeyTest_WhenInvalidPasswordFromTheBoard()
        {
            int numberBoard = 1;
            long idMember = 12;
            int keyBoard = 1;
            string nameMember = "";
            string nameBoard = "nameBoard";

            Dictionary<int, Board> storageBoards = new Dictionary<int, Board>()
            {
                { numberBoard, new Board(numberBoard,idMember,nameBoard) }
            };

            storageBoards[numberBoard].Key = 2;

            DataStorage dataStorage = DataStorage.GetInstance();
            dataStorage.Boards = storageBoards;

            bool expectedFlag = false;
            bool actualFalg = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

            Assert.AreEqual(expectedFlag, actualFalg);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.RemoveBoardTestSource))]
        public void RemoveBoardTest(Dictionary<int, Board> baseBoards, int boardNumber, Dictionary<int, Board> expectedBoards, bool expectedBool)
        {
            _dataStorage.Boards = baseBoards;

            bool actualBool = _dataStorage.RemoveBoard(boardNumber);
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;

            Assert.That(actualBool, Is.EqualTo(expectedBool));
            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.AddBoardTestSource))]
        public void AddBoardTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, long idAdmin, Board expectedBoard, int expectedNumberBoard, string nameBoard)
        {
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;


            int actualNumberBoard = _dataStorage.AddBoard(idAdmin, nameBoard);
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;

            Assert.That(actualNumberBoard, Is.EqualTo(expectedNumberBoard));
            Assert.That(_dataStorage.Boards[expectedNumberBoard].IDAdmin.Contains(idAdmin)
                && _dataStorage.Boards[expectedNumberBoard].NameBoard == expectedBoard.NameBoard
                && _dataStorage.Boards[expectedNumberBoard].NumberBoard == expectedBoard.NumberBoard);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.GetAllBoardsByNumbersOfBoardTestCaseSource))]
        public void GetAllBoardsByNumbersOfBoardTest(Dictionary<int, Board> baseBoards, List<int> baseBoardsForUser, List<Board> expectedBoards)
        {
            _dataStorage.Boards = baseBoards;
            List<Board> actualBoards = _dataStorage.GetAllBoardsByNumbersOfBoard(baseBoardsForUser);

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


