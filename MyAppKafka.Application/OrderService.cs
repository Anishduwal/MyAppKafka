using Microsoft.Extensions.Configuration;
using MyAppKafka.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Application
{
    public class OrderService : IOrderService
    {
        private readonly IMessageProducer _producer;
        private readonly string _topic;

        public OrderService(IMessageProducer producer, IConfiguration config)
        {
            _producer = producer;
            _topic = config["Kafka:Topic"] ?? "orders";
        }

        public async Task<Result> CreateOrderAsync(CreateOrderCommand cmd, CancellationToken ct = default)
        {
            var order = new Order
            {
                Id = new OrderId(Guid.NewGuid()),
                Customer = cmd.Customer,
                Amount = cmd.Amount,
                CreatedAt = DateTime.UtcNow
            };

            var @event = new Domain.OrderCreatedEvent(order);
            // publish the event (serialized as JSON)
            var result = await _producer.ProduceAsync(_topic, @event, ct);
            //var result = await _producer.ProduceAsync(topic, msg, ct);


            //return order.Id;
            return result;
        }
    }
}
