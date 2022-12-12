using TaskManager.Enums;

namespace TaskManager
{
    public class Issue
    {
        public int NumberTask { get; set; }

        public string Description { get; set; }

        public string IdUser { get; set; }

        public string Comment { get; set; }

        public IssueStatus Status { get; set; }

        public Issue(int numberTask, string description)
        {
            NumberTask = numberTask;
            Description = description;
        }
    }
}
