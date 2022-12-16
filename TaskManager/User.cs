namespace TaskManager
{
    public class User
    {
        IUser _user;

        public void SelectRole(bool isAdmin)
        {
            if (isAdmin)
            {
                _user = new Admin();
            }
            else
            {
                _user = new Member();
            }
        }

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
                   NameUser == user.NameUser;
        }

        internal void AddNewIssue(string v)
        {
            throw new NotImplementedException();
        }
    }
}
