using static TaskManager.Tests.TestCases.BoardTestCaseSource;

namespace TaskManager.Tests
{
    public class BoardTests
    {
        private Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = new Board(1, "@test");
        }

        [TestCaseSource(typeof(TestCaseForAddNewIssueTest))]
        public void AddNewIssueTest(List<Issue> issues, string description, bool exceptionResult, int exceptionNumberNewIssue)
        {
            _board.Issues.AddRange(issues);
            _board.AddNewIssue(description);
            bool actualResult = _board.Issues.Exists(issue => issue.Description == description);
            int actualNumberNewIssue = _board.Issues[_board.Issues.Count - 1].NumberIssue;

            Assert.AreEqual(exceptionResult, actualResult);
            Assert.AreEqual(exceptionNumberNewIssue, actualNumberNewIssue);
        }

        [TestCaseSource(typeof(TestCaseForRemoveIssueTest))]
        public void RemoveIssueTest(List<Issue> issues, int numberIssues, bool exceptionResult)
        {
            _board.Issues.AddRange(issues);
            _board.RemoveIssue(numberIssues);
            bool actualResult = _board.Issues.Exists(issues => issues.NumberIssue == numberIssues);

            Assert.AreEqual(exceptionResult, actualResult);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(3)]
        public void RemoveIssueTest_WhenNumberIssueIncorrect_ShouldThrowArgumentOutOfRangeException(int numberIssue)
        {
            List<Issue> newIssue = new List<Issue>()
            {
                new Issue(1,"QQQ"),
                new Issue(2,"WWW")
            };
            _board.Issues.AddRange(newIssue);

            Assert.Throws<ArgumentOutOfRangeException>(() => _board.RemoveIssue(numberIssue));
        }
    }
}

