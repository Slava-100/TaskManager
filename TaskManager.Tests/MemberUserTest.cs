using FluentAssertions;
using System.Text.Json;
using TaskManager.Tests.TestCaseSource;

namespace TaskManager.Tests
{
    public class MemberUserTest
    {
        [TestCaseSource(typeof(MemberUserTestCaseSource), nameof(MemberUserTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(List<Issue> baseIssues, Board board, Issue attachIssue, long IDUser, List<Issue> expectedIssues)
        {
            DataStorage.GetInstance().PathFileForClient = @"C:\Users\Кристина\Desktop\MakeUPro\Коды\Tests\AdminUserTest.txt";
            string path = DataStorage.GetInstance().PathFileForClient;
            DataStorage.GetInstance().Boards = new Dictionary<int, Board> { { board.NumberBoard, board } };
            board.IDMember.Add(IDUser);
            attachIssue.IdUser = IDUser;
            using (StreamWriter sw = new StreamWriter(path))
            {
                string jsn = JsonSerializer.Serialize(baseIssues);
                sw.WriteLine(jsn);
            }
            AdminUser adminUser = new AdminUser();
            adminUser.AttachIssueToClient(board, attachIssue, IDUser);
            List<Issue> actualIssues = new List<Issue>();
            using (StreamReader sr = new StreamReader(path))
            {
                string jsn = sr.ReadLine();
                actualIssues = JsonSerializer.Deserialize<List<Issue>>(jsn);
            }

            actualIssues.Should().BeEquivalentTo(expectedIssues);
        }
    }
}
