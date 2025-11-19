using MyAppKafka.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Application
{
    // generic abstraction for producing domain events/messages
    public interface IMessageProducer
    {
        Task<Result> ProduceAsync<T>(string topic, T message, CancellationToken ct = default);
    }
}
