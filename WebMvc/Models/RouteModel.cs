using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using DomainModel;

namespace WebMvc.Models
{
    public class RouteViewModel
    {
        public int Id {get;set;}
        public int Order {get;set;}
        public int StopId {get;set;}

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
        [Required]
        public int Id {get;set;}
        [Required]
        public int Order {get;set;}

        [Required]
        public int StopId {get;set;}
        [Required]
        public List<Stop> Stops {get;set;}
        
        [Required]
        public int LoopId {get;set;}
        [Required]
        public List<Loop> Loops {get;set;}

        public static RouteCreateModel CreateRoute(int id, List<Stop> stops, List<Loop> loops)
        {
            return new RouteCreateModel{
                Id = id,
                Order = 0,
                Stops = stops,
                Loops = loops
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