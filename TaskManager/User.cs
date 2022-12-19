using System.Security.Cryptography.X509Certificates;

namespace TaskManager
{
    public class User
    {
        private AbstractUser _userRole;

        public long IDUser { get; private set; }

        public string NameUser { get; private set; }

        public List<int> BoardsForUser { get; private set; }

        public Board ActiveBoard { get; private set; }

        public User(long idUser, string nameUser)
        {
            IDUser = idUser;
            NameUser = nameUser;
            BoardsForUser = new List<int>();
        }

        public bool SelectRole()
        {
            if (ActiveBoard.IDAdmin.Contains(IDUser))
            {
                _userRole = new AdminUser();
                return true;
            }
            else if (ActiveBoard.IDMembers.Contains(IDUser))
            {
                _userRole = new MemberUser();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewIssue(string description)
        {
            if (_userRole is AdminUser)
            {
                return ((AdminUser)_userRole).AddNewIssue(ActiveBoard, description);
            }
            return false;
        }

        public bool RemoveIssue(int numberIssue)
        {
            if (_userRole is AdminUser)
            {
                return ((AdminUser)_userRole).RemoveIssue(ActiveBoard, numberIssue);
            }
            return false;
        }

        public void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            if (_userRole is AdminUser adminUser)
            {
                adminUser.AddBlokingAndBlockedByIssue(ActiveBoard, blockedByCurrentIssue, blockingCurrentIssue);
            }
        }

        public int AddBoard() => _userRole.AddBoard(IDUser);

        public bool RemoveBoard(int numberBoard)
        {
            if (_userRole is AdminUser adminUser)
            {
                return adminUser.RemoveBoard(numberBoard);
            }
            return false;
        }

        public void AddNewUserByKey(int idBoard, int keyBoard)
        {
            DataStorage.GetInstance().AddNewUserByKey(idBoard, keyBoard, IDUser, NameUser);
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
