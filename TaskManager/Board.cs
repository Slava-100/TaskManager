using TaskManager.Enums;
using Telegram.Bot.Types;

namespace TaskManager
{
    public class Board
    {
        private int _numberNextIssue = 1;

        public long OwnerBoard { get; set; }

        public int NumberBoard { get; set; }

        public string NameBoard { get;private  set; }

        public List<long> IDMembers { get;  set; }

        public List<long> IDAdmin { get;  set; }

        public List<Issue> Issues { get; set; }

        public long Key { get;private set; }

        public Board(int numberBoard, long idAdmin, string nameBoard)
        {
            IDMembers = new List<long>();
            IDAdmin = new List<long>();
            Issues = new List<Issue>();
            NameBoard = nameBoard;
            NumberBoard = numberBoard;
            IDAdmin.Add(idAdmin);
        }

        public Board()
        {
            IDMembers = new List<long>();
            IDAdmin = new List<long>();
            Issues = new List<Issue>();
        }

        public override string ToString()
        {
            return $"Номер доски: {NumberBoard} | Название доски: {NameBoard} ";
        }

        public override bool Equals(object? obj)
        {
            #region
            //if (obj is Board)
            //{
            //    List<long> idMambers = ((Board)obj).IDMembers;

            //    if (idMambers.Count != IDMembers.Count)
            //    {
            //        return false;
            //    }
            //    for (int i = 0; i < IDMembers.Count; i++)
            //    {
            //        if (!IDMembers[i].Equals(idMambers[i]))
            //        {
            //            return false;
            //        }
            //    }

            //    List<long> idAdmins = ((Board)obj).IDAdmin;

            //    if (idAdmins.Count != IDAdmin.Count)
            //    {
            //        return false;
            //    }
            //    for (int i = 0; i < IDAdmin.Count; i++)
            //    {
            //        if (!IDAdmin[i].Equals(idAdmins[i]))
            //        {
            //            return false;
            //        }
            //    }

            //    List<Issue> tmpIssues = ((Board)obj).Issues;

            //    if (tmpIssues.Count != Issues.Count)
            //    {
            //        return false;
            //    }
            //    for (int i = 0; i < Issues.Count; i++)
            //    {
            //        if (!Issues[i].Equals(tmpIssues[i]))
            //        {
            //            return false;
            //        }
            //    }
            //}
            #endregion
            return obj is Board board &&
                   OwnerBoard == board.OwnerBoard &&
                   NameBoard == board.NameBoard &&
                   _numberNextIssue == board._numberNextIssue &&
                   NumberBoard == board.NumberBoard &&
                   IDMembers.SequenceEqual(board.IDMembers) &&
                   IDAdmin.SequenceEqual(board.IDAdmin) &&
                   Issues.SequenceEqual(board.Issues) &&
                   Key == board.Key;
        }

        public int GetNextNumberIssue()
        {
            if (Issues.Count > 0)
            {
                Issue issue = Issues.LastOrDefault();
                int issueNumber = issue.NumberIssue;
                return issueNumber + 1;
            }
            else
            {
                return 1;
            }
        }

        private bool CheckIssueAvailabilityByNumber(int numberIssue)
        {
            return Issues.Exists(issue => issue.NumberIssue == numberIssue);
        }

        public bool AddNewIssue(string description)
        {
            DataStorage _dataStorage = DataStorage.GetInstance();

            while (CheckIssueAvailabilityByNumber(_numberNextIssue))
            {
                _numberNextIssue += 1;
            }

            Issues.Add(new Issue(_numberNextIssue, description));
            Issues.Last().Status = IssueStatus.Backlog;
            _numberNextIssue += 1;
            _dataStorage.RewriteFileForBoards();

            return true;
        }

        public bool RemoveIssue(int numberIssue)
        {
            DataStorage _dataStorage = DataStorage.GetInstance();

            if (CheckIssueAvailabilityByNumber(numberIssue))
            {
                Issue removableIssue = Issues.Find(issue => issue.NumberIssue == numberIssue);
                Issues.Remove(removableIssue);
                _dataStorage.RewriteFileForBoards();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetBlockforIssue(int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            foreach (var currentIssue in Issues)
            {
                if (currentIssue.NumberIssue == blockingCurrentIssue)
                {
                    currentIssue.BlockedByCurrentIssue.Add(blockedByCurrentIssue);
                }
                if (currentIssue.NumberIssue == blockedByCurrentIssue)
                {
                    currentIssue.BlockingIssues.Add(blockingCurrentIssue);
                }
            }
        }

        public List<Issue> GetAllIssuesAbountIdUser(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();

            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if (issue.IdUser == idUser)
                        allIssues.Add(issue);
                }
            }
            return allIssues;
        }

        public List<Issue> GetIssuesInProgressUser(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();

            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if (issue.IdUser == idUser && issue.Status == IssueStatus.InProgress) 
                        allIssues.Add(issue);
                }
            }
            return allIssues;
        }

        public List<Issue> GetIssuesDoneForUser(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();
            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if ((issue.IdUser == idUser) && (issue.Status == Enums.IssueStatus.Done))
                        allIssues.Add(issue);
                }
                return allIssues;
            }
            return new List<Issue>();
        }

        public List<Issue> GetIssuesCompletedForUser(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();
            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if ((issue.IdUser == idUser) && ((issue.Status == Enums.IssueStatus.Done) || (issue.Status == Enums.IssueStatus.Review)))
                        allIssues.Add(issue);
                }
                return allIssues;
            }
            return new List<Issue>();
        }

        public List<Issue> GetIssuesReviewForUser(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();
            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if ((issue.IdUser == idUser) && (issue.Status == Enums.IssueStatus.Review))
                    {
                        allIssues.Add(issue);
                    }
                }
                return allIssues;
            }
            return new List<Issue>();
        }

        public List<Issue> GetIssuesFreeInBoard(long idUser)
        {
            List<Issue> allIssues = new List<Issue>();
            if (IDMembers.Contains(idUser) || IDAdmin.Contains(idUser))
            {
                foreach (Issue issue in Issues)
                {
                    if ((issue.IdUser != idUser) && (issue.Status == Enums.IssueStatus.UserStory) || (issue.IdUser != idUser) && (issue.Status == Enums.IssueStatus.Backlog))
                    {
                        allIssues.Add(issue);
                    }
                }
                return allIssues;
            }
            return new List<Issue>();
        }

        public List<Issue> GetAllIssuesInBoard()
        {
            return Issues;
        }

        public List<Issue> GetIssuesReviewForUsersBoard()
        {
            List<Issue> allIssues = new List<Issue>();

            foreach (Issue issue in Issues)
            {
                if (issue.Status == Enums.IssueStatus.Review)
                {
                    allIssues.Add(issue);
                }
            }
            return allIssues;
        }

        public void ChangeRoleMemberToAdmin(long idMemeber)
        {
            if (IDMembers.Contains(idMemeber))
            {
                IDMembers.Remove(idMemeber);
                IDAdmin.Add(idMemeber);
            }
        }

        public void ChangeRoleAdminToMember(long idAdmin)
        {
            if (IDAdmin.Contains(idAdmin))
            {
                IDAdmin.Remove(idAdmin);
                IDMembers.Add(idAdmin);
            }
        }
    }
}