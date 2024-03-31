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

    [Authorize("IsManager")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult BusView()
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

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult DriverView()
    {
        return View(_shuttleService.GetAllDrivers().Select(bus => DriverViewModel.FromDriver(bus)));
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult DriverCreate()
    {
        return View(DriverCreateModel.CreateDriver(_shuttleService.GetAllDrivers().Count() + 1));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DriverCreate([Bind("Id,FirstName,LastName")] DriverCreateModel driver)
    {
        if(!ModelState.IsValid) return View(driver);
#pragma warning disable CS8604 // Possible null reference argument.

        await Task.Run(() => _shuttleService.CreateNewDriver(new Driver(driver.Id, driver.FirstName, driver.LastName)));
#pragma warning restore CS8604 // Possible null reference argument.

        return RedirectToAction("DriverView");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult DriverUpdate([FromRoute] int id)
    {
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
    public async Task<IActionResult> DriverUpdate(DriverUpdateModel DriverUpdateModel)
    {
        if(!ModelState.IsValid) return View(DriverUpdateModel);
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
        await Task.Run(() => _shuttleService.UpdateDriverByID(DriverUpdateModel.Id, DriverUpdateModel.FirstName, DriverUpdateModel.LastName));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult DriverDelete([FromRoute] int id)
    {
        return View(DriverDeleteModel.DeleteDriver(id));
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DriverDelete(DriverDeleteModel DriverDeleteModel)
    {
        if(!ModelState.IsValid) return View(DriverDeleteModel);
        await Task.Run(() => _shuttleService.DeleteDriverByID(DriverDeleteModel.Id));
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult RouteView()
    {
        return View(_shuttleService.GetAllRoutes().Select(route => RouteViewModel.FromRoute(route)));
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult RouteCreate()
    {
        return View(RouteCreateModel.CreateRoute(_shuttleService.GetAllRoutes().Count() + 1));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> RouteCreate([Bind("Id,Order")] RouteCreateModel route)
    {
        if(!ModelState.IsValid) return View(route);
        await Task.Run(() => _shuttleService.CreateNewRoute(new RouteDomainModel(route.Id, route.Order)));
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult RouteUpdate([FromRoute] int id)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        RouteDomainModel selectedRoute = _shuttleService.FindRouteByID(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
        return View(RouteUpdateModel.FromRoute(selectedRoute));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> RouteUpdate(RouteUpdateModel RouteUpdateModel)
    {
        if(!ModelState.IsValid) return View(RouteUpdateModel);
        await Task.Run(() => _shuttleService.UpdateRouteByID(RouteUpdateModel.Id, RouteUpdateModel.Order));
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult RouteDelete([FromRoute] int id)
    {
        return View(RouteDeleteModel.DeleteRoute(id));
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> RouteDelete(RouteDeleteModel RouteDeleteModel)
    {
        if(!ModelState.IsValid) return View(RouteDeleteModel);
        await Task.Run(() => _shuttleService.DeleteRouteById(RouteDeleteModel.Id));
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult StopView()
    {
        return View(_shuttleService.GetAllStops().Select(stop => StopViewModel.FromStop(stop)));
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult StopCreate()
    {
        return View(StopCreateModel.CreateStop(_shuttleService.GetAllStops().Count() + 1));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> StopCreate([Bind("Id,Name,Latitude,Longitude,RouteId")] StopCreateModel stop)
    {
        if(!ModelState.IsValid) return View(stop);
        await Task.Run(() => _shuttleService.CreateNewStop(new Stop(stop.Id, stop.Name, stop.Latitude, stop.Longitude)));
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult StopUpdate([FromRoute] int id)
    {
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
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult StopDelete([FromRoute] int id)
    {
        return View(StopDeleteModel.DeleteStop(id));
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> StopDelete(StopDeleteModel StopDeleteModel)
    {
        if(!ModelState.IsValid) return View(StopDeleteModel);
        await Task.Run(() => _shuttleService.DeleteStopById(StopDeleteModel.Id));
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult LoopView()
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

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult EntryView()
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
