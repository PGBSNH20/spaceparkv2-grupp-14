using System;

namespace SpaceParkAPI.Models
{
    public class Receipt
    {
        public int ID { get; set; }
        public int PayID { get; set; }
        public string PersonName { get; set; }
        public string SpaceShip { get; set; }
        public Double Price { get; set; }
    }
}