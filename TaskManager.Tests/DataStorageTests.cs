using System;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
	public class DataStorageTests
	{
		[TestCaseSource(typeof(AddNewUserByKeyTestCaseSource))]
		public void AddNewUserByKeyTest(int numberBoard, Dictionary<int,Board> StorageBoard, string idMember, int keyBoard, List<string> expectedIdMembers)
		{
			DataStorage dataStorage = new DataStorage();
			dataStorage.Boards = StorageBoard;
			string nameUser = "";

			dataStorage.AddNewUserByKey(numberBoard, keyBoard, idMember, nameUser);
			List<string> actualIdMembers = dataStorage.Boards[numberBoard].IDMembers;

            CollectionAssert.AreEqual(expectedIdMembers, actualIdMembers);


		}
	}
}

