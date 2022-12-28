using TaskManager.Enums;

namespace TaskManager
{
    public class AdminUser : AbstractUser
    {
        private DataStorage _dataStorage = DataStorage.GetInstance();

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
            bool tmp = _dataStorage.RemoveBoard(numberBoard);
            _dataStorage.RewriteFileForBoards();
            return tmp;
        }

        public void ChangeRoleFromMemberToAdmin(long idMemeber, Board board)
        {
            board.ChangeRoleFromMemberToAdmin(idMemeber);
            _dataStorage.RewriteFileForBoards();
        }

        public void ChangeRoleFromAdminToMember(long idAdmin, Board board)
        {
            board.ChangeRoleFromAdminToMember(idAdmin);
            _dataStorage.RewriteFileForBoards();
        }

        public void MoveIssueFromReviewToDone(Board board, Issue issue)
        {
            issue.Status = IssueStatus.Done;
            _dataStorage.RewriteFileForBoards();
        }
    }
}
