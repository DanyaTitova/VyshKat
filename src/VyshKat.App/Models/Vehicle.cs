using VyshKat.App.Abstractions;

namespace VyshKat.App.Models;

public abstract class Vehicle : IInventory, IEnergyConsumer
{
    public int InventoryNumber { get; }
    public string Name { get; }
    public double DailyEnergyConsumption { get; }
    public int Simplicity { get; }

    protected Vehicle(int inventoryNumber, string name, double dailyEnergyConsumption, int simplicity)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
        DailyEnergyConsumption = dailyEnergyConsumption;
        Simplicity = Math.Clamp(simplicity, 1, 10);
    }
}