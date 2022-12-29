namespace TaskManager
{
    public class Client
    {
        private AbstractUser _userRole;

        public Board ActiveBoard { get; set; }

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
            ActiveBoard = DataStorage.GetInstance().Boards[numberBoard];
            SelectRole();
        }

        public Board GetActiveBoard()
        {
            return ActiveBoard;
        }

        public string GetRole()
        {
            string result = "";
            if (_userRole is AdminUser)
            {
                result = "Админ";
            }
            else
            {
                result = "Участник";
            }

            return result;
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
                adminUser.SetBlockforIssue(ActiveBoard, blockedByCurrentIssue, blockingCurrentIssue);
            }
        }

        public int AddBoard(string nameBoard)
        {
            return DataStorage.GetInstance().AddBoard(IDUser, nameBoard);
        }

        public bool RemoveBoard(int numberBoard)
        {
            if (_userRole is AdminUser adminUser)
            {
                return adminUser.RemoveBoard(numberBoard);
            }

            return false;
        }

        public void AddNewUserByKey(int idBoard, long keyBoard)
        {
            DataStorage.GetInstance().AddNewUserByKey(idBoard, keyBoard, IDUser, NameUser);
        }

        public bool AttachIssueToClient(int IdIssue)
        {
            var issue = ActiveBoard.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);

            if ((issue != null && issue.Status == Enums.IssueStatus.UserStory) || (issue != null && issue.Status == Enums.IssueStatus.Backlog))
            {
                var issueInWork = ActiveBoard.Issues.FirstOrDefault(crntIssue => crntIssue.Status == Enums.IssueStatus.InProgress && crntIssue.IdUser == IDUser);

                if (issueInWork == null && issue.IsAssignable && SelectRole())
                {
                    _userRole.AttachIssueToClient(ActiveBoard, issue, IDUser);
                    return true;
                }
            }

            return false;
        }

        public void MoveIssueFromInProgressToBacklog(int idIssue)
        {
            _userRole.MoveIssueToBacklog(ActiveBoard, idIssue, IDUser);
        }

        public void MoveIssueFromInProgressToReview(int idIssue)
        {
            _userRole.MoveIssueToReview(ActiveBoard, idIssue, IDUser);
        }

        public bool MoveIssueFromReviewToDone(int idIssue)
        {
            var issue = ActiveBoard.Issues.FirstOrDefault(currentIssue => idIssue == currentIssue.NumberIssue);
            if (issue != null && _userRole is AdminUser)
            {
                ((AdminUser)_userRole).MoveIssueFromReviewToDone(ActiveBoard, issue);
                return true;
            }

            return false;
        }


        public List<Board> GetAllBoardsByNumbersOfBoard()
        {
            return DataStorage.GetInstance().GetAllBoardsByNumbersOfBoard(BoardsForUser);
        }

        public List<Issue> GetAllIssuesInBoardForClientByBoard()
        {
            return _userRole.GetAllIssuesAbountIdUser(IDUser, ActiveBoard);
        }

        public List<Issue> GetIssuesInProgressForClientInBoard()
        {
            return _userRole.GetIssuesInProgressForUser(IDUser, ActiveBoard);
        }


        public List<Issue> GetIssuesDoneInBoardForClientByBoard()
        {
            return _userRole.GetIssuesDoneForUser(IDUser, ActiveBoard);
        }

        public List<Issue> GetIssuesReviewAndDoneForClientInBoard()
        {
            return _userRole.GetIssuesCompletedForUser(IDUser, ActiveBoard);
        }


        public List<Issue> GetIssuesReviewForClientInBoard()
        {
            return _userRole.GetIssuesReviewForUser(IDUser, ActiveBoard);
        }

        public List<Issue> GetIssuesFreeInBoardForClientByBoard()
        {
            return _userRole.GetIssuesFreeInBoard(IDUser, ActiveBoard);
        }

        public List<Issue> GetAllIssuesInBoard()
        {
            if (_userRole is AdminUser)
            {
                return ((AdminUser)_userRole).GetAllIssuesInBoard(ActiveBoard);
            }

            return new List<Issue>();
        }

        public List<Issue> GetAllIssuesReviewForAllClientsInBoard()
        {
            if (_userRole is AdminUser)
            {
                return ((AdminUser)_userRole).GetIssuesReviewForUsersBoard(ActiveBoard);
            }

            return new List<Issue>();
        }


        public List<Board> GetAllBoardsAdmins()
        {
            return DataStorage.GetInstance().GetAllAdminsBoardsByNumbersOfBoard(BoardsForUser, IDUser);
        }

        public List<Board> GetAllBoardsMembers()
        {
            return DataStorage.GetInstance().GetAllMembersBoardsByNumbersOfBoard(BoardsForUser, IDUser);
        }

        public override bool Equals(object? obj)
        {
            return obj is Client user &&
                   IDUser == user.IDUser &&
                   NameUser == user.NameUser &&
                   BoardsForUser.SequenceEqual(user.BoardsForUser);
        }

        public void ChangeRoleFromMemberToAdmin(long idMemeber)
        {
            if (_userRole is AdminUser)
            {
                ((AdminUser)_userRole).ChangeRoleFromMemberToAdmin(idMemeber, ActiveBoard);
            }
        }

        public void ChangeRoleFromAdminToMember(long idAdmin)
        {
            if (_userRole is AdminUser)
            {
                ((AdminUser)_userRole).ChangeRoleFromAdminToMember(idAdmin, ActiveBoard);
            }
        }

        public List<Board> GetAllBoardsToWhichYouCanJoin()
        {
            return DataStorage.GetInstance().GetAllBoardsToWhichYouCanJoin(BoardsForUser);
        }


    }
}
