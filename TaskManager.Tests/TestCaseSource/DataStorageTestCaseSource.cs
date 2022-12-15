using System;
using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
	public class AddNewUserByKeyTestCaseSource : IEnumerable
    {
		public IEnumerator GetEnumerator()
		{
			Dictionary<int, Board> StorageBoard = new Dictionary<int, Board>();
			int numberBoard = 1;
			string idMember = "id";
			int keyBoard = 1;
			Board board = new Board(numberBoard, idMember, keyBoard);
            board.IDMembers.Add(idMember);

			List<string> expectedIdMembers = board.IDMembers;

			yield return new Object[] { numberBoard, StorageBoard, idMember, keyBoard, expectedIdMembers};
		}
	}
}

