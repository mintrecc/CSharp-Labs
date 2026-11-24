using System;

public class Competition
{
    private readonly Hunter _first;
    private readonly Hunter _second;
    private readonly int _rounds;

    public Competition(Hunter first, Hunter second, int rounds)
    {
        if (rounds <= 0)
            throw new ArgumentException("Кількість раундів має бути додатною.");

        _first = first;
        _second = second;
        _rounds = rounds;
    }

    public void Run()
    {
        Console.WriteLine("=== Початкові дані ===");
        Console.WriteLine(_first.GetInfo());
        Console.WriteLine(_second.GetInfo());

        for (int round = 1; round <= _rounds; round++)
        {
            Console.WriteLine($"\n--- Раунд {round} ---");
            MakeTurn(_first);
            MakeTurn(_second);
        }

        PrintResults();
    }

    private void MakeTurn(Hunter hunter)
    {
        if (!hunter.IsAlive)
        {
            Console.WriteLine($"{hunter.Name} загинув і пропускає хід.");
            return;
        }

        if (hunter.Arrows == 0)
        {
            int canBuy = Math.Min(hunter.MaxAffordableArrows(), 5);
            if (canBuy == 0)
            {
                Console.WriteLine($"{hunter.Name} не має ні стріл, ні золота на їх купівлю.");
                return;
            }

            hunter.BuyArrows(canBuy);
            Console.WriteLine($"{hunter.Name} купив стріл: {canBuy}.");
        }

        Animal animal = Wildlife.GetRandom();
        HuntResult result = hunter.Hunt(animal);
        Console.WriteLine($"{hunter.Name}: {result}");
    }

    private void PrintResults()
    {
        Console.WriteLine("\n=== Підсумки ===");
        Console.WriteLine(_first.GetInfo());
        Console.WriteLine(_second.GetInfo());
        Console.WriteLine();

        if (_first.Prey > _second.Prey)
            Console.WriteLine($"Переміг {_first.Name} зі здобиччю {_first.Prey} проти {_second.Prey}!");
        else if (_second.Prey > _first.Prey)
            Console.WriteLine($"Переміг {_second.Name} зі здобиччю {_second.Prey} проти {_first.Prey}!");
        else
            Console.WriteLine($"Нічия! Обидва мисливці здобули по {_first.Prey}.");
    }
}