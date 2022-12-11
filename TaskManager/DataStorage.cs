namespace TaskManager
{
    public class DataStorage
    {
        public string Path { get; set; }

        public Dictionary<string,Board> Boards { get; set; }

        public List <User> Users { get; set; }

        public DataStorage()
        {
            Boards = new Dictionary<string,Board>();
            Users = new List<User>();
            Path = @".\DataStorage.txt";
        }
    }
}
