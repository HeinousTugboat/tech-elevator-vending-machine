using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum ItemType
    {
        Chip = 'A',
        Candy = 'B',
        Drink = 'C',
        Gum = 'D'
    }
    public class VendingMachineItem
    {
        public ItemType Type { get; set; }
        public int Slot { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }

        public VendingMachineItem(string name, decimal price, ItemType type, int slot)
        {
            Name = name;
            Type = type;
            Slot = slot;
            Price = price;
            Quantity = 5;
        }
    }
}
