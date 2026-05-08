using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamC_
{

    internal class Program
    {
//Dictionary слово-список переводов
                         //ключ       значение
        static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        static string dictionaryType = "Англо-Русский";


        static void Main(string[] args)
        {
            //загрузка словаря из файла
            LoadFromFile();
            // бесконечный цикл
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== СЛОВАРЬ =====");
                Console.WriteLine("Тип словаря: " + dictionaryType);
                Console.WriteLine();

                Console.WriteLine("1 - Добавить слово");
                Console.WriteLine("2 - Найти перевод");
                Console.WriteLine("3 - Изменить перевод");
                Console.WriteLine("4 - Удалить перевод");
                Console.WriteLine("5 - Показать все слова");
                Console.WriteLine("6 - Сохранить в файл");
                Console.WriteLine("0 - Выход");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice) 
                {
                    case "1":
                        AddWord();
                        break;
                    case "2":
                        FindWord();
                        break;
                    case "3":
                        EditTranslation();
                        break;
                    case "4":
                        DeleteTranslation();
                        break;
                    case "5":
                        ShowAll();
                        break;
                    case "6":
                        SaveToFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт. Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void AddWord()
        {
            Console.Clear();

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower(); //перевод в нижний регистр

            Console.Write("Введите перевод: ");
            string translation = Console.ReadLine();
            //проверка есть ли уже такое слово в словаре
            if (!dictionary.ContainsKey(word))
            {
                dictionary[word] = new List<string>(); //создание нового списка переводов для этого слова
            }

            dictionary[word].Add(translation);

            Console.WriteLine("Слово добавлено!");
            Console.ReadKey();
        }

        static void FindWord()
        {
            Console.Clear();

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();
            
            if (dictionary.ContainsKey(word))
            {
                Console.WriteLine("Переводы:");
                //вывод всех переводов для данного слова
                foreach (string t in dictionary[word])
                {
                    Console.WriteLine("- " + t);
                }
            }
            else
            {
                Console.WriteLine("Слово не найдено");
            }
            Console.ReadKey();
        }

        static void EditTranslation()
        {
            Console.Clear();

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();
            
            if (dictionary.ContainsKey(word))
            {
                Console.WriteLine("Текущие переводы:");
                for (int i = 0; i < dictionary[word].Count; i++)//вывод всех переводов с их индексами
                {
                    Console.WriteLine(i + " - " + dictionary[word][i]);
                }

                Console.Write("Введите номер перевода: ");
                int index = Convert.ToInt32(Console.ReadLine()); //преобразование строки в число

                Console.Write("Введите новый перевод: ");
                string newTranslation = Console.ReadLine();

                dictionary[word][index] = newTranslation; //замена старого перевода на новый

                Console.WriteLine("Перевод изменен!");
            }
            else
            {
                Console.WriteLine("Слово не найдено");
            }

            Console.ReadKey();
        }
        static void DeleteTranslation()
        {
            Console.Clear();

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();

            if (dictionary.ContainsKey(word))
            {
                if (dictionary[word].Count == 1)
                {
                    Console.WriteLine("Нельзя удалить последний перевод!");
                }
                else
                {
                    Console.WriteLine("Переводы:");

                    for (int i = 0; i < dictionary[word].Count; i++) //вывод всех переводов с индексами
                    {
                        Console.WriteLine(i + " - " + dictionary[word][i]);
                    }

                    Console.Write("Введите номер перевода: ");
                    int index = Convert.ToInt32(Console.ReadLine());

                    dictionary[word].RemoveAt(index);

                    Console.WriteLine("Перевод удален!");
                }
            }
            else
            {
                Console.WriteLine("Слово не найдено");
            }

            Console.ReadKey();
        }

        static void ShowAll()
        {
            Console.Clear();

            foreach (var item in dictionary) 
            {
                Console.Write(item.Key + " -> "); 

                foreach (string t in item.Value)
                {
                    Console.Write(t + ", ");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static void SaveToFile()
        {
            StreamWriter writer = new StreamWriter("dictionary.txt");

            foreach (var item in dictionary)
            {
                                    //Hello:Привет        //Hello:Привет,Здравствуй
                string line = item.Key + ":" + string.Join(",", item.Value);
                writer.WriteLine(line);
            }

            writer.Close();
        }
        static void LoadFromFile()
        {
            if (!File.Exists("dictionary.txt"))
                return;

            StreamReader reader = new StreamReader("dictionary.txt");

            string line;

            while ((line = reader.ReadLine()) != null) 
            {
                string[] parts = line.Split(':'); 

                string word = parts[0]; 

                List<string> translations = new List<string>(parts[1].Split(',')); 

                dictionary[word] = translations;  //добавление слова и его переводов в словарь
            }

            reader.Close();
        }
    }
}