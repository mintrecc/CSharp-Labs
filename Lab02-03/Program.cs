using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n1. Один мисливець");
            Console.WriteLine("2. Змагання мисливців");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1": SingleHunterMode(); break;
                case "2": CompetitionMode(); break;
                case "0": return;
                default: Console.WriteLine("Невідома команда."); break;
            }
        }
    }

    static void SingleHunterMode()
    {
        Console.Write("Введіть ім'я мисливця: ");
        string name = Console.ReadLine();

        Hunter hunter;
        try
        {
            hunter = new Hunter(name);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine(hunter.GetInfo());

        while (true)
        {
            Console.WriteLine("\n1. Полювати");
            Console.WriteLine("2. Купити стріли");
            Console.WriteLine("3. Інформація");
            Console.WriteLine("0. Назад");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "0") return;

            try
            {
                switch (choice)
                {
                    case "1":
                        HuntResult result = hunter.Hunt(Wildlife.GetRandom());
                        Console.WriteLine(result);
                        Console.WriteLine(hunter.GetInfo());
                        break;
                    case "2":
                        Console.Write($"Скільки стріл купити (ціна {Hunter.ArrowPrice} за шт.): ");
                        if (!int.TryParse(Console.ReadLine(), out int count))
                        {
                            Console.WriteLine("Введіть ціле число.");
                            break;
                        }
                        hunter.BuyArrows(count);
                        Console.WriteLine($"Куплено стріл: {count}.");
                        Console.WriteLine(hunter.GetInfo());
                        break;
                    case "3":
                        Console.WriteLine(hunter.GetInfo());
                        break;
                    default:
                        Console.WriteLine("Невідома команда.");
                        break;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!hunter.IsAlive)
            {
                Console.WriteLine("Мисливець загинув. Гру завершено.");
                return;
            }
        }
    }

    static void CompetitionMode()
    {
        Console.Write("Ім'я першого мисливця: ");
        string name1 = Console.ReadLine();
        Console.Write("Ім'я другого мисливця (досвідченого): ");
        string name2 = Console.ReadLine();

        try
        {
            Hunter first = new Hunter(name1);
            Hunter second = new SkilledHunter(name2);
            Competition competition = new Competition(first, second, 10);
            competition.Run();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}