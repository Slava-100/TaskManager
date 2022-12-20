using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class MemberUserTestCaseSource
    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue1 = new Issue();
            Issue issue2 = new Issue(20, "20");
            Issue attachIssue = new Issue(30, "30");
            List<Issue> baseIssues = new List<Issue> { issue1, issue2, attachIssue };
            Board board = new Board(50, 50);
            long IDUser = 50;
            List<Issue> expectedIssues = new List<Issue> { issue1, issue2, attachIssue };

            yield return new Object[] { baseIssues, board, attachIssue, IDUser, expectedIssues };

            attachIssue = new Issue(80, "80");
            baseIssues = new List<Issue> { attachIssue };
            expectedIssues = new List<Issue> { attachIssue };

            yield return new Object[] { baseIssues, board, attachIssue, IDUser, expectedIssues };
        }
    }
}
