using System;

namespace DeliveryApp
{
    public class Order
    {
        public int Number { get; }
        public string Description { get; }
        public double WeightKg { get; }
        public double DistanceKm { get; }

        public Transport AssignedTransport { get; set; }
        public decimal? FinalCost { get; set; }
        public string PaymentName { get; set; }

        public Order(int number, string description, double weightKg, double distanceKm)
        {
            if (weightKg <= 0) throw new ArgumentException("Вага має бути більшою за 0.");
            if (distanceKm <= 0) throw new ArgumentException("Відстань має бути більшою за 0.");

            Number = number;
            Description = description;
            WeightKg = weightKg;
            DistanceKm = distanceKm;
        }

        public override string ToString()
        {
            string transport = AssignedTransport == null
                ? "транспорт не призначено"
                : $"транспорт: {AssignedTransport.Name}";
            return $"Замовлення №{Number} \"{Description}\", {WeightKg} кг, {DistanceKm} км ({transport})";
        }
    }
}
