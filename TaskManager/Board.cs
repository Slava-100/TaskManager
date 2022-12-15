namespace TaskManager
{
    public class Board
    {

        public int NumberBoard { get; private set; }

        public List<string> IDMembers { get; private set; }

        public List<string> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public int Key { get; private set; }

        public Board(int numberBoard, string idAdmin, int key)
        {
            IDMembers = new List<string>();
            IDAdmin = new List<string>();
            Issues = new List<Issue>();
            Key = key;
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

        public override bool Equals(object? obj)
        {
            return obj is Board board &&
                   NumberBoard == board.NumberBoard &&
                   EqualityComparer<List<string>>.Default.Equals(IDMembers, board.IDMembers) &&
                   EqualityComparer<List<string>>.Default.Equals(IDAdmin, board.IDAdmin) &&
                   EqualityComparer<List<Issue>>.Default.Equals(Issues, board.Issues) &&
                   Key == board.Key;
        }
    }
}
