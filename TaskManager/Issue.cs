using TaskManager.Enums;

namespace TaskManager
{
    public class Issue
    {
        public int NumberIssue { get; set; }

        public string Description { get; set; }

        public string IdUser { get; set; }

        public string Comment { get; set; }

        public IssueStatus Status { get; set; }

        public Issue(int numberIssue, string description)
        {
            NumberIssue = numberIssue;
            Description = description;
        }

        public override bool Equals(object? obj)
        {
            return obj is Issue issue &&
                   NumberIssue == issue.NumberIssue &&
                   Description == issue.Description &&
                   IdUser == issue.IdUser &&
                   Comment == issue.Comment &&
                   Status == issue.Status;
        }
    }
}
