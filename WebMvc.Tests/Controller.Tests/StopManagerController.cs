using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class StopManagerControllerTests
{
    private static readonly string HomeAction = "Index";
    private static readonly string mockError = "I'm error";
    private static readonly List<Stop> testStops = [
        new(1, "Joe Stop", -9999, 9999),
        new(2, "Momma Stop", 0, 0),
    ];
    private static readonly CreateStopModel creationModel = new() {
        Id = testStops.Count + 1, Name = "Mushroom Kingdom", Latitude = 500, Longitude = 1000
    };
    private static readonly UpdateStopModel updatorModel = UpdateStopModel.FromStop(testStops[0]);
    private static readonly DeleteStopModel deletionModel = DeleteStopModel.DeleteStop(testStops[0].Id);

    private static readonly Mock<IDatabaseService> mockDatabase = new();
    private readonly StopManagerController _controller;

    public StopManagerControllerTests()
    {
        _controller = new StopManagerController(mockDatabase.Object);
        mockDatabase.Setup(x => x.GetAll<Stop>()).Returns(testStops);
        mockDatabase.Setup(x => x.GetById<Stop>(testStops[0].Id)).Returns(testStops[0]);
        mockDatabase.Setup(x => x.GetById<Stop>(testStops[1].Id)).Returns(testStops[1]);
        mockDatabase.Setup(x => x.GenerateId<Stop>()).Returns(testStops.Count + 1);
    }

    [Fact]
    public void Index_Get()
    {
        IEnumerable<StopViewModel> viewModels = [
            StopViewModel.FromStop(testStops[0]),
            StopViewModel.FromStop(testStops[1])
        ];
        var result = (ViewResult) _controller.Index();
        Assert.Equivalent(viewModels, result.Model);
    }

    [Fact]
    public void CreateStop_Get()
    {
        CreateStopModel initialModel = CreateStopModel.CreateStop(testStops.Count + 1);
        var result = (ViewResult) _controller.CreateStop();
        Assert.Equivalent(initialModel, result.Model);
    }

    [Fact]
    public async void CreateStop_ModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, mockError);
        var result = (ViewResult) await _controller.CreateStop(creationModel);
        Assert.Equal(creationModel, result.Model);
    }

    [Fact]
    public async void CreateStop_Post()
    {
        Stop expectedStop = new (creationModel.Id, creationModel.Name, creationModel.Longitude, creationModel.Latitude);
        mockDatabase.Setup(x => x.CreateEntity(expectedStop));
        var result = (RedirectToActionResult) await _controller.CreateStop(creationModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void UpdateStop_Get()
    {
        var result = (ViewResult) _controller.UpdateStop(updatorModel.Id);
        Assert.Equivalent(updatorModel, result.Model);
    }

    [Fact]
    public async void UpdateStop_ModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, mockError);
        var result = (ViewResult) await _controller.UpdateStop(updatorModel);
        Assert.Equal(updatorModel, result.Model);
    }

    [Fact]
    public async void UpdateStop_Post()
    {
        Stop expectedStop = new(updatorModel.Id, updatorModel.Name, updatorModel.Longitude, updatorModel.Latitude);
        mockDatabase.Setup(x => x.UpdateById(updatorModel.Id, expectedStop));
        var result = (RedirectToActionResult) await _controller.UpdateStop(updatorModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void DeleteStop_Get()
    {
        var result = (ViewResult) _controller.DeleteStop(deletionModel.Id);
        Assert.Equivalent(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteStop_ModelError()
    {
        _controller.ModelState.AddModelError(string.Empty, "I'm in your walls.");
        var result = (ViewResult) await _controller.DeleteStop(deletionModel);
        Assert.Equal(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteStop_Post()
    {
        mockDatabase.Setup(x => x.DeleteById<Driver>(deletionModel.Id));
        var result = (RedirectToActionResult) await _controller.DeleteStop(deletionModel);
        Assert.Equal(HomeAction, result.ActionName);
    }
}
}