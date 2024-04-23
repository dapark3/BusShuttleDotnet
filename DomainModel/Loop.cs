using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Loop
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public virtual List<RouteDomainModel> Routes {get; set;} = [];

        public Loop(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void AddRoute(RouteDomainModel route)
        {
            Routes.Add(route);
        }
    }
}