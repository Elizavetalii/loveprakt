using System.Text;

namespace Будущий_10
{
    public class HR_manager : ICRUD
    {
        public static List<Employee> employees = new();
        public static string filePath = "Employees.json";
        public static int objectPos;
        static Employee objectFromPos;
        public HR_manager()
        {
            List<Employee> newemployees = General.MyDeserialize<Employee>(filePath);
            if (newemployees != null && newemployees.Count > 0)
            {
                employees = newemployees;
            }

            PrintList();

            int pos = Arrows.Show(employees.Count);
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
                var Employees = new Employee();

            }
            else if (pos == (int)Keys.F2)
            {
                Find();
                var Employees = new Employee();
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
                employees.Add(objectFromPos);
                General.MySerialize(employees, filePath);
            }
            var HR = new HR_manager();
        }
        public void Read(int pos)
        {
            objectPos = pos;
            objectFromPos = employees[pos];
            var MenuEmployee = PrintObject(Visual.MenuB);
        }
        public void Update()
        {
            if (Edit() == (int)Keys.S)
            {
                employees[objectPos].FirstName = objectFromPos.FirstName;
                employees[objectPos].LastName = objectFromPos.LastName;
                employees[objectPos].MiddleName = objectFromPos.MiddleName;
                employees[objectPos].DateOfBirth = objectFromPos.DateOfBirth;
                employees[objectPos].ID = objectFromPos.ID;
                employees[objectPos].PassportNumber = objectFromPos.PassportNumber;
                employees[objectPos].Position = objectFromPos.Position;
                employees[objectPos].Salary = objectFromPos.Salary;
                employees[objectPos].UserID = objectFromPos.UserID;

                General.MySerialize(employees, filePath);
            }
        }
        public void Delete()
        {
            employees.Remove(objectFromPos);
            General.MySerialize(employees, filePath);
            var Employees = new Employee();
        }
        public void Find()
        {
            while (true)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuAd = EmployeeMenu();

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
                    column = "LastName";
                    Console.WriteLine("Введите Фамилию: ");
                    errorText = "Такоой Фамилии не существует";
                }
                else if (pos == 2)
                {
                    column = "FirstName";
                    Console.WriteLine("Введите Имя: ");
                    errorText = "Такого Имени не существует";
                }
                else if (pos == 3)
                {
                    column = "MiddleName";
                    Console.WriteLine("Введите Отчество: ");
                    errorText = "Такого Отчества не существует";
                }
                else if (pos == 4)
                {
                    column = "Role";
                    Console.WriteLine("Введите Должность: ");
                    errorText = "Такой Должности не существует";
                }
                else if (pos == 5)
                {
                    column = "DateOfBirth";
                    Console.WriteLine("Введите Дату рождения: ");
                    errorText = "Такой Даты не существует";
                }
                else if (pos == 6)
                {
                    column = "PassportNumber";
                    Console.WriteLine("Введите Серию/номер пасспорта: ");
                    errorText = "Такой Серии/номера пасспорта не существует";
                }
                else if (pos == 7)
                {
                    column = "Salary";
                    Console.WriteLine("Введите Зарплату: ");
                    errorText = "Такой Зарплаты не существует";
                }
                else if (pos == 8)
                {
                    column = "UserID";
                    Console.WriteLine("Введите ID пользователя: ");
                    errorText = "Такого ID пользователя не существует";
                }
                searchtext = Console.ReadLine();
                List<Employee> FindEmployee = General.SearchList(employees, column, searchtext);
                if (FindEmployee.Count == 0)
                {
                    Console.WriteLine(errorText);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
                    "  ID",
                    "Фамилия",
                    "Имя",
                    "Отчество",
                    "Должность");
                    foreach (Employee rowemploee in FindEmployee)
                    {
                        Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
                            "  " + rowemploee.ID.ToString(),
                            rowemploee.LastName,
                            rowemploee.FirstName,
                            rowemploee.MiddleName,
                            rowemploee.Position);

                    }
                    ConsoleKeyInfo key = Console.ReadKey(true);
                }
                break;
            }
        }       
        public int Edit()
        {
            var MenuEmployee = PrintObject(Visual.MenuA);
            var NullMenuEmployee = new Employee().ToString();

            int pos = Arrows.Show(MenuEmployee.Length);
            int lineCursor = pos + Arrows.startLine;

            while (true)
            {

                if (pos == (int)Keys.Escape || pos == (int)Keys.S)
                {
                    return pos;
                }
                else if (pos == 0)
                {

                    ClearEditString(NullMenuEmployee, pos, lineCursor);

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
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());
                    }
                }
                else if (pos == 1)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);

                    string inputFirstName = Console.ReadLine();
                    try
                    {
                        objectFromPos.FirstName = inputFirstName;
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для Имени необходимо вводить только буквы!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputFirstName.Length).ToString());


                    }
                }
                else if (pos == 2)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);

                    string inputLastName = Console.ReadLine();
                    try
                    {
                        objectFromPos.LastName = inputLastName;
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для фамилии необходимо вводить только буквы!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputLastName.Length).ToString());


                    }

                }
                else if (pos == 3)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);

                    string inputMiddleName = Console.ReadLine();
                    try
                    {
                        objectFromPos.MiddleName = inputMiddleName;
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для отчества необходимо вводить только буквы!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputMiddleName.Length).ToString());


                    }

                }
                else if (pos == 4)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);
                    string inputDateOfBirth = Console.ReadLine();
                    try
                    {

                        objectFromPos.DateOfBirth = Convert.ToDateTime(inputDateOfBirth);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для даты рождения необходимо вводить только цифры!");

                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputDateOfBirth.ToString().Length).ToString());


                    }

                }
                else if (pos == 5)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);

                    string inputPassportNumber = Console.ReadLine();
                    try
                    {
                        objectFromPos.PassportNumber = inputPassportNumber;
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для Сериии/номера пасспорта необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputPassportNumber.Length).ToString());
                    }
                }
                else if (pos == 6)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);
                    objectFromPos.Position = Console.ReadLine();
                }
                else if (pos == 7)
                {
                    ClearEditString(NullMenuEmployee, pos, lineCursor);
                    string inputSalary = Console.ReadLine();
                    try
                    {
                        objectFromPos.Salary = double.Parse(inputSalary);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для зарплаты необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputSalary.Length).ToString());

                    }

                }
                else if (pos == 8)
                {

                    ClearEditString(NullMenuEmployee, pos, lineCursor);

                    string inputUserID = Console.ReadLine();
                    try
                    {
                        objectFromPos.UserID = int.Parse(inputUserID);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для ID необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuEmployee[pos] + new StringBuilder().Insert(0, " ", inputUserID.Length).ToString());
                    }
                }

                pos = Arrows.Show(MenuEmployee.Length);
            }

        }      
        public void ClearEditString(string[] NullMenuEmployee, int pos, int lineCursor)
        {
            Console.SetCursorPosition(NullMenuEmployee[pos].Length, lineCursor + pos);
            Console.WriteLine(" ", 20);
            Console.SetCursorPosition(NullMenuEmployee[pos].Length, lineCursor + pos);
        }
        public void PrintList()
        {
            int indexTable = 0;

            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
            "  ID",
            "Фамилия",
            "Имя",
            "Отчество",
            "Должность",
            "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
            indexTable++;

            //Строчки таблицы

            foreach (Employee rowemploee in employees)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
                    "  " + rowemploee.ID.ToString(),
                    rowemploee.LastName,
                    rowemploee.FirstName,
                    rowemploee.MiddleName,
                    rowemploee.Position,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
                indexTable++;
            }
        }
        public string[] PrintObject(string[] VisualMenu)
        {
            var MenuEmployee = objectFromPos.ToString();

            Program.WelcomePrint();
            Console.WriteLine("");
            for (int i = 0; i < MenuEmployee.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", MenuEmployee[i], "|" + Visual.getlineMenu(VisualMenu, i));
            }

            for (int i = MenuEmployee.Length; i <= VisualMenu.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(VisualMenu, i));
            }

            return MenuEmployee;
        }

        public static string[] EmployeeMenu()
        {
            string[] MenuEmployee = { "  ID" , "  Имя","  Фамилия", "  Отчество","  Должность",
            "  Дата рождения","  Серия/номер пасспорта", "  Зарплата", "  ID пользователя"};

            return MenuEmployee;
        }
        public class Employee
        {
            public int ID { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string PassportNumber { get; set; }
            public string Position { get; set; }
            public double Salary { get; set; }
            public int UserID { get; set; }

            public Employee(int id = 999, string lastName = "", string firstName = "", string middleName = "",
            DateTime dateOfBirth = new DateTime(), string passportNumber = "", string position = "", double salary = 0.0, int userId = 99)
            {
                ID = id;
                LastName = lastName;
                FirstName = firstName;
                MiddleName = middleName;
                DateOfBirth = dateOfBirth;
                PassportNumber = passportNumber;
                Position = position;
                Salary = salary;
                UserID = userId;
            }
         
            public string[] ToString()
            {
                var strEmployeeMenu = EmployeeMenu();

                string idline = "";
                if (ID != 999)
                {
                    idline += ID;
                }               
                string salaryline = "";
                if (Salary != 0.0)
                {
                    salaryline += Salary;
                }
                string userIdline = "";
                if (UserID != 99)
                {
                    userIdline += UserID;
                }
                strEmployeeMenu[0] = $"{strEmployeeMenu[0]}: {idline}";
                strEmployeeMenu[1] = $"{strEmployeeMenu[1]}: {FirstName}";
                strEmployeeMenu[2] = $"{strEmployeeMenu[2]}: {LastName}";
                strEmployeeMenu[3] = $"{strEmployeeMenu[3]}: {MiddleName}";
                strEmployeeMenu[4] = $"{strEmployeeMenu[4]}: {DateOfBirth}";
                strEmployeeMenu[5] = $"{strEmployeeMenu[5]}: {PassportNumber}";
                strEmployeeMenu[6] = $"{strEmployeeMenu[6]}: {Position}";
                strEmployeeMenu[7] = $"{strEmployeeMenu[7]}: {salaryline}";
                strEmployeeMenu[8] = $"{strEmployeeMenu[8]}: {userIdline}";
                return strEmployeeMenu;
            }
        }
    }
}
