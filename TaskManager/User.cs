namespace TaskManager
{
    public class User
    {
        public string IDUser { get; private set; }
        public string NameUser { get; private set; }
        public List<Board> BoardsForUser { get; private set; }
        public User(string idUser, string nameUser)
        {
            IDUser = idUser;
            NameUser = nameUser;
            BoardsForUser = new List<Board>();
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   IDUser == user.IDUser &&
                   NameUser == user.NameUser;
        }
    }
}
