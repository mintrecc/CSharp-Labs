namespace DeliveryApp
{
    public class Truck : Transport
    {
        public Truck(int id, string name, double maxWeightKg = 2000) : base(id, name, maxWeightKg) { }

        public override decimal CalculateCost(Order order) =>
            200m + 15m * (decimal)order.DistanceKm + 0.5m * (decimal)order.WeightKg;
    }
}
