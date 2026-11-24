namespace DeliveryApp
{
    public interface IPayment
    {
        string Name { get; }
        string Pay(decimal amount);
    }
}
