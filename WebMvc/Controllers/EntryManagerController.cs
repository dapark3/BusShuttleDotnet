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
            return View(EntryCreateModel.CreateEntry(_shuttleService.GetAllEntries().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EntryCreate([Bind("Id,Timestamp,Boarded,LeftBehind")] EntryCreateModel entry)
        {
            if(!ModelState.IsValid) return View(entry);
            await Task.Run(() => _shuttleService.CreateNewEntry(new Entry(entry.Id, entry.Boarded, entry.LeftBehind)));
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