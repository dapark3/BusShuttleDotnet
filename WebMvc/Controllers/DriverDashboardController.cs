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
    public class DriverDashboardController: Controller
    {
        private readonly ILogger<DriverDashboardController> _logger;
        private readonly IBusShuttleService _shuttleService;
        private readonly IUserService _userService;

        private static readonly string BUS_ID_KEY = "busId";
        private static readonly string LOOP_ID_KEY = "loopId";

        public DriverDashboardController(ILogger<DriverDashboardController> logger, IBusShuttleService shuttleService, IUserService userService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult DriverDashboard()
        {
            _logger.LogInformation("Accessed Driver Dashboard.");
            List<Loop> loops = _shuttleService.GetAllLoops();
            List<Bus> buses = _shuttleService.GetAllBuses();
            return View(DriverDashboardModel.CreateUsingLists(loops, buses));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DriverDashboard([Bind ("LoopId,BusId")]DriverDashboardModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            RouteValueDictionary routeDictionary = [];
            await Task.Run(() => {
                routeDictionary.Add(BUS_ID_KEY, model.BusId);
                routeDictionary.Add(LOOP_ID_KEY, model.LoopId);
            });
            _logger.LogInformation("Redirected to Entry Page");
            return RedirectToAction("DriverEntry", routeDictionary);
        }

        [HttpGet]
        public async Task<IActionResult> DriverEntry([FromQuery] int busId, [FromQuery] int loopId)
        {
            _logger.LogInformation("Accessed Driver Entry Page.");
            string email = await _userService.GetUserEmail(ControllerContext.HttpContext);
            int nextId = _shuttleService.GenerateId();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Driver driver = _shuttleService.FindDriverByEmail(email);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            Bus? bus = _shuttleService.FindBusByID(busId);
            Loop? loop = _shuttleService.FindLoopByID(loopId);
            if(bus == null || loop == null)
            {
                return RedirectToAction("Index");
            }
            List<Stop> stops = GenerateStopList(loop);
#pragma warning disable CS8604 // Possible null reference argument.
            return View(EntrySelectModel.SelectEntry(nextId, bus, driver, loop, stops));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private static List<Stop> GenerateStopList(Loop loop)
        {
            List<Stop> output = [];
            foreach(var route in loop.Routes)
            {
                if(route.Stop == null) 
                {
                    continue;
                }
                output.Add(route.Stop);
            }
            return output;
        }

        [HttpPost]
        public async Task<IActionResult> DriverEntry([Bind("Id,Boarded,LeftBehind,BusId,DriverId,LoopId,StopId")]EntrySelectModel entry)
        {
            if(ModelState.IsValid)
            {
                await Task.Run(() => {
                Entry newEntry = new Entry(entry.Id, entry.Boarded, entry.LeftBehind);
                newEntry.SetBus(_shuttleService.FindBusByID(entry.BusId) ?? throw new InvalidOperationException());
                newEntry.SetDriver(_shuttleService.FindDriverByID(entry.DriverId) ?? throw new InvalidOperationException());
                newEntry.SetLoop(_shuttleService.FindLoopByID(entry.LoopId) ?? throw new InvalidOperationException());
                newEntry.SetStop(_shuttleService.FindStopByID(entry.StopId) ?? throw new InvalidOperationException());
                _shuttleService.CreateNewEntry(newEntry);
            });
            }
            RouteValueDictionary routeDictionary = [];
            routeDictionary.Add(BUS_ID_KEY, entry.BusId);
            routeDictionary.Add(LOOP_ID_KEY, entry.LoopId);
            _logger.LogInformation("Entry Created By Driver");
            return RedirectToAction("DriverEntry", routeDictionary);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}