using System;

public class Hunter
{
    public const int ArrowPrice = 5;
    public const int MaxHealth = 100;

    public string Name { get; }
    public int Health { get; private set; }
    public int Gold { get; private set; }
    public int Arrows { get; private set; }
    public int Prey { get; private set; }

    public bool IsAlive => Health > 0;
    public bool CanHunt => IsAlive && Arrows > 0;

    protected virtual double HitBonus => 0.0;
    protected virtual string Title => "Мисливець";

    public Hunter(string name, int health = MaxHealth, int gold = 20, int arrows = 5)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ім'я не може бути порожнім.");
        if (health <= 0 || health > MaxHealth)
            throw new ArgumentException($"Здоров'я має бути від 1 до {MaxHealth}.");
        if (gold < 0 || arrows < 0)
            throw new ArgumentException("Золото і стріли не можуть бути від'ємними.");

        Name = name;
        Health = health;
        Gold = gold;
        Arrows = arrows;
        Prey = 0;
    }

    public HuntResult Hunt(Animal animal)
    {
        if (!IsAlive)
            throw new InvalidOperationException($"{Name} загинув і не може полювати.");
        if (Arrows <= 0)
            throw new InvalidOperationException($"{Name} не має стріл для полювання.");

        Arrows--;

        double chance = Math.Clamp(1.0 - animal.Difficulty + HitBonus, 0.05, 0.95);
        bool success = Random.Shared.NextDouble() < chance;

        if (success)
        {
            Prey++;
            Gold += animal.Reward;
            return new HuntResult(animal, true, animal.Reward, 0);
        }

        int damage = Math.Min(animal.Damage, Health);
        Health -= damage;
        return new HuntResult(animal, false, 0, damage);
    }

    public void BuyArrows(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Кількість стріл має бути додатною.");

        int cost = count * ArrowPrice;
        if (cost > Gold)
            throw new InvalidOperationException(
                $"Недостатньо золота: потрібно {cost}, є {Gold}.");

        Gold -= cost;
        Arrows += count;
    }

    public int MaxAffordableArrows()
    {
        return Gold / ArrowPrice;
    }

    public string GetInfo()
    {
        return $"{Title} {Name}: здоров'я {Health}, золото {Gold}, стріл {Arrows}, здобич {Prey}";
    }
}