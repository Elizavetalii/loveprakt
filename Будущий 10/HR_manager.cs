using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Будущий_10.Administrator;

namespace Будущий_10
{
    public class HR_manager 
    {
        public static List<Employee> employees = new();
        public static string filePath = "Employees.json";

        
        public static void HR_managering()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            int indexTable = 0;
            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine(
                "{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
                    "  ID",
                    "Фамилия",
                    "Имя",
                    "Отчество",
                    "Должность",
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
            );
            indexTable++;

            foreach (Employee rowemploee in employees)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-15}",
                    "  " + rowemploee.ID.ToString(),
                    rowemploee.LastName,
                    rowemploee.FirstName,
                    rowemploee.MiddleName,
                    rowemploee.Position,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
                    );
            }
            int pos = Arrows.Show(users.Count);

            while (pos >= 0)
            {
                Console.Clear();
                Program.WelcomePrint();

                int indexObject = 0;
                Employee currentEmployee = employees[pos];
                var MenuNewEmployee = currentEmployee.ToString();
                foreach (string item in MenuNewEmployee)
                {
                    //Console.WriteLine(item);
                    Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                    indexObject++;

                }
                return;

                if (key.Key == ConsoleKey.Delete)
                {

                    var usertodelet = users;



                }

            }
            if (pos == -11)
            {
                Employee.Create();
            }
            if (Arrows.key.Key == ConsoleKey.F2)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuDd =
                {
                    "  ID" , "  Имя","  Фамилия", "  Отчество","  Должность", "  Дата рождения","  Серия/номер пасспорта", "  Зарплата", "  ID пользователя"
                };

                string column = "";
                string searchtext = "";
                foreach (string cmd in MenuDd)
                {
                    Console.WriteLine(cmd);
                }
                pos = Arrows.Show(MenuDd.Length);
                int lineCursor = Arrows.startLine + MenuDd.Length + 2;
                Console.SetCursorPosition(0, lineCursor);
                if (pos == 0)
                {
                    column = "ID";
                    Console.WriteLine("Введите ID: ");

                }
                else if (pos == 1)
                {
                    column = "LastName";
                    Console.WriteLine("Введите Фамилию: ");

                }
                else if (pos == 2)
                {
                    column = "FirstName";
                    Console.WriteLine("Введите Имя: ");

                }
                else if (pos == 3)
                {
                    column = "MiddleName";
                    Console.WriteLine("Введите Отчество: ");

                }
                else if (pos == 4)
                {
                    column = "Role";
                    Console.WriteLine("Введите Должность: ");

                }
                else if (pos == 5)
                {
                    column = "DateOfBirth";
                    Console.WriteLine("Введите Дату рождения: ");

                }
                else if (pos == 6)
                {
                    column = "PassportNumber";
                    Console.WriteLine("Введите Серию/номер пасспорта: ");

                }
                else if (pos == 7)
                {
                    column = "Salary";
                    Console.WriteLine("Введите Зарплату: ");

                }
                else if (pos == 8)
                {
                    column = "UserID";
                    Console.WriteLine("Введите ID пользователя: ");
                }

                searchtext = Console.ReadLine();
            }
        }



    }
    public class Employee : IDataRepository<Employee>
    {
        private List<Employee> employees;       
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
            string idline = "  ID: ";
            if (ID != 999)
            {
                idline += ID;
            }
            string firstName = $"  Имя: {FirstName}";
            string lastName = $"  Фамилия: {LastName}";
            string middleName = $"  Отчество: {MiddleName}";    
            string dateOfBirth = $"  Дата рождения: {DateOfBirth}";
            string passportNumber = $"  Серия/номер паспорта: {PassportNumber}";
            string position = $"  Должность: {Position}";
            string salaryline = "Зарплата: ";
            if (Salary != 0.0)
            {
                salaryline += Salary;
            }
            string userIdline = "ID: ";
            if (UserID != 99)
            {
                userIdline += UserID;
            }

            string[] result = { idline, lastName, firstName, middleName, dateOfBirth, passportNumber, position , salaryline, userIdline};
            return result;
        }

        //public Employee(string filePath)
        //{
        //    dataFilePath = filePath;
        //    LoadData();
        //}

        public static void Create()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Program.WelcomePrint();

            //Печать меню объекта и меню операций над ним
            int indexObject = 0;
            Employee NEWEmployee = new();
            var MenuNewEmployee = NEWEmployee.ToString();
            Console.WriteLine("");
            foreach (string item in MenuNewEmployee)
            {
                Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                indexObject++;
            }

            int pos = Arrows.Show(MenuNewEmployee.Length);
            int lineCursor = pos + Arrows.startLine;
            while (pos != -15 || pos != Arrows.startLine - 1)
            {
                if (pos == 0)
                {
                    Console.SetCursorPosition(MenuNewEmployee[pos].Length, lineCursor);
                    string inputFirstName = Console.ReadLine();
                    try
                    {
                        NEWEmployee.FirstName = inputFirstName;
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewEmployee[pos] + new StringBuilder().Insert(0, " ", inputFirstName.Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для Имени необходимо вводить только буквы!");
                    }
                }
                else if (pos == 1)
                {
                    Console.SetCursorPosition(MenuNewEmployee[pos].Length, lineCursor);
                    string inputLastName = Console.ReadLine();
                    NEWEmployee.LastName = inputLastName;
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
                    Console.SetCursorPosition(MenuNewEmployee[pos].Length, lineCursor);
                    string inputMiddleName = Console.ReadLine();
                    NEWEmployee.MiddleName = inputMiddleName;

                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(MenuNewEmployee[pos].Length, lineCursor);
                    string inputDateOfBirth = Console.ReadLine();
                    try
                    {

                        NEWEmployee.DateOfBirth = Convert.ToDateTime(inputDateOfBirth);
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewEmployee[pos] + new StringBuilder().Insert(0, " ", inputDateOfBirth.ToString().Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для даты рождения необходимо вводить только цифры!");
                    }
                    Console.SetCursorPosition(MenuNewEmployee[pos].Length, lineCursor);
                    //string inputRole = Console.ReadLine();
                    //NEWUser.Role = int.Parse(inputRole);
                    //NEWUser.Role = (inputRole)Enum.Parse(typeof(Roles));

                }

                pos = Arrows.Show(2);
            }
        }

        public Employee Read(int id)
        {
            return employees.Find(e => e.ID == id);
        }

        public void Update(Employee updatedEmployee)
        {
            Employee employee = Read(updatedEmployee.ID);
            if (employee != null)
            {
                employee.LastName = updatedEmployee.LastName;
                employee.Position = updatedEmployee.Position;
                // Другие поля...
                
            }
        }

        public void Delete(int id)
        {
            Employee employee = Read(id);
            if (employee != null)
            {
                employees.Remove(employee);
                
            }
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        private int GetNextId()
        {
            if (employees.Count > 0)
            {
                return employees[employees.Count - 1].ID + 1;
            }
            else
            {
                return 1;
            }
        }
        void IDataRepository<Employee>.Create(Employee item)
        {
            throw new NotImplementedException();
        }

        //private void LoadData()
        //{
        //    if (File.Exists(dataFilePath))
        //    {
        //        string jsonData = File.ReadAllText(dataFilePath);
        //        employees = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
        //    }
        //    else
        //    {
        //        employees = new List<Employee>();
        //    }
        //}

        //private void SaveData()
        //{
        //    string jsonData = JsonConvert.SerializeObject(employees);
        //    File.WriteAllText(dataFilePath, jsonData);
        //}
    }
}
