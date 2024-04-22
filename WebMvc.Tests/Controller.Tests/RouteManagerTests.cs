using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class RouteManagerControllerTests
{
    private static readonly string HomeAction = "Index";
    private static readonly string mockError = "I'm error";
    private static readonly List<Loop> testLoops = [
        new(1, "Joe Loop"),
        new(2, "Momma Loop")
    ];
    private static readonly List<RouteDomainModel> testRoutes = [
        new RouteDomainModel(1, 1, new Stop(1, "Joe Stop", 500, -400)), 
        new RouteDomainModel(2, 10, new Stop(2, "Momma Stop", 100, 30))
    ];
    private static readonly CreateRouteModel creationModel = new()
    {
        Id = testRoutes.Count + 1, Order = 3, StopId = testStops[2].Id, Stops = testStops
    };
    private static readonly UpdateRouteModel updatorModel = UpdateRouteModel.FromRoute(testRoutes[0]);
    private static readonly DeleteRouteModel deletionModel = DeleteRouteModel.DeleteRoute(testRoutes[1].Id);

    private static readonly Mock<IDatabaseService> mockDatabaseService = new();
    private readonly RouteManagerController _controller;

    public RouteManagerControllerTests()
    {
        _controller = new RouteManagerController(mockDatabaseService.Object);
        mockDatabaseService.Setup(x => x.GetAll<Stop>()).Returns(testStops);
        foreach(Stop stop in testStops.ToList())
        {
            mockDatabaseService.Setup(x => x.GetById<Stop>(stop.Id)).Returns(stop);
        }
        mockDatabaseService.Setup(x => x.GetAll<BusRoute>()).Returns(testRoutes);
        foreach(BusRoute route in testRoutes.ToList())
        {
            mockDatabaseService.Setup(x => x.GetById<BusRoute>(route.Id)).Returns(route);
        }
        mockDatabaseService.Setup(x => x.GenerateId<Stop>()).Returns(testStops.Count + 1);
        mockDatabaseService.Setup(x => x.GenerateId<BusRoute>()).Returns(testRoutes.Count + 1);
    }

    [Fact]
    public void Index_Get()
    {
        IEnumerable<RouteViewModel> viewModels = [
            RouteViewModel.FromRoute(testRoutes[0]),
            RouteViewModel.FromRoute(testRoutes[1])
        ];
        var result = (ViewResult) _controller.Index();
        Assert.Equivalent(viewModels, result.Model);
    }

    [Fact]
    public void CreateRoute_Get()
    {
        CreateRouteModel initialModel = CreateRouteModel.CreateRoute(testRoutes.Count + 1, testStops);
        var result = (ViewResult) _controller.CreateRoute();
        Assert.Equivalent(initialModel, result.Model);
    }

    [Fact]
    public async void CreateRoute_ReturnPageOnModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, mockError);
        var result = (ViewResult) await _controller.CreateRoute(creationModel);
        Assert.Equal(creationModel, result.Model);
    }

    [Fact]
    public async void CreateRoute_Post()
    {
        BusRoute expectedRoute = new BusRoute(creationModel.Id, creationModel.Order)
            .SetStop(creationModel.Stops.Single(stop => stop.Id == creationModel.StopId));
        mockDatabaseService.Setup(x => x.CreateEntity(expectedRoute));
        var result = (RedirectToActionResult) await _controller.CreateRoute(creationModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void UpdateRoute_Get()
    {
        var result = (ViewResult) _controller.UpdateRoute(updatorModel.Id);
        Assert.Equivalent(updatorModel, result.Model);
    }

    [Fact]
    public async void UpdateRoute_ReturnPageOnModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, mockError);
        var result = (ViewResult) await _controller.UpdateRoute(updatorModel);
        Assert.Equal(updatorModel, result.Model);
    }

    [Fact]
    public async void UpdateRoute_Post()
    {
        BusRoute expectedRoute = new(updatorModel.Id, updatorModel.Order);
        mockDatabaseService.Setup(x => x.UpdateById(updatorModel.Id, expectedRoute));
        var result = (RedirectToActionResult) await _controller.UpdateRoute(updatorModel);
        Assert.Equal(HomeAction, result.ActionName);
    }


    [Fact]
    public void DeleteRoute_Get()
    {
        var result = (ViewResult) _controller.DeleteRoute(deletionModel.Id);
        Assert.Equivalent(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteRoute_ReturnPageOnModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, mockError);
        var result = (ViewResult) await _controller.DeleteRoute(deletionModel);
        Assert.Equal(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteRoute_Post()
    {
        mockDatabaseService.Setup(x => x.DeleteById<BusRoute>(deletionModel.Id));
        var result = (RedirectToActionResult) await _controller.DeleteRoute(deletionModel);
        Assert.Equal(HomeAction, result.ActionName);
    }
}
}