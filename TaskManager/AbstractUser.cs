using TaskManager;
using TaskManager.Enums;

namespace TaskManager
{
    public abstract class AbstractUser
    {
        protected DataStorage _dataStorage = DataStorage.GetInstance();

        public void AttachIssueToClient(Board board, Issue issue, long IDUser)
        {
            issue.IdUser = IDUser;
            issue.Status = IssueStatus.InProgress;
            _dataStorage.RewriteFileForBoards();
            _dataStorage.RewriteFileForClients();
        }

        public List<Issue> GetAllIssuesInBoardByIdUser(long idUser, Board board)
        {
            return board.GetAllIssuesInBoard(idUser).OrderBy(issue =>issue.Status).ToList();
        }

        public List<Issue> GetIssuesDoneInBoardByIdUser(long idUser, Board board)
        {
            return board.GetIssuesDoneInBoard(idUser);
        }

        public List<Issue> GetIssuesFreeInBoardByIdUser(long idUser, Board board)
        {
            return board.GetIssuesFreeInBoard(idUser);
        }
    }
}

