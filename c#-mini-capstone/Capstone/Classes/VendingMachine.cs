using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    interface IVendingMachine
    {
        // Functions that touch CurrentBalance
        VendingMachineTransaction FeedMoney(int amountToAdd);
        VendingMachineTransaction FinishTransaction();
        VendingMachineTransaction PurchaseItem(ItemType type, int slot);

        // Functions for Data Manager or User Interface to call
        VendingMachineItem CheckItem(ItemType type, int slot);
        Dictionary<ItemType, VendingMachineItem[]> GetAllItems();
        List<VendingMachineTransaction> GetAllTransactions();
    }

    public class VendingMachine : IVendingMachine
    {
        private Dictionary<ItemType, VendingMachineItem[]> StockList { get; }
        private List<VendingMachineTransaction> TransactionLog { get; }
        public decimal CurrentBalance { get; private set; }

        //public VendingMachine() : this(new List<VendingMachineItem>()) { }

        public VendingMachine(List<VendingMachineItem> list)
        {
            // Assign items to StockList
        }

        public VendingMachineTransaction FeedMoney(int amountToAdd)
        {
            throw new NotImplementedException();
        }

        public VendingMachineTransaction FinishTransaction()
        {
            throw new NotImplementedException();
        }

        public VendingMachineTransaction PurchaseItem(ItemType type, int slot)
        {
            throw new NotImplementedException();
        }

        public VendingMachineItem CheckItem(ItemType type, int slot)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ItemType, VendingMachineItem[]> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public List<VendingMachineTransaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
