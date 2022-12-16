using System;
using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
	public class AddNewUserByKeyTestCaseSource : IEnumerable
    {
		public IEnumerator GetEnumerator()
		{
            int numberBoard = 1;
			string idMember = "id";
			int keyBoard = 1;
            string nameMember = "";
            Board board = new Board(numberBoard, "id2", keyBoard);

			Dictionary<int, Board> storageBoard = new Dictionary<int, Board>()
			{
				{1, board}
            };

            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = storageBoard;

            board.IDMembers.Add(idMember);

			User user = new User(idMember, nameMember);

			user.BoardsForUser.Add(numberBoard);

			List<int> expectedBoardForUser = user.BoardsForUser;

			Dictionary<string, User> expectedDictionaryUsers = new Dictionary<string, User>
			{
				{idMember, user}
			};

			List<string> expectedIdMembers = board.IDMembers;

			yield return new Object[] {nameMember, numberBoard, dataStorage, idMember, keyBoard, expectedIdMembers, expectedDictionaryUsers, expectedBoardForUser};

			//User user2 = new User("1234", "qwerty");
			//Dictionary<string, User> storageUsers = new Dictionary<string, User>()
			//{
			//	{"1234", user2}
			//};

			//dataStorage.Users = storageUsers;

			//expectedDictionaryUsers = dataStorage.Users;

   //         yield return new Object[] { "qwerty", numberBoard, dataStorage, "1234", keyBoard, expectedIdMembers, expectedDictionaryUsers, expectedBoardForUser };
        }
	}
}

