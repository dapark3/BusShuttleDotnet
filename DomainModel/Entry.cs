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

        public Entry(int id, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId)
    {
        Id = id;
        Timestamp = DateTime.Now;
        Boarded = boarded;
        LeftBehind = leftBehind;
    }

    public void Update(DateTime timestamp, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId)
    {
        Timestamp = timestamp;
        Boarded = boarded;
        LeftBehind = leftBehind;
    }
    }
}