using TaskManager;
using Telegram.Bot.Types;

Console.WriteLine("Hy");


//TelegramService client = new TelegramService();
//Console.ReadLine();

TaskManager.User admin = new TaskManager.User("adminID", "admin");
TaskManager.User member = new TaskManager.User("memberID", "member");

int boardAdmin = admin.AddBoard();
int boardMember = member.AddBoard();

DataStorage dataStorage = DataStorage.GetInstance();
Board boardA = dataStorage.Boards[boardAdmin];

admin.SelectRole(boardA);
admin.RemoveBoard(boardAdmin);

Board boardM = dataStorage.Boards[boardMember];
boardM.IDMembers.Add(admin.IDUser);

admin.SelectRole(boardM);
admin.RemoveBoard(boardMember);



TaskManager.User user = new TaskManager.User("3", "3");
user.AddNewUserByKey(boardMember, 0);

Console.WriteLine();

