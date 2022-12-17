using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager
{
    public class DataStorage
    {
        public int NextNumberBoard { get; private set; }

        public string Path { get; set; }

        public Dictionary<int, Board> Boards { get; set; }

        public Dictionary<string, User> Users { get; set; }

        private static DataStorage _instance;

        public DataStorage()
        {
            Boards = new Dictionary<int, Board>();
            Users = new Dictionary<string, User>();
            Path = @".\DataStorage.txt";
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

        public void RewriteFile()
        {
            using (StreamWriter file = new StreamWriter(Path))
            {
                string serialiseForFile = JsonSerializer.Serialize(Boards);
                file.WriteLine(serialiseForFile);
                serialiseForFile= JsonSerializer.Serialize(Users);
                file.WriteLine(serialiseForFile);
            }
        }

        public void ReturnFromFile()
        {
            if(File.Exists(Path))
            {
                using (StreamReader file = new StreamReader(Path))
                {
                    string deserialiseFromFile = file.ReadLine();
                    Boards = JsonSerializer.Deserialize<Dictionary<int, Board>>(deserialiseFromFile);
                    deserialiseFromFile = file.ReadLine();
                    Users = JsonSerializer.Deserialize<Dictionary<string, User>>(deserialiseFromFile);
                }
            }
        }

        private void UpdateNextNumberBoard()
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

        public int AddBoard(string idAdmin)
        {
            Board board = new Board(NextNumberBoard, idAdmin);
            Boards.Add(board.NumberBoard, board);
            NextNumberBoard = NextNumberBoard + 1;
            return board.NumberBoard;
        }

        public bool AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            bool flag = false;

            if (Boards.ContainsKey(idBoard))
            {
                if (keyBoard == Boards[idBoard].Key)
                {
                    if (Users.ContainsKey(idUser) == false)
                    {
                        User user = new User(idUser, nameUser);
                        Boards[idBoard].IDMembers.Add(user.IDUser);
                        user.BoardsForUser.Add(idBoard);
                        Users.Add(idUser, user);

                        flag = true;
                    }
                    else if (Boards[idBoard].IDMembers.Contains(idUser) == false)
                    {
                        Boards[idBoard].IDMembers.Add(idUser);
                        Users[idUser].BoardsForUser.Add(idBoard);

                        flag = true;
                    }
                }
            }
           
            return flag;
        }
    }
}

