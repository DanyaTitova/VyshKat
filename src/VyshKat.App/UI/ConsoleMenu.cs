using VyshKat.App.Models;
using VyshKat.App.Services;

namespace VyshKat.App.UI;

public class ConsoleMenu
{
    private readonly FleetManager _fleet;
    private readonly IConsoleAdapter _console;

    public ConsoleMenu(FleetManager fleet, IConsoleAdapter console)
    {
        _fleet = fleet;
        _console = console;
    }

    public void Run()
    {
        while (true)
        {
            _console.WriteLine("\n КИКШЕРИНГ \"ВЫШКАТ\" — МЕНЮ УПРАВЛЕНИЯ:");
            _console.WriteLine("1. Добавить новое транспортное средство (с техосмотром)");
            _console.WriteLine("2. Добавить вещь/оборудование на баланс");
            _console.WriteLine("3. Рассчитать суммарное суточное энергопотребление");
            _console.WriteLine("4. Вывести список транспорта для новичков (Простота >= 6)");
            _console.WriteLine("5. Вывести весь инвентарный опись (Техника + Вещи)");
            _console.WriteLine("0. Выход");
            _console.Write("Выберите пункт: ");

            var choice = _console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1": AddVehicleMenu(); break;
                case "2": AddItemMenu(); break;
                case "3": ShowTotalEnergy(); break;
                case "4": ShowBeginnerVehicles(); break;
                case "5": ShowAllInventory(); break;
                default: _console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    private void AddVehicleMenu()
    {
        _console.WriteLine("\nВыберите тип ТС");
        _console.WriteLine("1. Самокат | 2. Велосипед | 3. Электровелосипед");
        _console.Write("Тип: ");
        var type = _console.ReadLine();

        _console.Write("Инвентарный номер (int): ");
        if (!int.TryParse(_console.ReadLine(), out int invNum)) return;

        _console.Write("Модель: ");
        var model = _console.ReadLine();

        _console.Write("Суточное энергопотребление (кВт·ч): ");
        if (!double.TryParse(_console.ReadLine(), out double energy)) return;

        _console.Write("Простота для новичка (1-10): ");
        if (!int.TryParse(_console.ReadLine(), out int simplicity)) return;

        Vehicle? vehicle = type switch
        {
            "1" => new Scooter(invNum, model, energy, simplicity),
            "2" => new Bike(invNum, model, energy, simplicity),
            "3" => new EBike(invNum, model, energy, simplicity),
            _ => null
        };

        if (vehicle == null)
        {
            _console.WriteLine("Ошибка: некорректный тип ТС.");
            return;
        }

        bool accepted = _fleet.AddVehicle(vehicle);
        _console.WriteLine(accepted 
            ? $"[ОДОБРЕНО] {vehicle.Name} успешно прошёл техосмотр и добавлен в парк!" 
            : $"[ОТКЛОНЕН] {vehicle.Name} не прошёл техосмотр и отклонён!");
    }

    private void AddItemMenu()
    {
        _console.Write("Инвентарный номер (int): ");
        if (!int.TryParse(_console.ReadLine(), out int invNum)) return;

        _console.Write("Наименование вещи (например, Шлем M): ");
        var name = _console.ReadLine();

        _fleet.AddItem(new Item(invNum, name));
        _console.WriteLine($"Вещь \"{name}\" успешно поставлена на баланс.");
    }

    private void ShowTotalEnergy()
    {
        _console.WriteLine($"\nСуммарное энергопотребление флота: {_fleet.GetTotalDailyEnergy()} кВт·ч");
    }

    private void ShowBeginnerVehicles()
    {
        _console.WriteLine("\nТС для новичков (Простота >= 6)");
        foreach (var v in _fleet.GetBeginnerVehicles())
        {
            _console.WriteLine($"- Инв. №{v.InventoryNumber} | {v.Name} | Простота: {v.Simplicity}");
        }
    }

    private void ShowAllInventory()
    {
        _console.WriteLine("\n Полный инвентарный опись ");
        foreach (var item in _fleet.GetAllInventory())
        {
            _console.WriteLine($"Инв. №{item.InventoryNumber} | {item.Name}");
        }
    }
}