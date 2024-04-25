using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Loop
    {
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        public List<RouteDomainModel> Routes {get; set;} = [];

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