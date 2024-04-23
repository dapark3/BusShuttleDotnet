using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using DomainModel;

namespace WebMvc.Models
{
    public class DriverDashboardModel
    {
        [Required]
        public List<Loop> Loops { get; set; } = [];
        
        [Required]
        public List<Bus> Buses { get; set; } = [];
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Loop Id.")]
        [Display(Name = "Loop")]
        public int LoopId { get; set; } = -1;
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Bus Id.")]
        [Display(Name = "Bus Number")]
        public int BusId { get; set; } = -1;

        public static DriverDashboardModel CreateUsingLists(List<Loop> loops, List<Bus> buses)
        {
            return new DriverDashboardModel
            {
                Loops = loops,
                Buses = buses
            };
        }
    }
}