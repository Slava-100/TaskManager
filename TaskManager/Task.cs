namespace TaskManager
{
    public class Task
    {
        public enum StatusTask
        {
            UserStory,
            Backlog,
            InProgress,
            Review,
            Done
        }

        public int NumberTask { get; set; }

        public string Description { get; set; }

        public string IdUser { get; set; }

        public string Comment { get; set; }

        public StatusTask Status { get; set; }

        public Task()
        {
        }

        public Task(string description)
        {
            Description = description;
        }
    }
}
