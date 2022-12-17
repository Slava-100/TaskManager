using TaskManager;

User user = new User("1", "one");

Board board = new(1,"11");

board.IDAdmin.Add(user.IDUser);

//if (user.SelectRole(board))
//{
//    user.AddNewIssue(board,"111");
//    user.AddNewIssue(board,"222");
//    var idIssue = board.Issues.First(issue => issue.Description == "222").NumberIssue;
//    user.RemoveIssue(board, idIssue);
//}

if (user.SelectRole(board))
{
    user.AddNewIssue(board, "333", out int issueId333);
    user.AddNewIssue(board,"444", out int issueId444);
    user.RemoveIssue(board, issueId444);
}

Console.WriteLine();
Console.WriteLine("Hy");