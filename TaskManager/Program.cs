using TaskManager;
using Telegram.Bot.Types;
using Client = TaskManager.Client;

//Client cl = new Client(1, "1");
//Board boardA = new Board(1, 1);
//Board boardB = new Board(2, 2);
////boardB.IDMembers.Add(1);
//Board boardC = new Board(3, 3);
//Board boardD = new Board(4, 4);


////List<int> baseNumberBoardsForUser = new List<int> { 1, 2 };
//Dictionary<int, Board> baseBoards = new Dictionary<int, Board>
//            {
//                {boardA.NumberBoard, boardA },
//                {boardB.NumberBoard, boardB },
//                {boardC.NumberBoard, boardC },
//                {boardD.NumberBoard, boardD },
//            };
//DataStorage.GetInstance().Boards = baseBoards;
//DataStorage.GetInstance().RewriteFileForBoards();

//DataStorage.GetInstance().Boards = baseBoards;
//client.BoardsForUser = baseNumberBoardsForUser;
//c.GetAllBoardsToWhichYouCanJoin();

//Console.WriteLine("Hy");

//DataStorage storage = DataStorage.GetInstance();

//Client user = new User(1, "qqq");
//user.AddBoard();
//storage.ReturnFromFile();
//Console.WriteLine(storage.Boards[1].NumberBoard);


//DataStorage _dataStorage = DataStorage.GetInstance();

//_dataStorage.Boards[1].AddNewIssue("задача1");
//_dataStorage.Boards[1].AddNewIssue("задача2");
//_dataStorage.Boards[1].AddNewIssue("задача3");
//_dataStorage.Boards[1].AddNewIssue("задача4");

TelegramService client = new TelegramService();
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



