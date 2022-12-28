using TaskManager;
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

        public void MoveIssueFromInProgressToBacklog(Board board, int IdIssue, long iDUser)
        {
            var issue = board.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);
            if (issue.IdUser == iDUser)
            {
                issue.IdUser = 0;
                issue.Status = IssueStatus.Backlog;
                _dataStorage.RewriteFileForBoards();
            }
        }

        public void MoveIssueFromInProgressToReview(Board board, int IdIssue, long iDUser)
        {
            var issue = board.Issues.FirstOrDefault(currentIssue => IdIssue == currentIssue.NumberIssue);
            if (issue.IdUser == iDUser)
            {
                issue.Status = IssueStatus.Review;
                _dataStorage.RewriteFileForBoards();
            }
        }

        public List<Issue> GetAllIssuesInBoardByIdUser(long idUser, Board board)
        {
            return board.GetAllIssuesForClientInBoard(idUser).OrderBy(issue => issue.Status).ToList();
        }

        public List<Issue> GetIssuesInProfressForClientInBoard(long idUser, Board board)
        {
            return board.GetIssuesInProfressForClientInBoard(idUser);
        }
              

        public List<Issue> GetIssuesDoneInBoardByIdUser(long idUser, Board board)
        {
            return board.GetIssuesDoneForClientInBoard(idUser);
        }

        public List<Issue> GetIssuesReviewAndDoneInBoard(long idUser, Board board)
        {
            return board.GetIssuesReviewAndDoneForClientInBoard(idUser);
        }
        

        public List<Issue> GetIssuesReviewInBoard(long idUser, Board board)
        {
            return board.GetIssuesReviewForClientInBoard(idUser);
        }

        public List<Issue> GetIssuesFreeInBoardByIdUser(long idUser, Board board)
        {
            return board.GetIssuesFreeForClientInBoard(idUser);
        }
    }
}

