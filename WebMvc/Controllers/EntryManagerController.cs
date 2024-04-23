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
    public class EntryManagerController: Controller
    {

        private readonly ILogger<EntryManagerController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public EntryManagerController(ILogger<EntryManagerController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View(_shuttleService.GetAllEntries().Select(entry => EntryViewModel.FromEntry(entry)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult EntryCreate()
        {
            int newId = _shuttleService.GenerateId();
            List<Bus> buses = _shuttleService.GetAllBuses();
            List<Driver> drivers = _shuttleService.GetAllDrivers();
            List<Stop> stops = _shuttleService.GetAllStops();
            List<Loop> loops = _shuttleService.GetAllLoops();
            EntryCreateModel creationModel = EntryCreateModel.CreateEntry(
                newId, buses, drivers, loops, stops);
        return View(creationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EntryCreate([Bind("Id,Timestamp,Boarded,LeftBehind,BusId,DriverId,LoopId,StopId")] EntryCreateModel entry)
        {
            if(!ModelState.IsValid) return View(entry);
             await Task.Run(() => {
                Entry newEntry = new Entry(entry.Id, entry.Boarded, entry.LeftBehind);
                newEntry.SetBus(_shuttleService.FindBusByID(entry.BusId) ?? throw new InvalidOperationException());
                newEntry.SetDriver(_shuttleService.FindDriverByID(entry.DriverId) ?? throw new InvalidOperationException());
                newEntry.SetLoop(_shuttleService.FindLoopByID(entry.LoopId) ?? throw new InvalidOperationException());
                newEntry.SetStop(_shuttleService.FindStopByID(entry.StopId) ?? throw new InvalidOperationException());
                _shuttleService.CreateNewEntry(newEntry);
            });
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult EntryUpdate([FromRoute] int id)
        {
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry selectedEntry = _shuttleService.FindEntryByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.
            return View(EntryUpdateModel.UpdateEntry(selectedEntry));
    #pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EntryUpdate(EntryUpdateModel EntryUpdateModel)
        {
            if(!ModelState.IsValid) return View(EntryUpdateModel);
            await Task.Run(() => _shuttleService.UpdateEntryByID(EntryUpdateModel.Id, EntryUpdateModel.Timestamp, EntryUpdateModel.Boarded, EntryUpdateModel.LeftBehind));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult EntryDelete([FromRoute] int id)
        {
            return View(EntryDeleteModel.DeleteEntry(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EntryDelete(EntryDeleteModel EntryDeleteModel)
        {
            if(!ModelState.IsValid) return View(EntryDeleteModel);
            await Task.Run(() => _shuttleService.DeleteEntryByID(EntryDeleteModel.Id));
            return RedirectToAction("Index");
        }
    }
}