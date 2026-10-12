namespace VyshKat.App.Models;

public class Bike : Vehicle
{
    public Bike(int inventoryNumber, string name, double dailyEnergyConsumption, int simplicity)
        : base(inventoryNumber, name, dailyEnergyConsumption, simplicity) { }
}
