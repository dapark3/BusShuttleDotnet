using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

using DomainModel;
using WebMvc.Models;
using WebMvc.Service;

namespace WebMvc.Controllers
{
    public class RouteManagerController: Controller
    {
        private readonly ILogger<RouteManagerController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public RouteManagerController(ILogger<RouteManagerController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            _logger.LogInformation("Accessed Route Index Page");
            return View(_shuttleService.GetAllRoutes().Select(route => RouteViewModel.FromRoute(route)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteCreate()
        {
            _logger.LogInformation("Accessed Route Create Page");
            List<Stop> stops = _shuttleService.GetAllStops();
            foreach(Stop stop in stops.ToList())
            {
                if(stop.Route != null)
                {
                    stops.Remove(stop);
                }
            }
            List<Loop> loops = _shuttleService.GetAllLoops();
            return View(RouteCreateModel.CreateRoute(_shuttleService.GetAllRoutes().Count() + 1, stops, loops));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RouteCreate([Bind("Id,Order,StopId,LoopId")] RouteCreateModel route)
        {
            if(!ModelState.IsValid) return View(route);
            Stop? stop = _shuttleService.FindStopByID(route.StopId);
            if(stop == null) {
                ModelState.AddModelError(string.Empty, "Selected Stop doesn't exist.");
                return View(route);
            }
            Loop? loop = _shuttleService.FindLoopByID(route.LoopId);
            if(loop == null) {
                ModelState.AddModelError(string.Empty, "Selected Loop doesn't exist.");
                return View(route);
            }
            await Task.Run(() => {
                RouteDomainModel newRoute = new(route.Id, route.Order);
            newRoute.SetStop(stop);
            newRoute.SetLoop(loop);
            stop.SetRoute(newRoute);
            _shuttleService.CreateNewRoute(newRoute);
            });
            _logger.LogInformation("Created Route");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteUpdate([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Route Update Page");
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            RouteDomainModel selectedRoute = _shuttleService.FindRouteByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.
            return View(RouteUpdateModel.FromRoute(selectedRoute));
    #pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RouteUpdate(RouteUpdateModel RouteUpdateModel)
        {
            if(!ModelState.IsValid) return View(RouteUpdateModel);
            await Task.Run(() => _shuttleService.UpdateRouteByID(RouteUpdateModel.Id, RouteUpdateModel.Order));
            _logger.LogInformation("Updated Route");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteDelete([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Route Delete Page");
            return View(RouteDeleteModel.DeleteRoute(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RouteDelete(RouteDeleteModel RouteDeleteModel)
        {
            if(!ModelState.IsValid) return View(RouteDeleteModel);
            await Task.Run(() => _shuttleService.DeleteRouteById(RouteDeleteModel.Id));
            _logger.LogInformation("Deleted Route");
            return RedirectToAction("Index");
        }
    }
}