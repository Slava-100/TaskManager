using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public static class AdminUserTestCaseSource
    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue1 = new Issue();
            Issue issue2 = new Issue(2, "2");
            Issue attachIssue = new Issue(3, "3");
            List<Issue> baseIssues = new List<Issue> { issue1, issue2, attachIssue };
            Board board = new Board(10, 10);
            long IDUser = 100;
            List<Issue> expectedIssues = new List<Issue> { issue1, issue2, attachIssue };

            yield return new Object[] { baseIssues, board, attachIssue, IDUser, expectedIssues };

            attachIssue = new Issue(8, "8");
            baseIssues = new List<Issue> { attachIssue };
            expectedIssues = new List<Issue> { attachIssue };

            yield return new Object[] { baseIssues, board, attachIssue, IDUser, expectedIssues };
        }
    }
}

//(List<Issue> baseIssues, Board board, Issue attachIssue, long IDUser, List<Issue> expectedIssues)