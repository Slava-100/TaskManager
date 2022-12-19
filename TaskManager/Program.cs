using TaskManager;
using Telegram.Bot.Types;
using User = TaskManager.User;

Console.WriteLine("Hy");


User user = new User(1, "qqq");
user.AddBoard();

//TelegramService client = new TelegramService();
//Console.ReadLine();

//TaskManager.User admin = new TaskManager.User(12, "admin");
//TaskManager.User member = new TaskManager.User(23, "member");

//int boardAdmin = admin.AddBoard();
//int boardMember = member.AddBoard();

//DataStorage dataStorage = DataStorage.GetInstance();
//Board boardA = dataStorage.Boards[boardAdmin];

//admin.SelectRole(boardA);
//admin.RemoveBoard(boardAdmin);

//Board boardM = dataStorage.Boards[boardMember];
//boardM.IDMembers.Add(admin.IDUser);

//admin.SelectRole(boardM);
//admin.RemoveBoard(boardMember);



//TaskManager.User user = new TaskManager.User(3, "3");
//user.AddNewUserByKey(boardMember, 0);

//Console.WriteLine();



