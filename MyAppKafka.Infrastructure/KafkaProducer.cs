using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using MyAppKafka.Application;
using MyAppKafka.Domain;
using MyAppKafka.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Infrastructure
{
    public class KafkaProducer : IMessageProducer, IDisposable
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(IConfiguration config)
        {
            var cfg = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };
            _producer = new ProducerBuilder<string, string>(cfg).Build();
        }

        public async Task<Result> ProduceAsync<T>(string topic, T message, CancellationToken ct = default)
        {
            var key = Guid.NewGuid().ToString();
            var value = JsonSerializer.Serialize(message);
            var msg = new Message<string, string> { Key = key, Value = value };

            // ProduceAsync returns delivery result
            var result = await _producer.ProduceAsync(topic, msg, ct);
            if (result.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception("Kafka message not persisted");
            }

            return new Result
            {
                Topic = result.Topic,
                Partition = result.Partition.Value.ToString(),
                Offset = result.Offset.Value.ToString()
            };
        }

        public void Dispose() => _producer?.Flush(TimeSpan.FromSeconds(5));
    }

}


