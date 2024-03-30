using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebMvc.Models
{
    public class BusContext : DbContext
    {
        public DbSet<BusDataModel> Tasks { get; set; }
        public string DbPath { get; }

        public BusContext()
        {
            DbPath = "bus.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}