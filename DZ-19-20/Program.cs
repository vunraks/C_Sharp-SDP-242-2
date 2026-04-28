using System;
using System.IO;

namespace DZ_19_20
{
    class TemperatureSensor
    {
        public int Temperature;

        public event Action<int> OnTemperatureTooHigh;

        public void SetTemperature(int temp)
        {
            Temperature = temp;

            if (temp > 30)
                OnTemperatureTooHigh?.Invoke(temp);
        }
    }

    class Order
    {
        public int Id;
        public string Name;

        public event Action<Order> OnOrderCreated;

        public void Create()
        {
            OnOrderCreated?.Invoke(this);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("log.txt")) File.Create("log.txt").Close();
            if (!File.Exists("users.txt")) File.Create("users.txt").Close();
            if (!File.Exists("orders.txt")) File.Create("orders.txt").Close();
            if (!File.Exists("errors.txt")) File.Create("errors.txt").Close();

            TemperatureSensor sensor = new TemperatureSensor();

            sensor.OnTemperatureTooHigh += (temp) =>
            {
                string msg = $"Опасная температура: {temp}";

                Console.WriteLine(msg);

                try
                {
                    File.AppendAllText("log.txt", msg + "\n");
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            };

            Order order = new Order();

            order.OnOrderCreated += (o) =>
            {
                Console.WriteLine($"Создан заказ: {o.Id} {o.Name}");
            };

            order.OnOrderCreated += (o) =>
            {
                try
                {
                    File.AppendAllText("orders.txt", $"{o.Id} {o.Name}\n");
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            };

            while (true)
            {
                Console.WriteLine("\n1-Температура 2-Пользователь 3-Показать пользователей 4-Заказ 0-Выход");
                string c = Console.ReadLine();

                if (c == "1")
                {
                    Console.Write("Введите температуру: ");
                    int t = int.Parse(Console.ReadLine());
                    sensor.SetTemperature(t);
                }

                if (c == "2")
                {
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();

                    try
                    {
                        File.AppendAllText("users.txt", name + "\n");
                    }
                    catch (Exception ex)
                    {
                        HandleError(ex);
                    }
                }

                if (c == "3")
                {
                    try
                    {
                        string[] users = File.ReadAllLines("users.txt");
                        foreach (var u in users)
                            Console.WriteLine(u);
                    }
                    catch (Exception ex)
                    {
                        HandleError(ex);
                    }
                }

                if (c == "4")
                {
                    Console.Write("Id: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    order.Id = id;
                    order.Name = name;

                    order.Create();
                }

                if (c == "0")
                    break;
            }
        }

        static void HandleError(Exception ex)
        {
            Console.WriteLine("Произошла ошибка");

            try
            {
                File.AppendAllText("errors.txt", ex.Message + "\n");
            }
            catch { }
        }
    }
}