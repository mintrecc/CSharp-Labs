namespace DeliveryApp
{
    public class Bike : Transport
    {
        public const double MaxDistanceKm = 10;

        public Bike(int id, string name, double maxWeightKg = 5) : base(id, name, maxWeightKg) { }

        public override decimal CalculateCost(Order order) =>
            20m + 5m * (decimal)order.DistanceKm;

        public override bool CanTake(Order order) =>
            base.CanTake(order) && order.DistanceKm <= MaxDistanceKm;
    }
}
