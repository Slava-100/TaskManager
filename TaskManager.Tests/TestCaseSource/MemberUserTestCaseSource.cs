using System.Collections;

namespace TaskManager.Tests.TestCaseSource
{
    public class MemberUserTestCaseSource
    {
        public static IEnumerable AttachIssueToClientTestCaseSource()
        {
            Issue issue10 = new Issue(10, "10");
            Issue attachIssue = new Issue(20, "20");
            Client admin = new Client(50, "50");
            Board board = new Board(100, 50);
            board.Issues.Add(issue10);
            board.Issues.Add(attachIssue);
            Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
            {
                { board.NumberBoard, board}
            };
            Client client = new Client(40, "40");
            Dictionary<long, Client> baseClients = new Dictionary<long, Client>
            {
                { 50, admin },
                { 40, client}
            };

            Issue expIssue10 = new Issue(10, "10");
            Issue expIttachIssue = new Issue(20, "20");
            Board expBoard = new Board(100, 50);
            Client expAdmin = new Client(50, "50");
            Client expClient = new Client(40, "40");
            expIttachIssue.IdUser = expClient.IDUser;
            expBoard.Issues.Add(expIssue10);
            expBoard.Issues.Add(expIttachIssue);
            Dictionary<int, Board> expectedBoards = new Dictionary<int, Board>
            {
                { expBoard.NumberBoard, expBoard}
            };
            long idUser = expClient.IDUser;
            Dictionary<long, Client> expectedClients = new Dictionary<long, Client>
             {
                 { 50, expAdmin },
                { 40, expClient}
            };

            yield return new object[] { baseBoards, baseClients, board, attachIssue, idUser, expectedBoards, expectedClients };
        }

        public static IEnumerable GetAllIssuesInBoardByIdUserTestCaseSource()
        {
            Client admin = new Client(709, "709");
            Client member = new Client(88, "88");
            Issue issue1 = new Issue(19, "19");
            issue1.IdUser = member.IDUser;
            Issue issue2 = new Issue(29, "29");
            issue2.IdUser = member.IDUser;
            Board board = new Board(709, 709);
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            board.IDMembers.Add(88);
            long idUser = 88;

            Client expAdmin = new Client(709, "709");
            Client expMember = new Client(88, "88");
            Issue expIssue1 = new Issue(19, "19");
            expIssue1.IdUser = expMember.IDUser;
            Issue expIssue2 = new Issue(29, "29");
            expIssue2.IdUser = expMember.IDUser;
            Board expBoard = new Board(709, 709);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.IDMembers.Add(88);
            List<Issue> expectedIssues = new List<Issue> { expIssue1, expIssue2 };

            yield return new Object[] { idUser, board, expectedIssues };
        }

        public static IEnumerable GetIssuesInProgressInBoardByIdUserTestCaseSource()
        {
            Client admin = new Client(709, "709");
            Client member = new Client(88, "88");
            Issue issue1 = new Issue(19, "19");
            issue1.IdUser = member.IDUser;
            issue1.IdUser = member.IDUser;
            issue1.Status = Enums.IssueStatus.Backlog;
            Issue issue2 = new Issue(29, "29");
            issue2.IdUser = member.IDUser;
            issue2.Status = Enums.IssueStatus.InProgress;
            Board board = new Board(709, 709);
            board.Issues.Add(issue1);
            board.Issues.Add(issue2);
            board.IDMembers.Add(88);
            long idUser = 88;

            Client expAdmin = new Client(709, "709");
            Client expMember = new Client(88, "88");
            Issue expIssue1 = new Issue(19, "19");
            expIssue1.IdUser = expMember.IDUser;
            expIssue1.Status = Enums.IssueStatus.Backlog;
            Issue expIssue2 = new Issue(29, "29");
            expIssue2.IdUser = expMember.IDUser;
            expIssue2.Status = Enums.IssueStatus.InProgress;
            Board expBoard = new Board(709, 709);
            expBoard.Issues.Add(expIssue1);
            expBoard.Issues.Add(expIssue2);
            expBoard.IDMembers.Add(88);
            List<Issue> expectedIssues = new List<Issue> {  expIssue2 };

            yield return new Object[] { idUser, board, expectedIssues };
        }
    }
}
