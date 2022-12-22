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

        public List <Board> GetAllBoardsByNumbersOfBoard(List<int> boardsForUser)
        {
          return _dataStorage.GetAllBoardsByNumbersOfBoard(boardsForUser);
        }
    }
}
