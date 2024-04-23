using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using DomainModel;

namespace WebMvc.Models
{
    public class RoutesInLoopAddModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Loop Id.")]
        public int LoopId { get; set; }
        [Required]
        public List<RouteDomainModel> Routes { get; set; } = [];
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Route Id.")]
        public int RouteId { get; set; } = 1;

        public static RoutesInLoopAddModel FromId(int Id, List<RouteDomainModel> routes)
        {
            return new RoutesInLoopAddModel
            {
                LoopId = Id,
                Routes = routes
            };
        }
    }

    public class RoutesInLoopUpdateModel
    {
        public int LoopId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public List<RouteDomainModel> Routes { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static RoutesInLoopUpdateModel FromLoop(int loopId, List<RouteDomainModel> routes)
        {
            return new RoutesInLoopUpdateModel
            {
                LoopId = loopId,
                Routes = routes
            };
        }
    }
}