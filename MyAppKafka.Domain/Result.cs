using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppKafka.Domain
{
    public class Result
    {
        public string Topic { get; set; }
        public string Partition { get; set; }
        public string Offset { get; set; }
    }
}
