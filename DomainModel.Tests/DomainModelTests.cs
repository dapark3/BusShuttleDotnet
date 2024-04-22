using DomainModel;

namespace DomainModel.Tests;

public class BusTests
{
    private static readonly int id = 0;
    private static readonly int busNumber = 0;

    [Fact]
    public void CreateBus()
    {
        Bus bus = new Bus(id, busNumber);
        Assert.Equal(id, bus.Id);
        Assert.Equal(busNumber, bus.BusNumber);
    }

    [Fact]
    public void UpdateBus()
    {
        Bus bus = new Bus(id, busNumber);
        const newBusNumber = 1;
        Bus updatedBus = new Bus(id, newBusNumber);
        bus.Update(updatedBus);
        Assert.Equal(newBusNumber, bus.BusNumber);
    }
}

public class DriverTests
{
    private static readonly int id = 0;
    private static readonly string firstName = "Joe";
    private static readonly string lastName = "Momma";
    private static readonly string email = "abc@123.com";
    private static readonly bool activated = true;

    [Fact]
    public void CreateDriver()
    {
        Driver driver = new Driver(id, firstName, lastName, email, activated);
        Assert.Equal(id, driver.Id);
        Assert.Equal(firstName, driver.FirstName);
        Assert.Equal(lastName, driver.LastName);
        Assert.Equal(email, driver.Email);
        Assert.Equal(activated, driver.Activated);
    }

    [Fact]
    public void UpdateDriver()
    {
        Driver driver = new Driver(id, firstName, lastName, email, activated);
        const newFirstName = "Joeseph";
        const newLastName = "Mother";
        const newEmail = "123@abc.com";
        const newActivated = false;
        Driver updatedDriver = new Driver(id, newFirstName, newLastName, newEmail, newActivated);
        driver.Update(updatedDriver);
        Assert.Equal(newFirstName, driver.FirstName);
        Assert.Equal(newLastName, driver.LastName);
        Assert.Equal(newEmail, driver.Email);
        Assert.Equal(newActivated, driver.Activated);
    }
}

public class EntryTests
{
    private static readonly int id = 0;
    private static readonly DateTime timestamp = new DateTime(2024, 4, 22, 4, 1, 50);
    private static readonly int boarded = 1;
    private static readonly int leftBehind = 2;
    private static readonly Bus bus = new Bus(id, 1);
    private static readonly Driver driver = new Driver(id, "Joe", "Momma", "abc@123.com", true);
    private static readonly Loop loop = new Loop(id, "Joe Loop");
    private static readonly Stop stop = new Stop(id, "Joe Stop", 1.11, 2.22);

    [Fact]
    public void CreateEntry()
    {
        Entry entry = new Entry(id, timestamp, boarded, leftBehind, bus, driver, loop, stop);
        Assert.Equal(id, entry.Id);
        Assert.Equal(timestamp, entry.Timestamp);
        Assert.Equal(boarded, entry.Boarded);
        Assert.Equal(leftBehind, entry.LeftBehind);
        Assert.Equal(bus, entry.Bus);
        Assert.Equal(driver, entry.Driver);
        Assert.Equal(loop, entry.Loop);
        Assert.Equal(stop, entry.Stop);
    }

    [Fact]
    public void UpdateEntry()
    {
        Entry entry = new Entry(id, timestamp, boarded, leftBehind, bus, driver, loop, stop);
        const newTimestamp = new DateTime(2023, 4, 22, 4, 2, 50);
        const newBoarded = 5;
        const newLeftBehind = 0;
        const newBus = new Bus(id, 1);
        const newDriver = new Driver(id, "Joe", "Momma", "abc@123.com", true);
        const newLoop = new Loop(id, "Joe Loop");
        const newStop = new Stop(id, "Joe Stop", 1.11, 2.22);
        Entry updatedEntry = new Entry(id, newTimestamp, newBoarded, newLeftBehind, newBus, newDriver, newLoop, newStop);
        entry.Update(updatedEntry);
        Assert.Equal(newTimestamp, entry.Timestamp);
        Assert.Equal(newBoarded, entry.Boarded);
        Assert.Equal(newLeftBehind, entry.LeftBehind);
        Assert.Equal(newBus, entry.Bus);
        Assert.Equal(newDriver, entry.Driver);
        Assert.Equal(newLoop, entry.Loop);
        Assert.Equal(newStop, entry.Stop);
    }
}

public class LoopTests
{
    private static readonly int id = 0;
    private static readonly string name = "Joe Loop";

    private static readonly List<RouteDomainModel> routes = new List<RouteDomainModel>();

    [Fact]
    public void CreateLoop()
    {
        Loop loop = new Loop(id, name, routes);
        Assert.Equal(id, loop.Id);
        Assert.Equal(name, loop.Name);
        Assert.Equal(routes, loop.Routes);
    }

    [Fact]
    public void UpdateLoop()
    {
        Loop loop = new Loop(id, name);
        const newName = "Momma Loop";
        Loop updatedLoop = new Loop(id, newName, routes);
        loop.Update(updatedLoop);
        Assert.Equal(newName, loop.Name);
    }
}

public class RouteTests
{
    private static readonly int id = 0;
    private static readonly int order = 1;
    private static readonly Stop stop = new Stop(id, "Joe Stop", 1.11, 2.22);

    [Fact]
    public void CreateRoute()
    {
        RouteDomainModel route = new Route(id, order, stop);
        Assert.Equal(id, route.Id);
        Assert.Equal(order, route.Order);
        Assert.Equal(stop, route.Stop);
    }

    [Fact]
    public void UpdateRoute()
    {
        RouteDomainModel route = new Route(id, order, stop);
        const newOrder = "Momma Loop";
        const newStop = new Stop(id, "Momma Stop", 3.33, 4.22);
        RouteDomainModel updatedRoute = new Route(id, newOrder, newStop);
        route.Update(updatedRoute);
        Assert.Equal(newOrder, route.Order);
        Assert.Equal(newStop, route.Stop);
    }
}

public class StopTests
{
    private static readonly int id = 0;
    private static readonly string name = "Joe Stop";
    private static readonly double latitude = 1.11;
    private static readonly double longitude = 2.22;

    [Fact]
    public void CreateStop()
    {
        Stop stop = new Stop(id, name, latitude, longitude);
        Assert.Equal(id, stop.Id);
        Assert.Equal(name, stop.Name);
        Assert.Equal(latitude, stop.Latitude);
        Assert.Equal(longitude, stop.Longitude);
    }

    [Fact]
    public void UpdateStop()
    {
        Stop stop = new Stop(id, name, latitude, longitude);
        const newName = "Momma Loop";
        const newLatitude = 4.22;
        const newLongitude = 3.33;
        Stop updatedStop = new Stop(id, newName, newLatitude, newLongitude);
        stop.Update(updatedStop);
        Assert.Equal(newName, stop.Name);
        Assert.Equal(newLatitude, stop.Latitude);
        Assert.Equal(newLongitude, stop.Longitude);
    }
}