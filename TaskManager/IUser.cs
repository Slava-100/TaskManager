namespace TaskManager
{
    public interface IUser
    {
        bool AddNewIssue(Board board,string description, out int issueId);

        bool RemoveIssue(Board board, int numberIssue);

        bool AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue);
        
        int AddBoard(string idAdmin);

        void RemoveBoard(int numberBoard);

        void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser);
    }
}
