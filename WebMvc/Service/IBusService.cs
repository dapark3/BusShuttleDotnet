using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;

namespace WebMvc.Service
{
    public interface IBusService
    {
        public List<Bus> GetAllBuses();

        public Bus? FindBusByID(int id);

        public void UpdateBusByID(int id);

        public int GetNumBuses();

        public void CreateNewBus(int id, int BusNumber);
    }
}