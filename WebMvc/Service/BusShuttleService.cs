using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;
using WebMvc.Models;

namespace WebMvc.Service
{
    public class BusShuttleService : IBusShuttleService
    {
        private readonly BusShuttleContext _context = new BusShuttleContext();

        public BusShuttleService(){}

        public List<Bus> GetAllBuses()
        {
            return _context.Buses.OrderBy(bus => bus.Id).ToList();
        }

        public Bus? FindBusByID(int id)
        {
            return GetAllBuses().Find(b => b.Id == id);
        }

        public void UpdateBusByID(int id, int busNumber)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Bus selectedBus = FindBusByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedBus == null) return;
            selectedBus.Update(busNumber);
            _context.SaveChanges();
        }

        public void CreateNewBus(Bus bus)
        {
            _context.Buses.Add(bus);
        }

        public void DeleteBusByID(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Bus selectedBus = FindBusByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedBus == null) return;
            _context.Buses.Remove(selectedBus);
            _context.SaveChanges();
        }

        public List<Driver> GetAllDrivers()
        {
            return _context.Drivers.OrderBy(driver => driver.Id).ToList();
        }

        public Driver? FindDriverByID(int id)
        {
            return GetAllDrivers().Find(driver => driver.Id == id);
        }

        public void UpdateDriverByID(int id, string firstName, string lastName)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Driver selectedDriver = FindDriverByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedDriver == null) return;
            selectedDriver.Update(firstName, lastName);
            _context.SaveChanges();
        }

        public void CreateNewDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
        }

        public void DeleteDriverByID(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Driver selectedDriver = FindDriverByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedDriver == null) return;
            _context.Drivers.Remove(selectedDriver);
            _context.SaveChanges();
        }

        public List<Entry> GetAllEntries()
        {
            return _context.Entries.OrderBy(entry => entry.Id).ToList();
        }

        public Entry? FindEntryByID(int id)
        {
            return GetAllEntries().Find(entry => entry.Id == id);
        }

        public void UpdateEntryByID(int id, DateTime timestamp, int boarded, int leftBehind)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry selectedEntry = FindEntryByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedEntry == null) return;
            selectedEntry.Update(timestamp, boarded, leftBehind);
            _context.SaveChanges();
        }

        public void CreateNewEntry(Entry entry)
        {
            _context.Entries.Add(entry);
        }

        public void DeleteEntryByID(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry selectedEntry = FindEntryByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedEntry == null) return;
            _context.Entries.Remove(selectedEntry);
            _context.SaveChanges();
        }

        public List<Loop> GetAllLoops()
        {
            return _context.Loops.OrderBy(loop => loop.Id).ToList();
        }

        public Loop? FindLoopByID(int id)
        {
            return GetAllLoops().Find(loop => loop.Id == id);
        }

        public void UpdateLoopByID(int id, string name)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Loop selectedLoop = FindLoopByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedLoop == null) return;
            selectedLoop.Update(name);
            _context.SaveChanges();
        }

        public int GetNumLoops()
        {
            return GetAllLoops().Count();
        }

        public void CreateNewLoop(Loop loop)
        {
            _context.Loops.Add(loop);
        }

        public void DeleteLoopById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Loop selectedLoop = FindLoopByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedLoop == null) return;
            _context.Loops.Remove(selectedLoop);
            _context.SaveChanges();
        }

        public List<RouteDomainModel> GetAllRoutes()
        {
            return _context.Routes.OrderBy(route => route.Id).ToList();
        }

        public RouteDomainModel? FindRouteByID(int id)
        {
            return GetAllRoutes().Find(route => route.Id == id);
        }

        public void UpdateRouteByID(int id, int order)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            RouteDomainModel selectedRoute = FindRouteByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedRoute == null) return;
            selectedRoute.Update(order);
            _context.SaveChanges();
        }

        public int GetNumRoutes()
        {
            return GetAllRoutes().Count();
        }

        public void CreateNewRoute(RouteDomainModel route)
        {
            _context.Routes.Add(route);
        }

        public void DeleteRouteById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            RouteDomainModel selectedRoute = FindRouteByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedRoute == null) return;
            _context.Routes.Remove(selectedRoute);
            _context.SaveChanges();
        }

        public List<Stop> GetAllStops()
        {
            return _context.Stops.OrderBy(stop => stop.Id).ToList();
        }

        public Stop? FindStopByID(int id)
        {
            return GetAllStops().Find(stop => stop.Id == id);
        }

        public void UpdateStopByID(int id, string name, double latitude, double longitude)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Stop selectedStop = FindStopByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedStop == null) return;
            selectedStop.Update(name, latitude, longitude);
            _context.SaveChanges();
        }

        public int GetNumStops()
        {
            return GetAllStops().Count();
        }

        public void CreateNewStop(Stop stop)
        {
            _context.Stops.Add(stop);
        }

        public void DeleteStopById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Stop selectedStop = FindStopByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if(selectedStop == null) return;
            _context.Stops.Remove(selectedStop);
            _context.SaveChanges();
        }
    }
}