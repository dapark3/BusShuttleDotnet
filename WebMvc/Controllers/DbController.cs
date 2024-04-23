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
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Entry> Entries { get; set; }
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
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(busConfig =>
            {
                busConfig.HasKey(bus => bus.Id).HasName("PrimaryKey_Id");
            });
            modelBuilder.Entity<Driver>(driverConfig =>
            {
                driverConfig.HasKey(driver => driver.Id).HasName("PrimaryKey_Id");
            });
            modelBuilder.Entity<RouteDomainModel>(routeConfig =>
            {
                routeConfig.HasKey(route => route.Id).HasName("PrimaryKey_Id");
                routeConfig.HasOne(route => route.Loop).WithMany(loop => loop.Routes);
            });
            modelBuilder.Entity<Stop>(stopConfig =>
            {
                stopConfig.HasKey(stop => stop.Id).HasName("PrimaryKey_Id");
                stopConfig.HasOne(stop => stop.Route).WithOne(route => route.Stop).IsRequired();
            });
            modelBuilder.Entity<Loop>(loopConfig =>
            {
                loopConfig.HasKey(loop => loop.Id).HasName("PrimaryKey_Id");
                loopConfig.HasMany(loop => loop.Routes).WithOne(route => route.Loop);
            });
            modelBuilder.Entity<Entry>(entryConfig =>
            {
                entryConfig.HasKey(entry => entry.Id).HasName("PrimaryKey_Id");
            });
        }
    }
}