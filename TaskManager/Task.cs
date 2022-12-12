using TaskManager.Enums;

namespace TaskManager
{
    public class Task
    {
        public int NumberTask { get; set; }

        public string Description { get; set; }

        public string IdUser { get; set; }

        public string Comment { get; set; }

        public StatusOfTask Status { get; set; }

        public Task()
        {
        }

        public Task(string description)
        {
            Description = description;
        }
    }
}
