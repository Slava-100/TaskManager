using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public static class DataStorageTestCaseSource
    {
        public static IEnumerable RemoveBoardTestSource()
        {
            Board board1 = new Board(1, 1);
            Board board2 = new Board(2, 2);
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

            Board board10 = new Board(10, 10);
            Board board21 = new Board(21, 21);
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
            long idAdmin = 12;
            Board addBoard = new Board(1, 12);
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
                long idMember = 123;
                int keyBoard = 1;
                string nameMember = "";
                Board board = new Board(numberBoard, 1234);
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

                Dictionary<long, User> expectedDictionaryUsers = new Dictionary<long, User>
                {
                {idMember, user}
                };

                List<long> expectedIdMembers = board.IDMembers;

                yield return new Object[] { nameMember, numberBoard, dataStorage, idMember, keyBoard, expectedIdMembers, expectedDictionaryUsers, expectedBoardForUser };
            }


        }

        public class TestCaseForRewriteAndReturnTest : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                Dictionary<int, Board> boards = new Dictionary<int, Board>()
                {
                    {1,new Board(1,22) },
                    {2,new Board(2,33) },
                    {3,new Board(3,44) }
                };
                List<Issue> tasks = new List<Issue>()
                {
                    new Issue(1,"QQQ"),
                    new Issue(2,"QQQ"),
                    new Issue(3,"QQQ")
                };

                for (int i = 1; i < boards.Count; i++)
                {
                    boards[i].Issues.AddRange(tasks);
                }

                Dictionary<string, User> users = new Dictionary<string, User>()
                {
                    {"22",new User(22,"Valerka")},
                    {"33",new User(33,"Pasha")},
                    {"44",new User(44,"Kesha")}
                };

                yield return new object[] { boards, users };

                users.Clear();

                yield return new object[] { boards, users };

                boards.Clear();
                users = new Dictionary<string, User>()
                {
                    {"22",new User(22,"Valerka")},
                    {"33",new User(33,"Pasha")},
                    {"44",new User(44,"Kesha")}
                };

                yield return new object[] { boards, users };
            }
        }
    }
}

