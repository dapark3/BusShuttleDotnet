using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class LoopManagerControllerTests
{
    private static readonly string HomeAction = "Index";
    private static readonly List<Loop> testLoops = [
        new(1, "Joe Loop"),
        new(2, "Momma Loop")
    ];
    private static readonly int mockRandomResult = 999;
    private static readonly List<RouteDomainModel> testRoutes = [
        new RouteDomainModel(1, 1, new Stop(1, "Joe Stop", 500, -400)), 
        new RouteDomainModel(2, 10, new Stop(2, "Momma Stop", 100, 30))
    ];
    private static readonly CreateLoopModel creationModel = new()
    {
        Id = mockRandomResult, Name = "Logan Loop"
    };
    private static readonly UpdateLoopModel updateModel = UpdateLoopModel.FromLoop(testLoops[0]);
    private static readonly AddRouteToLoopModel routeAdditionModel = AddRouteToLoopModel.FromId(testLoops[1].Id, testRoutes);
    private static readonly DeleteLoopModel deletionModel = DeleteLoopModel.DeleteLoop(testLoops[0].Id);

    private static readonly Mock<IDatabaseService> mockService = new();
    private readonly LoopManagerController controller;

    public LoopManagerControllerTests()
    {
        controller = new LoopManagerController(mockService.Object);
        mockService.Setup(x => x.GetAll<Loop>()).Returns(testLoops);
        mockService.Setup(x => x.GetById<Loop>(testLoops[0].Id)).Returns(testLoops[0]);
        mockService.Setup(x => x.GetById<Loop>(testLoops[1].Id)).Returns(testLoops[1]);
        mockService.Setup(x => x.GenerateId<Loop>()).Returns(mockRandomResult);
    }

    [Fact]
    public void Index_Get()
    {
        IEnumerable<LoopViewModel> expectedModels = [
            LoopViewModel.FromLoop(testLoops[0]),
            LoopViewModel.FromLoop(testLoops[1])
        ];
        var result = (ViewResult) controller.Index();
        Assert.Equivalent(expectedModels, result.Model);
    }

    [Fact]
    public void CreateLoop_Get()
    {
        CreateLoopModel initialModel = CreateLoopModel.CreateLoop(mockRandomResult);
        var result = (ViewResult) controller.CreateLoop();
        Assert.Equivalent(initialModel, result.Model);
    }

    [Fact]
    public async void CreateLoop_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, string.Empty);
        var result = (ViewResult) await controller.CreateLoop(creationModel);
        Assert.Equal(creationModel, result.Model);
    }

    [Fact]
    public async void CreateLoop_Post()
    {
        Loop expectedLoop = new(creationModel.Id, creationModel.Name);
        mockService.Setup(x => x.CreateEntity(expectedLoop));
        var result = (RedirectToActionResult) await controller.CreateLoop(creationModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void UpdateLoop_Get()
    {
        var result = (ViewResult) controller.UpdateLoop(updateModel.Id);
        Assert.Equivalent(updateModel, result.Model);
    }

    [Fact]
    public async void UpdateLoop_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, "error description");
        var result = (ViewResult) await controller.UpdateLoop(updateModel);
        Assert.Equal(updateModel, result.Model);
    }

    [Fact]
    public async void UpdateLoop_Post()
    {
        mockService.Setup(x => x.UpdateById(updateModel.Id, new Loop(updateModel.Id, updateModel.Name)));
        var result = (RedirectToActionResult) await controller.UpdateLoop(updateModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void DeleteLoop_Get()
    {
        var result = (ViewResult) controller.DeleteLoop(deletionModel.Id);
        Assert.Equivalent(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteLoop_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, "yet another error :/");
        var result = (ViewResult) await controller.DeleteLoop(deletionModel);
        Assert.Equal(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteLoop_Post()
    {
        mockService.Setup(x => x.DeleteById<Loop>(deletionModel.Id));
        var result = (RedirectToActionResult) await controller.DeleteLoop(deletionModel);
        Assert.Equal(HomeAction, result.ActionName);
    }
}
}