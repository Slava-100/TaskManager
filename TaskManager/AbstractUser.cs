namespace TaskManager
{
    public abstract class AbstractUser
    {
        protected DataStorage DataStorage => DataStorage.GetInstance();

        public abstract bool AddNewIssue(Board board, string description);

        public abstract bool RemoveIssue(Board board, int numberIssue);

        public abstract void AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue);

        public abstract bool RemoveBoard(int numberBoard);
    }
}
