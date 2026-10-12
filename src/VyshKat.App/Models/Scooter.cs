namespace VyshKat.App.Models;

public class Scooter : Vehicle
{
    public Scooter(int inventoryNumber, string name, double dailyEnergyConsumption, int simplicity)
        : base(inventoryNumber, name, dailyEnergyConsumption, simplicity) { }
}
