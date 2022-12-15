namespace TaskManager
{
    public class DataStorage
    {
        public int NextNumberBoard { get; private set; }

        public string Path { get; set; }

        public Dictionary<int, Board> Boards { get; set; }

        public Dictionary<string, User> Users { get; set; }

        public DataStorage()
        {
            Boards = new Dictionary<int, Board>();
            Users = new Dictionary<string, User>();
            Path = @".\DataStorage.txt";
            UpdateNextNumberBoard();
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

        public void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            if (Boards.ContainsKey(idBoard))
            {
                if (keyBoard == Boards[idBoard].Key)
                {
                    User user = new User(idUser, nameUser);
                    Boards[idBoard].IDMembers.Add(user.IDUser);
                    user.BoardsForUser.Add(Boards[idBoard]);
                    Users.Add(idUser, user);
                }
                else
                {
                    throw new Exception($"invalid password from the board {keyBoard}");
                }
            }
            else
            {
                throw new Exception($"does not exist with this id {idBoard}");
            }
        }
    }
}

