using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using WebMvc.Service;

namespace WebMvc.Models
{
    public class EntryDataModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}
    }

    public class EntryViewModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}

        public static EntryViewModel FromEntry(Entry entry)
        {
            return new EntryViewModel{
                Id = entry.Id,
                Timestamp = entry.Timestamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind
            };
        }
    }

    public class EntryCreateModel
    {

        public int Id {get;set;}

        public DateTime Timestamp {get;set;}

        public int Boarded {get;set;}

        public int LeftBehind {get;set;}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Bus Bus {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Driver Driver {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Loop Loop {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Stop Stop {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static EntryCreateModel CreateEntry(int id)
        {
            BusShuttleService busShuttleService = new BusShuttleService();

#pragma warning disable CS8601 // Possible null reference assignment.
            return new EntryCreateModel{
                Id = id,
                Timestamp = DateTime.Now,
                Boarded = 0,
                LeftBehind = 0,
                Bus = busShuttleService.FindBusByID(id),
                Driver = busShuttleService.FindDriverByID(id),
                Loop = busShuttleService.FindLoopByID(id),
                Stop = busShuttleService.FindStopByID(id)
            };
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }

    public class EntryUpdateModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}

        public static EntryUpdateModel UpdateEntry(Entry entry)
        {
            return new EntryUpdateModel{
                Id = entry.Id,
                Timestamp = entry.Timestamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind
            };
        }
    }

    public class EntryDeleteModel
    {
        public int Id {get;set;}

        public static EntryDeleteModel DeleteEntry(int id)
        {
            return new EntryDeleteModel{
                Id = id
            };
        }
    }
}