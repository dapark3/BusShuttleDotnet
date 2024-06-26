using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Entry
    {
        [Key]
        public int Id {get; set;}
        public DateTime Timestamp {get; set;}
        public int Boarded {get; set;}
        public int LeftBehind {get; set;}

        public virtual Bus Bus { get; set; }
        [ForeignKey("Bus")]
        public int BusId { get; set; }

        public virtual Driver Driver { get; set; }
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public virtual Loop Loop { get; set; }
        [ForeignKey("Loop")]
        public int LoopId { get; set; }
        
        public virtual Stop Stop { get; set; }
        [ForeignKey("Stop")]
        public int StopId { get; set; }

        public Entry(int id, int boarded, int leftBehind)
        {
            Id = id;
            Timestamp = DateTime.Now;
            Boarded = boarded;
            LeftBehind = leftBehind;
        }

        public void Update(DateTime timestamp, int boarded, int leftBehind)
        {
            Timestamp = timestamp;
            Boarded = boarded;
            LeftBehind = leftBehind;
        }

        public Entry SetBus(Bus bus)
        {
            Bus = bus;
            BusId = bus.Id;
            return this;
        }

        public Entry SetDriver(Driver driver)
        {
            Driver = driver;
            DriverId = driver.Id;
            return this;
        }

        public Entry SetLoop(Loop loop)
        {
            Loop = loop;
            LoopId = loop.Id;
            return this;
        }

        public Entry SetStop(Stop stop)
        {
            Stop = stop;
            StopId = stop.Id;
            return this;
        }
    }
}