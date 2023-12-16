using System.Text;


namespace Будущий_10
{
    public class WarehouseManager : ICRUD
    {
        public static List<Product> products = new();
        public static string filePath = "Product.json";
        public static int objectPos;
        static Product objectFromPos;
        public WarehouseManager()
        {
            List<Product> newproducts = General.MyDeserialize<Product>(filePath);
            if (newproducts != null && newproducts.Count > 0)
            {
                products = newproducts;
            }
            PrintList();

            int pos = Arrows.Show(products.Count);

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
                var WarehManager = new WarehouseManager();

            }
            else if (pos == (int)Keys.F2)
            {
                Find();
                var WarehManager = new WarehouseManager();
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
                products.Add(objectFromPos);
                General.MySerialize(products, filePath);
            }
            var WarehManager = new WarehouseManager();
        }
        public void Read(int pos)
        {
            objectPos = pos;
            objectFromPos = products[pos];
            var MenuProduct = PrintObject(Visual.MenuB);
        }
        public void Update()
        {
            if (Edit() == (int)Keys.S)
            {
                products[objectPos].ID = objectFromPos.ID;
                products[objectPos].Name = objectFromPos.Name;
                products[objectPos].Price = objectFromPos.Price;
                products[objectPos].Quantity = objectFromPos.Quantity;
                General.MySerialize(products, filePath);
            }
        }
        public void Delete()
        {
            products.Remove(objectFromPos);
            General.MySerialize(products, filePath);
            var WarehManager = new WarehouseManager();
        }
        public void ClearEditString(string[] NullMenuProduct, int pos, int lineCursor)
        {
            Console.SetCursorPosition(NullMenuProduct[pos].Length, lineCursor + pos);
            Console.WriteLine(" ", 20);
            Console.SetCursorPosition(NullMenuProduct[pos].Length, lineCursor + pos);
        }
        public string[] PrintObject(string[] VisualMenu)
        {
            var MenuProduct = objectFromPos.ToString();

            Program.WelcomePrint();
            Console.WriteLine("");
            for (int i = 0; i < MenuProduct.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", MenuProduct[i], "|" + Visual.getlineMenu(VisualMenu, i));
            }

            for (int i = MenuProduct.Length; i <= VisualMenu.Length; i++)
            {
                Console.WriteLine("{0,-60}{1,-10}", "", "|" + Visual.getlineMenu(VisualMenu, i));
            }

            return MenuProduct;
        }
        public int Edit()
        {
            var MenuProduct = PrintObject(Visual.MenuA);
            var NullMenuProduct = new Product().ToString();

            int pos = Arrows.Show(MenuProduct.Length);
            int lineCursor = pos + Arrows.startLine;
            while (true)
            {
                if (pos == (int)Keys.Escape || pos == (int)Keys.S)
                { 
                    return pos;
                }
                else if (pos == 0)
                {

                    ClearEditString(NullMenuProduct, pos, lineCursor);

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
                        Console.WriteLine(NullMenuProduct[pos] + new StringBuilder().Insert(0, " ", inputID.Length).ToString());
                    }
                }
                else if (pos == 1)
                {
                    ClearEditString(NullMenuProduct, pos, lineCursor);
                    objectFromPos.Name = Console.ReadLine();
                }
                
                else if (pos == 2)
                {
                    ClearEditString(NullMenuProduct, pos, lineCursor);
                    string inputPrice = Console.ReadLine();
                    try
                    {
                        objectFromPos.Price = int.Parse(inputPrice);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для Цены необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuProduct[pos] + new StringBuilder().Insert(0, " ", inputPrice.Length).ToString());
                    }
  
                }
                else if (pos == 3)
                {
                    ClearEditString(NullMenuProduct, pos, lineCursor);
                    string inputQuantity = Console.ReadLine();
                    try
                    {
                        objectFromPos.Quantity = int.Parse(inputQuantity);
                    }
                    catch
                    {
                        Console.SetCursorPosition(10, 10);
                        Console.WriteLine("Для Колличества необходимо вводить только цифры!");

                        Console.SetCursorPosition(0, lineCursor + pos);
                        Console.WriteLine(NullMenuProduct[pos] + new StringBuilder().Insert(0, " ", inputQuantity.Length).ToString());
                    }
                }
                pos = Arrows.Show(MenuProduct.Length);
            }
        }
        public void PrintList()
        {
            int indexTable = 0;
            Program.WelcomePrint();

            //Заголовок таблицы
            Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "  ID",
                    "Название",
                    "Цена",
                    "Количество",
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
            indexTable++;

            foreach (Product rowproduct in products)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "  " + rowproduct.ID.ToString(),
                    rowproduct.Name,
                    rowproduct.Price,
                    rowproduct.Quantity,
                    "|" + Visual.getlineMenu(Visual.MenuF, indexTable));
                indexTable++;

            }
            for (int i = indexTable; i <= Visual.MenuF.Length; i++)
            {
                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "",
                    "",
                    "",
                    "", 
                    "|" + Visual.getlineMenu(Visual.MenuF, i));
            }

        }

        public static string[] ProductMenu()
        {
            string[] MenuProduct = {"  ID" , "  Название","  Цена", "  Количество"};
            return MenuProduct;
        }

        public void Find()
        {
            while (true)
            {
                Program.WelcomePrint();
                Console.WriteLine("Выберите, по какому пункту вы хотите произвести поиск:");

                string[] MenuAd = ProductMenu();
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
                    column = "Price";
                    Console.WriteLine("Введите Цену: ");
                    errorText = "Такой Цены не существует";

                }
                else if (pos == 3)
                {
                    column = "Quantity";
                    Console.WriteLine("Введите Количество: ");
                    errorText = "Такого Количества не существует";

                }
                searchtext = Console.ReadLine();
                List<Product> FindProduct = General.SearchList(products, column, searchtext);
                if (FindProduct.Count == 0)
                {
                    Console.WriteLine(errorText);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                    "  ID",
                    "Название",
                    "Цена",
                    "Количество");


                    foreach (Product rowproduct in products)
                    {
                        Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
                        "  " + rowproduct.ID.ToString(),
                        rowproduct.Name,
                        rowproduct.Price,
                        rowproduct.Quantity);
                    }
                    ConsoleKeyInfo key = Console.ReadKey(true);
                }
                break;
            }
        }

        public class Product
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
                var strProductMenu = ProductMenu();
                string idline = "";
                if (ID != 999)
                {
                    idline += ID;
                }
                string priceline = "";
                if (Price != 0.0)
                {
                    priceline += Price;
                }
                string quantityline = "";
                if (Quantity != 0)
                {
                    quantityline += Quantity;
                }

                strProductMenu[0] = $"{strProductMenu[0]}: {idline}";
                strProductMenu[1] = $"{strProductMenu[1]}: {Name}";
                strProductMenu[2] = $"{strProductMenu[2]}: {priceline}";
                strProductMenu[3] = $"{strProductMenu[3]}: {quantityline}";
               
                return strProductMenu;
            }

        }
    }
}

