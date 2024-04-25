using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Driver
    {
        [Key]
        public int Id {get; set;}

        public string FirstName {get; set;}
        
        public string LastName {get; set;}

        public string Email {get; set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Driver(int id, string firstName, string lastName, string email)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
        }
        
        public void Update(string newFirstName, string newLastName)
        {
                FirstName = newFirstName;
                LastName = newLastName;
        }
    }
}