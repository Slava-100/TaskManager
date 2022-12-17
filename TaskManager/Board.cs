namespace TaskManager
{
    public class Board
    {
        private int _numberNextIssue = 1;

        public int NumberBoard { get; private set; }

        public List<string> IDMembers { get; private set; }

        public List<string> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public int Key { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Board board &&
                   _numberNextIssue == board._numberNextIssue &&
                   NumberBoard == board.NumberBoard &&
                   IDMembers.SequenceEqual(board.IDMembers) &&
                   IDAdmin.SequenceEqual(board.IDAdmin) &&
                   Issues.SequenceEqual(board.Issues)&&
                   Key == board.Key;
        }

        public Board(int numberBoard, string idAdmin)
        {
            IDMembers = new List<string>();
            IDAdmin = new List<string>();
            Issues = new List<Issue>();
            Key = 0;
            NumberBoard = numberBoard;
            IDAdmin.Add(idAdmin);
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

        public override bool Equals(object? obj)
        {
            return obj is Board board &&
                   NumberBoard == board.NumberBoard &&
                   Key == board.Key &&
                   IDMembers.SequenceEqual(board.IDMembers);
        }
    }
}
