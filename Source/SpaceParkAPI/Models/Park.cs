using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceParkAPI.Models
{
    public class Park
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string SpaceShip { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool Payed { get; set; }

        public static IEnumerable<Park> GetAll()
        {
            using var ctx = new SpaceParkContext();
            return ctx.Park.ToList();
        }


    }
}