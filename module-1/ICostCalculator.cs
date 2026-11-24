namespace DeliveryApp
{
    public interface ICostCalculator
    {
        decimal CalculateCost(Order order);
    }
}
