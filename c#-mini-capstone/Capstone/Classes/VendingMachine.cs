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
        // Properties
        private Dictionary<ItemType, VendingMachineItem[]> StockList { get; }
        private List<VendingMachineTransaction> TransactionLog { get; }
        public decimal CurrentBalance { get; private set; }

        // Constructor
        public VendingMachine(List<VendingMachineItem> items)
        {
            StockList = new Dictionary<ItemType, VendingMachineItem[]>();
            StockList.Add(ItemType.Candy, new VendingMachineItem[10]);
            StockList.Add(ItemType.Chip, new VendingMachineItem[10]);
            StockList.Add(ItemType.Drink, new VendingMachineItem[10]);
            StockList.Add(ItemType.Gum, new VendingMachineItem[10]);

            foreach (VendingMachineItem item in items)
            {
                StockList[item.Type][item.Slot] = item;
            }
        }

        // Methods
        public Dictionary<ItemType, VendingMachineItem[]> GetAllItems()
        {
            return StockList;
        }

        public VendingMachineTransaction FeedMoney(int amountToAdd)
        {
            List<int> validBills = new List<int> { 1, 2, 5, 10, 20 };
            bool isValid = validBills.Contains(amountToAdd);
            VendingMachineTransaction result;

            if (!isValid)
            {
                result = new VendingMachineTransaction(TransactionType.InvalidBill, 0);
                amountToAdd = 0;
            }
            else
            {
                result = new VendingMachineTransaction(TransactionType.FeedMoney, amountToAdd);
                CurrentBalance += amountToAdd;
            }

            return result;
        }

        public VendingMachineItem CheckItem(ItemType type, int slot)
        {
            throw new NotImplementedException();
        }

        public VendingMachineTransaction PurchaseItem(ItemType type, int slot)
        {
            throw new NotImplementedException();
        }

        public VendingMachineTransaction FinishTransaction()
        {
            throw new NotImplementedException();
        }

        public List<VendingMachineTransaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
