﻿namespace TaskManager
{
    public class AdminUser : AbstractUser
    {
        public bool AddNewIssue(Board board, string description)
        {
            return board.AddNewIssue(description);
        }

        public bool RemoveIssue(Board board, int numberIssue)
        {
            return board.RemoveIssue(numberIssue);
        }

        public void AddBlokingAndBlockedByIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            board.AddBlokingAndBlockedByIssue(blockedByCurrentIssue, blockingCurrentIssue);
        }

        public bool RemoveBoard(int numberBoard)
        {
            DataStorage dataStorage = DataStorage.GetInstance();
            bool tmp = dataStorage.RemoveBoard(numberBoard);
            dataStorage.RewriteFileForBoards();
            return tmp;
        }

        public void ChangeRoleFromMemberToAdmin(long idMemeber, Board board)
        {
            board.ChangeRoleFromMemberToAdmin(idMemeber);
        }

        public void ChangeRoleFromAdminToMember(long idAdmin, Board board)
        {
            board.ChangeRoleFromAdminToMember(idAdmin);
        }
    }
}
