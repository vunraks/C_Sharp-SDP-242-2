using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23_24_DZ
{
    class Utils
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }


        public static bool TryDivide(double a, double b, out double result)
        {
            if (b == 0)
            {
                result = 0;
                return false;
            }

            result = a / b;
            return true;
        }


        public static void PrintArray<T>(T[] array)
        {
            foreach (T item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }


        public static int Sum(in int a, in int b)
        {
            return a + b;
        }


        public static T FindMax<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                throw new Exception("Массив пустой");

            T max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
            }

            return max;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 10;

            Utils.Swap(ref a, ref b);
            Console.WriteLine($"Swap: a = {a}, b = {b}");


            bool success = Utils.TryDivide(10, 2, out double result);
            Console.WriteLine($"TryDivide success = {success}, result = {result}");
            bool fail = Utils.TryDivide(10, 0, out double result2);
            Console.WriteLine($"TryDivide success = {fail}, result = {result2}");


            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.Write("Array: ");
            Utils.PrintArray(numbers);
            int sum = Utils.Sum(in a, in b);
            Console.WriteLine($"Sum = {sum}");


            int max = Utils.FindMax(numbers);
            Console.WriteLine($"Max = {max}");
        }
    }
}
