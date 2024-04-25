using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using DomainModel;
using WebMvc.Models;
using WebMvc.Service;

namespace WebMvc.Controllers
{
    public class DriverManagerController: Controller
    {
        private readonly ILogger<DriverManagerController> _logger;

        private readonly IBusShuttleService _shuttleService;

        private readonly IUserService _userService;

        private readonly UserManager<IdentityUser> _userManager;

        public DriverManagerController(ILogger<DriverManagerController> logger, IBusShuttleService shuttleService, UserManager<IdentityUser> userManager, IUserService userService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            _logger.LogInformation("Accessed Driver Index Page");
            return View(_shuttleService.GetAllDrivers().Select(bus => DriverViewModel.FromDriver(bus)));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult DriverUpdate([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Driver Update Page");
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Driver selectedDriver = _shuttleService.FindDriverByID(id);
    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    #pragma warning disable CS8604 // Possible null reference argument.
            return View(DriverUpdateModel.UpdateDriver(selectedDriver));
    #pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DriverUpdate([Bind("Id,FirstName,LastName") ]DriverUpdateModel DriverUpdateModel)
        {
            if(!ModelState.IsValid) return View(DriverUpdateModel);
    #pragma warning disable CS8604 // Possible null reference argument.
            await Task.Run(() => _shuttleService.UpdateDriverByID(DriverUpdateModel.Id, DriverUpdateModel.FirstName, DriverUpdateModel.LastName));
    #pragma warning restore CS8604 // Possible null reference argument.
            _logger.LogInformation("Updated Driver.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult DriverDelete([FromRoute] int id)
        {
            _logger.LogInformation("Accessed Driver Delete Page");
            return View(DriverDeleteModel.DeleteDriver(id));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DriverDelete(DriverDeleteModel DriverDeleteModel)
        {
            if(!ModelState.IsValid) return View(DriverDeleteModel);
            await Task.Run(() => _shuttleService.DeleteDriverByID(DriverDeleteModel.Id));
            _logger.LogInformation("Deleted Driver");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ActivateDriver([FromRoute] string id)
        {
            _logger.LogInformation("Activated Driver");
            await _userService.UpdateAccountActivation(id, true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateDriver([FromRoute] string id)
        {
            _logger.LogInformation("Deactivated Driver");
            await _userService.UpdateAccountActivation(id, false);
            return RedirectToAction("Index");
        }
    }
}