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
        public void VendingMachine_FeedMoney()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine(new List<VendingMachineItem>());
            //List<VendingMachineItem> myVendingMachineItems = new List<VendingMachineItem>
            //{
            //    new VendingMachineItem("Butterfinger", 10.00M, ItemType.Candy, 5),
            //    new VendingMachineItem("Coke", 15.00M, ItemType.Drink, 5),
            //    new VendingMachineItem("Bubbleyum", 20.00M, ItemType.Gum, 4)
            //};
            //VendingMachine vendingMachine = new VendingMachine(myVendingMachineItems);
            VendingMachineTransaction expectedResult = new VendingMachineTransaction(TransactionType.FeedMoney, 20, true);

            // Act
            VendingMachineTransaction result = vendingMachine.FeedMoney(20);

            // Assert
            Assert.AreEqual(20, vendingMachine.CurrentBalance);
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Item, result.Item);
            Assert.AreEqual(expectedResult.IsValid, result.IsValid);

            // Act part 2
            result = vendingMachine.FeedMoney(-20);
            expectedResult = new VendingMachineTransaction(TransactionType.FeedMoney, -20, false);

            // Assert part 2
            Assert.AreEqual(20, vendingMachine.CurrentBalance);
            Assert.AreEqual(expectedResult.Type, result.Type);
            Assert.AreEqual(expectedResult.Amount, result.Amount);
            Assert.AreEqual(expectedResult.IsValid, result.IsValid);
        }

        [TestMethod]
        public void VendingMachine_FinishTransaction()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PurchaseItemTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CheckItemTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllItemsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllTransactionsTest()
        {
            Assert.Fail();
        }
    }
}