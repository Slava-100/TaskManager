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
           return RemoveBoard(numberBoard);
        }

        public override int AddBoard(string idAdmin)
        {
            return AddBoard(idAdmin);
        }

        public override void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            AddNewUserByKey(idBoard, keyBoard, idUser, nameUser);
        }
    }
}
