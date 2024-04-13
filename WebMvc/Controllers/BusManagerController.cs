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
    public class BusManagerController: Controller
    {

        private readonly ILogger<BusManagerController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public BusManagerController(ILogger<BusManagerController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View(_shuttleService.GetAllBuses().Select(bus => BusViewModel.FromBus(bus)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult BusCreate()
        {
            return View(BusCreateModel.CreateBus(_shuttleService.GetAllBuses().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> BusCreate([Bind("Id,BusNumber")] BusCreateModel bus)
        {
            if(!ModelState.IsValid) return View(bus);
            await Task.Run(() => _shuttleService.CreateNewBus(new Bus(bus.Id, bus.BusNumber)));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult BusUpdate([FromRoute] int id)
        {
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Bus selectedBus = _shuttleService.FindBusByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.

            return View(BusUpdateModel.UpdateBus(selectedBus));
    #pragma warning restore CS8604 // Possible null reference argument.

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> BusUpdate(BusUpdateModel BusUpdateModel)
        {
            if(!ModelState.IsValid) return View(BusUpdateModel);
            await Task.Run(() => _shuttleService.UpdateBusByID(BusUpdateModel.Id, BusUpdateModel.BusNumber));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult BusDelete([FromRoute] int id)
        {
            return View(BusDeleteModel.DeleteBus(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> BusDelete(BusDeleteModel BusDeleteModel)
        {
            if(!ModelState.IsValid) return View(BusDeleteModel);
            await Task.Run(() => _shuttleService.DeleteBusByID(BusDeleteModel.Id));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}