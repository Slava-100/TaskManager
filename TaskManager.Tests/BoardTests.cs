using TaskManager.Tests.TestCases;
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

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetNextNumberIssueTestSource))]
        public void GetNextNumberIssueTest(Board board, int expectedNumberIssue)
        {
            int actualNumberIssue = board.GetNextNumberIssue();
            Assert.AreEqual(expectedNumberIssue, actualNumberIssue);
        }

        [TestCaseSource(typeof(TestCaseForAddNewIssueTest))]
        public void AddNewIssueTest(List<Issue> issues, string description, bool exceptionResult, int exceptionNumberNewIssue)
        {
            _board.Issues.AddRange(issues);
            int actualNumberNewIssue = _board.AddNewIssue(description);
            bool actualResult = _board.Issues.Exists(issue => issue.Description == description);

            Assert.AreEqual(exceptionResult, actualResult);
            Assert.AreEqual(exceptionNumberNewIssue, actualNumberNewIssue);
        }

        [TestCaseSource(typeof(TestCaseForRemoveIssueTest))]
        public void RemoveIssueTest(List<Issue> issues, int numberIssues, bool exceptionResult)
        {
            _board.Issues.AddRange(issues);
            bool actualResult = _board.RemoveIssue(numberIssues); ;

            Assert.AreEqual(exceptionResult, actualResult);
        }
    }
}

