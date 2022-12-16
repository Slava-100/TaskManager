using System;
using TaskManager.Tests.TestCaseSource;


namespace TaskManager.Tests
{
	public class DataStorageTests
	{
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
            bool actualFlag = dataStorage.AddNewUserByKey(numberBoard,keyBoard,idMember,nameMember);

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
        public void RemoveBoardTest(Dictionary<int, Board> baseBoards, int boardNumber, Dictionary<int,Board> expectedBoards, bool expectedBool )
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
    }
}


