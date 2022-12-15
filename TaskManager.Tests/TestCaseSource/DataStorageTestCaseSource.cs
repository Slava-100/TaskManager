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
            Board board = new Board(numberBoard, idMember, keyBoard);

			Dictionary<int, Board> storageBoard = new Dictionary<int, Board>()
			{
				{1, board}
            };

            board.IDMembers.Add(idMember);

			User user = new User(idMember, nameMember);

			user.BoardsForUser.Add(board);

			List<Board> expectedBoardForUser = user.BoardsForUser;

			Dictionary<string, User> expectedDictionaryUsers = new Dictionary<string, User>
			{
				{idMember, user}
			};

			List<string> expectedIdMembers = board.IDMembers;

			yield return new Object[] {nameMember, numberBoard, storageBoard, idMember, keyBoard, expectedIdMembers, expectedDictionaryUsers, expectedBoardForUser};
		}
	}
}

