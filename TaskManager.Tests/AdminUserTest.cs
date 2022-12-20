using System.Text.Json;
using TaskManager;
using TaskManager.Tests.TestCaseSource;
using FluentAssertions;


namespace TaskManager.Tests
{
    public class AdminUserTest
    {
        [TestCaseSource(typeof(AdminUserTestCaseSource), nameof(AdminUserTestCaseSource.AttachIssueToClientTestCaseSource))]
        public void AttachIssueToClientTest(List<Issue> baseIssues, Board board, Issue attachIssue, long IDUser, List<Issue> expectedIssues)
        {
            string path = @".\PathFileForClientTest.txt";
            //DataStorage.GetInstance().PathFileForClient = path;
            //DataStorage.GetInstance().Boards = new Dictionary<int, Board> { { board.NumberBoard, board } };
            //board.IDAdmin.Add(IDUser);
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

        [TearDown]
        public void Teardown()
        {
            File.Delete(@"C:\Users\Кристина\Desktop\MakeUPro\Коды\Tests\AdminUserTest.txt");
        }
    }
}

