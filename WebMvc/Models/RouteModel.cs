using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace WebMvc.Models
{
    public class RouteDataModel
    {
        public int Id {get;set;}
        public int Order {get;set;}
    }

    public class RouteViewModel
    {
        public int Id {get;set;}
        public int Order {get;set;}

        public static RouteViewModel FromRoute(RouteDomainModel route)
        {
            return new RouteViewModel{
                Id = route.Id,
                Order = route.Order
            };
        }
    }

    public class RouteCreateModel
    {
        public int Id {get;set;}
        public int Order {get;set;}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Stop Stop {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static RouteCreateModel CreateRoute(int id)
        {
            return new RouteCreateModel{
                Id = id
            };
        }
    }

    public class RouteUpdateModel
    {
        public int Id {get;set;}
        public int Order {get;set;}

        public static RouteUpdateModel FromRoute(RouteDomainModel route)
        {
            return new RouteUpdateModel{
                Id = route.Id,
                Order = route.Order
            };
        }
    }

    public class RouteDeleteModel
    {
        public int Id {get;set;}

        public static RouteDeleteModel DeleteRoute(int id)
        {
            return new RouteDeleteModel{
                Id = id
            };
        }
    }
}