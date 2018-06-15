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
    public class VendingMachineItemTests
    {
        [TestMethod]
        public void VendingMachineItem_Constructor()
        {
            // Arrange
            string fakeItemName = "My Vending Machine Item";
            decimal fakeItemPrice = 10.0M;
            ItemType fakeItemType = ItemType.Candy;
            int fakeItemSlot = 4;

            // Act
            VendingMachineItem result = new VendingMachineItem(fakeItemName, fakeItemPrice, fakeItemType, fakeItemSlot);

            // Assert
            // Item.Quantity
            Assert.AreEqual(5, result.Quantity);
            // Item.Name
            Assert.AreEqual("My Vending Machine Item", result.Name);
            // Item.Slot
            Assert.AreEqual(fakeItemSlot, result.Slot);
            // Item.Price
            Assert.AreEqual(10M, result.Price);
            // Item.Type
            Assert.AreEqual(ItemType.Candy, result.Type);
        }
    }
}