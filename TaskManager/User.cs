using System.Security.Cryptography.X509Certificates;

namespace TaskManager
{
    public class User
    {
        public IUser _user;

        //public void SelectRole(bool isAdmin)
        public bool SelectRole(Board board)
        {
            if (board.IDAdmin.Contains(IDUser))
            {
                _user = new Admin();
                return true;
            }
            else if(board.IDMembers.Contains(IDUser))
            {
                _user = new Member();
                return true;
            }
            else 
            {
                return false;
            }
        }

        //public bool AddNewIssue(string description)
        //{
        //    return _user.AddNewIssue(description);
        //}

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
    }
}
