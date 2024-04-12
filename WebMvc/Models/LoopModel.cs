using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using WebMvc.Service;

namespace WebMvc.Models
{

    public class LoopDataModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public class LoopViewModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static LoopViewModel FromLoop(Loop loop)
        {
            return new LoopViewModel{
                Id = loop.Id,
                Name = loop.Name
            };
        }
    }

    public class LoopCreateModel
    {

        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public List<RouteDomainModel> Routes {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static LoopCreateModel CreateLoop(int id)
        {
            BusShuttleService busShuttleService = new BusShuttleService();

            return new LoopCreateModel{
                Id = id,
                Name = "",
                Routes = busShuttleService.GetAllRoutes()
            };
        }
    }

    public class LoopUpdateModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static LoopUpdateModel UpdateLoop(Loop loop)
        {
            return new LoopUpdateModel{
                Id = loop.Id,
                Name = loop.Name
            };
        }
    }

    public class LoopDeleteModel
    {
        public int Id {get;set;}

        public static LoopDeleteModel DeleteLoop(int id)
        {
            return new LoopDeleteModel{
                Id = id
            };
        }
    }
}