using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum TransactionType
    {
        // Feed money transactions
        FeedMoney,
        InvalidBill,

        // Purchase item transactions
        PurchaseItem,
        ItemOutOfStock,
        NotSufficientFunds,
        InvalidPurchase,

        // Finish transaction.. transactions
        GiveChange,
    }
    public struct VendingMachineTransaction
    {
        public TransactionType Type { get; }
        public DateTime Timestamp { get; }
        public VendingMachineItem Item { get; }
        public decimal Amount { get; }

        public VendingMachineTransaction(TransactionType type, decimal amount) : 
            this(type, amount, null) { }

        public VendingMachineTransaction(TransactionType type, VendingMachineItem item) :
            this(type, item.Price, item) { }

        public VendingMachineTransaction(TransactionType type, decimal amount, VendingMachineItem item)
        {
            Type = type;
            Amount = amount;
            Item = item;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp} {Type} {Amount.ToString("C")}";
        }
    }
}
