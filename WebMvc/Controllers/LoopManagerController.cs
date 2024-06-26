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
    public class LoopManagerController: Controller
    {
         private readonly ILogger<LoopManagerController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public LoopManagerController(ILogger<LoopManagerController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            _logger.LogInformation("Accessed Loop Index Page");
            return View(_shuttleService.GetAllLoops().Select(loop => LoopViewModel.FromLoop(loop)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopCreate()
        {
            _logger.LogInformation("Accessed Loop Create Page");
            return View(LoopCreateModel.CreateLoop(_shuttleService.GetAllLoops().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LoopCreate([Bind("Id,Name")] LoopCreateModel loop)
        {
            if(!ModelState.IsValid) return View(loop);
            await Task.Run(() => _shuttleService.CreateNewLoop(new Loop(loop.Id, loop.Name)));
            _logger.LogInformation("Created Loop");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopUpdate([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Loop Update Page");
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Loop selectedLoop = _shuttleService.FindLoopByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.
            return View(LoopUpdateModel.UpdateLoop(selectedLoop));
    #pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LoopUpdate(LoopUpdateModel LoopUpdateModel)
        {
            if(!ModelState.IsValid) return View(LoopUpdateModel);
            await Task.Run(() => _shuttleService.UpdateLoopByID(LoopUpdateModel.Id, LoopUpdateModel.Name));
            _logger.LogInformation("Updated loop");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopDelete([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Driver Delete Page");
            return View(LoopDeleteModel.DeleteLoop(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LoopDelete(LoopDeleteModel LoopDeleteModel)
        {
            if(!ModelState.IsValid) return View(LoopDeleteModel);
            await Task.Run(() => _shuttleService.DeleteLoopById(LoopDeleteModel.Id));
            _logger.LogInformation("Deleted Driver");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult UpdateRoutesInLoop([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Route in Loop Page");
            Loop? loop = _shuttleService.FindLoopByID(id);
            if(loop == null) return RedirectToAction("Index");
            return View(RoutesInLoopUpdateModel.FromLoop(id, loop.Routes));
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult AddRoute([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Route Add Page");
            List<RouteDomainModel> routes = _shuttleService.GetAllRoutes();
            return View(RoutesInLoopAddModel.FromId(id, routes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddRoute([Bind("LoopId,RouteId")] RoutesInLoopAddModel model)
        {
            if(!ModelState.IsValid) return View(model);
            RouteDomainModel? route = _shuttleService.FindRouteByID(model.RouteId);
            if(route == null) return View(model);
            await Task.Run(() => {
                Loop? loop = _shuttleService.FindLoopByID(model.LoopId) ?? throw new InvalidOperationException();
                route.SetLoop(loop);
                loop.AddRoute(route);
                _shuttleService.SaveChanges();
            });
            _logger.LogInformation("Added route to loop");
            return RedirectToAction("Index");
        }

    }
}