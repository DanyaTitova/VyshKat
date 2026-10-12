using VyshKat.App.Models;

namespace VyshKat.App.Services;

public class StandardServiceCenter : IServiceCenter
{
    public bool Inspect(Vehicle vehicle)
    {
        return vehicle.DailyEnergyConsumption <= 50.0;
    }
}