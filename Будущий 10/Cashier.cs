using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Будущий_10
{
    public class Cashier 
    {

    }
    public class LogicCashier
    {
        private List<Product> products;
        private List<Product> cart;

        public LogicCashier(List<Product> products)
        {
            this.products = products;
            cart = new List<Product>();
        }

        public void ShowAllProducts()
        {
            Console.WriteLine("Список товаров:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ID},Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
            }
            Console.WriteLine();
        }

        public void AddToCart()
        {
            Console.WriteLine("Введите название товара, который хотите добавить в корзину:");
            string productName = Console.ReadLine();
            Product product = products.Find(p => p.Name == productName);

            if (product != null)
            {
                Console.Write("Введите количество: ");
                int quantity = int.Parse(Console.ReadLine());

                if (quantity > product.Quantity)
                {
                    Console.WriteLine("Недостаточно товара на складе.");
                }
                else if (quantity < 0)
                {
                    Console.WriteLine("Количество товара не может быть отрицательным.");
                }
                else
                {
                    cart.Add(new Product( product.ID , product.Name, product.Price, quantity));
                    Console.WriteLine("Товар успешно добавлен в корзину.");
                    product.Quantity -= quantity;
                }
            }
            else
            {
                Console.WriteLine("Товар с таким названием не найден.");
            }
            Console.WriteLine();
        }

        public void Checkout()
        {
            Console.WriteLine("Оформление заказа...");
            double totalPrice = 0;

            foreach (var product in cart)
            {
                totalPrice += product.Price * product.Quantity;
            }

            Console.WriteLine($"Общая сумма заказа: {totalPrice}");

            // Запись информации о прибыли в бухгалтерию

            // Удаление купленных товаров со склада
            foreach (var product in cart)
            {
                Product warehouseProduct = products.Find(p => p.Name == product.Name);
                warehouseProduct.Quantity -= product.Quantity;
            }

            cart.Clear();
            Console.WriteLine("Заказ успешно завершен.");
            Console.WriteLine();
        }
    }
}
