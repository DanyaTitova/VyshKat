using VyshKat.App.Abstractions;
using VyshKat.App.Models;

namespace VyshKat.App.Services;

public class FleetManager
{
    private readonly IServiceCenter _serviceCenter;
    private readonly List<Vehicle> _vehicles = new();
    private readonly List<IInventory> _inventoryItems = new();

    public FleetManager(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        if (_serviceCenter.Inspect(vehicle))
        {
            _vehicles.Add(vehicle);
            _inventoryItems.Add(vehicle);
            return true;
        }
        return false;
    }

    public void AddItem(IInventory item)
    {
        _inventoryItems.Add(item);
    }

    public double GetTotalDailyEnergy() => _vehicles.Sum(v => v.DailyEnergyConsumption);

    public IEnumerable<Vehicle> GetBeginnerVehicles() => _vehicles.Where(v => v.Simplicity >= 6);

    public IReadOnlyCollection<IInventory> GetAllInventory() => _inventoryItems.AsReadOnly();

    public int VehicleCount => _vehicles.Count;
}