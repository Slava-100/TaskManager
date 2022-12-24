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
    }
}

