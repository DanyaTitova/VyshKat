using NSubstitute;
using VyshKat.App.Models;
using VyshKat.App.Services;
using VyshKat.App.UI;
using Xunit;

namespace VyshKat.Tests;

public class ConsoleMenuTests
{
    [Fact]
    public void AddVehicleMenu_ValidScooterInput_CallsFleetAddVehicle()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        fakeServiceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);

        var fleetManager = new FleetManager(fakeServiceCenter);
        var fakeConsole = Substitute.For<IConsoleAdapter>();

        fakeConsole.ReadLine().Returns("1", "1", "101", "Pro", "3,5", "8", "0");

        var menu = new ConsoleMenu(fleetManager, fakeConsole);

        menu.Run();

        Assert.Equal(1, fleetManager.VehicleCount);
        fakeConsole.Received().WriteLine(Arg.Is<string>(s => s.Contains("[ОДОБРЕНО]")));
    }

    [Fact]
    public void ShowBeginnerVehicles_OutputsOnlySuitableVehicles()
    {
        var fakeServiceCenter = Substitute.For<IServiceCenter>();
        fakeServiceCenter.Inspect(Arg.Any<Vehicle>()).Returns(true);

        var fleetManager = new FleetManager(fakeServiceCenter);
        fleetManager.AddVehicle(new Scooter(101, "EasyScooter", 2.0, 9));  // Подходит (9 >= 6)
        fleetManager.AddVehicle(new EBike(102, "HardBike", 5.0, 3));      // Не подходит (3 < 6)

        var fakeConsole = Substitute.For<IConsoleAdapter>();
        fakeConsole.ReadLine().Returns("4", "0");

        var menu = new ConsoleMenu(fleetManager, fakeConsole);

        menu.Run();
        fakeConsole.Received().WriteLine(Arg.Is<string>(s => s.Contains("EasyScooter")));
        fakeConsole.DidNotReceive().WriteLine(Arg.Is<string>(s => s.Contains("HardBike")));
    }
}