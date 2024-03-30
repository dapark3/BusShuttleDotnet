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

        public RouteDomainModel(int id, int order)
        {
            Id = id;
            Order = order;
        }

        public void Update(int newOrder)
        {
            Order = newOrder;
        }
    }
}