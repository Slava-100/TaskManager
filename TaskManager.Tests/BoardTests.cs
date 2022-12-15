using System.Text.Json;

namespace TaskManager.Tests
{
    public class BoardTests
    {
        //[TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetNextNumberIssueTestSource))]
        //public void GetNextNumberIssueTest(Board board, int expectedNumberIssue)
        //{
        //    int actualNumberIssue = board.GetNextNumberIssue();
        //    Assert.AreEqual(expectedNumberIssue, actualNumberIssue);
        //}

        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.AddBlokingAndBlockedByIssueTestSource))]
        public void AddBlokingAndBlockedByIssueTest(Issue blockedByCurrentIssue, Issue blockingCurrentIssue, List<int> expectedBlockedByCurrentIssue, List<int> expectedBlockingIssues)
        {
            Board board = new Board(1, "1");
            board.Issues.Add(blockedByCurrentIssue);
            board.Issues.Add(blockingCurrentIssue);
            board.AddBlokingAndBlockedByIssue(blockedByCurrentIssue.NumberIssue, blockingCurrentIssue.NumberIssue);

            var actualBlockedByCurrentIssue = blockingCurrentIssue.BlockedByCurrentIssue;
            var actualBlockingIssues = blockedByCurrentIssue.BlockingIssues;

            CollectionAssert.AreEqual(expectedBlockedByCurrentIssue, actualBlockedByCurrentIssue);
            CollectionAssert.AreEqual(expectedBlockingIssues, actualBlockingIssues);
        }
        //[TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetNextNumberIssueTestSource))]
        //public void GetNextNumberIssueTest(Board board, int expectedNumberIssue)
        //{
        //    int actualNumberIssue = board.GetNextNumberIssue();
        //    Assert.AreEqual(expectedNumberIssue, actualNumberIssue);
        //}
    }
}

