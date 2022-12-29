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

        //[TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
        //public void AddNewUserByKeyTest(string nameMember, int numberBoard, DataStorage dataStorage, long idMember, int keyBoard, List<long> expectedIdMembers, Dictionary<long, Client> expectedDictionaryUsers, List<int> expectedBoardsForUser)
        //{
        //    bool expectedFlag = true;
        //    bool actualFlag = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

        //    Assert.AreEqual(expectedFlag, actualFlag);

        //    List<long> actualIdMembers = dataStorage.Boards[numberBoard].IDMembers;

        //    CollectionAssert.AreEqual(expectedIdMembers, actualIdMembers);

        //    List<int> actualBoardForUser = dataStorage.Clients[idMember].BoardsForUser;

        //    CollectionAssert.AreEqual(expectedBoardsForUser, actualBoardForUser);

        //    Dictionary<long, Client> actualDictionaryUsers = dataStorage.Clients;

        //    CollectionAssert.AreEqual(expectedDictionaryUsers, actualDictionaryUsers);
        //}

        [Test]
        public void AddNewUserByKeyTest_WhenBoardDoesNotExistWithId()
        {
            DataStorage dataStorage = new DataStorage();
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

            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = storageBoards;

            bool expectedFlag = false;
            bool actualFalg = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

            Assert.AreEqual(expectedFlag, actualFalg);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.RemoveBoardTestSource))]
        public void RemoveBoardTest(Dictionary<int, Board> baseBoards, int boardNumber, Dictionary<int, Board> expectedBoards, bool expectedBool)
        {
            //Given
            _dataStorage.Boards = baseBoards;

            //When
            bool actualBool = _dataStorage.RemoveBoard(boardNumber);
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;

            //Then
            Assert.That(actualBool, Is.EqualTo(expectedBool));
            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.AddBoardTestSource))]
        public void AddBoardTest(Dictionary<int, Board> baseBoards, Dictionary<long, Client> baseClients, long idAdmin, Board expectedBoard, int expectedNumberBoard, string nameBoard)
        {
            //Given
            _dataStorage.Boards = baseBoards;
            _dataStorage.Clients = baseClients;


            //When
            int actualNumberBoard = _dataStorage.AddBoard(idAdmin, nameBoard);
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;

            //Then
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

        //[Test]
        //public void testboard()
        //{
        //    string path = @".\mazafakatest.txt";

        //    Board expectedBoard = new Board(1,11);

        //    using (StreamWriter sw = new StreamWriter(path))
        //    {
        //        string serialiseForFile = JsonSerializer.Serialize(expectedBoard);
        //        sw.WriteLine(serialiseForFile);
        //    }

        //    Board actualBoard = new Board();

        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        string jsn = sr.ReadLine();
        //        actualBoard = JsonSerializer.Deserialize<Board>(jsn);
        //    }

        //    Assert.AreEqual(expectedBoard, actualBoard);

        //}

        //[TestCaseSource(typeof(TestCaseForRewriteAndReturnTest))]
        //public void RewriteFileTest(Dictionary<int, Board> boards, Dictionary<long, User> users)
        //{
        //    _dataStorage.Boards = boards;
        //    _dataStorage.Clients = users;
        //    _dataStorage.RewriteFile();
        //    using (StreamReader sr = new StreamReader(_dataStorage.Path))
        //    {
        //        string jsn = sr.ReadLine();
        //        _dataStorage.Boards = JsonSerializer.Deserialize<Dictionary<int, Board>>(jsn);
        //        jsn = sr.ReadLine();
        //        _dataStorage.Clients = JsonSerializer.Deserialize<Dictionary<long, User>>(jsn);
        //    }
        //    Dictionary<int, Board> expectedBoards = boards;
        //    Dictionary<int, Board> actualBoards = _dataStorage.Boards;
        //    Dictionary<long, User> expectedUsers = users;
        //    Dictionary<long, User> actualUsers = _dataStorage.Clients;

        //    CollectionAssert.AreEqual(expectedBoards, actualBoards);
        //    CollectionAssert.AreEqual(expectedUsers, actualUsers);
        //}

        //[TestCaseSource(typeof(TestCaseForRewriteAndReturnTest))]
        //public void ReturnFromFIleTest(Dictionary<int, Board> boards, Dictionary<long, User> users)
        //{
        //    _dataStorage.Boards = boards;
        //    _dataStorage.Clients = users;

        //    using (StreamWriter sw = new StreamWriter(_dataStorage.Path))
        //    {
        //        string serialiseToFile = JsonSerializer.Serialize(_dataStorage.Boards);
        //        sw.WriteLine(serialiseToFile);
        //        serialiseToFile = JsonSerializer.Serialize(_dataStorage.Clients);
        //        sw.WriteLine(serialiseToFile);
        //    }
        //    _dataStorage.ReturnFromFile();
        //    Dictionary<int, Board> expectedBoards = boards;
        //    Dictionary<int, Board> actualBoards = _dataStorage.Boards;
        //    Dictionary<long, User> expectedUsers = users;
        //    Dictionary<long, User> actualUsers = _dataStorage.Clients;

        //    CollectionAssert.AreEqual(expectedBoards,actualBoards);
        //    CollectionAssert.AreEqual(expectedUsers,actualUsers);
        //}
    }
}


