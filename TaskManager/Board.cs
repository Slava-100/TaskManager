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

        private bool CheckIssueAvailabilityByNumber(int numberIssue)
        {
            return Issues.Exists(issue => issue.NumberIssue == numberIssue);
        }

        public void AddNewIssue(string description)
        {
            while (CheckIssueAvailabilityByNumber(_numberNextIssue))
            {
                _numberNextIssue += 1;
            }

            Issues.Add(new Issue(_numberNextIssue, description));
            _numberNextIssue += 1;
        }

        public void RemoveIssue(int numberIssue)
        {
            if (CheckIssueAvailabilityByNumber(numberIssue))
            {
                Issue removableIssue = Issues.Find(issue => issue.NumberIssue == numberIssue);
                Issues.Remove(removableIssue);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"no issue with a number {numberIssue}");
            }
        }
    }
}
