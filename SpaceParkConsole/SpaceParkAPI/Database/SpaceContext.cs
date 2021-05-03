﻿using Microsoft.EntityFrameworkCore;
using SpaceParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Database
{
    public class SpaceContext : DbContext
    {
        private const string connectionString = "Data source=LAPTOP-45O2J506;Database=ParkingSpaceVersion2;Trusted_Connection=True;";
        public DbSet<People> Peoples { get; set; }
        public SpaceContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}
