using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum ActionType
    {
        FeedMoney,
        GiveChange,
        PurchaseItem
    }
    public struct VendingMachineTransaction
    {
        public ActionType Action { get; }
        public DateTime Timestamp { get; }
        public VendingMachineItem Item { get; }
        public decimal Amount { get; }

        public VendingMachineTransaction(ActionType action, decimal amount) : 
            this(action, amount, null) { }

        public VendingMachineTransaction(ActionType action, VendingMachineItem item) :
            this(action, item.Price, item) { }

        public VendingMachineTransaction(ActionType action, decimal amount, VendingMachineItem item)
        {
            Action = action;
            Amount = amount;
            Item = item;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp} {Action} {Amount.ToString("C")}";
        }
    }
}
