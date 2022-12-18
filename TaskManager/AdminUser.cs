namespace TaskManager
{
    public class AdminUser : AbstractUser
    {
        public override bool AddNewIssue(Board board, string description)
        {
            return board.AddNewIssue(description);
        }

        public override bool RemoveIssue(Board board, int numberIssue)
        {
            return board.RemoveIssue(numberIssue);
        }

        public override void AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            board.AddBlokingAndBlockedByIssue(blockedByCurrentIssue, blockingCurrentIssue);
        }

        public override bool RemoveBoard(int numberBoard)
        {
            return DataStorage.RemoveBoard(numberBoard);
        }
    }
}
