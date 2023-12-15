using System;
using System.Data.Common;
using System.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static Будущий_10.Administrator;

namespace Будущий_10
{

    public static class Administrator
    {

        public static List<User> users = new()
        {
            new User(0, "1", "1", 0)
        };
        public static string filePath = "Users.json";
        
        public static void Administratoring()
        {
            int indexTable = 0;
            ConsoleKeyInfo key;

            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine(
                "{0,-7} {1,-10} {2,-15} {3,-45} {4,-20}",
                    "  ID",
                    "Логин",
                    "Пароль",
                    "Роль",
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
                    );
            indexTable++;

            //Строчки таблицы
            foreach (User rowuser in users)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-45} {4,-20}",
                    "  " + rowuser.ID.ToString(),
                    rowuser.Login,
                    rowuser.Password,
                    rowuser.Role,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
                    );
            }
            int pos = Arrows.Show(users.Count);

            

            if (pos >= 0)
            {
                User.Reade();
                return;

            }
            if (pos == -11)
            {              
                User.Create();
            }
            if (Arrows.key.Key == ConsoleKey.F2)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");
                
                string[] MenuAd =
                {
                    "  ID" ,"  Логин", "  Пароль", "  Роль"
                };

                string column = "";
                string searchtext = "";
                foreach (string cmd in MenuAd)
                {
                    Console.WriteLine(cmd);
                }
                pos = Arrows.Show(MenuAd.Length);
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
                var FindEmployee = General.Search(HR_manager.employees, column, searchtext);
                if (FindEmployee == null)
                {
                    Console.WriteLine(errorText);
                    Thread.Sleep(500);
                }
            }         
        }


        public class User : IDataRepository<User>
        {
            private List<User> users;

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
            public string[] ToString()
            {
                string idline = "  ID: ";
                if (ID != 999)
                {
                    idline += ID;
                }
                string loginline = $"  Логин: {Login}";
                string passwordline = $"  Пароль: {Password}";
                string roleline = "  Роль: ";
                if (Role != Roles.HOH)
                {
                    roleline += Role;
                }
                string[] result = { idline, loginline, passwordline, roleline };
                return result;
            }

            public static void printMenuB(int indexObject, string[] MenuB)
            {

                for (int i = indexObject; i <= MenuB.Length; i++)
                {
                    Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(MenuB, i));
                }
            }

            public static void printMenuA(int indexObject, string[] MenuA)
            {

                for (int i = indexObject; i <= MenuA.Length; i++)
                {
                    Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(MenuA, i));
                }
            }
            public static void Reade()
            {
                ConsoleKeyInfo key;
                int pos = Arrows.Show(Administrator.users.Count);
                Console.Clear();
                Program.WelcomePrint();

                int indexObject = 0;
                User currentUser = Administrator.users[pos];
                var MenuNewUser = currentUser.ToString();
                foreach (string item in MenuNewUser)
                {
                    //Console.WriteLine(item);
                    Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuB, indexObject));
                    indexObject++;
                }
                printMenuB(indexObject, Visual.MenuB);
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Delete)
                {
                    var usertodelet = Administrator.users;

                }
                if (key.Key == ConsoleKey.F10)
                {
                }
            }
            public static void Create()
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Program.WelcomePrint();

                //Печать меню объекта и меню операций над ним
                int indexObject = 0;
                User NEWUser = new();
                var MenuNewUser = NEWUser.ToString();
                Console.WriteLine("");
                foreach (string item in MenuNewUser)
                {                    
                    Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                    indexObject++;
                }
                printMenuA(indexObject, Visual.MenuA);
                //for (int i = indexObject; i == Visual.MenuA.Length; i++)
                //{
                //    Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                //}

                int pos = Arrows.Show(MenuNewUser.Length);
                int lineCursor = pos + Arrows.startLine;
                while (pos != -15 || pos != Arrows.startLine - 1)
                {
                    if (pos == 0)
                    {
                        Console.SetCursorPosition(MenuNewUser[pos].Length, lineCursor);
                        string inputID = Console.ReadLine();
                        try
                        {
                            NEWUser.ID = int.Parse(inputID);
                        }
                        catch
                        {
                            Console.SetCursorPosition(2, lineCursor);
                            Console.WriteLine(MenuNewUser[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());

                            Console.SetCursorPosition(10, 10);
                            Console.WriteLine("Для ID необходимо вводить только цифры!");
                        }
                    }
                    else if (pos == 1)
                    {
                        Console.SetCursorPosition(MenuNewUser[pos].Length, lineCursor);
                        string inputLogin = Console.ReadLine();
                        NEWUser.Login = inputLogin;
                        int i = 11;
                        key = Console.ReadKey(true);
                        while (key.Key != ConsoleKey.Enter)
                        {
                            if (key.Key == ConsoleKey.Backspace)
                            {
                                if (i > 11)
                                {
                                    Console.Write("\b ");
                                    i--;
                                    Console.SetCursorPosition(i, 3);
                                }
                            }
                            key = Console.ReadKey(true);

                        }
                    }

                    else if (pos == 2)
                    {
                        Console.SetCursorPosition(MenuNewUser[pos].Length, lineCursor);
                        string inputPassword = Console.ReadLine();
                        NEWUser.Password = inputPassword;

                    }
                    else if (pos == 3)
                    {
                        Console.SetCursorPosition(MenuNewUser[pos].Length, lineCursor);
                        
                        try
                        {
                            int inputRole = Convert.ToInt32(Console.ReadLine());
                            NEWUser.Role = (Roles)inputRole;                           
                        }
                        catch
                        {
                            int inputRole = Convert.ToInt32(Console.ReadLine());
                            Console.SetCursorPosition(2, lineCursor);
                            Console.WriteLine(MenuNewUser[pos] + new StringBuilder().Insert(0, " ", inputRole.ToString().Length).ToString());

                            Console.SetCursorPosition(10, 10);
                            Console.WriteLine("Для роли необходимо вводить только цифры!");
                        }
                        Console.SetCursorPosition(MenuNewUser[pos].Length, lineCursor);             
                    }
                    pos = Arrows.Show(2);
                }

            }

            public User Read(int id)
            {
                return users.Find(u => u.ID == id);
            }

            public void Update(User updatedUser, string filePath)
            {
                User user = Read(updatedUser.ID);
                if (user != null)
                {
                    user.Login = updatedUser.Login;
                    user.Password = updatedUser.Password;
                    General.MySerialize(users, filePath);
                }
            }

            public void Delete(int id, string filePath)
            {
                User ID = Read(id);
                if (ID != null)
                {
                    users.Remove(ID);
                    General.MySerialize(ID, filePath);
                }

            }

            public List<User> GetAll()
            {
                return users;
            }

            private int GetNextId()
            {
                if (users.Count > 0)
                {
                    return users[users.Count - 1].ID + 1;
                }
                else
                {
                    return 1;
                }
            }

            public void Save()
            {

            }

            void IDataRepository<User>.Create(User item)
            {
                throw new NotImplementedException();
            }

            void IDataRepository<User>.Update(User item)
            {
                throw new NotImplementedException();
            }

            void IDataRepository<User>.Delete(int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}

