using NSubstitute;
using VyshKat.App.Models;
using VyshKat.App.Services;
using Xunit;

namespace VyshKat.Tests;

public class FleetManagerTests
{
    [Fact]
    public void AddVehicle_ApprovedByServiceCenter_ShouldAddToFleet()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        var scooter = new Scooter(1, "Ninebot", 2.0, 8);
        fakeServiceCenter.Inspect(scooter).Returns(true);

        var manager = new FleetManager(fakeServiceCenter);
        bool result = manager.AddVehicle(scooter);

        Assert.True(result);
        Assert.Equal(1, manager.VehicleCount);
    }

    [Fact]
    public void AddVehicle_RejectedByServiceCenter_ShouldNotAddToFleet()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        var scooter = new Scooter(2, "Broken", 50.0, 3);
        fakeServiceCenter.Inspect(scooter).Returns(false);

        var manager = new FleetManager(fakeServiceCenter);
        bool result = manager.AddVehicle(scooter);

        Assert.False(result);
        Assert.Equal(0, manager.VehicleCount);
    }

    [Fact]
    public void GetBeginnerVehicles_ShouldFilterBySimplicitySixOrHigher()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        fakeServiceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);

        var manager = new FleetManager(fakeServiceCenter);
        manager.AddVehicle(new Scooter(1, "Easy", 2.0, 8));
        manager.AddVehicle(new EBike(2, "Hard", 5.0, 4));

        var beginnerVehicles = manager.GetBeginnerVehicles().ToList();

        Assert.Single(beginnerVehicles);
        Assert.Equal(1, beginnerVehicles[0].InventoryNumber);
    }

    [Fact]
    public void GetTotalDailyEnergy_ShouldCalculateSumCorrectly()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        fakeServiceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);

        var manager = new FleetManager(fakeServiceCenter);
        manager.AddVehicle(new Scooter(1, "V1", 3.5, 7));
        manager.AddVehicle(new EBike(2, "V2", 6.5, 7));

        Assert.Equal(10.0, manager.GetTotalDailyEnergy());
    }
}
