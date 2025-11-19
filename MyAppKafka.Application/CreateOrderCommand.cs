using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Application
{
    public record CreateOrderCommand(string Customer, decimal Amount);
}
