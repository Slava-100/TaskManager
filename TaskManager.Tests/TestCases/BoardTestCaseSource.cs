using System.Collections;

namespace TaskManager.Tests.TestCases
{
    public static class BoardTestCaseSource
    {
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
                bool exceptionResult = false;

                yield return new object[] { issues, numberIssue, exceptionResult };
            }
        }
    }
}
