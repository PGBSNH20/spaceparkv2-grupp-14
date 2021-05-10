using System.Collections.Generic;

namespace SpaceParkAPI.Models
{
    public class PeopleResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<People> Results { get; set; }
    }
}
