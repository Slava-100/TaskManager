namespace TaskManager
{
    public interface IUser
    {
        void AddNewIssue();

        void RemoveIssue(int numberIssue);

        void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue);

        void RemoveBoard(int numberBoard);

        void AddBoard(string idAdmin);

        void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser);

    }
}
