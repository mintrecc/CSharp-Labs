public class SkilledHunter : Hunter
{
    protected override double HitBonus => 0.15;
    protected override string Title => "Досвідчений мисливець";

    public SkilledHunter(string name, int health = MaxHealth, int gold = 20, int arrows = 5)
        : base(name, health, gold, arrows)
    {
    }
}