using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Driver
    {
        public int Id {get; set;}

        public string FirstName {get; set;}
        
        public string LastName {get; set;}

        public string Email {get; set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public bool Activated {get; set;}
        public Driver(int id, string firstName, string lastName, string email, bool activated)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Activated = activated;
        }
        
        public void Update(string newFirstName, string newLastName, bool newActivated)
        {
                FirstName = newFirstName;
                LastName = newLastName;
                Activated = newActivated;
        }
    }
}