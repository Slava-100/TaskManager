namespace TaskManager
{
    public class Board
    {
        public int NumberBoard { get; private set; }

        public List<string> IDMembers { get; private set; }

        public List<string> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public List<int> Keys { get; private set; }

        private int _numberNextIssue = 1;

        public Board(int numberBoard, string idAdmin)
        {
            IDMembers = new List<string>();
            IDAdmin = new List<string>();
            Issues = new List<Issue>();
            Keys = new List<int>();
            NumberBoard = numberBoard;
            IDAdmin.Add(idAdmin);
        }

        public void AddNewIssue(string description)
        {
            Issues.Add(new Issue(_numberNextIssue, description));
            _numberNextIssue += 1;
        }
    }
}
