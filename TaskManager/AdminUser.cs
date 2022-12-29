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

        public void SetBlockforIssue(Board board, int blockedByCurrentIssue, int blockingCurrentIssue)
        {
            board.SetBlockforIssue(blockedByCurrentIssue, blockingCurrentIssue);
        }

        public bool RemoveBoard(int numberBoard)
        {
            bool tmp = _dataStorage.RemoveBoard(numberBoard);
            _dataStorage.RewriteFileForBoards();

            return tmp;
        }

        public void ChangeRoleFromMemberToAdmin(long idMemeber, Board board)
        {
            board.ChangeRoleMemberToAdmin(idMemeber);
            _dataStorage.RewriteFileForBoards();
        }

        public void ChangeRoleFromAdminToMember(long idAdmin, Board board)
        {
            board.ChangeRoleAdminToMember(idAdmin);
            _dataStorage.RewriteFileForBoards();
        }

        public void MoveIssueFromReviewToDone(Board board, Issue issue)
        {
            issue.Status = IssueStatus.Done;
            _dataStorage.RewriteFileForBoards();
        }

        public List<Issue> GetAllIssuesInBoard(Board board)
        {
            return board.GetAllIssuesInBoard();
        }

        public List<Issue> GetIssuesReviewForUsersBoard(Board board)
        {
            return board.GetIssuesReviewForUsersBoard();
        }
    }
}