using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace WebMvc.Models
{
    public class DriverDataModel
    {
        public int Id {get;set;}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string FirstName {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? LastName {get;set;}
    }

    public class DriverViewModel
    {
        public int Id {get;set;}
        public string? FirstName {get;set;}
        public string? LastName {get;set;}

        public static DriverViewModel FromDriver(Driver driver)
        {
            return new DriverViewModel{
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName
            };
        }
    }

    public class DriverCreateModel
    {
        public int Id {get;set;}
        public string? FirstName {get;set;}
        public string? LastName {get;set;}

        public static DriverCreateModel CreateDriver(int id, string FirstName, string LastName)
        {
            return new DriverCreateModel{
                Id = id,
                FirstName = FirstName,
                LastName = LastName
            };
        }

    }

    public class DriverUpdateModel
    {
        public int Id {get;set;}
        public string? FirstName {get;set;}
        public string? LastName {get;set;}

        public static DriverUpdateModel UpdateDriver(int id, string FirstName, string LastName)
        {
            return new DriverUpdateModel{
                Id = id,
                FirstName = FirstName,
                LastName = LastName
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