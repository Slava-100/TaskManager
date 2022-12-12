namespace TaskManager
{
    public class DataStorage
    {
        public string Path { get; set; }

        public Dictionary<string,Board> Boards { get; set; }

        public Dictionary <string,User> Users { get; set; }

        public DataStorage()
        {
            Boards = new Dictionary<string,Board>();
            Users = new Dictionary<string, User>();
            Path = @".\DataStorage.txt";
        }
    }
}
