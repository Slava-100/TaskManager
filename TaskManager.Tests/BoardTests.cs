using FluentAssertions;
using FluentAssertions.Equivalency;
using TaskManager;
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
            _board = new Board(1, 0);
        }

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetNextNumberIssueTestSource))]
        public void GetNextNumberIssueTest(Board board, int expectedNumberIssue)
        {
            int actualNumberIssue = board.GetNextNumberIssue();
            Assert.AreEqual(expectedNumberIssue, actualNumberIssue);
        }

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.AddBlokingAndBlockedByIssueTestSource))]
        public void AddBlokingAndBlockedByIssueTest(Issue blockedByCurrentIssue, Issue blockingCurrentIssue, List<int> expectedBlockedByCurrentIssue, List<int> expectedBlockingIssues)
        {
            Board board = new Board(1, 1);
            board.Issues.Add(blockedByCurrentIssue);
            board.Issues.Add(blockingCurrentIssue);
            board.AddBlokingAndBlockedByIssue(blockedByCurrentIssue.NumberIssue, blockingCurrentIssue.NumberIssue);

            var actualBlockedByCurrentIssue = blockingCurrentIssue.BlockedByCurrentIssue;
            var actualBlockingIssues = blockedByCurrentIssue.BlockingIssues;

            CollectionAssert.AreEqual(expectedBlockedByCurrentIssue, actualBlockedByCurrentIssue);
            CollectionAssert.AreEqual(expectedBlockingIssues, actualBlockingIssues);
        }

        [TestCaseSource(typeof(TestCaseForAddNewIssueTest))]
        public void AddNewIssueTest(List<Issue> issues, string description, bool exceptionResult)
        {
            _board.Issues.AddRange(issues);
            bool actualResult = _board.AddNewIssue(description);

            Assert.AreEqual(exceptionResult, actualResult);
        }

        [TestCaseSource(typeof(TestCaseForRemoveIssueTest))]
        public void RemoveIssueTest(List<Issue> issues, int numberIssues, bool exceptionResult)
        {
            _board.Issues.AddRange(issues);
            bool actualResult = _board.RemoveIssue(numberIssues); ;

            Assert.AreEqual(exceptionResult, actualResult);
        }

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetAllIssuesInBoardTestCaseSource))]
        public void GetAllIssuesInBoardTest(Board baseBoard, long idUser, List<Issue> expectedIssues)
        {
            List<Issue> actualIssues = baseBoard.GetAllIssuesInBoard(idUser);

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetIssuesInProgressInBoardTestCaseSource))]
        public void GetIssuesInProgressInBoardTest(Board baseBoard, long idUser, List<Issue> expectedIssues)
        {
            List<Issue> actualIssues = baseBoard.GetIssuesInProgressInBoard(idUser);

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }
    }
}


