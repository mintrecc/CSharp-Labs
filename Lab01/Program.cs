using System;

class Program
{
    static void Main()
    {
        double a = ReadSide("a");
        double b = ReadSide("b");
        double c = ReadSide("c");

        if (!AreSidesPositive(a, b, c))
        {
            Console.WriteLine("Помилка: сторони повинні бути додатними числами.");
            return;
        }

        if (!IsTriangle(a, b, c))
        {
            Console.WriteLine("Такий трикутник не існує.");
            return;
        }

        Console.WriteLine("Трикутник існує.");
        Console.WriteLine($"Периметр: {Perimeter(a, b, c):F2}");
        Console.WriteLine($"Площа: {Area(a, b, c):F2}");
        Console.WriteLine($"Вид: {GetTriangleType(a, b, c)}");
    }

    static double ReadSide(string name)
    {
        while (true)
        {
            Console.Write($"Введіть сторону {name}: ");
            if (double.TryParse(Console.ReadLine(), out double value))
                return value;
            Console.WriteLine("Некоректне число, спробуйте ще раз.");
        }
    }

    static bool AreSidesPositive(double a, double b, double c)
    {
        return a > 0 && b > 0 && c > 0;
    }

    static bool IsTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    static double Perimeter(double a, double b, double c)
    {
        return a + b + c;
    }

    static double Area(double a, double b, double c)
    {
        double p = Perimeter(a, b, c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    static bool AreEqual(double x, double y)
    {
        return Math.Abs(x - y) < 1e-9;
    }

    static bool IsEquilateral(double a, double b, double c)
    {
        return AreEqual(a, b) && AreEqual(b, c);
    }

    static bool IsIsosceles(double a, double b, double c)
    {
        return AreEqual(a, b) || AreEqual(b, c) || AreEqual(a, c);
    }

    static bool IsRight(double a, double b, double c)
    {
        double[] s = { a, b, c };
        Array.Sort(s);
        return AreEqual(s[0] * s[0] + s[1] * s[1], s[2] * s[2]);
    }

    static string GetTriangleType(double a, double b, double c)
    {
        if (IsEquilateral(a, b, c)) return "рівносторонній";
        if (IsRight(a, b, c) && IsIsosceles(a, b, c)) return "прямокутний рівнобедрений";
        if (IsRight(a, b, c)) return "прямокутний";
        if (IsIsosceles(a, b, c)) return "рівнобедрений";
        return "довільний";
    }
}