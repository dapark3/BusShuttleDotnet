using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Loop
    {
        public int Id {get; set;}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name {get; set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Loop(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}