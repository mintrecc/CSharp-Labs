public class HuntResult
{
    public Animal Animal { get; }
    public bool Success { get; }
    public int GoldEarned { get; }
    public int DamageTaken { get; }

    public HuntResult(Animal animal, bool success, int goldEarned, int damageTaken)
    {
        Animal = animal;
        Success = success;
        GoldEarned = goldEarned;
        DamageTaken = damageTaken;
    }

    public override string ToString()
    {
        if (Success)
            return $"Влучно! {Animal.Name} упіймано. Винагорода: {GoldEarned} золота.";
        return $"Промах! {Animal.Name} втік і поранив мисливця на {DamageTaken} здоров'я.";
    }
}