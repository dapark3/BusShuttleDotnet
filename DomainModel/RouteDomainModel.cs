using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class RouteDomainModel
    {
        [Key]
        public int Id {get; set;}
        public int Order {get; set;}
        public Stop Stop {get; set;}
        [ForeignKey("StopId")]
        public int StopId { get; set; }
        public Loop Loop {get;set;}
        [ForeignKey("LoopId")]
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