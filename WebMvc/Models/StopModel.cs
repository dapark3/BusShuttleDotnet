using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace WebMvc.Models
{
    public class StopDataModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public double Longitude {get;set;}
        public double Latitude {get;set;}

    }

    public class StopViewModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public double Longitude {get;set;}
        public double Latitude {get;set;}

        public static StopViewModel FromStop(Stop stop)
        {
            return new StopViewModel{
                Id = stop.Id,
                Name = stop.Name,
                Longitude = stop.Longitude,
                Latitude = stop.Latitude
            };
        }
    }
    
    public class StopCreateModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public double Longitude {get;set;}
        public double Latitude {get;set;}

        public static StopCreateModel CreateStop(int id)
        {
            return new StopCreateModel{
                Id = id,
                Name = "",
                Longitude = 0.0,
                Latitude = 0.0
            };
        }
    }
    
    public class StopUpdateModel
    {
       public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public double Longitude {get;set;}
        public double Latitude {get;set;}

        public static StopUpdateModel UpdateStop(Stop stop)
        {
            return new StopUpdateModel{
                Id = stop.Id,
                Name = stop.Name,
                Longitude = stop.Longitude,
                Latitude = stop.Latitude
            };
        } 
    }

    public class StopDeleteModel
    {
        public int Id {get;set;}

        public static StopDeleteModel DeleteStop(int id)
        {
            return new StopDeleteModel{
                Id = id
            };
        }
    }
}