namespace TaskManager
{
    public class Board
    {
        private int _numberNextIssue = 1;

        public int NumberBoard { get; private set; }

        public List<string> IDMembers { get; private set; }

        public List<string> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public List<int> Keys { get; private set; }

        public Board(int numberBoard, string idAdmin)
        {
            IDMembers = new List<string>();
            IDAdmin = new List<string>();
            Issues = new List<Issue>();
            Keys = new List<int>();
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

        public int AddNewIssue(string description)
        {
            while (CheckIssueAvailabilityByNumber(_numberNextIssue))
            {
                _numberNextIssue += 1;
            }

            Issues.Add(new Issue(_numberNextIssue, description));
            _numberNextIssue += 1;

            return Issues[Issues.Count-1].NumberIssue;
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
