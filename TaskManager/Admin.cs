namespace TaskManager
{
    public class Admin : IUser
    {
        public void AddNewIssue(string description)
        {
            AddNewIssue(description);
        }

        public void RemoveIssue(int numberIssue)
        {
            RemoveIssue(numberIssue);
        }

        public void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            AddBlokingAndBlockedByIssue(blockedByCurrentIssue, blockingCurrentIssue);
        }

        public void RemoveBoard(int numberBoard)
        {
            RemoveBoard(numberBoard);
        }

        public void AddBoard(string idAdmin)
        {
            AddBoard(idAdmin);
        }

        public void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            AddNewUserByKey(idBoard, keyBoard, idUser, nameUser);
        }
    }
}
