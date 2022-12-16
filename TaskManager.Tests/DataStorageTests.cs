using System;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
	public class DataStorageTests
	{
		[TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
		public void AddNewUserByKeyTest(string nameMember, int numberBoard, DataStorage dataStorage, string idMember, int keyBoard, List<string> expectedIdMembers, Dictionary<string, User> expectedDictionaryUsers, List<int> expectedBoardsForUser)
		{
			dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

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
                { numberBoard, new Board(numberBoard,idMember,2) } 
            };

            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = storageBoards;

            bool expectedFlag = false;
            bool actualFalg = dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

            Assert.AreEqual(expectedFlag, actualFalg);
        }
    }
}

