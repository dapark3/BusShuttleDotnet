using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Service;

using DomainModel;

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

    [Authorize("IsActivated")]
    public IActionResult Index()
    {
        _logger.LogInformation("Accessed Home Index Page");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogInformation("Error");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
