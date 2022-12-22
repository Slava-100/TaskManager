using Telegram.Bot.Types;

namespace TaskManager
{
    public class Client
    {
        private AbstractUser _userRole;

        private DataStorage _dataStorage = DataStorage.GetInstance();

        private Board _activeBoard;

        public long IDUser { get; set; }

        public string NameUser { get; set; }

        public List<int> BoardsForUser { get; set; }

        public Client(long idUser, string nameUser)
        {
            IDUser = idUser;
            NameUser = nameUser;
            BoardsForUser = new List<int>();
        }

        public Client()
        {
            BoardsForUser = new List<int>();
        }

        public void SetActiveBoard(int numberBoard)
        {
            _activeBoard = _dataStorage.Boards[numberBoard];
            SelectRole();
        }

        public bool SelectRole()
        {
            if (_activeBoard.IDAdmin.Contains(IDUser))
            {
                _userRole = new AdminUser();
                return true;
            }
            else if (_activeBoard.IDMembers.Contains(IDUser))
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
                return ((AdminUser)_userRole).AddNewIssue(_activeBoard, description);
            }
            return false;
        }

        public bool RemoveIssue(int numberIssue)
        {
            if (_userRole is AdminUser)
            {
                return ((AdminUser)_userRole).RemoveIssue(_activeBoard, numberIssue);
            }
            return false;
        }

        public void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            if (_userRole is AdminUser adminUser)
            {
                adminUser.AddBlokingAndBlockedByIssue(_activeBoard, blockedByCurrentIssue, blockingCurrentIssue);
            }
        }

        public int AddBoard()
        {
            return _dataStorage.AddBoard(IDUser);
        }

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

        public void AttachIssueToClient(int IdIssue)
        {
            var issue = _activeBoard.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);

            if (issue != null)
            {
                var issueInWork = _activeBoard.Issues.FirstOrDefault(crntIssue => crntIssue.Status == Enums.IssueStatus.InProgress && crntIssue.IdUser == IDUser);

                if (issueInWork == null && issue.IsAssignable && SelectRole())
                {
                    _userRole.AttachIssueToClient(_activeBoard, issue, IDUser);
                }
            }
        }

        public List<Issue> GetAllIssuesInBoardByBoard()
        {
            return _userRole.GetAllIssuesInBoardByIdUser(IDUser, _activeBoard);
        }

        public List<Issue> GetIssuesInProgressInBoardByBoard()
        {
            return _userRole.GetIssuesInProgressInBoardByIdUser(IDUser, _activeBoard);
        }

        public override bool Equals(object? obj)
        {
            return obj is Client user &&
                   IDUser == user.IDUser &&
                   NameUser == user.NameUser &&
                   BoardsForUser.SequenceEqual(user.BoardsForUser);
        }
    }
}
