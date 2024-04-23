using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class BusManagerControllerTests
    {
        private static readonly string HomeAction = "Index";
        private static readonly List<Bus> testBuses = [ new(1, 69), new(2, 42) ];
        private static readonly Mock<IDatabaseService> mockDatabaseService = new();
        private readonly BusManagerController controller;

        public BusManagerControllerTests()
        {
            controller = new BusManagerController(mockDatabaseService.Object);
            mockDatabaseService.Setup(x => x.GetAll<Bus>()).Returns(testBuses);
            mockDatabaseService.Setup(x => x.GenerateId<Bus>()).Returns(testBuses.Count + 1);
        }

        [Fact]
        public void Index_Get()
        {
            IEnumerable<BusViewModel> viewModels = [
                BusViewModel.FromBus(testBuses[0]), BusViewModel.FromBus(testBuses[1])
            ];
            var result = (ViewResult) controller.Index();
            Assert.Equivalent(viewModels, result.Model);
        }

        [Fact]
        public void CreateBus_Get()
        {
            CreateBusModel creationModel = CreateBusModel.CreateBus(testBuses.Count + 1);
            var result = (ViewResult) controller.CreateBus();
            Assert.Equivalent(creationModel, result.Model);
        }

        [Fact]
        public async void CreateBus_Post()
        {
            Bus newBus = new(3, 89);
            CreateBusModel creationModel = new()
            {
                Id = newBus.Id, BusNumber = newBus.BusNumber
            };
            mockDatabaseService.Setup(x => x.CreateEntity(newBus));
            var result = (RedirectToActionResult) await controller.CreateBus(creationModel);
            Assert.Equal(HomeAction, result.ActionName);
        }

        [Fact]
        public void UpdateBus_Get()
        {
            Bus selectedBus = testBuses[0];
            mockDatabaseService.Setup(x => x.GetById<Bus>(selectedBus.Id)).Returns(selectedBus);
            EditBusModel expectedModel = EditBusModel.FromBus(selectedBus);
            var result = (ViewResult) controller.EditBus(selectedBus.Id);
            Assert.Equivalent(expectedModel, result.Model);
        }

        [Fact]
        public async void UpdateBus_Post()
        {
            const int updatedBusNumber = 0;
            EditBusModel editModel = EditBusModel.FromBus(testBuses[0]);
            editModel.BusNumber = updatedBusNumber;
            mockDatabaseService.Setup(x => x.UpdateById(editModel.Id, new Bus(editModel.Id, updatedBusNumber)));
            var result = (RedirectToActionResult) await controller.EditBus(editModel);
            Assert.Equal(HomeAction, result.ActionName);
        }

        [Fact]
        public void DeleteBus_Get()
        {
            Bus selectedBus = testBuses[1];
            mockDatabaseService.Setup(x => x.GetById<Bus>(selectedBus.Id)).Returns(selectedBus);
            DeleteBusModel expectedModel = DeleteBusModel.DeleteBus(selectedBus.Id);
            var result = (ViewResult) controller.DeleteBus(selectedBus.Id);
            Assert.Equivalent(expectedModel, result.Model);
        }

        [Fact]
        public async void DeleteBus_Post()
        {
            DeleteBusModel deleteModel = new()
            {
                Id = testBuses[1].Id
            };
            mockDatabaseService.Setup(x => x.DeleteById<Bus>(deleteModel.Id));
            var result = (RedirectToActionResult) await controller.DeleteBus(deleteModel);
            Assert.Equal(HomeAction, result.ActionName);
        }
    }
}