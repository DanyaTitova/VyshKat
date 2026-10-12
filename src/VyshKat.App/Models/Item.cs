using VyshKat.App.Abstractions;

namespace VyshKat.App.Models;

public class Item : IInventory
{
    public int InventoryNumber { get; }
    public string Name { get; }

    public Item(int inventoryNumber, string name)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
    }
}