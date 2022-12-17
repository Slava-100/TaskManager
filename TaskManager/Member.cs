namespace TaskManager
{
    public class Member : IUser
    {
        public bool AddNewIssue(Board board, string description)
        {
            return false;
        }

        public bool RemoveIssue(Board board, int numberIssue)
        {
            return false;
        }

        public bool AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            return false;
        }

        public void RemoveBoard(int numberBoard)
        {
            RemoveBoard(numberBoard);
        }

        public int AddBoard(string idAdmin)
        {
            return -1;
        }

        public void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            AddNewUserByKey(idBoard, keyBoard, idUser, nameUser);
        }
    }
}
