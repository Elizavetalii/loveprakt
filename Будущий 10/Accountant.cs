using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Будущий_10.Administrator;

namespace Будущий_10
{
    public class Accountant 
    {
        public static List<Transaction> transactions = new();
        public static string filePath = "Transaction.json";

        public static void WarehouseManagering()
        {
            int indexTable = 0;
            ConsoleKeyInfo key;

            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine(
                "{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-10}",
                    "  ID",
                    "Название",
                    "Сумма",
                    "Дата",
                    "Прибавка",
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
            );
            indexTable++;

            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20} {5,-10}",
                    "  " + transaction.ID.ToString(),
                    transaction.Name,
                    transaction.Amount,
                    transaction.Date,
                    transaction.IsIncome,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
                    );
            }
            int pos = Arrows.Show(transactions.Count);

            if (pos >= 0)
            {
                

            }
            if (pos == -11)
            {
                Product.Create();
            }
        }
    }
    public class Transaction
    {
        public List<Transaction> transactions;

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
            string idline = "  ID: ";
            if (ID != 999)
            {
                idline += ID;
            }

            string nameline = $"  Название: {Name}";

            string amountline = "  Сумма: ";
            if (Amount != 0.0)
            {
                amountline += Amount;
            }

            string dateline = $"  Дата: {Date}";

            string IsIncomeline = $"  Прибавка: ";
            if (IsIncome != false)
            {
                IsIncomeline += IsIncome;
            }
            string[] result = { idline, nameline, amountline, dateline, IsIncomeline};
            return result;
        }
        public static void Reade()
        {
            ConsoleKeyInfo key;
            int pos = Arrows.Show(Accountant.transactions.Count);
            Console.Clear();
            Program.WelcomePrint();
            
            int indexObject = 0;
            Transaction currentTransaction = Accountant.transactions[pos];
            var MenuNewTransaction = currentTransaction.ToString();
            foreach (string item in MenuNewTransaction)
            {
                //Console.WriteLine(item);
                Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                indexObject++;

            }
            return;
        }
        public static void Create()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Program.WelcomePrint();

            //Печать меню объекта и меню операций над ним
            int indexObject = 0;
            Transaction NEWTransaction = new();
            var MenuNewTransaction = NEWTransaction.ToString();
            Console.WriteLine("");
            foreach (string item in MenuNewTransaction)
            {
                Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                indexObject++;
            }
            int pos = Arrows.Show(MenuNewTransaction.Length);
            int lineCursor = pos + Arrows.startLine;
            while (pos != -15 || pos != Arrows.startLine - 1)
            {
                if (pos == 0)
                {
                    Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);
                    string inputID = Console.ReadLine();
                    try
                    {
                        NEWTransaction.ID = int.Parse(inputID);
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewTransaction[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для ID необходимо вводить только цифры!");
                    }
                }
                else if (pos == 1)
                {
                    Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);
                    string inputName = Console.ReadLine();
                    NEWTransaction.Name = inputName;
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
                    Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);
                    double inputAmount = Convert.ToInt32(Console.ReadLine());
                    NEWTransaction.Amount = inputAmount;

                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);
                    int inputDate = Convert.ToInt32(Console.ReadLine());
                    try
                    {

                        NEWTransaction.Date = Convert.ToDateTime(inputDate);
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewTransaction[pos] + new StringBuilder().Insert(0, " ", inputDate.ToString().Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для даты необходимо вводить только цифры!");
                    }
                }
                else if (pos == 4)
                {
                    Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);
                    bool inputIsIncome = Convert.ToBoolean(Console.ReadLine());
                    try
                    {

                        NEWTransaction.IsIncome = inputIsIncome;
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewTransaction[pos] + new StringBuilder().Insert(0, " ", inputIsIncome.ToString().Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для определения прибавки необходимо ввести только True/False!");
                    }
                }
            }
            Console.SetCursorPosition(MenuNewTransaction[pos].Length, lineCursor);

        }

        public static List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public static string FilePath { get; set; } = "transactions.json";

        private void ShowAllTransactions()
        {
            Console.WriteLine("Записи о денежных транзакциях:");

            if (Transactions.Count == 0)
            {
                Console.WriteLine("Нет данных.");
            }
            else
            {
                foreach (var transaction in Transactions)
                {
                    string[] Menu1 =
                    {
                        "F1- Добавить запись",
                        "F2- Найти запись",
                    };

                    Console.WriteLine(
                    "\n{0,-55} {1,10} {2,25} {3,20}",
                                ("ID", 55),
                                "Описание",
                                "Сумма",
                                "Дата",
                                "Прибавка" + "|"
                                );
                    Console.WriteLine(
                   "\n{0,-55} {1,10} {2,25} {3,20}",
                               ($"{transaction.ID}"),
                            ($"{transaction.Name}"),
                            ($"{transaction.Amount}"),
                            ($"{transaction.Date}"),
                            ($"{transaction.IsIncome}") + "|",
                            Menu1);

                    Console.WriteLine($"Сумма: {transaction.Amount}");

                }                
            }           
        }
    }
}
