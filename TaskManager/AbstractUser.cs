using TaskManager.Enums;

namespace TaskManager
{
    public abstract class AbstractUser
    {
        protected DataStorage _dataStorage = DataStorage.GetInstance();

        public void AttachIssueToClient(Board board, Issue issue, long iDUser)
        {
            issue.IdUser = iDUser;
            issue.Status = IssueStatus.InProgress;
            _dataStorage.RewriteFileForBoards();
            _dataStorage.RewriteFileForClients();
        }

        public void MoveIssueToBacklog(Board board, int IdIssue, long iDUser)
        {
            var issue = board.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);
            if (issue.IdUser == iDUser)
            {
                issue.IdUser = 0;
                issue.Status = IssueStatus.Backlog;
                _dataStorage.RewriteFileForBoards();
            }
        }

        public void MoveIssueToReview(Board board, int IdIssue, long iDUser)
        {
            var issue = board.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);
            if (issue.IdUser == iDUser)
            {
                issue.Status = IssueStatus.Review;
                _dataStorage.RewriteFileForBoards();
            }
        }

        public List<Issue> GetAllIssuesAbountIdUser(long idUser, Board board)
        {
            return board.GetAllIssuesAbountIdUser(idUser).OrderBy(issue => issue.Status).ToList();
        }

        public List<Issue> GetIssuesInProgressForUser(long idUser, Board board)
        {
            return board.GetIssuesInProgressUser(idUser);
        }

        public List<Issue> GetIssuesDoneForUser(long idUser, Board board)
        {
            return board.GetIssuesDoneForUser(idUser);
        }

        public List<Issue> GetIssuesCompletedForUser(long idUser, Board board)
        {
            return board.GetIssuesCompletedForUser(idUser);
        }

        public List<Issue> GetIssuesReviewForUser(long idUser, Board board)
        {
            return board.GetIssuesReviewForUser(idUser);
        }

        public List<Issue> GetIssuesFreeInBoard(long idUser, Board board)
        {
            return board.GetIssuesFreeInBoard(idUser);
        }
    }
}

