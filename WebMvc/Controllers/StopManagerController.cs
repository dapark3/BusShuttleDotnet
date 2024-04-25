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
    public class StopManagerController: Controller
    {

        private readonly ILogger<StopManagerController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public StopManagerController(ILogger<StopManagerController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            _logger.LogInformation("Accessed Stop Index Page");
            return View(_shuttleService.GetAllStops().Select(stop => StopViewModel.FromStop(stop)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult StopCreate()
        {
            _logger.LogInformation("Accessed Stop Create Page");
            return View(StopCreateModel.CreateStop(_shuttleService.GetAllStops().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> StopCreate([Bind("Id,Name,Latitude,Longitude,RouteId")] StopCreateModel stop)
        {
            if(!ModelState.IsValid) return View(stop);
            await Task.Run(() => _shuttleService.CreateNewStop(new Stop(stop.Id, stop.Name, stop.Latitude, stop.Longitude)));
            _logger.LogInformation("Created stop");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult StopUpdate([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Stop Update Page");
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Stop selectedStop = _shuttleService.FindStopByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.
            return View(StopUpdateModel.UpdateStop(selectedStop));
    #pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> StopUpdate(StopUpdateModel StopUpdateModel)
        {
            if(!ModelState.IsValid) return View(StopUpdateModel);
            await Task.Run(() => _shuttleService.UpdateStopByID(StopUpdateModel.Id, StopUpdateModel.Name, StopUpdateModel.Latitude, StopUpdateModel.Longitude));
            _logger.LogInformation("Updated Stop");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult StopDelete([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Stop Delete Page");
            return View(StopDeleteModel.DeleteStop(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> StopDelete(StopDeleteModel StopDeleteModel)
        {
            if(!ModelState.IsValid) return View(StopDeleteModel);
            await Task.Run(() => _shuttleService.DeleteStopById(StopDeleteModel.Id));
            _logger.LogInformation("Deleted Stop");
            return RedirectToAction("Index");
        }
    }
}