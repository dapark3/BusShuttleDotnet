using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;

namespace WebMvc.Models
{
    public class BusDataModel
    {
        public int Id {get; set;}

        public int BusNumber {get; set;}
    }

    public class BusViewModel
    {
        public int Id {get;set;}
        public int BusNumber {get;set;}

        public static BusViewModel FromBus(Bus bus)
        {
            return new BusViewModel{
                Id = bus.Id,
                BusNumber = bus.BusNumber
            };
        }
    }

    public class BusCreateModel
    {
        public int Id {get;set;}
        public int BusNumber {get;set;}

        public static BusCreateModel CreateBus(int id)
        {
            return new BusCreateModel{
                Id = id,
                BusNumber = 7
            };
        }
    }

    public class BusUpdateModel
    {
        public int Id {get;set;}
        public int BusNumber {get;set;}

        public static BusUpdateModel UpdateBus(Bus bus)
        {
            return new BusUpdateModel{
                Id = bus.Id,
                BusNumber = bus.BusNumber
            };
        }
    }

    public class BusDeleteModel
    {
        public int Id {get;set;}

        public static BusDeleteModel DeleteBus(int id)
        {
            return new BusDeleteModel{
                Id = id
            };
        }
    }
}