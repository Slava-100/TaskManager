using FluentAssertions;
using System;
using NUnit.Framework;
using System.Text.Json;
//using Newtonsoft.Json;
using TaskManager.Tests.TestCaseSource;
using static TaskManager.Tests.TestCaseSource.DataStorageTestCaseSource;

namespace TaskManager.Tests
{
    public class DataStorageTests
    {
        private string _path;

        private DataStorage _dataStorage;


        [SetUp]

        public void SetUp()
        {
            _path = @".\TestDataStorage";
            _dataStorage = DataStorage.GetInstance();
            _dataStorage.Path = _path;
        }

        [TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
        public void AddNewUserByKeyTest(string nameMember, int numberBoard, DataStorage dataStorage, string idMember, int keyBoard, List<string> expectedIdMembers, Dictionary<string, User> expectedDictionaryUsers, List<int> expectedBoardsForUser)
        {
            bool expectedFlag = true;
            bool actualFlag = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

            Assert.AreEqual(expectedFlag, actualFlag);

            List<string> actualIdMembers = dataStorage.Boards[numberBoard].IDMembers;

            CollectionAssert.AreEqual(expectedIdMembers, actualIdMembers);

            List<int> actualBoardForUser = dataStorage.Users[idMember].BoardsForUser;

            CollectionAssert.AreEqual(expectedBoardsForUser, actualBoardForUser);

            Dictionary<string, User> actualDictionaryUsers = dataStorage.Users;

            CollectionAssert.AreEqual(expectedDictionaryUsers, actualDictionaryUsers);
        }

        [Test]
        public void AddNewUserByKeyTest_WhenBoardDoesNotExistWithId()
        {
            DataStorage dataStorage = new DataStorage();
            int numberBoard = 1;
            string idMember = "id";
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
            string idMember = "id";
            int keyBoard = 1;
            string nameMember = "";

            Dictionary<int, Board> storageBoards = new Dictionary<int, Board>()
            {
                { numberBoard, new Board(numberBoard,idMember) }
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
            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = baseBoards;

            //When
            bool actualBool = dataStorage.RemoveBoard(boardNumber);
            Dictionary<int, Board> actualBoards = dataStorage.Boards;

            //Then
            Assert.That(actualBool, Is.EqualTo(expectedBool));
            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.AddBoardTestSource))]
        public void AddBoardTest(Dictionary<int, Board> baseBoards, string idAdmin, Dictionary<int, Board> expectedBoards, int expectedNumberBoard)
        {
            //Given
            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = baseBoards;

            //When
            int actualNumberBoard = dataStorage.AddBoard(idAdmin);
            Dictionary<int, Board> actualBoards = dataStorage.Boards;

            //Then
            Assert.That(actualNumberBoard, Is.EqualTo(expectedNumberBoard));
            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }

        [TestCaseSource(typeof(TestCaseForRewriteAndReturnTest))]
        public void RewriteFileTest(Dictionary<int, Board> boards, Dictionary<string, User> users)
        {
            _dataStorage.Boards = boards;
            _dataStorage.Users = users;
            _dataStorage.RewriteFile();
            using (StreamReader sr = new StreamReader(_dataStorage.Path))
            {
                string jsn = sr.ReadLine();
                _dataStorage.Boards = JsonSerializer.Deserialize<Dictionary<int, Board>>(jsn);
                jsn = sr.ReadLine();
                _dataStorage.Users = JsonSerializer.Deserialize<Dictionary<string, User>>(jsn);
            }
            Dictionary<int, Board> expectedBoards = boards;
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;
            Dictionary<string, User> expectedUsers = users;
            Dictionary<string, User> actualUsers = _dataStorage.Users;

            CollectionAssert.AreEqual(actualBoards, expectedBoards);
            CollectionAssert.AreEqual(actualUsers, expectedUsers);
        }

        [TestCaseSource(typeof(TestCaseForRewriteAndReturnTest))]
        public void ReturnFromFIleTest(Dictionary<int, Board> boards, Dictionary<string, User> users)
        {
            _dataStorage.Boards = boards;
            _dataStorage.Users = users;
            using (StreamWriter sw = new StreamWriter(_dataStorage.Path))
            {
                string serialiseToFile = JsonSerializer.Serialize(_dataStorage.Boards);
                sw.WriteLine(serialiseToFile);
                serialiseToFile = JsonSerializer.Serialize(_dataStorage.Users);
                sw.WriteLine(serialiseToFile);
            }
            _dataStorage.ReturnFromFile();
            Dictionary<int, Board> expectedBoards = boards;
            Dictionary<int, Board> actualBoards = _dataStorage.Boards;
            Dictionary<string, User> expectedUsers = users;
            Dictionary<string, User> actualUsers = _dataStorage.Users;

            CollectionAssert.AreEqual(actualBoards, expectedBoards);
            CollectionAssert.AreEqual(actualUsers, expectedUsers);
        }
    }    
}


