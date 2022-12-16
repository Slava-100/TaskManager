using System;
using System.Collections;

namespace TaskManager.Tests.TestCaseSource
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

        public class AddNewUserByKeyTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                int numberBoard = 1;
                string idMember = "id";
                int keyBoard = 1;
                string nameMember = "";
                Board board = new Board(numberBoard, "id2");
                board.Key = keyBoard;

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

                yield return new Object[] { nameMember, numberBoard, dataStorage, idMember, keyBoard, expectedIdMembers, expectedDictionaryUsers, expectedBoardForUser };
            }


        }
    }
}

