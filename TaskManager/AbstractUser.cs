namespace TaskManager
{
    public abstract class AbstractUser
    {
        public abstract bool AddNewIssue(Board board, string description);

        public abstract bool RemoveIssue(Board board, int numberIssue);

        public abstract void AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue);

        public abstract int AddBoard(string idAdmin);

        public abstract void RemoveBoard(int numberBoard);

        public abstract void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser);
    }
}
