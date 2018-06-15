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
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Item, result.Item);

            // Act part 2 -- Tests for "-$20" inserted
            result = vendingMachine.FeedMoney(-20);
            expectedResult = new VendingMachineTransaction(TransactionType.InvalidBill, 0);

            // Assert part 2
            Assert.AreEqual(20, vendingMachine.CurrentBalance);
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Item, result.Item);

            // Act part 3 -- Tests for "$10" inserted
            result = vendingMachine.FeedMoney(10);
            expectedResult = new VendingMachineTransaction(TransactionType.FeedMoney, 10);

            // Assert part 3
            Assert.AreEqual(30, vendingMachine.CurrentBalance);
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Item, result.Item);

            // Act part 4 -- Tests for "$6" inserted
            result = vendingMachine.FeedMoney(6);
            expectedResult = new VendingMachineTransaction(TransactionType.InvalidBill, 0);

            // Assert part 4
            Assert.AreEqual(30, vendingMachine.CurrentBalance);
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Item, result.Item);
        }

        [TestMethod]
        public void VendingMachine_CheckItem()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VendingMachine_PurchaseItem()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VendingMachine_FinishTransaction()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VendingMachine_GetAllTransactions()
        {
            Assert.Fail();
        }
    }
}