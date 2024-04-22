using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;

namespace WebMvc.Service
{
    public interface IBusShuttleService
    {
        public List<Bus> GetAllBuses();
        public Bus? FindBusByID(int id);
        public void UpdateBusByID(int id, int busNumber);
        public void CreateNewBus(Bus bus);
        public void DeleteBusByID(int id);

        public List<Driver> GetAllDrivers();
        public Driver? FindDriverByID(int id);
        public void UpdateDriverByID(int id, string firstName, string lastName, bool activated);
        public void CreateNewDriver(Driver driver);
        public void DeleteDriverByID(int id);

        public List<Entry> GetAllEntries();
        public Entry? FindEntryByID(int id);
        public void UpdateEntryByID(int id, DateTime timestamp, int boarded, int leftBehind);
        public void CreateNewEntry(Entry entry);
        public void DeleteEntryByID(int id);

        public List<Loop> GetAllLoops();
        public Loop? FindLoopByID(int id);
        public void UpdateLoopByID(int id, string name);
        public int GetNumLoops();
        public void CreateNewLoop(Loop loop);
        public void DeleteLoopById(int id);

        public List<RouteDomainModel> GetAllRoutes();
        public RouteDomainModel? FindRouteByID(int id);
        public void UpdateRouteByID(int id, int order);
        public int GetNumRoutes();
        public void CreateNewRoute(RouteDomainModel route);
        public void DeleteRouteById(int id);

        public List<Stop> GetAllStops();
        public Stop? FindStopByID(int id);
        public void UpdateStopByID(int id, string name, double latitude, double longitude);
        public int GetNumStops();
        public void CreateNewStop(Stop stop);
        public void DeleteStopById(int id);
    }
}