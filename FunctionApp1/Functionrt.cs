using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    using System.Text.Json;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.Logging;

    public class OrderProcessor
    {
        private readonly ILogger _logger;

        public OrderProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrderProcessor>();
        }
      
        [Function("ProcessOrder")]
        public void Run(
            [ServiceBusTrigger("order-queue", Connection = "ServiceBusConnection")]
        string message)
        {
            var order = JsonSerializer.Deserialize<Order>(message);

            _logger.LogInformation($"Processing Order: {order.OrderId}");

            // Simulate business logic
            ProcessCustomer(order.CustomerId);
            ProcessProduct(order.ProductId);
            ProcessPayment(order.Amount);

            _logger.LogInformation($"Order Completed: {order.OrderId}");
        }

        private void ProcessCustomer(string customerId)
        {
            Console.WriteLine($"Validating Customer: {customerId}");
        }

        private void ProcessProduct(string productId)
        {
            Console.WriteLine($"Checking Product: {productId}");
        }

        private void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing Payment: {amount}");
        }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public decimal Amount { get; set; }
    }

}
