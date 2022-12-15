using System.Collections;

namespace TaskManager.Tests
{
    public static class DataStorageTestCaseSource
    {
        public static IEnumerable RemoveBoardTestSource()
        {
            Board board1 = new Board(1, "1");
            Board board2 = new Board(2, "2");
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { 1, board1},
                { 2, board2}
            };
            int boardNumber = 2;
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { 1, board1}
            };
            bool expectedBool = true;

            yield return new Object[] { baseBoards, boardNumber, expectedBoards, expectedBool };

            Board board10 = new Board(10, "10");
            Board board21 = new Board(21, "21");
            baseBoards = new Dictionary<int, Board>
            {
                { 10, board10},
                { 21, board21}
            };
            boardNumber = 20;
            expectedBoards = new Dictionary<int, Board>
            {
                { 10, board10},
                { 21, board21}
            };
            expectedBool = false;

            yield return new Object[] { baseBoards, boardNumber, expectedBoards, expectedBool };

            baseBoards = new Dictionary<int, Board>();
            expectedBoards = new Dictionary<int, Board>();

            yield return new Object[] { baseBoards, boardNumber, expectedBoards, expectedBool };
        }
    }
}
