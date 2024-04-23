using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Stop
    {
        public int Id {get; set;}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get; set;}

        public double Latitude {get; set;}

        public double Longitude {get; set;}

        public virtual RouteDomainModel? Route {get; set;}

        public Stop(int id, string name, double latitude, double longitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void Update(string newName, double newLatitude, double newLongitude)
        {
            Name = newName;
            Latitude = newLatitude;
            Longitude = newLongitude;
        }

        public Stop SetRoute(RouteDomainModel route)
        {
            Route = route;
            return this;
        }
    }
}