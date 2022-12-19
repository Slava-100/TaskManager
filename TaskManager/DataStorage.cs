using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager
{
    public class DataStorage
    {
        public int NextNumberBoard { get; private set; }

        public string PathFileForBoards { get; set; }
        
        public string PathFileForClient { get; set; }

        public Dictionary<int, Board> Boards { get; set; }

        public Dictionary<long, Client> Clients { get; set; }

        private static DataStorage _instance=new DataStorage();

        public DataStorage()
        {
            Boards = new Dictionary<int, Board>();
            Clients = new Dictionary<long, Client>();
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
            if(File.Exists(PathFileForBoards))
            {
                using (StreamReader file = new StreamReader(PathFileForBoards))
                {
                    string deserialiseFromFile = file.ReadLine();
                    Boards = JsonSerializer.Deserialize<Dictionary<int, Board>>(deserialiseFromFile);
                }
            }

            if (File.Exists(PathFileForClient))
            {
                using (StreamReader file = new StreamReader(PathFileForClient))
                {
                    string deserialiseFromFile = file.ReadLine();
                    Clients = JsonSerializer.Deserialize<Dictionary<long,Client>>(deserialiseFromFile);
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

        public int AddBoard(long idAdmin)
        {
            Board board = new Board(NextNumberBoard, idAdmin);
            Boards.Add(board.NumberBoard, board);
            NextNumberBoard = NextNumberBoard + 1;
            RewriteFileForBoards();
            return board.NumberBoard;
        }

        public bool AddNewUserByKey(int idBoard, int keyBoard, long idUser, string nameUser)
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
                    }
                    else if (Boards[idBoard].IDMembers.Contains(idUser) == false)
                    {
                        Boards[idBoard].IDMembers.Add(idUser);
                        Clients[idUser].BoardsForUser.Add(idBoard);

                        flag = true;
                    }
                }
            }
           
            return flag;
        }
    }
}

