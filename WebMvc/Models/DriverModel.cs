using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace WebMvc.Models
{
    public class DriverViewModel
    {
        public int Id {get;set;}
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public string? Email { get; set; }

        public static DriverViewModel FromDriver(Driver driver)
        {
            return new DriverViewModel{
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email
            };
        }
    }

    public class DriverCreateModel
    {
        [Required]
        public int Id {get;set;}
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? FirstName {get;set;}
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? LastName {get;set;}
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public static DriverCreateModel CreateDriver(int id)
        {
            return new DriverCreateModel{
                Id = id,
                FirstName = "",
                LastName = "",
                Email = ""
            };
        }

    }

    public class DriverUpdateModel
    {
        public int Id {get;set;}
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public string? Email {get;set;}

        public static DriverUpdateModel UpdateDriver(Driver driver)
        {
            return new DriverUpdateModel{
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email
            };
        }
    }

    public class DriverDeleteModel
    {
        public int Id {get;set;}

        public static DriverDeleteModel DeleteDriver(int id)
        {
            return new DriverDeleteModel{
                Id = id
            };
        }
    }
}