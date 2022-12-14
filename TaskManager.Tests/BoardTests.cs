using System.Text.Json;

namespace TaskManager.Tests
{
    public class BoardTests
    {
        [TestCaseSource(typeof(BoardTestCaseSource), nameof(BoardTestCaseSource.GetNextNumberIssueTestSource))]
        public void GetNextNumberIssueTest(Board board, int expectedNumberIssue)
        {
            int actualNumberIssue = board.GetNextNumberIssue();
            Assert.AreEqual(expectedNumberIssue, actualNumberIssue);
        }
    }
}

