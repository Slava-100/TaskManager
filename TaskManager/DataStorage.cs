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
    }
}

