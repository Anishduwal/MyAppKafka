using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyAppKafka.Domain;
using MyAppKafka.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Infrastructure
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IConfiguration _config;
        public KafkaConsumerHostedService(IConfiguration config)
        {
            _config = config;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                var conf = new ConsumerConfig
                {
                    BootstrapServers = _config["Kafka:BootstrapServers"],
                    GroupId = _config["Kafka:GroupId"] ?? "myapp-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = true
                };

                using var consumer = new ConsumerBuilder<string, string>(conf).Build();
                consumer.Subscribe(_config["Kafka:Topic"] ?? "orders");

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(stoppingToken);
                        var json = cr.Message.Value;
                        // Assuming OrderCreatedEvent
                        var evt = JsonSerializer.Deserialize<OrderCreatedEvent>(json);
                        if (evt != null)
                        {
                            // handle event (e.g. log or call domain logic)
                            Console.WriteLine($"[Consumer] Received order {evt.Order.Id.Value} for {evt.Order.Customer} amount {evt.Order.Amount}");
                        }
                    }
                }
                catch (OperationCanceledException) { }
                finally
                {
                    consumer.Close();
                }
            }, stoppingToken);
        }
    }
}
