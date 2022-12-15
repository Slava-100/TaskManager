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

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   EqualityComparer<DataStorage>.Default.Equals(_storage, user._storage) &&
                   IDUser == user.IDUser &&
                   NameUser == user.NameUser &&
                   EqualityComparer<List<Board>>.Default.Equals(BoardsForUser, user.BoardsForUser);
        }
    }
}
