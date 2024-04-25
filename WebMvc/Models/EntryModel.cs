using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Service;
using System.ComponentModel.DataAnnotations;

using DomainModel;

namespace WebMvc.Models
{
    public class EntryViewModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}
        public int BusId { get; set; }
        public int DriverId { get; set; }
        public int LoopId { get; set; }
        public int StopId { get; set; }

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
        [Required]
        public int Id {get;set;}
        [Required]
        public DateTime Timestamp {get;set;}
        [Required]
        public int Boarded {get;set;}
        [Required]
        public int LeftBehind {get;set;}

        [Required]
        public int BusId {get;set;}
        public List<Bus>? Buses {get;set;}

        [Required]
        public int DriverId {get;set;}
        public List<Driver>? Drivers {get;set;}

        [Required]
        public int LoopId {get;set;}
        public List<Loop>? Loops {get;set;}

        [Required]
        public int StopId {get;set;}
        public List<Stop>? Stops {get;set;}

        public static EntryCreateModel CreateEntry(int id, List<Bus> buses, List<Driver> drivers, List<Loop> loops, List<Stop> stops)
        {

#pragma warning disable CS8601 // Possible null reference assignment.
            return new EntryCreateModel{
                Id = id,
                Timestamp = DateTime.Now,
                Boarded = 0,
                LeftBehind = 0,
                Buses = buses,
                Drivers = drivers,
                Loops = loops,
                Stops = stops
            };
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        public static EntryCreateModel CreateEntryFromIDs(int id, int busId, int driverId, int loopId, int stopId)
        {

#pragma warning disable CS8601 // Possible null reference assignment.
            return new EntryCreateModel{
                Id = id,
                Timestamp = DateTime.Now,
                Boarded = 0,
                LeftBehind = 0,
                BusId = busId,
                DriverId = driverId,
                LoopId = loopId,
                StopId = stopId
            };
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }

    public class EntryUpdateModel
    {
        [Required]
        public int Id {get;set;}
        [Required]
        public DateTime Timestamp {get;set;}
        [Required]
        public int Boarded {get;set;}
        [Required]
        public int LeftBehind {get;set;}

        [Required]
        public int BusId {get;set;}
        public List<Bus>? Buses {get;set;}

        [Required]
        public int DriverId {get;set;}
        public List<Driver>? Drivers {get;set;}

        [Required]
        public int LoopId {get;set;}
        public List<Loop>? Loops {get;set;}

        [Required]
        public int StopId {get;set;}
        public List<Stop>? Stops {get;set;}

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

    public class EntrySelectModel
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
        public List<Stop> Stops {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int BusId {get;set;}
        public int DriverId {get;set;}
        public int LoopId {get;set;}
        public int StopId {get;set;}

        public static EntrySelectModel SelectEntry(int id, Bus bus, Driver driver, Loop loop, List<Stop> stops)
        {
           return new EntrySelectModel{
                Id = id,
                Timestamp = DateTime.Now,
                Boarded = 0,
                LeftBehind = 0,
                Bus = bus,
                Driver = driver,
                Loop = loop,
                Stops = stops
            }; 
        }
    }
}