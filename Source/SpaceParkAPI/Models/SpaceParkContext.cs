using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Models
{
    public class SpaceParkContext:DbContext
    {

        public virtual DbSet<Pay> Pay { get; set; }
        public DbSet<Park> Park { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }

        public SpaceParkContext()
        {
        }

        public SpaceParkContext(DbContextOptions<SpaceParkContext> options) : base(options)
        {
            //this._connString = AppConfig.GetConfig().ConnectionString;
        }

        //Byt connection string så det funkar för dig
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=SpacePortDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Park>().HasData(
                new Park { ID = 1, PersonName = "Watto", SpaceShip = "x-wing", ArrivalTime = DateTime.Now, Payed = false }
            );
        }
    }
}
 