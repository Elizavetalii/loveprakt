using System.Text;

namespace Будущий_10
{
    public class Accountant : ICRUD
    {
        public static List<Transaction> transactions = new();
        public static string filePath = "Transaction.json";
        public static int objectPos;
        static Transaction objectFromPos;

        public Accountant()
        {
            List<Transaction> newtransactions = General.MyDeserialize<Transaction>(filePath);
            if (newtransactions != null && newtransactions.Count > 0)
            {
                transactions = newtransactions;
            }
            PrintList();

            int pos = Arrows.Show(transactions.Count);

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
                var Account = new Accountant();

            }
            else if (pos == (int)Keys.F2)
            {
                Find();
                var Account = new Accountant();
            }
            else if (pos == (int)Keys.Escape)
            {
                return;
            }
        }
        public void PrintList()
        {
            int indexTable = 0;
            Program.WelcomePrint();
            Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-10}",
                "  ID",
                "Название",
                "Сумма",
                "Дата",
                "Прибавка",
                "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
            indexTable++;

            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-10}",
                    "  " + transaction.ID.ToString(),
                    transaction.Name,
                    transaction.Amount,
                    transaction.Date,
                    transaction.IsIncome,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
                indexTable++;
            }
        }
        public void Update()
        {
            if (Edit() == (int)Keys.S)
            {
                transactions[objectPos].ID = objectFromPos.ID;
                transactions[objectPos].Name = objectFromPos.Name;
                transactions[objectPos].Amount = objectFromPos.Amount;
                transactions[objectPos].Date = objectFromPos.Date;
                transactions[objectPos].IsIncome = objectFromPos.IsIncome;

                General.MySerialize(transactions, filePath);
            }
        }
        public void Create()
        {
            objectFromPos = new();

            if (Edit() == (int)Keys.S)
            {
                transactions.Add(objectFromPos);
                General.MySerialize(transactions, filePath);
            }
            var Account = new Accountant();
        }
        public void Delete()
        {
            transactions.Remove(objectFromPos);
            General.MySerialize(transactions, filePath);
            var Account = new Accountant();
        }

        public int Edit()
        {
            var MenuTransaction = PrintObject(Visual.MenuA);
            var NullMenuTransaction = new Transaction().ToString();

            int pos = Arrows.Show(MenuTransaction.Length);
            int lineCursor = pos + Arrows.startLine;

            while (true)
            {
                if (pos == (int)Keys.Escape || pos == (int)Keys.S)
                {
                    return pos;
                }
                else if (pos == 0)
                {
                    ClearEditString(NullMenuTransaction, pos, lineCursor);
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
                        Console.WriteLine(NullMenuTransaction[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());

                    }
                }
                else if (pos == 1)
                {
                    ClearEditString(NullMenuTransaction, pos, lineCursor);
                    objectFromPos.Name = Console.ReadLine();

                }

                else if (pos == 2)
                {
                    ClearEditString(NullMenuTransaction, pos, lineCursor);
                    string inputAmount = Console.ReadLine();                 
                    try
                    {
                        objectFromPos.Amount = double.Parse(inputAmount);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для суммы необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuTransaction[pos] + new StringBuilder().Insert(0, " ", inputAmount.Length).ToString());
                    }

                }
                else if (pos == 3)
                {

                    ClearEditString(NullMenuTransaction, pos, lineCursor);
                    string inputDate = Console.ReadLine();


                    try
                    {
                        objectFromPos.Date = Convert.ToDateTime(inputDate);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для даты необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuTransaction[pos] + new StringBuilder().Insert(0, " ", inputDate.Length).ToString());

                    }
                }
                else if (pos == 4)
                {
                    ClearEditString(NullMenuTransaction, pos, lineCursor);
                    string inputIsIncome = Console.ReadLine();

                    try
                    {

                        objectFromPos.IsIncome = Convert.ToBoolean(inputIsIncome);
                    }
                    catch
                    {

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для определения прибавки необходимо ввести только True/False!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuTransaction[pos] + new StringBuilder().Insert(0, " ", inputIsIncome.Length).ToString());
                    }
                }
                pos = Arrows.Show(MenuTransaction.Length);
            }
        }
        public void ClearEditString(string[] NullMenuTransaction, int pos, int lineCursor)
        {
            Console.SetCursorPosition(NullMenuTransaction[pos].Length, lineCursor + pos);
            Console.WriteLine(" ", 20);
            Console.SetCursorPosition(NullMenuTransaction[pos].Length, lineCursor + pos);
        }
        public string[] PrintObject(string[] VisualMenu)
        {
            var MenuTransaction = objectFromPos.ToString();

            Program.WelcomePrint();
            Console.WriteLine("");
            for (int i = 0; i < MenuTransaction.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", MenuTransaction[i], "|" + Visual.getlineMenu(VisualMenu, i));
            }

            for (int i = MenuTransaction.Length; i <= VisualMenu.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(VisualMenu, i));
            }

            return MenuTransaction;
        }
        public void Read(int pos)
        {
            objectPos = pos;
            objectFromPos = transactions[pos];
            var MenuTransaction = PrintObject(Visual.MenuB);
        }

        public static string[] TransactionMenu()
        {
            string[] MenuTransaction = { "  ID", "  Название", "  Сумма", "  Дата", "  Прибавка" };
            return MenuTransaction;
        }
        public void Find()
        {
            while (true)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuAd = TransactionMenu();

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
                    column = "Name";
                    Console.WriteLine("Введите Название: ");
                    errorText = "Такого Названия не существует";

                }
                else if (pos == 2)
                {
                    column = "Amount";
                    Console.WriteLine("Введите Сумму: ");
                    errorText = "Такого Суммы не существует";

                }
                else if (pos == 3)
                {
                    column = "Date";
                    Console.WriteLine("Введите Дату: ");
                    errorText = "Такой Даты не существует";
                }
                else if (pos == 4)
                {
                    column = "IsIncome";
                    Console.WriteLine("Введите имеется ли Прибавка: ");
                    errorText = "Такой Прибавки не существует";
                }
                searchtext = Console.ReadLine();
                List<Transaction> FindTransaction = General.SearchList(transactions, column, searchtext);
                if (FindTransaction.Count == 0)
                {
                    Console.WriteLine(errorText);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("\n{0,-55} {1,10} {2,25} {3,20}",
                    "ID",
                    "Описание",
                    "Сумма",
                    "Дата",
                    "Прибавка" + "|");
                    foreach (var transaction in FindTransaction)
                    {
                        Console.WriteLine(
                        "\n{0,-55} {1,10} {2,25} {3,20}",
                               transaction.ID,
                            transaction.Name,
                            transaction.Amount,
                            transaction.Date);


                        ConsoleKeyInfo key = Console.ReadKey(true);
                    }
                    break;
                }
            }
        } 
        public class Transaction
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public double Amount { get; set; }
            public DateTime Date { get; set; }
            public bool IsIncome { get; set; }

            public Transaction(int id = 999, string name = "", double amount = 0.0, DateTime date = new DateTime(), bool isIncome = false)
            {
                ID = id;
                Name = name;
                Amount = amount;
                Date = date;
                IsIncome = isIncome;
            }
            public string[] ToString()
            {
                var strTransactionMenu = TransactionMenu();
                string idline = "  ID: ";
                if (ID != 999)
                {
                    idline += ID;
                }
                string amountline = "";
                if (Amount != 0.0)
                {
                    amountline += Amount;
                }

                string IsIncomeline = $"";
                if (IsIncome != false)
                {
                    IsIncomeline += IsIncome;
                }
                strTransactionMenu[0] = $"{strTransactionMenu[0]}: {idline}";
                strTransactionMenu[1] = $"{strTransactionMenu[1]}: {Name}";
                strTransactionMenu[2] = $"{strTransactionMenu[2]}: {amountline}";
                strTransactionMenu[3] = $"{strTransactionMenu[3]}: {Date}";
                strTransactionMenu[4] = $"{strTransactionMenu[4]}: {IsIncomeline}";
                return strTransactionMenu;
            }           
        }
    }
}
