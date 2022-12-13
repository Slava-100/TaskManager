namespace TaskManager
{
    public class User
    {
        private DataStorage _storage;
        public string IDUser { get; private set; }
        public string NameUser { get; private set; }
        public List<Board> BoardsForUser { get; private set; }
        public User(string idUser, string nameUser)
        {
            IDUser = idUser;
            NameUser = nameUser;
        }
    }
}
