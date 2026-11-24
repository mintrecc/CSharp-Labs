namespace DeliveryApp
{
    public class Car : Transport
    {
        public Car(int id, string name, double maxWeightKg = 200) : base(id, name, maxWeightKg) { }

        public override decimal CalculateCost(Order order) =>
            50m + 8m * (decimal)order.DistanceKm + 1m * (decimal)order.WeightKg;
    }
}
