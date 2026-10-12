namespace VyshKat.App.Models;

public class EBike : Vehicle
{
    public EBike(int inventoryNumber, string name, double dailyEnergyConsumption, int simplicity)
        : base(inventoryNumber, name, dailyEnergyConsumption, simplicity) { }
}
