using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Domain
{

    public record OrderId(Guid Value);
    public class Order
    {
        public OrderId Id { get; init; }
        public string Customer { get; init; }
        public decimal Amount { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
