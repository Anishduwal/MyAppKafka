using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Domain
{
    public interface IEvent { }
    public record OrderCreatedEvent(Order Order) : IEvent;
}
