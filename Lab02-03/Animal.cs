public class Animal
{
    public string Name { get; }
    public int Reward { get; }
    public int Damage { get; }
    public double Difficulty { get; }

    public Animal(string name, int reward, int damage, double difficulty)
    {
        Name = name;
        Reward = reward;
        Damage = damage;
        Difficulty = difficulty;
    }
}