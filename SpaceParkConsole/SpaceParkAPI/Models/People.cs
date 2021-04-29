using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Models
{
    public class People
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> Vehicles { get; set; }
    }

}
