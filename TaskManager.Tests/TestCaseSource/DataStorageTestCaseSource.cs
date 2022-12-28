using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public static class DataStorageTestCaseSource
    {
        public static IEnumerable RemoveBoardTestSource()
        {
            Board board1 = new Board(1, 1, "nameOfBoard");
            Board board2 = new Board(2, 2, "nameOfBoard");
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

            Board board10 = new Board(10, 10, "nameOfBoard");
            Board board21 = new Board(21, 21, "nameOfBoard");
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
            Dictionary<long, Client> baseClients = new()
            {
                { 12, new Client(12, "12") }
            };
            long idAdmin = 12;
            Board expectedBoard = new Board(1, 12, "nameOfBoard");
            int expectedNumberBoard = 1;
            string nameBoard = "nameOfBoard";

            yield return new Object[] { baseBoards, baseClients, idAdmin, expectedBoard, expectedNumberBoard, nameBoard };
        }

        public class AddNewUserByKeyTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                int numberBoard = 1;
                long idMember = 123;
                int keyBoard = 1;
                string nameMember = "";
                Board board = new Board(numberBoard, 1234, "nameOfBoard");
                board.Key = keyBoard;

                Dictionary<int, Board> storageBoard = new Dictionary<int, Board>()
                {
                {1, board}
                };

                DataStorage dataStorage = new DataStorage();
                dataStorage.Boards = storageBoard;

                //board.IDMembers.Add(idMember);

                Client user = new Client(idMember, nameMember);

                user.BoardsForUser.Add(numberBoard);

                List<int> expectedBoardForUser = user.BoardsForUser;

                Dictionary<long, Client> expectedDictionaryUsers = new Dictionary<long, Client>
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
                    {1,new Board(1,22, "boardName") },
                    {2,new Board(2,33, "boardName") },
                    {3,new Board(3,44, "boardName") }
                };
                List<Issue> tasks = new List<Issue>()
                {
                    new Issue(1,"QQQ"),
                    new Issue(2,"WWW"),
                    new Issue(3,"EEE")
                };

                for (int i = 1; i < boards.Count; i++)
                {
                    boards[i].Issues.AddRange(tasks);
                }

                Dictionary<long, Client> users = new Dictionary<long, Client>()
                {
                    {22,new Client(22,"Valerka")},
                    {33,new Client(33,"Pasha")},
                    {44,new Client(44,"Kesha")}
                };

                yield return new object[] { boards, users };

                users = new Dictionary<long, Client>();

                yield return new object[] { boards, users };

                boards = new Dictionary<int, Board>();
                users = new Dictionary<long, Client>()
                {
                    {22,new Client(22,"Valerka")},
                    {33,new Client(33,"Pasha")},
                    {44,new Client(44,"Kesha")}
                };

                yield return new object[] { boards, users };
            }
        }

        public static IEnumerable GetAllBoardsByNumbersOfBoardTestCaseSource()
        {
            //1. Проверка, если запрашиваем доски для Админа

            Client client = new Client(11, "11");
            Board board1 = new Board(1, 11, "boardName");
            board1.IDAdmin.Add(11);
            Board board2 = new Board(2, 11, "boardName");
            board2.IDAdmin.Add(11);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
            };
            List<int> baseBoardsForUser = new List<int> { board1.NumberBoard, board2.NumberBoard };

            Client expClient = new Client(11, "11");
            Board expBoard1 = new Board(1, 11, "boardName");
            expBoard1.IDAdmin.Add(11);
            Board expBoard2 = new Board(2, 11, "boardName");
            expBoard2.IDAdmin.Add(11);
            Dictionary<int, Board> expBoards = new Dictionary<int, Board>
            {
                { expBoard1.NumberBoard, expBoard1},
                { expBoard2.NumberBoard, expBoard2},
            };
            List<int> expBoardsForUser = new List<int> { expBoard1.NumberBoard, expBoard2.NumberBoard };
            List<Board> expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { baseBoards, baseBoardsForUser, expectedBoards };

            //2. Проверка, если запрашиваем доски для Мембера

            Client admin = new Client(112, "112");
            client = new Client(2, "2");
            board1 = new Board(12, 112, "boardName");
            board1.IDAdmin.Add(2);
            board2 = new Board(22, 112, "boardName");
            board2.IDAdmin.Add(2);
            baseBoards = new Dictionary<int, Board>
            {
                { board1.NumberBoard, board1},
                { board2.NumberBoard, board2},
            };
            baseBoardsForUser = new List<int> { board1.NumberBoard, board2.NumberBoard };

            Client expAdmin = new Client(112, "112");
            expClient = new Client(2, "2");
            expBoard1 = new Board(12, 112, "boardName");
            expBoard1.IDAdmin.Add(2);
            expBoard2 = new Board(22, 112, "boardName");
            expBoard2.IDAdmin.Add(2);
            expBoards = new Dictionary<int, Board>
            {
                { expBoard1.NumberBoard, expBoard1},
                { expBoard2.NumberBoard, expBoard2},
            };
            expBoardsForUser = new List<int> { expBoard1.NumberBoard, expBoard2.NumberBoard };
            expectedBoards = new List<Board> { expBoard1, expBoard2 };

            yield return new Object[] { baseBoards, baseBoardsForUser, expectedBoards };
        }
    }
}

