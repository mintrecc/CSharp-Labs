using System;
using System.Text;

namespace DeliveryApp
{
    public class Program
    {
        private static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"=== {title} ===");
        }

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var service = new DeliveryService();
            IPayment card = new CardPayment();
            IPayment cash = new CashPayment();

            Section("1. Створення транспорту");
            service.CreateBike("Велосипед #1");
            service.CreateCar("Автомобіль #1");
            service.CreateTruck("Вантажівка #1");
            service.CreateBike("Велосипед #2");
            service.PrintTransports();

            Section("2. Створення замовлень");
            service.CreateOrder("Документи", 1, 5);
            service.CreateOrder("Коробка з книгами", 15, 8);
            service.CreateOrder("Меблі", 300, 20);
            service.CreateOrder("Піца", 2, 15);
            service.CreateOrder("Холодильник", 5000, 10);
            service.CreateOrder("Некоректне замовлення", -3, 5);
            service.PrintActiveOrders();

            Section("3. Вартість доставки кожним транспортом");
            service.ShowCosts(1);
            service.ShowCosts(2);
            service.ShowCosts(3);
            service.ShowCosts(4);
            service.ShowCosts(5);
            service.ShowCosts(99);

            Section("4. Призначення замовлень");
            service.Assign(1, 1);
            service.Assign(4, 1);
            service.Assign(4, 4);
            service.Assign(2, 4);
            service.Assign(2, 2);
            service.Assign(3, 2);
            service.Assign(3, 3);
            service.Assign(5, 3);
            service.Assign(1, 4);
            service.Assign(77, 1);
            service.Assign(4, 77);
            service.PrintTransports();
            service.PrintActiveOrders();

            Section("5. Доставка з оплатою");
            service.Deliver(4, card);
            service.Deliver(88, cash);
            service.Deliver(1, card);
            service.Deliver(1, card);
            service.Deliver(2, cash);
            service.PrintTransports();

            Section("6. Повторне використання звільненого транспорту");
            service.Assign(4, 4);
            service.Assign(4, 2);
            service.Deliver(4, cash);
            service.Deliver(3, card);
            service.PrintTransports();

            Section("7. Активні замовлення та історія");
            service.PrintActiveOrders();
            service.PrintHistory();

            Console.WriteLine();
            Console.WriteLine("Роботу завершено.");
        }
    }
}
