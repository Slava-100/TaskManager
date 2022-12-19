namespace TaskManager
{
    public class Board
    {
        private int _numberNextIssue = 1;

        public int NumberBoard { get; private set; }

        public List<long> IDMembers { get; private set; }

        public List<long> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public int Key { get; set; }

        public Board(int numberBoard, long idAdmin)
        {
            IDMembers = new List<long>();
            IDAdmin = new List<long>();
            Issues = new List<Issue>();
            Key = 0;
            NumberBoard = numberBoard;
            IDAdmin.Add(idAdmin);
        }

        public Board()
        {
            IDMembers = new List<long>();
            IDAdmin = new List<long>();
            Issues = new List<Issue>();
        }

        public Board(int numberNextIssue, int numberBoard, List<long> iDMembers, List<long> iDAdmin, List<Issue> issues, int key)
        {
            _numberNextIssue = numberNextIssue;
            NumberBoard = numberBoard;
            IDMembers = iDMembers;
            IDAdmin = iDAdmin;
            Issues = issues;
            Key = key;
        }

        public override string ToString()
        {
            return $"{NumberBoard},{IDAdmin}";
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
                while (CheckIssueAvailabilityByNumber(_numberNextIssue))
                {
                    _numberNextIssue += 1;
                }

                Issues.Add(new Issue(_numberNextIssue, description));
                _numberNextIssue += 1;

                return true;
        }

        public bool RemoveIssue(int numberIssue)
        {
            if (CheckIssueAvailabilityByNumber(numberIssue))
            {
                Issue removableIssue = Issues.Find(issue => issue.NumberIssue == numberIssue);
                Issues.Remove(removableIssue);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue)
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
    }
}
