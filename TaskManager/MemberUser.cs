namespace TaskManager
{
    public class MemberUser : AbstractUser
    {
        public override bool AddNewIssue(Board board, string description)
        {
            return false;
        }

        public override bool RemoveIssue(Board board, int numberIssue)
        {
            return false;
        }

        public override void AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
        }

        public override bool RemoveBoard(int numberBoard)
        {
            return false;
        }

        public override int AddBoard(string idAdmin)
        {
            return -1;
        }

        public override void AddNewUserByKey(int idBoard, int keyBoard, string idUser, string nameUser)
        {
            AddNewUserByKey(idBoard, keyBoard, idUser, nameUser);
        }
    }
}
