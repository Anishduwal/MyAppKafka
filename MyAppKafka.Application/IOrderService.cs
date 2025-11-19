using MyAppKafka.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Application
{
    public interface IOrderService
    {
        Task<Result> CreateOrderAsync(CreateOrderCommand cmd, CancellationToken ct = default);
    }
}
