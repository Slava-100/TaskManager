namespace TaskManager
{
    public class Admin : IUser
    {
        public bool AddNewIssue(Board board, string description, out int issueId)
        {
            var isAddIssue = board.AddNewIssue(description);
            issueId = board.Issues.First(issue=>issue.Description==description).NumberIssue;
            return isAddIssue;
        }

        public bool RemoveIssue(Board board, int numberIssue)
        {
            return board.RemoveIssue(numberIssue);
        }

        public bool AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            board.AddBlokingAndBlockedByIssue(blockedByCurrentIssue, blockingCurrentIssue);
            return true;
        }

        public void RemoveBoard(int numberBoard)
        {
            RemoveBoard(numberBoard);
        }

        public int AddBoard(string idAdmin)
        {
            return AddBoard(idAdmin);
        }

        public void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            AddNewUserByKey(idBoard, keyBoard, idUser, nameUser);
        }
    }
}
