namespace TaskManager
{
    public abstract class AbstractUser
    {
        public void AttachIssueToClient(Board board, Issue issue, long IDUser)
        {
            issue.IdUser = IDUser;
        }
    }
}
