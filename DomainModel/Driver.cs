using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Driver
    {
        public int Id {get; set;}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string FirstName {get; set;}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string LastName {get; set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Driver(int id, string firstName, string lastName)
        {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
        }
        
        public void Update(string newFirstName, string newLastName)
        {
                FirstName = newFirstName;
                LastName = newLastName;
        }
    }
}