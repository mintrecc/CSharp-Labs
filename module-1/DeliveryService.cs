using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApp
{
    public class DeliveryService
    {
        private readonly List<Transport> _transports = new List<Transport>();
        private readonly List<Order> _activeOrders = new List<Order>();
        private readonly List<Order> _history = new List<Order>();

        private int _nextTransportId = 1;
        private int _nextOrderNumber = 1;

        public Bike CreateBike(string name, double maxWeightKg = 5)
        {
            var transport = new Bike(_nextTransportId++, name, maxWeightKg);
            _transports.Add(transport);
            Console.WriteLine($"Створено транспорт: {transport}");
            return transport;
        }

        public Car CreateCar(string name, double maxWeightKg = 200)
        {
            var transport = new Car(_nextTransportId++, name, maxWeightKg);
            _transports.Add(transport);
            Console.WriteLine($"Створено транспорт: {transport}");
            return transport;
        }

        public Truck CreateTruck(string name, double maxWeightKg = 2000)
        {
            var transport = new Truck(_nextTransportId++, name, maxWeightKg);
            _transports.Add(transport);
            Console.WriteLine($"Створено транспорт: {transport}");
            return transport;
        }

        public Order CreateOrder(string description, double weightKg, double distanceKm)
        {
            try
            {
                var order = new Order(_nextOrderNumber++, description, weightKg, distanceKm);
                _activeOrders.Add(order);
                Console.WriteLine($"Створено: {order}");
                return order;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка створення замовлення: {ex.Message}");
                return null;
            }
        }

        public void ShowCosts(int orderNumber)
        {
            Order order = FindActive(orderNumber);
            if (order == null)
            {
                Console.WriteLine($"Замовлення №{orderNumber} не знайдено серед активних.");
                return;
            }

            Console.WriteLine($"Вартість доставки замовлення №{order.Number} ({order.WeightKg} кг, {order.DistanceKm} км):");
            var suitable = _transports.Where(t => t.CanTake(order)).ToList();

            if (suitable.Count == 0)
            {
                Console.WriteLine("  Жоден транспорт не може взяти це замовлення.");
                return;
            }

            foreach (var transport in suitable)
            {
                Console.WriteLine($"  {transport.Name}: {transport.CalculateCost(order):F2} грн ({(transport.IsFree ? "вільний" : "зайнятий")})");
            }
        }

        public bool Assign(int orderNumber, int transportId)
        {
            Order order = FindActive(orderNumber);
            if (order == null)
            {
                Console.WriteLine($"Призначення неможливе: замовлення №{orderNumber} не знайдено.");
                return false;
            }

            Transport transport = _transports.FirstOrDefault(t => t.Id == transportId);
            if (transport == null)
            {
                Console.WriteLine($"Призначення неможливе: транспорт [{transportId}] не знайдено.");
                return false;
            }

            if (order.AssignedTransport != null)
            {
                Console.WriteLine($"Призначення неможливе: замовлення №{order.Number} вже призначено на {order.AssignedTransport.Name}.");
                return false;
            }

            if (!transport.IsFree)
            {
                Console.WriteLine($"Призначення неможливе: {transport.Name} зайнятий.");
                return false;
            }

            if (!transport.CanTake(order))
            {
                Console.WriteLine($"Призначення неможливе: {transport.Name} не може взяти замовлення №{order.Number}.");
                return false;
            }

            order.AssignedTransport = transport;
            transport.Occupy();
            Console.WriteLine($"Замовлення №{order.Number} призначено на {transport.Name}. Транспорт тепер зайнятий.");
            return true;
        }

        public bool Deliver(int orderNumber, IPayment payment)
        {
            Order order = FindActive(orderNumber);
            if (order == null)
            {
                Console.WriteLine($"Доставка неможлива: замовлення №{orderNumber} не знайдено серед активних.");
                return false;
            }

            if (order.AssignedTransport == null)
            {
                Console.WriteLine($"Доставка неможлива: для замовлення №{order.Number} не призначено транспорт.");
                return false;
            }

            Transport transport = order.AssignedTransport;
            decimal cost = transport.CalculateCost(order);

            Console.WriteLine($"Доставка замовлення №{order.Number} транспортом {transport.Name}...");
            Console.WriteLine(payment.Pay(cost));

            order.FinalCost = cost;
            order.PaymentName = payment.Name;

            transport.Release();
            _activeOrders.Remove(order);
            _history.Add(order);

            Console.WriteLine($"Замовлення №{order.Number} доставлено. {transport.Name} знову вільний.");
            return true;
        }

        public void PrintTransports()
        {
            Console.WriteLine("Транспорт:");
            if (_transports.Count == 0) Console.WriteLine("  (немає)");
            foreach (var transport in _transports) Console.WriteLine($"  {transport}");
        }

        public void PrintActiveOrders()
        {
            Console.WriteLine("Активні замовлення:");
            if (_activeOrders.Count == 0)
            {
                Console.WriteLine("  (немає)");
                return;
            }
            foreach (var order in _activeOrders) Console.WriteLine($"  {order}");
        }

        public void PrintHistory()
        {
            Console.WriteLine("Історія доставок:");
            if (_history.Count == 0)
            {
                Console.WriteLine("  (порожньо)");
                return;
            }
            foreach (var order in _history)
            {
                Console.WriteLine($"  №{order.Number} \"{order.Description}\", {order.WeightKg} кг, {order.DistanceKm} км, " +
                                  $"{order.AssignedTransport.Name}, {order.FinalCost:F2} грн, оплата: {order.PaymentName}");
            }
        }

        private Order FindActive(int number) => _activeOrders.FirstOrDefault(o => o.Number == number);
    }
}
