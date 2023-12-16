//using static Будущий_10.Accountant;
//using static Будущий_10.WarehouseManager;

//namespace Будущий_10
//{

//    public class Cashier
//    {
//        private static List<Product> products = WarehouseManager.products;
//        private static List<Product> carts = new();
//        private static List<Transaction> transactions = Accountant.transactions;
//        private static string filePathW = WarehouseManager.filePath;
//        private static string filePathA = Accountant.filePath;
//        private static int objectPos;
//        private static Product objectFromPos;
//        private static double summCarts;

//        public Cashier()
//        {
            
//            foreach (var item in products)
//            {
//                item.Quantity = 0;
//                carts.Add(item);
//            }
//            PrintList();

//            int pos = Arrows.Show(carts.Count);

//            if (pos > -1)
//            {
//                Update(pos); 

//                ConsoleKeyInfo key = Console.ReadKey(true);

//                while (true)
//                {
//                    if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.F10 || key.Key == ConsoleKey.Delete)
//                    {
//                        break;
//                    }
//                };

//                if (key.Key == ConsoleKey.F10)
//                {
//                    Update();

//                }
//                else if (key.Key == ConsoleKey.Delete)
//                {
//                    Delete();
//                }
//                var Admin = new Administrator();

//            }
//            else if (pos == (int)Keys.Escape)
//            {
//                return;
//            }
//            else if (pos == (int)Keys.S)
//            {
//                SaveCart();
//            }


//        }

//        private void Update(int pos)
//        {
//            objectPos = pos;
//            objectFromPos = users[pos];
//            var MenuUser = PrintObject(Visual.MenuB);


//        }
//        public void PrintList()
//        {
//            int indexTable = 0;
//            summCarts = 0;

//            Program.WelcomePrint();

//            //Заголовок таблицы
//            Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
//                    "  ID",
//                    "Название",
//                    "Цена",
//                    "Количество",
//                    "|" + Visual.getlineMenu(Visual.MenuC, indexTable));
//            indexTable++;

//            foreach (Product rowproduct in carts)
//            {
//                Console.WriteLine("{0,-7} {1,-10} {2,-15} {3,-40} {4,-20}",
//                    "  " + rowproduct.ID.ToString(),
//                    rowproduct.Name,
//                    rowproduct.Price,
//                    rowproduct.Quantity,
//                    "|" + Visual.getlineMenu(Visual.MenuC, indexTable));
//                indexTable++;
//                summCarts += rowproduct.Price * rowproduct.Quantity;
//            }
//            Console.WriteLine(new string('-', 92));
//            Console.WriteLine("{0,92}", $"Итог: {summCarts}");

//        }
//        public void ShowAllProducts()
//        {
//            Console.WriteLine("Список товаров:");
//            foreach (var product in products)
//            {
//                Console.WriteLine($"ID: {product.ID},Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
//            }
//            Console.WriteLine();
//        }

//        public void AddToCart()
//        {
//            Console.WriteLine("Введите название товара, который хотите добавить в корзину:");
//            string productName = Console.ReadLine();
//            Product product = products.Find(p => p.Name == productName);

//            if (product != null)
//            {
//                Console.Write("Введите количество: ");
//                int quantity = int.Parse(Console.ReadLine());

//                if (quantity > product.Quantity)
//                {
//                    Console.WriteLine("Недостаточно товара на складе.");
//                }
//                else if (quantity < 0)
//                {
//                    Console.WriteLine("Количество товара не может быть отрицательным.");
//                }
//                else
//                {
//                    cart.Add(new Product(product.ID, product.Name, product.Price, quantity));
//                    Console.WriteLine("Товар успешно добавлен в корзину.");
//                    product.Quantity -= quantity;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Товар с таким названием не найден.");
//            }
//            Console.WriteLine();
//        }

//        public void Checkout()
//        {
//            Console.WriteLine("Оформление заказа...");
//            double totalPrice = 0;

//            foreach (var product in cart)
//            {
//                totalPrice += product.Price * product.Quantity;
//            }

//            Console.WriteLine($"Общая сумма заказа: {totalPrice}");

//            // Запись информации о прибыли в бухгалтерию

//            // Удаление купленных товаров со склада
//            foreach (var product in cart)
//            {
//                Product warehouseProduct = products.Find(p => p.Name == product.Name);
//                warehouseProduct.Quantity -= product.Quantity;
//            }

//            cart.Clear();
//            Console.WriteLine("Заказ успешно завершен.");
//            Console.WriteLine();
//        }
//    }
//}


