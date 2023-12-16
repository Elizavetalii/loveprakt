using System.Text;

namespace Будущий_10
{

    public class Administrator : ICRUD
    {
        public static List<User> users = new() {new User(0, "1", "1", 0)};
        public static string filePath = "Users.json";
        public static int objectPos;
        static User objectFromPos;       
        public Administrator()
        {
            List<User> newusers = General.MyDeserialize<User>(filePath);
            if (newusers != null && newusers.Count > 0)
            {
                users = newusers;
            }

            PrintList();
            
            int pos = Arrows.Show(users.Count);

            if (pos == (int)Keys.F1)
            {
                Create();
            }
            else if (pos > -1)
            {
                Read(pos);

                ConsoleKeyInfo key = Console.ReadKey(true);

                while (true)
                {
                    if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.F10 || key.Key == ConsoleKey.Delete)
                    {
                        break;
                    }
                };

                if (key.Key == ConsoleKey.F10)
                {
                    Update();

                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    Delete();
                }
                var Admin = new Administrator();

            }
            else if (pos == (int)Keys.F2)
            {
                Find();
                var Admin = new Administrator();
            }
            else if (pos == (int)Keys.Escape)
            {
                return;
            }
        }
   
        public void Create()
        {
            objectFromPos = new();

            if (Edit() == (int)Keys.S)
            {
                users.Add(objectFromPos);
                General.MySerialize(users, filePath);
            }
            var Admin = new Administrator();
        }

        public void Read(int pos)
        {
            objectPos = pos;
            objectFromPos = users[pos];
            var MenuUser = PrintObject(Visual.MenuB);
        }

        public void Update()
        {
            if (Edit() == (int)Keys.S)
            {
                users[objectPos].ID = objectFromPos.ID;
                users[objectPos].Login = objectFromPos.Login;
                users[objectPos].Password = objectFromPos.Password;
                users[objectPos].Role = objectFromPos.Role;

                General.MySerialize(users, filePath);
            }
        }

        public void Delete()
        {
            users.Remove(objectFromPos);
            General.MySerialize(users, filePath);
            var Admin = new Administrator();
        }

        public void Find()
        {
            while (true)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuAd = UserMenu();

                string column = "";
                string searchtext = "";
                foreach (string cmd in MenuAd)
                {
                    Console.WriteLine(cmd);
                }
                int pos = Arrows.Show(MenuAd.Length);
                int lineCursor = Arrows.startLine + MenuAd.Length + 2;
                Console.SetCursorPosition(0, lineCursor);
                string errorText = "";
                if (pos == 0)
                {
                    column = "ID";
                    Console.WriteLine("Введите ID: ");
                    errorText = "Такого ID не существует";
                }
                else if (pos == 1)
                {
                    column = "Login";
                    Console.WriteLine("Введите Логин: ");
                    errorText = "Такого Логина не существует";

                }
                else if (pos == 2)
                {
                    column = "Password";
                    Console.WriteLine("Введите Пароль: ");
                    errorText = "Такого Пароля не существует";

                }
                else if (pos == 3)
                {
                    column = "Role";
                    Console.WriteLine("Введите Роль: ");
                    errorText = "Такой Роли не существует";
                }
                searchtext = Console.ReadLine();
                List<User> FindUser = General.SearchList(users, column, searchtext);
                if (FindUser.Count == 0)
                {
                    Console.WriteLine(errorText);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-45}",
                    "  ID",
                    "Логин",
                    "Пароль",
                    "Роль");
                    foreach (var user in FindUser)
                    {
                        Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-45}",
                        user.ID,
                        user.Login,
                        user.Password,
                        user.Role);
                    }
                    ConsoleKeyInfo key = Console.ReadKey(true);
                }
                break;
            }
        }

        public int Edit()
        {
            var MenuUser = PrintObject(Visual.MenuA);
            var NullMenuUser = new User().ToString();

            int pos = Arrows.Show(MenuUser.Length);
            int lineCursor = pos + Arrows.startLine;
            while (true)
            {
                if (pos == (int)Keys.Escape || pos == (int)Keys.S)
                { 
                    return pos;
                }
                else if (pos == 0)
                {

                    ClearEditString(NullMenuUser, pos, lineCursor);

                    string inputID = Console.ReadLine();
                    try
                    {
                        objectFromPos.ID = int.Parse(inputID);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для ID необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuUser[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());
                    }
                }
                else if (pos == 1)
                {
                    ClearEditString(NullMenuUser, pos, lineCursor);
                    objectFromPos.Login = Console.ReadLine();
                }
                else if (pos == 2)
                {
                    ClearEditString(NullMenuUser, pos, lineCursor);
                    objectFromPos.Password = Console.ReadLine();
                }
                else if (pos == 3)
                {
                    ClearEditString(NullMenuUser, pos, lineCursor);
                    string inputRole = Console.ReadLine();

                    try
                    {
                        int iinputRole = Convert.ToInt32(inputRole);
                        objectFromPos.Role = (Roles)iinputRole;
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для роли необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuUser[pos] + new StringBuilder().Insert(0, " ", inputRole.Length).ToString());
                    }
                    
                }
                pos = Arrows.Show(MenuUser.Length);
            }

        }
        public void ClearEditString(string[] NullMenuUser, int pos, int lineCursor)
        {
            Console.SetCursorPosition(NullMenuUser[pos].Length, lineCursor + pos);
            Console.WriteLine(" ", 20);
            Console.SetCursorPosition(NullMenuUser[pos].Length, lineCursor + pos);
        }
        public void PrintList()
        {
            int indexTable = 0;

            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-45} {4,-20}",
            "  ID",
            "Логин",
            "Пароль",
            "Роль",
            "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
            indexTable++;

            //Строчки таблицы
            foreach (User rowuser in users)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-45} {4,-20}",
                    "  " + rowuser.ID.ToString(),
                    rowuser.Login,
                    rowuser.Password,
                    rowuser.Role,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
                indexTable++;
            }
        }
        public string[] PrintObject(string[] VisualMenu)
        {
            var MenuUser = objectFromPos.ToString();

            Program.WelcomePrint();
            Console.WriteLine("");
            for (int i = 0; i < MenuUser.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", MenuUser[i], "|" + Visual.getlineMenu(VisualMenu, i));
            }

            for (int i = MenuUser.Length; i <= VisualMenu.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(VisualMenu, i));
            }

            return MenuUser;
        }

        public static string[] UserMenu()
        {
            string[] MenuUser = { "  ID", "  Логин", "  Пароль", "  Роль" };
            return MenuUser;
        }

        public class User
        {
            public int ID { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public Roles Role { get; set; }

            public User(int id = 999, string login = "", string password = "", Roles role = Roles.HOH)
            {
                ID = id;
                Login = login;
                Password = password;
                Role = role;
            }

            public new string[] ToString()
            {
                var strUserMenu = UserMenu();

                string idline = "";
                if (ID != 999)
                {
                    idline += ID;
                }
                string roleline = "";
                if (Role != Roles.HOH)
                {
                    roleline += Role;
                }
                strUserMenu[0] = $"{strUserMenu[0]}: {idline}";
                strUserMenu[1] = $"{strUserMenu[1]}: {Login}";
                strUserMenu[2] = $"{strUserMenu[2]}: {Password}";
                strUserMenu[3] = $"{strUserMenu[3]}: {roleline}";

                return strUserMenu;
            }
        }
    }
}
