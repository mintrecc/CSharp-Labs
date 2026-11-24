namespace DeliveryApp
{
    public abstract class Transport : ICostCalculator
    {
        public int Id { get; }
        public string Name { get; }
        public double MaxWeightKg { get; }
        public bool IsFree { get; private set; } = true;

        protected Transport(int id, string name, double maxWeightKg)
        {
            Id = id;
            Name = name;
            MaxWeightKg = maxWeightKg;
        }

        public abstract decimal CalculateCost(Order order);

        public virtual bool CanTake(Order order) => order.WeightKg <= MaxWeightKg;

        public void Occupy() => IsFree = false;

        public void Release() => IsFree = true;

        public override string ToString() =>
            $"[{Id}] {Name} (макс. вага {MaxWeightKg} кг, {(IsFree ? "вільний" : "зайнятий")})";
    }
}
