public static class Wildlife
{
    private static readonly Animal[] Animals =
    {
        new Animal("Заєць", 8, 3, 0.25),
        new Animal("Качка", 10, 2, 0.30),
        new Animal("Лисиця", 15, 8, 0.45),
        new Animal("Олень", 25, 10, 0.55),
        new Animal("Вовк", 30, 20, 0.65),
        new Animal("Ведмідь", 50, 35, 0.80)
    };

    public static Animal GetRandom()
    {
        return Animals[Random.Shared.Next(Animals.Length)];
    }
}