namespace TaskManager
{
    public interface IUser
    {
        bool AddNewIssue(Board board,string description);

        void RemoveIssue(int numberIssue);

        void AddBlokingAndBlockedByIssue(int blockedByCurrentIssue, int blockingCurrentIssue);
        
        void AddBoard(string idAdmin);

        void RemoveBoard(int numberBoard);

        void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser);
    }
}
//, out int issueId