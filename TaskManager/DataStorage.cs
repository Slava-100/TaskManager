﻿using System.Text.Json;

namespace TaskManager
{
    public class DataStorage
    {
        public int NextNumberBoard { get; private set; }

        public string PathFileForBoards { get; set; }

        public string PathFileForClient { get; set; }

        public Dictionary<int, Board> Boards { get; set; }

        public Dictionary<long, Client> Clients { get; set; }

        public List<long> KeysForBoards { get; set; }

        private static DataStorage _instance;

        private DataStorage()
        {
            Boards = new Dictionary<int, Board>();
            Clients = new Dictionary<long, Client>();
            KeysForBoards = new List<long>();
            PathFileForBoards = @".\PathFileForBoards.txt";
            PathFileForClient = @".\PathFileForClient.txt";
            ReturnFromFile();
            UpdateNextNumberBoard();
        }

        public static DataStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataStorage();
            }
            return _instance;
        }

        public void RewriteFileForBoards()
        {
            using (StreamWriter file = new StreamWriter(PathFileForBoards))
            {
                string serialiseForFile = JsonSerializer.Serialize(Boards);
                file.WriteLine(serialiseForFile);
                serialiseForFile = JsonSerializer.Serialize(KeysForBoards);
                file.WriteLine(serialiseForFile);
            }
        }

        public void RewriteFileForClients()
        {
            using (StreamWriter file = new StreamWriter(PathFileForClient))
            {
                string serialiseForFile = JsonSerializer.Serialize(Clients);
                file.WriteLine(serialiseForFile);
            }
        }

        public void ReturnFromFile()
        {
            if (File.Exists(PathFileForBoards))
            {
                using (StreamReader file = new StreamReader(PathFileForBoards))
                {
                    string deserialiseFromFile = file.ReadLine();
                    Boards = JsonSerializer.Deserialize<Dictionary<int, Board>>(deserialiseFromFile);
                    deserialiseFromFile = file.ReadLine();
                    KeysForBoards = JsonSerializer.Deserialize<List<long>>(deserialiseFromFile);
                }
            }

            if (File.Exists(PathFileForClient))
            {
                using (StreamReader file = new StreamReader(PathFileForClient))
                {
                    string deserialiseFromFile = file.ReadLine();
                    Clients = JsonSerializer.Deserialize<Dictionary<long, Client>>(deserialiseFromFile);
                }
            }


        }

        public void UpdateNextNumberBoard()
        {
            int max = 0;
            foreach (int currentNumberBoard in Boards.Keys)
            {
                if (currentNumberBoard > max)
                {
                    max = currentNumberBoard;
                }
            }
            NextNumberBoard = max + 1;
        }

        public bool RemoveBoard(int numberBoard)
        {
            return Boards.Remove(numberBoard);
        }

        public int AddBoard(long idAdmin, string nameboard)
        {
            Board board = new Board(NextNumberBoard, idAdmin, nameboard);
            board.Key = GenerateKeyForBoards();
            Boards.Add(board.NumberBoard, board);
            DataStorage.GetInstance().Clients[idAdmin].BoardsForUser.Add(NextNumberBoard);
            NextNumberBoard = NextNumberBoard + 1;

            RewriteFileForBoards();
            RewriteFileForClients();

            return board.NumberBoard;
        }

        public void DeleteActiveBoard(Board activeBoard, long activeIdClient)
        {
            Boards.Remove(activeBoard.NumberBoard);
            Clients[activeIdClient].BoardsForUser.Remove(activeBoard.NumberBoard);

            RewriteFileForBoards();
            RewriteFileForClients();
        }

        private long GenerateKeyForBoards()
        {
            Random random = new Random();
            long key;

            do
            {
                key = random.Next(10000000, 99999999);
            }
            while (KeysForBoards.Contains(key));

            return key;
        }

        public bool AddNewUserByKey(int idBoard, long keyBoard, long idUser, string nameUser)
        {
            bool flag = false;

            if (Boards.ContainsKey(idBoard))
            {
                if (keyBoard == Boards[idBoard].Key)
                {
                    if (Clients.ContainsKey(idUser) == false)
                    {
                        Client user = new Client(idUser, nameUser);
                        Boards[idBoard].IDMembers.Add(user.IDUser);
                        user.BoardsForUser.Add(idBoard);
                        Clients.Add(idUser, user);

                        flag = true;
                        RewriteFileForClients();
                        RewriteFileForBoards();
                    }
                    else if (Boards[idBoard].IDMembers.Contains(idUser) == false)
                    {
                        Boards[idBoard].IDMembers.Add(idUser);
                        Clients[idUser].BoardsForUser.Add(idBoard);

                        flag = true;
                        RewriteFileForClients();
                        RewriteFileForBoards();
                    }
                }
            }

            return flag;
        }

        public List<Board> GetAllBoardsByNumbersOfBoard(List<int> boardsForUser)
        {
            return boardsForUser
                .Where(currentNumber => Boards.ContainsKey(currentNumber))
                .Select(currentNumber => Boards[currentNumber])
                .ToList();
        }

        public List<Board> GetAllAdminsBoardsByNumbersOfBoard(List<int> boardsForUser, long idUser)
        {
            return boardsForUser
                .Where(currentNumber => Boards.ContainsKey(currentNumber) && Boards[currentNumber].IDAdmin.Contains(idUser))
                .Select(currentNumber => Boards[currentNumber])
                .ToList();
        }

        public List<Board> GetAllMembersBoardsByNumbersOfBoard(List<int> boardsForUser, long idUser)
        {
            return boardsForUser
                .Where(currentNumber => Boards.ContainsKey(currentNumber) && Boards[currentNumber].IDMembers.Contains(idUser))
                .Select(currentNumber => Boards[currentNumber])
                .ToList();
        }

        public List<Board> GetAllBoardsToWhichYouCanJoin(List<int> boardsForUser)
        {
            return Boards
                .Where(crnBoard => !boardsForUser.Contains(crnBoard.Key))
                .Select(crnBoard => crnBoard.Value)
                .ToList();
        }
    }
}

