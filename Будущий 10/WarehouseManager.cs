using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Будущий_10;
using static Будущий_10.Administrator;

namespace Будущий_10
{
    public class WarehouseManager
    {
        public static List<Product> products = new();
        public static string filePath = "Product.json";

        public static void WarehouseManagering()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            int indexTable = 0;
            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine(
                "{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "  ID",
                    "Название",
                    "Цена",
                    "Количество",
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
            );
            indexTable++;

            foreach (Product rowproduct in products)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "  " + rowproduct.ID.ToString(),
                    rowproduct.Name,
                    rowproduct.Price,
                    rowproduct.Quantity,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable)
                    );
            }
            int pos = Arrows.Show(users.Count);

            while (pos >= 0)
            {
                Console.Clear();
                Program.WelcomePrint();

                int indexObject = 0;
                Product currentProduct = products[pos];
                var MenuNewProduct = currentProduct.ToString();
                foreach (string item in MenuNewProduct)
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
                Product.Create();
            }
            if (Arrows.key.Key == ConsoleKey.F2)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuDd =
                {
                    "  ID" , "  Название","  Цена", "  Количество"
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
                    column = "Name";
                    Console.WriteLine("Введите Название: ");

                }
                else if (pos == 2)
                {
                    column = "Price";
                    Console.WriteLine("Введите Цену: ");

                }
                else if (pos == 3)
                {
                    column = "Quantity";
                    Console.WriteLine("Введите Количество: ");

                }               
                searchtext = Console.ReadLine();
            }
        }
    }






    public class Product : IDataRepository<Product>
    {
        public List<Product> products;
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id = 999, string name = "", double price = 0.0, int quantity = 0)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public string[] ToString()
        {
            string idline = "  ID: ";
            if (ID != 999)
            {
                idline += ID;
            }
            string nameline = $"  Название: {Name}";
            string priceline = "  Цена: ";
            if (Price != 0.0)
            {
                priceline += Price;
            }
            string quantityline = "  Количество: ";
            if (Quantity != 0)
            {
                quantityline += Quantity;
            }


            string[] result = { idline, nameline, priceline, quantityline };
            return result;
        }

        public Product Read(int id)
        {
            return products.Find(u => u.ID == id);
        }

        public void Deserialize()
        {
            products = General.MyDeserialize<List<Product>>("products.json") ?? new List<Product>();
        }

        public void ShowAllProducts()
        {
            Console.WriteLine("Список товаров на складе:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ID}, Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
            }
            Console.WriteLine();
        }

        public static void Create()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Program.WelcomePrint();

            //Печать меню объекта и меню операций над ним
            int indexObject = 0;
            Product NEWProduct = new();
            var MenuNewProduct = NEWProduct.ToString();
            Console.WriteLine("");
            foreach (string item in MenuNewProduct)
            {
                Console.WriteLine("{0,-60}{1,-10}", item, "|" + Visual.getlineMenu(Visual.MenuA, indexObject));
                indexObject++;
            }
            int pos = Arrows.Show(MenuNewProduct.Length);
            int lineCursor = pos + Arrows.startLine;
            while (pos != -15 || pos != Arrows.startLine - 1)
            {
                if (pos == 0)
                {
                    Console.SetCursorPosition(MenuNewProduct[pos].Length, lineCursor);
                    string inputID = Console.ReadLine();
                    try
                    {
                        NEWProduct.ID = int.Parse(inputID);
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewProduct[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для ID необходимо вводить только цифры!");
                    }
                }
                else if (pos == 1)
                {
                    Console.SetCursorPosition(MenuNewProduct[pos].Length, lineCursor);
                    string inputName = Console.ReadLine();
                    NEWProduct.Name = inputName;
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
                    Console.SetCursorPosition(MenuNewProduct[pos].Length, lineCursor);
                    double inputPrice = Convert.ToInt32(Console.ReadLine());
                    NEWProduct.Price = inputPrice;

                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(MenuNewProduct[pos].Length, lineCursor);
                    int inputQuantity = Convert.ToInt32(Console.ReadLine());
                    try
                    {

                        NEWProduct.Quantity = inputQuantity;
                    }
                    catch
                    {
                        Console.SetCursorPosition(2, lineCursor);
                        Console.WriteLine(MenuNewProduct[pos] + new StringBuilder().Insert(0, " ", inputQuantity.ToString().Length).ToString());

                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для количестве необходимо вводить только цифры!");
                    }
                }
            }
            Console.SetCursorPosition(MenuNewProduct[pos].Length, lineCursor);

        }
        void IDataRepository<Product>.Create(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Console.WriteLine("Введите название товара, который хотите изменить:");
            string productName = Console.ReadLine();
            Product product = products.Find(p => p.Name == productName);

            if (product != null)
            {
                Console.WriteLine("Введите новую информацию о товаре:");
                Console.Write("ID: ");
                int ID = int.Parse(Console.ReadLine());
                Console.Write("Название: ");
                string name = Console.ReadLine();
                Console.Write("Цена: ");
                double price = double.Parse(Console.ReadLine());
                Console.Write("Количество: ");
                int quantity = int.Parse(Console.ReadLine());
                product.ID = ID;
                product.Name = name;
                product.Price = price;
                product.Quantity = quantity;

                Console.WriteLine("Информация о товаре успешно обновлена.");
            }
            else
            {
                Console.WriteLine("Товар с таким названием не найден на складе.");
            }
            Console.WriteLine();
        }
        void IDataRepository<Product>.Update(Product item)
        {
            throw new NotImplementedException();
        }

        void IDataRepository<Product>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            Console.WriteLine("Введите название товара, который хотите удалить:");
            string productName = Console.ReadLine();
            Product product = products.Find(p => p.Name == productName);

            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine("Товар успешно удален со склада.");
            }
            else
            {
                Console.WriteLine("Товар с таким названием не найден на складе.");
            }
            Console.WriteLine();
        }
        public List<Product> GetAll()
        {
            return products;
        }

        public void SaveChanges()
        {
            General.MySerialize(products, "products.json");
            Console.WriteLine("Изменения сохранены.");
            Console.WriteLine();
        }
    }
}

