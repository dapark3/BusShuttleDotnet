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
            return View(_shuttleService.GetAllRoutes().Select(route => RouteViewModel.FromRoute(route)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteCreate()
        {
            return View(RouteCreateModel.CreateRoute(_shuttleService.GetAllRoutes().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RouteCreate([Bind("Id,Order")] RouteCreateModel route)
        {
            if(!ModelState.IsValid) return View(route);
            await Task.Run(() => _shuttleService.CreateNewRoute(new RouteDomainModel(route.Id, route.Order)));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteUpdate([FromRoute] int id)
        {
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult RouteDelete([FromRoute] int id)
        {
            return View(RouteDeleteModel.DeleteRoute(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RouteDelete(RouteDeleteModel RouteDeleteModel)
        {
            if(!ModelState.IsValid) return View(RouteDeleteModel);
            await Task.Run(() => _shuttleService.DeleteRouteById(RouteDeleteModel.Id));
            return RedirectToAction("Index");
        }
    }
}