namespace TaskManager
{
    public class Board
    {
        public int NumberBoard { get; private set; }

        public List<string> IDMembers { get; private set; }

        public List<string> IDAdmin { get; private set; }

        public List<Issue> Issues { get; private set; }

        public List<int> Key { get; private set; }

        public Board(int numberBoard, string idAdmin)
        {
            NumberBoard = numberBoard;
            IDAdmin.Add(idAdmin);
        }
    }
}
