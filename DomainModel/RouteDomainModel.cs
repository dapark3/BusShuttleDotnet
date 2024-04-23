using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class RouteDomainModel
    {
        public int Id {get; set;}
        public int Order {get; set;}
        public virtual Stop? Stop {get; set;}
        public int StopId { get; set; }
        public virtual Loop? Loop {get;set;}
        public int LoopId { get; set; }
        
        public RouteDomainModel(int id, int order)
        {
            Id = id;
            Order = order;
        }

        public void Update(int newOrder)
        {
            Order = newOrder;
        }

        public RouteDomainModel SetLoop(Loop loop)
        {
            Loop = loop;
            LoopId = loop.Id;
            return this;
        }

        public RouteDomainModel SetStop(Stop stop)
        {
            Stop = stop;
            StopId = stop.Id;
            return this;
        }
    }
}