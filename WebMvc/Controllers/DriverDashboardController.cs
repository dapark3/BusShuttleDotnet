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
    public class DriverDashboardController: Controller
    {
        private readonly ILogger<DriverDashboardController> _logger;
        private readonly IBusShuttleService _shuttleService;

        public DriverDashboardController(ILogger<DriverDashboardController> logger, IBusShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        public IActionResult DriverDashboard()
        {
            return View(_shuttleService);
        }
    }
}