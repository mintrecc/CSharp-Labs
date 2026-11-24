namespace DeliveryApp
{
    public class CardPayment : IPayment
    {
        public string Name => "картка";

        public string Pay(decimal amount) => $"Оплачено карткою: {amount:F2} грн";
    }
}
