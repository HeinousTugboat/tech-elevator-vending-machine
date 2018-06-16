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

            TransactionLog = new List<VendingMachineTransaction>();
        }

        // Methods
        public Dictionary<ItemType, VendingMachineItem[]> GetAllItems()
        {
            // HACK: Extension idea: convert this into a safe method instead of returning reference to StockList.
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

            TransactionLog.Add(result);
            return result;
        }

        public VendingMachineItem CheckItem(ItemType type, int slot)
        {
            return StockList[type][slot];
        }

        public VendingMachineTransaction PurchaseItem(ItemType type, int slot)
        {
            VendingMachineTransaction result;

            if (StockList[type][slot] == null)
            {
                result = new VendingMachineTransaction(TransactionType.InvalidPurchase, 0, StockList[type][slot]);
            }
            else if (StockList[type][slot].Price > CurrentBalance)
            {
                result = new VendingMachineTransaction(TransactionType.NotSufficientFunds, StockList[type][slot]);
            }
            else if (StockList[type][slot].Quantity > 0)
            {
                CurrentBalance -= StockList[type][slot].Price;
                --StockList[type][slot].Quantity;
                result = new VendingMachineTransaction(TransactionType.PurchaseItem, StockList[type][slot]);
            }
            else
            {
                result = new VendingMachineTransaction(TransactionType.ItemOutOfStock, StockList[type][slot]);
            }

            TransactionLog.Add(result);
            return result;
        }

        public VendingMachineTransaction FinishTransaction()
        {
            VendingMachineTransaction transaction = new VendingMachineTransaction(TransactionType.GiveChange, CurrentBalance);
            CurrentBalance = 0;
            TransactionLog.Add(transaction);
            return transaction;
        }

        public List<VendingMachineTransaction> GetAllTransactions()
        {
            return TransactionLog;
        }
    }
}
