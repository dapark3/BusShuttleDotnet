using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class DriverManagerControllerTests
{
    private static readonly string HomeAction = "Index";
    private static readonly int mockRandomResult = 999;
    private static readonly List<Driver> testDrivers = [
        new(1, "Joe", "Momma", "123@abc.com", true),
        new(2, "Logan", "Parker", "lrparker2@bsu.edu", false)
    ];
    private static readonly CreateDriverModel creationModel = new()
    {
        Id = 3, FirstName = "Bill", LastName = "Gates", Email = "Watergate@outlook.com", Activated = true
    };
    private static readonly UpdateDriverModel updateModel = new()
    {
        Id = testDrivers[0].Id,
        FirstName = testDrivers[0].FirstName, 
        LastName = testDrivers[0].LastName,
        Email = testDrivers[0].Email
    };
    private static readonly DeleteDriverModel deletionModel = DeleteDriverModel.DeleteDriver(testDrivers[1].Id);

    private static readonly Mock<IAccountService> mockAccountService = new();
    private static readonly Mock<IDatabaseService> mockDatabaseService = new();
    private readonly DriverManagerController controller;

    public DriverManagerControllerTests()
    {
        controller = new(mockAccountService.Object, mockDatabaseService.Object);
        mockDatabaseService.Setup(x => x.GetAll<Driver>()).Returns(testDrivers);
        mockDatabaseService.Setup(x => x.GenerateId<Driver>()).Returns(mockRandomResult);
    }

    [Fact]
    public void Index_Get()
    {
        IEnumerable<DriverViewModel> expectedModels = [
            DriverViewModel.FromDriver(testDrivers[0]), 
            DriverViewModel.FromDriver(testDrivers[1])
        ];
        var result = (ViewResult) controller.Index();
        Assert.Equivalent(expectedModels, result.Model);
    }

    [Fact]
    public void CreateDriver_Get()
    {
        CreateDriverModel creationModel = CreateDriverModel.CreateDriver(mockRandomResult);
        var result = (ViewResult) controller.CreateDriver();
        Assert.Equivalent(creationModel, result.Model);
    }

    [Fact]
    public async void CreateDriver_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, "Invalid model");
        var result = (ViewResult) await controller.CreateDriver(creationModel);
        Assert.Equivalent(creationModel, result.Model);
    }

    [Fact]
    public async void CreateDriver_IdentityResultErrorReturnsPage()
    {
        IdentityResult identityResult = IdentityResult.Failed(
            new IdentityError(){Code = "This is a code", Description = "Insert some error here."},
            new IdentityError(){Code = "404", Description = "Quoth the Server"}
        );
        mockAccountService.Setup(x => x.CreateDriverAccount(creationModel.Email, creationModel.Password))
            .Returns(Task.FromResult(identityResult));
        var viewResult = (ViewResult) await controller.CreateDriver(creationModel);
        Assert.Equivalent(creationModel, viewResult.Model);
    }

    [Fact]
    public async void CreateDriver_Post()
    {
        mockAccountService.Setup(x => x.CreateDriverAccount(creationModel.Email, creationModel.Password))
            .Returns(Task.FromResult(IdentityResult.Success));
        Driver expectedDriver = new(creationModel.Id, creationModel.FirstName, creationModel.LastName, creationModel.Email);
        mockDatabaseService.Setup(x => x.CreateEntity(expectedDriver));
        var result = (RedirectToActionResult) await  controller.CreateDriver(creationModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void UpdateDriver_Get()
    {
        Driver selectedDriver = testDrivers[0];
        UpdateDriverModel updateModel = UpdateDriverModel.FromDriver(selectedDriver);
        mockDatabaseService.Setup(x => x.GetById<Driver>(selectedDriver.Id)).Returns(selectedDriver);
        var result = (ViewResult) controller.UpdateDriver(selectedDriver.Id);
        Assert.Equivalent(updateModel, result.Model);
    }

    [Fact]
    public async void UpdateDriver_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, "Invalid model");
        var result = (ViewResult) await controller.UpdateDriver(updateModel);
        Assert.Equivalent(updateModel, result.Model);
    }

    [Fact]
    public async void UpdateDriver_Post()
    {
        Driver expectedDriver = new(updateModel.Id, updateModel.FirstName, updateModel.LastName, updateModel.Email);
        mockDatabaseService.Setup(x => x.UpdateById(expectedDriver.Id, expectedDriver));
        var result = (RedirectToActionResult) await controller.UpdateDriver(updateModel);
        Assert.Equal(HomeAction, result.ActionName);
    }

    [Fact]
    public void DeleteDriver_Get()
    {
        var result = (ViewResult) controller.DeleteDriver(deletionModel.Id);
        Assert.Equivalent(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteDriver_ModelError()
    {
        controller.ModelState.AddModelError(string.Empty, "Invalid model");
        var result = (ViewResult) await controller.DeleteDriver(deletionModel);
        Assert.Equal(deletionModel, result.Model);
    }

    [Fact]
    public async void DeleteDriver_Post()
    {
        mockDatabaseService.Setup(x => x.DeleteById<Driver>(deletionModel.Id));
        var result = (RedirectToActionResult) await controller.DeleteDriver(deletionModel);
        Assert.Equal(HomeAction, result.ActionName);
    }
}
}