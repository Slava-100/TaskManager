using System.Collections;

namespace TaskManager.Tests.TestCases
{
    public static class BoardTestCaseSource
    {

        public static IEnumerable GetNextNumberIssueTestSource()
        {
            Issue issue1 = new Issue(1, "1");
            Issue issue2 = new Issue(2, "2");
            Board board = new Board(10, "10");
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            int expectedNumberIssue = 3;

            yield return new Object[] { board, expectedNumberIssue };

            board = new Board(11, "11");
            expectedNumberIssue = 1;

            yield return new Object[] { board, expectedNumberIssue };
        }

        public static IEnumerable AddBlokingAndBlockedByIssueTestSource()
        {
            Issue blockingCurrentIssue = new Issue(10, "10");
            Issue blockedByCurrentIssue = new Issue(1, "1");
            List<int> expectedBlockedByCurrentIssue = new List<int> { 1 };
            List<int> expectedBlockingIssues = new List<int> { 10 };

            yield return new Object[] { blockedByCurrentIssue, blockingCurrentIssue, expectedBlockedByCurrentIssue, expectedBlockingIssues };

            blockingCurrentIssue = new Issue(8, "8");
            blockingCurrentIssue.BlockedByCurrentIssue = new List<int> { 1 };
            blockedByCurrentIssue = new Issue(3, "3");
            expectedBlockedByCurrentIssue = new List<int> { 1, 3 };
            expectedBlockingIssues = new List<int> { 8 };

            yield return new Object[] { blockedByCurrentIssue, blockingCurrentIssue, expectedBlockedByCurrentIssue, expectedBlockingIssues };

            blockingCurrentIssue = new Issue(12, "12");
            blockedByCurrentIssue = new Issue(4, "4");
            blockedByCurrentIssue.BlockingIssues = new List<int> { 7 };
            expectedBlockedByCurrentIssue = new List<int> { 4 };
            expectedBlockingIssues = new List<int> { 7, 12 };

            yield return new Object[] { blockedByCurrentIssue, blockingCurrentIssue, expectedBlockedByCurrentIssue, expectedBlockingIssues };

            blockingCurrentIssue = new Issue(20, "20");
            blockingCurrentIssue.BlockedByCurrentIssue = new List<int> { 1, 3, 5 };
            blockedByCurrentIssue = new Issue(11, "11");
            blockedByCurrentIssue.BlockingIssues = new List<int> { 30, 19 };
            expectedBlockedByCurrentIssue = new List<int> { 1, 3, 5, 11 };
            expectedBlockingIssues = new List<int> { 30, 19, 20 };

            yield return new Object[] { blockedByCurrentIssue, blockingCurrentIssue, expectedBlockedByCurrentIssue, expectedBlockingIssues };
        }

        public class TestCaseForAddNewIssueTest : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                List<Issue> issues = new List<Issue>()
                {
                    new Issue(1, "1"),
                    new Issue(2, "1"),
                    new Issue(3, "1"),
                    new Issue(4, "1"),
                };
                string description = "QQQ";
                bool exceptionResult = true;
                int exceptionNumberNewIssue = 5;

                yield return new object[] { issues, description, exceptionResult, exceptionNumberNewIssue };

                issues = new List<Issue>();
                description = "YYY";
                exceptionResult = true;
                exceptionNumberNewIssue = 1;

                yield return new object[] { issues, description, exceptionResult, exceptionNumberNewIssue };
            }
        }

        public class TestCaseForRemoveIssueTest : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                List<Issue> issues = new List<Issue>()
                {
                    new Issue(1, "1"),
                    new Issue(2, "1"),
                    new Issue(3, "1"),
                    new Issue(4, "1"),
                };
                int numberIssue = 4;
                bool exceptionResult = true;

                yield return new object[] { issues, numberIssue, exceptionResult };

                numberIssue = 6;
                exceptionResult = false;

                yield return new object[] { issues, numberIssue, exceptionResult };
            }
        }
    }
}
