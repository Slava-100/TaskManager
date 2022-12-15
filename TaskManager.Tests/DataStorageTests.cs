using System;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
	public class DataStorageTests
	{
		[TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
		public void AddNewUserByKeyTest(string nameMember, int numberBoard, Dictionary<int,Board> storageBoard, string idMember, int keyBoard, List<string> expectedIdMembers, Dictionary<string, User> expectedDictionaryUsers, List<Board> expectedBoardsForUser)
		{
			DataStorage dataStorage = new DataStorage();
			dataStorage.Boards = storageBoard;
			
			dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

			List<string> actualIdMembers = dataStorage.Boards[numberBoard].IDMembers;

			CollectionAssert.AreEqual(expectedIdMembers, actualIdMembers);

			List<Board> actualBoardForUser = dataStorage.Users[idMember].BoardsForUser;

            CollectionAssert.AreEqual(expectedBoardsForUser, actualBoardForUser);

            Dictionary<string, User> actualDictionaryUsers = dataStorage.Users;

            CollectionAssert.AreEqual(expectedDictionaryUsers, actualDictionaryUsers);
        }

		[Test]
		public void AddNewUserByKeyTest_WhenBoardDoesNotExistWithId_ShuldException()
		{
			DataStorage dataStorage = new DataStorage();
            int numberBoard = 1;
            string idMember = "id";
            int keyBoard = 1;
            string nameMember = "";
			
			Assert.Throws<Exception>(() => dataStorage.AddNewUserByKey(numberBoard,keyBoard,idMember,nameMember));
        }

        [Test]
        public void AddNewUserByKeyTest_WhenInvalidPasswordFromTheBoard_ShuldException()
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
           
            Assert.Throws<Exception>(() => dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember));
        }
    }
}

