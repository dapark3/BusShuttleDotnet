using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Entry
    {
        public int Id {get; set;}

        public DateTime Timestamp {get; set;}

        public int Boarded {get; set;}

        public int LeftBehind {get; set;}

        public Bus Bus {get; set;}

        public Driver Driver {get; set;}

        public Loop Loop {get; set;}

        public Stop Stop {get; set;}

        public Entry(int id, int boarded, int leftBehind, Bus bus, Driver driver, Loop loop, Stop stop)
    {
        Id = id;
        Timestamp = DateTime.Now;
        Boarded = boarded;
        LeftBehind = leftBehind;
        Bus = bus;
        Driver = driver;
        Loop = loop;
        Stop = stop;
    }

    public void Update(DateTime timestamp, int boarded, int leftBehind)
    {
        Timestamp = timestamp;
        Boarded = boarded;
        LeftBehind = leftBehind;
    }
    }
}