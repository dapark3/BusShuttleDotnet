using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Service;

namespace WebMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBusShuttleService _shuttleService;

    public HomeController(ILogger<HomeController> logger, IBusShuttleService shuttleService)
    {
        _logger = logger;
        _shuttleService = shuttleService;
    }

    [Authorize("IsManager")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Manager")]
    public IActionResult DriverView()
    {
        return View(_shuttleService.GetAllDrivers().Select(bus => DriverViewModel.FromDriver(bus)));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
