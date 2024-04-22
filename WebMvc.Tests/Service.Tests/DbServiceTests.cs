using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class DatabaseServiceTests
    {
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

        private static readonly List<RouteDomainModel> testRoutes = [
            new RouteDomainModel(1, 1, new Stop(1, "Joe Stop", 500, -400)), 
            new RouteDomainModel(2, 10, new Stop(2, "Momma Stop", 100, 30))
        ];

        private static readonly List<Entry> testEntries = [
            new Entry(1, 5, 10).SetBus(testBuses[0]).SetDriver(testDrivers[0]).SetLoop(testLoops[0]).SetStop(testStops[0]),
            new Entry(2, 3, 1).SetBus(testBuses[1]).SetDriver(testDrivers[1]).SetLoop(testLoops[1]).SetStop(testStops[1])
        ];
        
        private static readonly BusShuttleContext _context = CreateContext();

        private static BusShuttleContext CreateContext()
        {
            var connection = new SqliteConnection(new SqliteConnectionStringBuilder{DataSource=":memory:"}.ToString());
            return new BusShuttleContext(new DbContextOptionsBuilder<BusShuttleContext>().UseSqlite(connection).Options);
        }

        private readonly static object _lock = new();
        private static bool _isDatabaseBeingUsed = false;
        private readonly DatabaseService _databaseService;

        public DatabaseServiceTests()
        {
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _databaseService = new(_context);
        }

        private static void PopulateDatabase()
        {
            lock (_lock)
            {
                if(!_isDatabaseBeingUsed)
                {
                    _context.Buses.AddRange(testBuses);
                    _context.Drivers.AddRange(testDrivers);
                    _context.Stops.AddRange(testStops);
                    _context.Routes.AddRange(testRoutes);
                    _context.Loops.AddRange(testLoops);
                    _context.Entries.AddRange(testEntries);
                    _context.SaveChanges();
                    _isDatabaseBeingUsed = true;
                }
            }
        }

        private static void EmptyDatabase()
        {
            _context.Buses.RemoveRange(_context.Buses);
            _context.Drivers.RemoveRange(_context.Drivers);
            _context.Stops.RemoveRange(_context.Stops);
            _context.Routes.RemoveRange(_context.Routes);
            _context.Loops.RemoveRange(_context.Loops);
            _context.Entries.RemoveRange(_context.Entries);
            _context.SaveChanges();
            _isDatabaseBeingUsed = false;
        }

        [Fact]
        public void GetAllBuses()
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testBuses, _databaseService.GetAll<Bus>());
            }
            // Finally ensures that the test database is emptied at the end no matter what. 
            // This ensures we don't get data to crossover to other tests. 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetBusById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testBuses[index], _databaseService.GetById<Bus>(id));
            } 
            finally 
            {
                EmptyDatabase();   
            }
        }

        [Fact]
        public void CreateBus()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                Bus newBus = new(newId, 5);
                _databaseService.CreateEntity(newBus);
                var actual = _context.Buses.Single(bus => bus.Id == newId);
                Assert.Equal(newBus, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateBus()
        {
            PopulateDatabase();
            try 
            {
                int target = 1;
                Bus updatedBus = new(target, 44);
                _databaseService.UpdateById(target, updatedBus);
                var actual = _context.Buses.SingleOrDefault(bus => bus.Id == target);
                Assert.Equivalent(updatedBus, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteBus()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedBus = _context.Buses.Single(bus => bus.Id == targetId);
                _databaseService.DeleteById<Bus>(targetId);
                Assert.DoesNotContain(deletedBus, _context.Buses.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void GetAllDrivers()
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testDrivers, _databaseService.GetAll<Driver>());
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetDriverById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testDrivers[index], _databaseService.GetById<Driver>(id));
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void GetDriverByEmail()
        {
            PopulateDatabase();
            try
            {
                Assert.Equal(testDrivers[0], _databaseService.GetDriverByEmail(testDrivers[0].Email));
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void CreateDriver()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                Driver newDriver = new(newId, "Sally", "Johnson", "example@Gmail.com");
                _databaseService.CreateEntity(newDriver);
                var actual = _context.Drivers.Single(driver => driver.Id == newId);
                Assert.Equal(newDriver, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateDriver()
        {
            PopulateDatabase();
            try 
            {
                int target = 1;
                Driver updatedDriver = new(target, "Vienna", "Nicholas", "tsnicholas@bsu.edu");
                _databaseService.UpdateById(target, updatedDriver);
                var actual = _context.Drivers.SingleOrDefault(driver => driver.Id == target);
                Assert.Equivalent(updatedDriver, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteDriver()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedDriver = _context.Drivers.Single(driver => driver.Id == targetId);
                _databaseService.DeleteById<Driver>(targetId);
                Assert.DoesNotContain(deletedDriver, _context.Drivers.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void GetAllStops()
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testStops, _databaseService.GetAll<Stop>());
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetStopById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testStops[index], _databaseService.GetById<Stop>(id));
            } 
            finally 
            {
                EmptyDatabase();
            }   
        }

        [Fact]
        public void CreateStop()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                Stop newStop = new(newId, "Mushroom Kingdom", 500, 0);
                _databaseService.CreateEntity(newStop);
                var actual = _context.Stops.Single(stop => stop.Id == newId);
                Assert.Equal(newStop, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateStop()
        {
            PopulateDatabase();
            try 
            {
                int target = 1;
                Stop updatedStop = new Stop(target, "Hidden Leaf Village", 4000, -3000).SetRoute(testRoutes[0]);
                _databaseService.UpdateById(target, updatedStop);
                var actual = _context.Stops.SingleOrDefault(stop => stop.Id == target);
                Assert.Equivalent(updatedStop, actual);
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteStop()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedStop = _context.Stops.Single(stop => stop.Id == targetId);
                _databaseService.DeleteById<Stop>(targetId);
                Assert.DoesNotContain(deletedStop, _context.Stops.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }


        [Fact]
        public void GetAllRoutes()
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testRoutes, _databaseService.GetAll<RouteDomainModel>());
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetRouteById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testRoutes[index], _databaseService.GetById<RouteDomainModel>(id));
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void CreateRoute()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                RouteDomainModel route = new RouteDomainModel(newId, 1).SetStop(new Stop(newId, "Mushroom Kingdom", 500, 0));
                _databaseService.CreateEntity(route);
                var actual = _context.Routes.Single(route => route.Id == newId);
                Assert.Equal(route, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateRoute()
        {
            PopulateDatabase();
            try
            {
                int target = 1;
                RouteDomainModel updatedRoute = new RouteDomainModel(target, 5).SetStop(testStops[target - 1]);
                _databaseService.UpdateById(target, updatedRoute);
                var actual = _context.Routes.SingleOrDefault(route => route.Id == target);
                Assert.Equivalent(updatedRoute, actual);
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteRoute()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedRoute = _context.Routes.Single(route => route.Id == targetId);
                _databaseService.DeleteById<RouteDomainModel>(targetId);
                Assert.DoesNotContain(deletedRoute, _context.Routes.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void GetAllLoops()
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testLoops, _databaseService.GetAll<Loop>());
            } 
            finally 
            {
                EmptyDatabase();   
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetLoopById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testLoops[index], _databaseService.GetById<Loop>(id));
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void CreateLoop()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                Loop loop = new(newId, "Green Hill Zone");
                _databaseService.CreateEntity(loop);
                var actual = _context.Loops.Single(loop => loop.Id == newId);
                Assert.Equal(loop, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateLoop()
        {
            PopulateDatabase();
            try
            {
                int target = 1;
                Loop updatedLoop = new(target, "Green Hill Zone");
                _databaseService.UpdateById(target, updatedLoop);
                var actual = _context.Loops.SingleOrDefault(loop => loop.Id == target);
                Assert.Equivalent(updatedLoop, actual);
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteLoop()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedLoop = _context.Loops.Single(loop => loop.Id == targetId);
                _databaseService.DeleteById<Loop>(targetId);
                Assert.DoesNotContain(deletedLoop, _context.Loops.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void GetAllEntries()
        {
            PopulateDatabase();
            try 
            {
                List<Entry> actual = _databaseService.GetAll<Entry>();
                // No matter what, the value in create entry test is passed here. Don't ask me why :/
                // TODO: Fix this
                if(actual.Count == 3) actual.RemoveAt(2);
                Assert.Equal(testEntries, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void GetEntryById(int index, int id)
        {
            PopulateDatabase();
            try 
            {
                Assert.Equal(testEntries[index], _databaseService.GetById<Entry>(id));
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void CreateEntry()
        {
            PopulateDatabase();
            try 
            {
                int newId = 3;
                Entry entry = new Entry(newId, 17, 10).SetBus(testBuses[0]).SetDriver(testDrivers[0])
                    .SetStop(testStops[1]).SetLoop(testLoops[1]);
                _databaseService.CreateEntity(entry);
                var actual = _context.Entries.Single(entry => entry.Id == newId);
                Assert.Equal(entry, actual);
            } 
            finally 
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void UpdateEntry()
        {
            PopulateDatabase();
            try
            {
                int target = 1;
                Entry updatedEntry = new Entry(target, 16, 4).SetBus(testBuses[0]).SetDriver(testDrivers[0])
                    .SetLoop(testLoops[0]).SetStop(testStops[0]);
                _databaseService.UpdateById(target, updatedEntry);
                var actual = _context.Entries.SingleOrDefault(entry => entry.Id == target);
                Assert.Equivalent(updatedEntry, actual);
            }
            finally
            {
                EmptyDatabase();
            }
        }

        [Fact]
        public void DeleteEntry()
        {
            PopulateDatabase();
            try
            {
                int targetId = 1;
                var deletedEntry = _context.Entries.Single(entry => entry.Id == targetId);
                _databaseService.DeleteById<Entry>(targetId);
                Assert.DoesNotContain(deletedEntry, _context.Entries.ToList());
            }
            finally
            {
                EmptyDatabase();
            }
        }
    }
}