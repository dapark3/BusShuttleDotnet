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
        public void UpdateBusByID(int id);
        public int GetNumBuses();
        public void CreateNewBus(int id, int BusNumber);
        public void DeleteBusByID(int id);

        public List<Driver> GetAllDrivers();
        public Driver? FindDriverByID(int id);
        public void UpdateDriverByID(int id);
        public int GetNumDrivers();
        public void CreateNewDriver(int id, string FirstName, string LastName);
        public void DeleteDriverByID(int id);

        public List<Entry> GetAllEntries();
        public Entry? FindEntryByID(int id);
        public void UpdateEntryByID(int id);
        public int GetNumEntries();
        public void CreateNewEntry(int id, DateTime timestamp, int boarded, int LeftBehind);
        public void DeleteEntryByID(int id);

        public List<Loop> GetAllLoops();
        public Loop? FindLoopByID(int id);
        public void UpdateLoopByID(int id);
        public int GetNumLoops();
        public void CreateNewLoop(int id, string name);
        public void DeleteLoopById(int id);

        public List<RouteDomainModel> GetAllRoutes();
        public RouteDomainModel? FindRouteByID(int id);
        public void UpdateRouteByID(int id);
        public int GetNumRoutes();
        public void CreateNewRoute(int id, int order);
        public void DeleteRouteById(int id);

        public List<Stop> GetAllStops();
        public Stop? FindStopByID(int id);
        public void UpdateStopByID(int id);
        public int GetNumStops();
        public void CreateNewStop(int id, string name, double Latitude, double Longitude);
        public void DeleteStopById(int id);
    }
}