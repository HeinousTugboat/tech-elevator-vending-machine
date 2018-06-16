using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        private static void AssertTransaction(VendingMachineTransaction expectedResult, VendingMachineTransaction result)
        {
            Assert.AreEqual(expectedResult.Amount, result.Amount, "\nTransaction amount mismatch.");
            Assert.AreEqual(expectedResult.Type, result.Type, "\nDifferent transaction types.");
            Assert.AreEqual(expectedResult.Item, result.Item, "\nTransaction item mismatch.");
        }

        [TestMethod]
        public void VendingMachine_Initial()
        {
            // Arrange
            List<VendingMachineItem> myVendingMachineItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 10.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 15.00M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 20.00M, ItemType.Gum, 4)
            };

            // Act
            VendingMachine result = new VendingMachine(myVendingMachineItems);

            // Assert
            Assert.AreEqual(0.00M, result.CurrentBalance);
        }

        [TestMethod]
        public void VendingMachine_GetAllItems()
        {
            // Arrange
            List<VendingMachineItem> exampleItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 10.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 15.00M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 20.00M, ItemType.Gum, 4)
            };
            VendingMachine vendingMachine = new VendingMachine(exampleItems);

            // Act
            Dictionary<ItemType, VendingMachineItem[]> result = vendingMachine.GetAllItems();

            Dictionary<ItemType, VendingMachineItem[]> expectedResult = new Dictionary<ItemType, VendingMachineItem[]>
            {
                { ItemType.Candy, new VendingMachineItem[10]
                    { null, null, null, null, null, exampleItems[0], null, null, null, null } },
                { ItemType.Drink, new VendingMachineItem[10]
                    { null, null, null, null, null, exampleItems[1], null, null, null, null } },
                { ItemType.Gum, new VendingMachineItem[10]
                    { null, null, null, null, exampleItems[2], null, null, null, null, null } },
                { ItemType.Chip, new VendingMachineItem[10]
                    { null, null, null, null, null, null, null, null, null, null } }
            };

            // Assert
            foreach (KeyValuePair<ItemType, VendingMachineItem[]> kvp in expectedResult)
            {
                CollectionAssert.AreEqual(kvp.Value, result[kvp.Key]);
            }
        }

        [TestMethod]
        public void VendingMachine_FeedMoney()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine(new List<VendingMachineItem>());
            VendingMachineTransaction expectedResult = new VendingMachineTransaction(TransactionType.FeedMoney, 20);

            // Act -- Tests for $20 inserted
            VendingMachineTransaction result = vendingMachine.FeedMoney(20);

            // Assert
            Assert.AreEqual(20, vendingMachine.CurrentBalance);
            AssertTransaction(expectedResult, result);

            // Act part 2 -- Tests for "-$20" inserted
            result = vendingMachine.FeedMoney(-20);
            expectedResult = new VendingMachineTransaction(TransactionType.InvalidBill, 0);

            // Assert part 2
            Assert.AreEqual(20, vendingMachine.CurrentBalance);
            AssertTransaction(expectedResult, result);

            // Act part 3 -- Tests for "$10" inserted
            result = vendingMachine.FeedMoney(10);
            expectedResult = new VendingMachineTransaction(TransactionType.FeedMoney, 10);

            // Assert part 3
            Assert.AreEqual(30, vendingMachine.CurrentBalance);
            AssertTransaction(expectedResult, result);

            // Act part 4 -- Tests for "$6" inserted
            result = vendingMachine.FeedMoney(6);
            expectedResult = new VendingMachineTransaction(TransactionType.InvalidBill, 0);

            // Assert part 4
            Assert.AreEqual(30, vendingMachine.CurrentBalance);
            AssertTransaction(expectedResult, result);
        }

       

        [TestMethod]
        public void VendingMachine_CheckItem()
        {
            // Arrange
            List<VendingMachineItem> exampleItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 10.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 15.00M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 20.00M, ItemType.Gum, 4)
            };
            VendingMachine vendingMachine = new VendingMachine(exampleItems);

            // Act
            VendingMachineItem result = vendingMachine.CheckItem(ItemType.Candy, 1);
            VendingMachineItem expectedResult = null;

            // Assert
            Assert.AreEqual(expectedResult, result);

            // Act part 2
            result = vendingMachine.CheckItem(ItemType.Candy, 5);
            expectedResult = exampleItems[0];

            // Assert part 2
            Assert.AreEqual(expectedResult, result);

            // Act part 3
            result = vendingMachine.CheckItem(ItemType.Drink, 5);
            expectedResult = exampleItems[1];

            // Assert part 3;
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void VendingMachine_PurchaseItem()
        {
            // Purchase item transactions
            //InvalidPurchase,

            // Arrange
            List<VendingMachineItem> exampleItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 1.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 1.25M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 2.00M, ItemType.Gum, 4)
            };
            VendingMachine vendingMachine = new VendingMachine(exampleItems);
            vendingMachine.FeedMoney(10);

            // Act
            VendingMachineTransaction result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            VendingMachineTransaction expectedResult = new VendingMachineTransaction(TransactionType.PurchaseItem, exampleItems[0]);

            // Assert
            AssertTransaction(expectedResult, result);

            // Act part 2
            // Item out of Stock..
            result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            result = vendingMachine.PurchaseItem(ItemType.Candy, 5);
            expectedResult = new VendingMachineTransaction(TransactionType.ItemOutOfStock, exampleItems[0]);

            // Assert part 2
            AssertTransaction(expectedResult, result);

            // Act part 3
            // NotSufficientFunds..
            result = vendingMachine.PurchaseItem(ItemType.Gum, 4);
            result = vendingMachine.PurchaseItem(ItemType.Gum, 4);
            result = vendingMachine.PurchaseItem(ItemType.Drink, 5);
            expectedResult = new VendingMachineTransaction(TransactionType.NotSufficientFunds, exampleItems[1]);

            // Assert part 3
            AssertTransaction(expectedResult, result);

            // Act part 4
            // InvalidPurchase
            vendingMachine.FeedMoney(5);
            result = vendingMachine.PurchaseItem(ItemType.Gum, 7);
            expectedResult = new VendingMachineTransaction(TransactionType.InvalidPurchase, 0, null);

            // Assert part 4
            AssertTransaction(expectedResult, result);
        }

        [TestMethod]
        public void VendingMachine_FinishTransaction()
        {
            // Arrange
            List<VendingMachineItem> exampleItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 1.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 1.25M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 2.00M, ItemType.Gum, 4)
            };
            VendingMachine vendingMachine = new VendingMachine(exampleItems);

            // Act
            VendingMachineTransaction result = vendingMachine.FinishTransaction();
            VendingMachineTransaction expectedResult = new VendingMachineTransaction(TransactionType.GiveChange, 0);

            // Assert
            AssertTransaction(expectedResult, result);

            // Arrange part 2
            vendingMachine.FeedMoney(5);
            vendingMachine.PurchaseItem(ItemType.Candy, 5);
            vendingMachine.PurchaseItem(ItemType.Drink, 5);
            vendingMachine.PurchaseItem(ItemType.Gum, 4);
            // Expect $0.75

            Assert.AreEqual(0.75M, vendingMachine.CurrentBalance);

            // Act part 2
            result = vendingMachine.FinishTransaction();
            expectedResult = new VendingMachineTransaction(TransactionType.GiveChange, 0.75M);

            // Assert part 2
            AssertTransaction(expectedResult, result);
        }

        [TestMethod]
        public void VendingMachine_GetAllTransactions()
        {
            // Arrange
            List<VendingMachineItem> exampleItems = new List<VendingMachineItem>
            {
                new VendingMachineItem("Butterfinger", 1.00M, ItemType.Candy, 5),
                new VendingMachineItem("Coke", 1.25M, ItemType.Drink, 5),
                new VendingMachineItem("Bubbleyum", 2.00M, ItemType.Gum, 4)
            };
            VendingMachine vendingMachine = new VendingMachine(exampleItems);
            List<VendingMachineTransaction> expectedResult = new List<VendingMachineTransaction>();

            // Act
            expectedResult.Add(vendingMachine.FeedMoney(5));
            expectedResult.Add(vendingMachine.FeedMoney(3));
            expectedResult.Add(vendingMachine.FinishTransaction());
            expectedResult.Add(vendingMachine.PurchaseItem(ItemType.Candy, 5));
            expectedResult.Add(vendingMachine.PurchaseItem(ItemType.Drink, 5));
            List<VendingMachineTransaction> result = vendingMachine.GetAllTransactions();

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}