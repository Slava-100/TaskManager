using TaskManager;

//Console.WriteLine("Hy");

//DataStorage storage = DataStorage.GetInstance();

//Client user = new User(1, "qqq");
//user.AddBoard();
//storage.ReturnFromFile();
//Console.WriteLine(storage.Boards[1].NumberBoard);


TelegramService client = TelegramService.GetInstance();
DataStorage dataStorage = DataStorage.GetInstance();
dataStorage.ReturnFromFile();
Console.ReadLine();

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

Console.WriteLine();



