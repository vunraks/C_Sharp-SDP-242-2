using System;
using System.Collections.Generic;
using System.Linq;

namespace dz_21_22
{
    // =======================
    // ЗАДАНИЕ 1: TEMPERATURE
    // =======================
    class Temperature
    {
        public double Value;
        public string Scale;

        public Temperature(double value, string scale)
        {
            Value = value;
            Scale = scale;
        }

        public double ToCelsius()
        {
            if (Scale == "C") return Value;
            if (Scale == "F") return (Value - 32) * 5 / 9;
            if (Scale == "K") return Value - 273.15;
            return Value;
        }

        public static Temperature operator +(Temperature a, Temperature b)
        {
            double sum = a.ToCelsius() + b.ToCelsius();
            return new Temperature(sum, "C");
        }

        public static bool operator ==(Temperature a, Temperature b)
        {
            return Math.Abs(a.ToCelsius() - b.ToCelsius()) < 0.0001;
        }

        public static bool operator !=(Temperature a, Temperature b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Temperature t)
                return this == t;
            return false;
        }

        public override int GetHashCode()
        {
            return ToCelsius().GetHashCode();
        }
    }

    // =======================
    // ЗАДАНИЕ 2: SHOPPING CART
    // =======================
    class ShoppingCart
    {
        public List<string> Items = new List<string>();

        public static ShoppingCart operator +(ShoppingCart cart, string item)
        {
            cart.Items.Add(item);
            return cart;
        }

        public static ShoppingCart operator -(ShoppingCart cart, string item)
        {
            cart.Items.Remove(item);
            return cart;
        }

        public static bool operator ==(ShoppingCart a, ShoppingCart b)
        {
            return a.Items.SequenceEqual(b.Items);
        }

        public static bool operator !=(ShoppingCart a, ShoppingCart b)
        {
            return !(a == b);
        }

        public static bool operator >(ShoppingCart a, ShoppingCart b)
        {
            return a.Items.Count > b.Items.Count;
        }

        public static bool operator <(ShoppingCart a, ShoppingCart b)
        {
            return a.Items.Count < b.Items.Count;
        }

        public override bool Equals(object obj)
        {
            if (obj is ShoppingCart c)
                return this == c;
            return false;
        }

        public override int GetHashCode()
        {
            return Items.Count;
        }
    }

    // =======================
    // ЗАДАНИЕ 3: TIMERANGE
    // =======================
    class TimeRange
    {
        public int Start;
        public int End;

        public TimeRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Length => End - Start;

        public static TimeRange operator +(TimeRange a, TimeRange b)
        {
            return new TimeRange(
                Math.Min(a.Start, b.Start),
                Math.Max(a.End, b.End)
            );
        }

        public static bool operator ==(TimeRange a, TimeRange b)
        {
            return a.Start == b.Start && a.End == b.End;
        }

        public static bool operator !=(TimeRange a, TimeRange b)
        {
            return !(a == b);
        }

        public static bool operator >(TimeRange a, TimeRange b)
        {
            return a.Length > b.Length;
        }

        public static bool operator <(TimeRange a, TimeRange b)
        {
            return a.Length < b.Length;
        }

        public override bool Equals(object obj)
        {
            if (obj is TimeRange t)
                return this == t;
            return false;
        }

        public override int GetHashCode()
        {
            return Start ^ End;
        }
    }

    // =======================
    // ЗАДАНИЕ 4: FILE SIZE
    // =======================
    class FileSize
    {
        public double Size;
        public string Unit;

        public FileSize(double size, string unit)
        {
            Size = size;
            Unit = unit;
        }

        public double ToKB()
        {
            if (Unit == "KB") return Size;
            if (Unit == "MB") return Size * 1024;
            if (Unit == "GB") return Size * 1024 * 1024;
            return Size;
        }

        public static FileSize operator +(FileSize a, FileSize b)
        {
            double total = a.ToKB() + b.ToKB();
            return new FileSize(total / 1024, "MB");
        }

        public static bool operator >(FileSize a, FileSize b)
        {
            return a.ToKB() > b.ToKB();
        }

        public static bool operator <(FileSize a, FileSize b)
        {
            return a.ToKB() < b.ToKB();
        }

        public override string ToString()
        {
            return $"{Size} {Unit}";
        }
    }

    // =======================
    // ЗАДАНИЕ 5: PERMISSION
    // =======================
    class Permission
    {
        public bool Read;
        public bool Write;
        public bool Execute;

        public Permission(bool r, bool w, bool e)
        {
            Read = r;
            Write = w;
            Execute = e;
        }

        public static Permission operator +(Permission a, Permission b)
        {
            return new Permission(
                a.Read || b.Read,
                a.Write || b.Write,
                a.Execute || b.Execute
            );
        }

        public static Permission operator -(Permission a, Permission b)
        {
            return new Permission(
                a.Read && !b.Read,
                a.Write && !b.Write,
                a.Execute && !b.Execute
            );
        }

        public static bool operator ==(Permission a, Permission b)
        {
            return a.Read == b.Read &&
                   a.Write == b.Write &&
                   a.Execute == b.Execute;
        }

        public static bool operator !=(Permission a, Permission b)
        {
            return !(a == b);
        }

        public static bool operator true(Permission p)
        {
            return p.Read || p.Write || p.Execute;
        }

        public static bool operator false(Permission p)
        {
            return !(p.Read || p.Write || p.Execute);
        }

        public override bool Equals(object obj)
        {
            if (obj is Permission p)
                return this == p;
            return false;
        }

        public override int GetHashCode()
        {
            return (Read, Write, Execute).GetHashCode();
        }
    }

    // =======================
    // MAIN
    // =======================
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Temperature(0, "C");
            var t2 = new Temperature(32, "F");
            Console.WriteLine(t1 == t2);

            var c1 = new ShoppingCart();
            c1 += "Apple";
            c1 += "Banana";

            var c2 = new ShoppingCart();
            c2 += "Apple";
            c2 += "Banana";

            Console.WriteLine(c1 == c2);

            var r1 = new TimeRange(10, 12);
            var r2 = new TimeRange(11, 14);
            var r3 = r1 + r2;

            Console.WriteLine($"{r3.Start}-{r3.End}");

            var f1 = new FileSize(1, "GB");
            var f2 = new FileSize(500, "MB");

            Console.WriteLine(f1 > f2);

            var p1 = new Permission(true, false, false);

            if (p1)
                Console.WriteLine("Есть права");
        }
    }
}