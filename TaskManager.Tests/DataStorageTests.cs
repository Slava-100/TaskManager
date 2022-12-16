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
    }
}

