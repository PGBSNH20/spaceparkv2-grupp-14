using System.Collections.Generic;

namespace SpaceParkAPI.Models
{
    public class StarshipRespone
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Starship> Results { get; set; }
    }
}
