namespace TaskManager
{
    public class Board
    {
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

        private int GetNextNumberIssue()
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
