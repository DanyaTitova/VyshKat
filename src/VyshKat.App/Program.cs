using Microsoft.Extensions.DependencyInjection;
using VyshKat.App.Services;
using VyshKat.App.UI;

namespace VyshKat.App;

internal class Program
{
    private static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IServiceCenter, StandardServiceCenter>()
            .AddSingleton<FleetManager>()
            .AddSingleton<IConsoleAdapter, SystemConsoleAdapter>()
            .AddSingleton<ConsoleMenu>()
            .BuildServiceProvider();

        var menu = serviceProvider.GetRequiredService<ConsoleMenu>();
        menu.Run();
    }
}
