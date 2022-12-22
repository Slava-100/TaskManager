using TaskManager;

namespace TaskManager
{
    public abstract class AbstractUser
    {
        protected DataStorage _dataStorage = DataStorage.GetInstance();

        public void AttachIssueToClient(Board board, Issue issue, long IDUser)
        {
            issue.IdUser = IDUser;
            _dataStorage.RewriteFileForBoards();
            _dataStorage.RewriteFileForClients();
        }

        public List<Issue> GetAllIssuesInBoardByBoard(long idUser, Board board)
        {
            return board.GetAllIssuesInBoard(idUser);
        }

        public List<Issue> GetIssuesInProgressInBoardByBoard(long idUser, Board board)
        {
            return board.GetIssuesInProgressInBoard(idUser);
        }
    }
}

