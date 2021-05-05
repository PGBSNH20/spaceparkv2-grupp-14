using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceParkAPI.Models
{
    public class Pay
    {
        public int ID { get; set; }
        [ForeignKey("ParkID")]
        public int ParkID { get; set; }
        public Park Park { get; set; }
        public int Invoice { get; set; }
        public DateTime DepartTime { get; set; }
    }
}