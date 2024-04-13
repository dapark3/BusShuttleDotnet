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
            return View(_shuttleService.GetAllLoops().Select(loop => LoopViewModel.FromLoop(loop)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopCreate()
        {
            return View(LoopCreateModel.CreateLoop(_shuttleService.GetAllLoops().Count() + 1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LoopCreate([Bind("Id,Name")] LoopCreateModel loop)
        {
            if(!ModelState.IsValid) return View(loop);
            await Task.Run(() => _shuttleService.CreateNewLoop(new Loop(loop.Id, loop.Name)));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopUpdate([FromRoute] int id)
        {
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult LoopDelete([FromRoute] int id)
        {
            return View(LoopDeleteModel.DeleteLoop(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> LoopDelete(LoopDeleteModel LoopDeleteModel)
        {
            if(!ModelState.IsValid) return View(LoopDeleteModel);
            await Task.Run(() => _shuttleService.DeleteLoopById(LoopDeleteModel.Id));
            return RedirectToAction("Index");
        }
    }
}