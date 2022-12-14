using System.Collections;

namespace TaskManager.Tests
{
    public static class DataStorageTestCaseSource
    {
        public static IEnumerable AddBoardTestSource()
        {
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>();
            string idAdmin = "idAdmin";
            Board addBoard = new Board(1, "idAdmin");
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
              {addBoard.NumberBoard, addBoard }
            };
            int expectedNumberBoard = 1;

            yield return new Object[] { baseBoards, idAdmin, expectedBoards, expectedNumberBoard };
        }
    }
}

