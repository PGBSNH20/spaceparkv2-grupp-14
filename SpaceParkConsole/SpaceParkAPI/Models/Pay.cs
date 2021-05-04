using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Models
{
    public class Pay
    {
        public int ID { get; set; }
        public DateTime StartParking { get; set; }
        public DateTime PaidAt { get; set; }
        public People Person { get; set; }
    }
}
