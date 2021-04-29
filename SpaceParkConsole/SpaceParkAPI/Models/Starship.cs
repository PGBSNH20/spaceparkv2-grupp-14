using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Models
{
    public class Starship
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public decimal Passengers { get; set; }
    }
}
