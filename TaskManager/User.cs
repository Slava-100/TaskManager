namespace TaskManager
{
    public class User
    {
        public string IDUser { get; private set; }
        public string NameUser { get; private set; }
        public List<int> BoardsForUser { get; private set; }
        public User(string idUser, string nameUser)
        {
            IDUser = idUser;
            NameUser = nameUser;
            BoardsForUser = new List<int>();
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   IDUser == user.IDUser &&
                   NameUser == user.NameUser &&
                   BoardsForUser.SequenceEqual(user.BoardsForUser);
        }
    }
}
