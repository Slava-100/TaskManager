using System.Collections;

namespace TaskManager.Tests.TestCases
{
    public static class BoardTestCaseSource
    {

        public static IEnumerable GetNextNumberIssueTestSource()
        {
            Issue issue1 = new Issue(1, "1");
            Issue issue2 = new Issue(2, "2");
            Board board = new Board(10, 10);
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            int expectedNumberIssue = 3;

            yield return new Object[] { board, expectedNumberIssue };

            board = new Board(11, 11);
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

                yield return new object[] { issues, description, exceptionResult };

                issues = new List<Issue>();
                description = "YYY";
                exceptionResult = true;

                yield return new object[] { issues, description, exceptionResult };
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

        public static IEnumerable GetAllIssuesInBoardTestCaseSource()
        {
            //1. Получаем все задания доски для Админа

            Client client = new Client(10, "10");
            Issue issue1 = new Issue(1, "1");
            issue1.IdUser = client.IDUser;
            Issue issue2 = new Issue(2, "2");
            issue2.IdUser = client.IDUser;
            long idUser = client.IDUser;
            Board baseBoard = new Board(100, 10);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);

            Client expClient = new Client(10, "10");
            Issue expIssue1 = new Issue(1, "1");
            expIssue1.IdUser = client.IDUser;
            Issue expIssue2 = new Issue(2, "2");
            expIssue2.IdUser = client.IDUser;
            List<Issue> expectedIssues = new List<Issue> { issue1, issue2 };

            yield return new Object[] { baseBoard, idUser, expectedIssues };

            //2. Получаем все задания доски для Мембера

            Client admin = new Client(1, "102");
            Client memeber = new Client(2, "2");
            issue1 = new Issue(12, "12");
            issue1.IdUser = memeber.IDUser;
            issue2 = new Issue(22, "22");
            issue2.IdUser = memeber.IDUser;
            idUser = memeber.IDUser;
            baseBoard = new Board(1002, 102);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.IDMembers.Add(2);

            Client expAdmin = new Client(1, "102");
            Client expMemeber = new Client(2, "2");
            expIssue1 = new Issue(12, "12");
            expIssue1.IdUser = expMemeber.IDUser;
            expIssue2 = new Issue(22, "22");
            expIssue2.IdUser = expMemeber.IDUser;
            expectedIssues = new List<Issue> { issue1, issue2 };
            Board expBoard = new Board(1002, 102);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.IDMembers.Add(2);

            yield return new Object[] { baseBoard, idUser, expectedIssues };

            //3. Проверяем, что должен прийти пустой лист, если обращается не Админ, и не Мембер

            client = new Client(103, "103");
            issue1 = new Issue(13, "13");
            issue1.IdUser = client.IDUser;
            issue2 = new Issue(23, "23");
            issue2.IdUser = client.IDUser;
            idUser = client.IDUser;
            baseBoard = new Board(1003, 10);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);

            expClient = new Client(103, "103");
            expIssue1 = new Issue(13, "13");
            expIssue1.IdUser = client.IDUser;
            expIssue2 = new Issue(23, "23");
            expIssue2.IdUser = client.IDUser;
            expectedIssues = new List<Issue>();

            yield return new Object[] { baseBoard, idUser, expectedIssues };

            //4. Получаем все задания доски для Админа, при этом в доске есть задания, которые закреплены за другим участником

            client = new Client(104, "104");
            Client otherClient = new Client(66, "66");
            issue1 = new Issue(14, "14");
            issue1.IdUser = client.IDUser;
            issue2 = new Issue(24, "24");
            issue2.IdUser = client.IDUser;
            Issue issueOther = new Issue(661, "661");
            issueOther.IdUser = otherClient.IDUser;
            idUser = client.IDUser;
            baseBoard = new Board(1004, 104);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issueOther);
            baseBoard.IDMembers.Add(otherClient.IDUser);

            expClient = new Client(104, "104");
            Client expOtherClient = new Client(66, "66");
            expIssue1 = new Issue(14, "14");
            expIssue1.IdUser = client.IDUser;
            expIssue2 = new Issue(24, "24");
            expIssue2.IdUser = client.IDUser;
            Issue expIssueOther = new Issue(661, "661");
            expIssueOther.IdUser = expOtherClient.IDUser;
            expBoard = new Board(1004, 104);
            expBoard.Issues.Add(issue1);
            expBoard.Issues.Add(issue2);
            expBoard.Issues.Add(issueOther);
            expBoard.IDMembers.Add(expOtherClient.IDUser);
            expectedIssues = new List<Issue> { expIssue1, expIssue2 };

            yield return new Object[] { baseBoard, idUser, expectedIssues };

            //5. Получаем все задания доски для Мембера, при этом в доске есть задания, которые закреплены за другим участником

            admin = new Client(50, "50");
            client = new Client(1045, "1045");
            otherClient = new Client(665, "665");
            issue1 = new Issue(145, "145");
            issue1.IdUser = client.IDUser;
            issue2 = new Issue(245, "245");
            issue2.IdUser = client.IDUser;
            issueOther = new Issue(6615, "6615");
            issueOther.IdUser = otherClient.IDUser;
            idUser = client.IDUser;
            baseBoard = new Board(10045, 50);
            baseBoard.Issues.Add(issue1);
            baseBoard.Issues.Add(issue2);
            baseBoard.Issues.Add(issueOther);
            baseBoard.IDMembers.Add(client.IDUser);
            baseBoard.IDMembers.Add(otherClient.IDUser);

            expAdmin = new Client(50, "50");
            expClient = new Client(1045, "1045");
            expOtherClient = new Client(665, "665");
            expIssue1 = new Issue(145, "145");
            expIssue1.IdUser = client.IDUser;
            expIssue2 = new Issue(245, "245");
            expIssue2.IdUser = client.IDUser;
            expIssueOther = new Issue(6615, "6615");
            expIssueOther.IdUser = expOtherClient.IDUser;
            expBoard = new Board(10045, 50);
            expBoard.Issues.Add(issue1);
            expBoard.Issues.Add(issue2);
            expBoard.Issues.Add(issueOther);
            expBoard.IDMembers.Add(expClient.IDUser);
            expBoard.IDMembers.Add(expOtherClient.IDUser);
            expectedIssues = new List<Issue> { expIssue1, expIssue2 };

            yield return new Object[] { baseBoard, idUser, expectedIssues };
        }
    }

}
