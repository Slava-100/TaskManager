using TaskManager;


User user = new User("1", "one");

Board board = new(1,"11");

board.IDAdmin.Add(user.IDUser);

if (user.SelectRole(board))
{
    user._user.AddNewIssue(board,"2222");
}

Console.WriteLine();
Console.WriteLine("Hy");