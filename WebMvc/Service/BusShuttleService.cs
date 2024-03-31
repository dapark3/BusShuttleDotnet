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
            return this.GetAllBuses().Find(b => b.Id == id);
        }

        public void UpdateBusByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumBuses()
        {
            throw new NotImplementedException();
        }

        public void CreateNewBus(int id, int BusNumber)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Driver> GetAllDrivers()
        {
            throw new NotImplementedException();
        }

        public Driver? FindDriverByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDriverByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumDrivers()
        {
            throw new NotImplementedException();
        }

        public void CreateNewDriver(int id, string FirstName, string LastName)
        {
            throw new NotImplementedException();
        }

        public void DeleteDriverByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetAllEntries()
        {
            throw new NotImplementedException();
        }

        public Entry? FindEntryByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntryByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumEntries()
        {
            throw new NotImplementedException();
        }

        public void CreateNewEntry(int id, DateTime timestamp, int boarded, int LeftBehind)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntryByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Loop> GetAllLoops()
        {
            throw new NotImplementedException();
        }

        public Loop? FindLoopByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLoopByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumLoops()
        {
            throw new NotImplementedException();
        }

        public void CreateNewLoop(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteLoopById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RouteDomainModel> GetAllRoutes()
        {
            throw new NotImplementedException();
        }

        public RouteDomainModel? FindRouteByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRouteByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumRoutes()
        {
            throw new NotImplementedException();
        }

        public void CreateNewRoute(int id, int order)
        {
            throw new NotImplementedException();
        }

        public void DeleteRouteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Stop> GetAllStops()
        {
            throw new NotImplementedException();
        }

        public Stop? FindStopByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStopByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNumStops()
        {
            throw new NotImplementedException();
        }

        public void CreateNewStop(int id, string name, double Latitude, double Longitude)
        {
            throw new NotImplementedException();
        }

        public void DeleteStopById(int id)
        {
            throw new NotImplementedException();
        }
    }
}