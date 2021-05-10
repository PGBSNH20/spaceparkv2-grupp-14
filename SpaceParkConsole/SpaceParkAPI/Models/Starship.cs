using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Models
{
    public class StarShip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Passengers { get; set; }
    }
}
