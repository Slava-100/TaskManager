using System;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
	public class DataStorageTests
	{
		[TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
		public void AddNewUserByKeyTest(List<Board> expectedBoardsForUser,Dictionary<int, User> expectedDictionaryUsers,string nameMember, int numberBoard, Dictionary<int,Board> StorageBoard, string idMember, int keyBoard, List<string> expectedIdMembers)
		{
			DataStorage dataStorage = new DataStorage();
			dataStorage.Boards = StorageBoard;
			

			dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameMember);

			List<string> actualIdMembers = dataStorage.Boards[numberBoard].IDMembers;

            CollectionAssert.AreEqual(expectedIdMembers, actualIdMembers);

			List<Board> actualBoardForUser = dataStorage.Users[idMember].BoardsForUser;

            CollectionAssert.AreEqual(expectedBoardsForUser, actualBoardForUser);

            Dictionary<string, User> actualDictionaryUsers = dataStorage.Users;

            CollectionAssert.AreEqual(expectedDictionaryUsers, actualDictionaryUsers);
        }
	}
}

