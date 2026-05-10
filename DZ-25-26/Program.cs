using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_25_26
{

    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;

        public override string ToString()
        {
            return $"ID: {Id} | Название: {Name} | Цена: {Price} | Количество: {Quantity}";
        }
    }


    class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public T Find(Predicate<T> predicate)
        {
            return items.Find(predicate);
        }
    }


    class ProductService
    {
        private Repository<Product> repository = new Repository<Product>();

        private int currentId = 1;

        public void AddProduct(string name, double price, int quantity)
        {
            if (price < 0 || quantity < 0)
            {
                Console.WriteLine("Ошибка! Цена и количество не могут быть отрицательными.");
                return;
            }

            Product product = new Product();

            product.Id = currentId++;
            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;

            repository.Add(product);

            Console.WriteLine("Товар добавлен!");
        }

        public void ShowAllProducts()
        {
            List<Product> products = repository.GetAll();

            if (products.Count == 0)
            {
                Console.WriteLine("Товаров нет.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        public Product FindProductById(int id)
        {
            return repository.Find(p => p.Id == id);
        }

        public void RemoveProduct(int id)
        {
            Product product = FindProductById(id);

            if (product != null)
            {
                repository.Remove(product);
                Console.WriteLine("Товар удалён.");
            }
            else
            {
                Console.WriteLine("Товар не найден.");
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            ProductService service = new ProductService();

            while (true)
            {
                Console.WriteLine("\n===== INVENTORY SYSTEM =====");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Показать все товары");
                Console.WriteLine("3. Найти товар по ID");
                Console.WriteLine("4. Удалить товар");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.Write("Введите название: ");
                        string name = Console.ReadLine();

                        Console.Write("Введите цену: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        Console.Write("Введите количество: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        service.AddProduct(name, price, quantity);

                        break;

                    case "2":

                        service.ShowAllProducts();

                        break;

                    case "3":

                        Console.Write("Введите ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());

                        Product product = service.FindProductById(id);

                        if (product != null)
                        {
                            Console.WriteLine(product);
                        }
                        else
                        {
                            Console.WriteLine("Товар не найден.");
                        }

                        break;

                    case "4":

                        Console.Write("Введите ID товара: ");
                        int removeId = Convert.ToInt32(Console.ReadLine());

                        service.RemoveProduct(removeId);

                        break;

                    case "0":

                        return;

                    default:

                        Console.WriteLine("Неверный пункт меню.");

                        break;
                }
            }
        }
    }
}