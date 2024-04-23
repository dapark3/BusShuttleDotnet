using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class EntryManagerControllerTests
    {
        private static readonly string HomeAction = "Index";
        private static readonly List<Bus> testBuses = [ new(1, 69), new(2, 42) ];
        private static readonly List<Driver> testDrivers = [
            new(1, "Joe", "Momma", "123@abc.com", true),
            new(2, "Logan", "Parker", "lrparker2@bsu.edu", false)
        ];
        private static readonly List<Loop> testLoops = [
            new(1, "Joe Loop"),
            new(2, "Momma Loop")
        ];
        private static readonly List<Stop> testStops = [
            new(1, "Joe Stop", -9999, 9999),
            new(2, "Momma Stop", 0, 0),
        ];
        private static readonly List<Entry> testEntries = [
            new Entry(1, 5, 8).SetBus(testBuses[0]).SetDriver(testDrivers[0]).SetLoop(testLoops[0]).SetStop(testStops[0]),
            new Entry(2, 10, 3).SetBus(testBuses[1]).SetDriver(testDrivers[1]).SetLoop(testLoops[1]).SetStop(testStops[1])
        ];
        private static readonly CreateEntryModel creationModel = new() {
            Id = testEntries.Count + 1, 
            Boarded = 1, 
            LeftBehind = 10, 
            BusId = testBuses[0].Id, 
            DriverId = testDrivers[0].Id, 
            StopId = testStops[1].Id,
            LoopId = testLoops[1].Id
        };
        private static readonly UpdateEntryModel updatorModel = UpdateEntryModel.FromEntry(
            testEntries[0], testBuses, testDrivers, testLoops, testStops);
        private static readonly DeleteEntryModel deletionModel = DeleteEntryModel.DeleteEntry(testEntries[1].Id);

        private static readonly Mock<IDatabaseService> mockDatabaseService = new();
        private readonly EntryManagerController _controller;

        public EntryManagerControllerTests()
        {
            _controller = new EntryManagerController(mockDatabaseService.Object);
            mockDatabaseService.Setup(x => x.GetAll<Bus>()).Returns(testBuses);
            foreach(Bus bus in testBuses)
            {
                mockDatabaseService.Setup(x => x.GetById<Bus>(bus.Id)).Returns(bus);
            }
            mockDatabaseService.Setup(x => x.GetAll<Driver>()).Returns(testDrivers);
            foreach(Driver driver in testDrivers)
            {
                mockDatabaseService.Setup(x => x.GetById<Driver>(driver.Id)).Returns(driver);
            }
            mockDatabaseService.Setup(x => x.GetAll<Stop>()).Returns(testStops);
            foreach(Stop stop in testStops)
            {
                mockDatabaseService.Setup(x => x.GetById<Stop>(stop.Id)).Returns(stop);
            }
            mockDatabaseService.Setup(x => x.GetAll<Loop>()).Returns(testLoops);
            foreach(Loop loop in testLoops)
            {
                mockDatabaseService.Setup(x => x.GetById<Loop>(loop.Id)).Returns(loop);
            }
            mockDatabaseService.Setup(x => x.GetAll<Entry>()).Returns(testEntries);
            foreach(Entry entry in testEntries)
            {
                mockDatabaseService.Setup(x => x.GetById<Entry>(entry.Id)).Returns(entry);
            }
            mockDatabaseService.Setup(x => x.GenerateId<Entry>()).Returns(testBuses.Count + 1);
        }

        [Fact]
        public void Index_Get()
        {
            IEnumerable<EntryViewModel> entryViews = [
                EntryViewModel.FromEntry(testEntries[0]),
                EntryViewModel.FromEntry(testEntries[1])
            ];
            var result = (ViewResult) _controller.Index();
            Assert.Equivalent(entryViews, result.Model);
        }

        [Fact]
        public void CreateEntry_Get()
        {
            CreateEntryModel initialModel = CreateEntryModel.CreateEntry(testEntries.Count + 1, testBuses, testDrivers, testLoops, testStops);
            var result = (ViewResult) _controller.CreateEntry();
            var resultingModel = result.Model as CreateEntryModel ?? throw new Exception("Model isn't the correct type.");
            initialModel.Timestamp = resultingModel.Timestamp;
            Assert.Equivalent(initialModel, resultingModel);
        }

        [Fact]
        public async void CreateEntry_ModelError()
        {
            _controller.ModelState.AddModelError(string.Empty, "Invalid model");
            var result = (ViewResult) await _controller.CreateEntry(creationModel);
            Assert.Equal(creationModel, result.Model);
        }

        [Fact]
        public async void CreateEntry_Post()
        {
            Entry expectedEntry = new Entry(creationModel.Id, creationModel.Boarded, creationModel.LeftBehind)
                .SetBus(testBuses.Single(bus => bus.Id == creationModel.BusId))
                .SetDriver(testDrivers.Single(driver => driver.Id == creationModel.DriverId))
                .SetStop(testStops.Single(stop => stop.Id == creationModel.StopId))
                .SetLoop(testLoops.Single(loop => loop.Id == creationModel.LoopId));
            mockDatabaseService.Setup(x => x.CreateEntity(expectedEntry));
            var result = (RedirectToActionResult) await _controller.CreateEntry(creationModel);
            Assert.Equal(HomeAction, result.ActionName);
        }

        [Fact]
        public void UpdateEntry_Get()
        {
            var result = (ViewResult) _controller.UpdateEntry(updatorModel.Id);
            Assert.Equivalent(updatorModel, result.Model);
        }

        [Fact]
        public async void UpdateEntry_ReturnPageOnModelError()
        {
            _controller.ModelState.AddModelError(string.Empty, "Invalid model");
            var result = (ViewResult) await _controller.UpdateEntry(updatorModel);
            Assert.Equal(updatorModel, result.Model);
        }

        [Fact]
        public async void UpdateEntry_Post()
        {
            mockDatabaseService.Setup(x => x.UpdateById(updatorModel.Id, testEntries[0]));
            var result = (RedirectToActionResult) await _controller.UpdateEntry(updatorModel);
            Assert.Equal(HomeAction, result.ActionName);
        }

        [Fact]
        public void DeleteEntry_Get()
        {
            var result = (ViewResult) _controller.DeleteEntry(deletionModel.Id);
            Assert.Equivalent(deletionModel, result.Model);
        }

        [Fact]
        public async void DeleteEntry_ReturnPageOnModelError()
        {
            _controller.ModelState.AddModelError(string.Empty, "Invalid model");
            var result = (ViewResult) await _controller.DeleteEntry(deletionModel);
            Assert.Equal(deletionModel, result.Model);
        }

        [Fact]
        public async void DeleteEntry_Post()
        {
            mockDatabaseService.Setup(x => x.DeleteById<Entry>(deletionModel.Id));
            var result = (RedirectToActionResult) await _controller.DeleteEntry(deletionModel);
            Assert.Equal(HomeAction, result.ActionName);
        }
    }
}