using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DomainModel;

namespace WebMvc.Models
{
    public class BusShuttleContext : DbContext
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers {get; set;}
        public DbSet<Entry> Entries { get; set;}
        public DbSet<Loop> Loops { get; set; }
        public DbSet<RouteDomainModel> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public string DbPath { get; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        public BusShuttleContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            DbPath = "BusShuttle.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}