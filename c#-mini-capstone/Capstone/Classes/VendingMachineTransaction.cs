using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum TransactionType
    {
        FeedMoney,
        GiveChange,
        PurchaseItem
    }
    public struct VendingMachineTransaction
    {
        public TransactionType Type { get; }
        public DateTime Timestamp { get; }
        public VendingMachineItem Item { get; }
        public decimal Amount { get; }
        public bool IsValid { get; }

        public VendingMachineTransaction(TransactionType type, decimal amount, bool isValid) : 
            this(type, amount, null, isValid) { }

        public VendingMachineTransaction(TransactionType type, VendingMachineItem item, bool isValid) :
            this(type, item.Price, item, isValid) { }

        public VendingMachineTransaction(TransactionType type, decimal amount, VendingMachineItem item, bool isValid)
        {
            Type = type;
            Amount = amount;
            Item = item;
            IsValid = isValid;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp} {Type} {Amount.ToString("C")}";
        }
    }
}
