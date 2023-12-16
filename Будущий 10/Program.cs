using Будущий_10;


class Program
{
    public static Administrator.User LoginUser = new();
    public static void Main()
    {
        //Console.SetCursorPosition(45, 10);
        //Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В МАГАЗИН 'ЯРУССКИЙ'");
        ////Thread.Sleep(2000);
        while (true)
        {
            string[] mainMenu = { "  Введите логин: ", "  Введите пароль: ", "  Авторизоваться" };
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("{0,65}", "Параметры авторизации");
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.SetCursorPosition(0, Arrows.startLine);
            foreach (string cmd in mainMenu)
            {
                Console.WriteLine(cmd);
            }

            int pos = Arrows.Show(mainMenu.Length);
            while (pos != 2)
            {
                int lineCursor = pos + Arrows.startLine;
                if (pos == 0)
                {
                    Console.SetCursorPosition(mainMenu[pos].Length, lineCursor);
                    LoginUser.Login = Console.ReadLine();
                    
                }
                if (pos == 1)
                {
                    Console.SetCursorPosition(mainMenu[pos].Length, lineCursor);
                    
                    int i = mainMenu[pos].Length;
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    while (key.Key != ConsoleKey.Enter)
                    {
                        if (key.Key != ConsoleKey.Backspace)
                        {
                            LoginUser.Password += key.KeyChar;
                            Console.Write("*");
                            i++;
                            Console.SetCursorPosition(i, lineCursor);
                        }
                        else 
                        {
                            if (i > mainMenu[pos].Length)
                            {

                                Console.Write("\b ");
                                if (LoginUser.Password.Length > 0)
                                {
                                    LoginUser.Password = LoginUser.Password.Remove(LoginUser.Password.Length - 1);
                                }

                                i--;
                                Console.SetCursorPosition(i, lineCursor);

                            }
                         
                        }
                        key = Console.ReadKey(true);

                    };                   
                }
                pos = Arrows.Show(mainMenu.Length);

            }
            while (pos == 2)
            {
                // Найдём пользователя в списке пользователей
                var FindUser = General.Search(Administrator.users, "Login", LoginUser.Login);
                if (FindUser != null && FindUser.Login == "")
                {
                    Console.SetCursorPosition(10, 10);
                    Console.WriteLine("Пользователь с таким логином не найден!");
                    Thread.Sleep(500);
                    break;
                }
                else if(FindUser?.Password != LoginUser.Password)
                {
                    Console.SetCursorPosition(10, 10);
                    Console.WriteLine("Введен неверный пароль!");
                    Thread.Sleep(500);
                    break;
                }
                LoginUser = FindUser;

                // Найдём пользователя в списке сотрудников
                var FindEmployee = General.Search(HR_manager.employees, "UserID", LoginUser.ID.ToString());
                if (FindEmployee != null && FindEmployee.UserID == LoginUser.ID)
                {
                    LoginUser.Login = FindEmployee.FirstName;
                }

                if (LoginUser.Role == Roles.Administrator)
                {
                    var Admin = new Administrator();
                }
                else if (LoginUser.Role == Roles.HR_manager)
                {
                    var Employees = new HR_manager();
                }
                else if(LoginUser.Role == Roles.Accountant)
                {
                    var Account = new Accountant();
                }
                else if(LoginUser.Role == Roles.WarehouseManager)
                {
                    var WarehManager = new WarehouseManager();
                }
                else if(LoginUser.Role == Roles.Cashier)
                {
                    var Cashier = new Cashier();
                }
                LoginUser = new();
                break;
            }
        }
    }

    public static void WelcomePrint()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("{0,50}{1,30}", $"Добро пожаловать, {LoginUser.Login}", $"Роль: {LoginUser.Role}");
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.SetCursorPosition(0, Arrows.startLine - 1);
    }
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Authorization()
        {
            Login = "";
            Password = "";
        }       
    }
}
