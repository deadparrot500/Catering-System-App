using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTests
    {

        [DataTestMethod]
        [DataRow("T2", 1, "No item with that ID exists.")]
        [DataRow("T1", 52, "Not enough items in stock")]
        [DataRow("T1", 25, "Not enough money in account.")]
        [DataRow("T1", 1, "Item added")]
        [TestMethod]
        public void AddToCartTest(string pID, int amountOrdered, string expectedValue)
        {
            CateringItem testItem = new CateringItem("T1", "Test Food", 5.00M, "T");
            testItem.InventoryAmount = 50;
            CustomerAccount.Balance = 100.00M;
            Catering.Inventory = new Dictionary<String, CateringItem>()
            {
                {"T1", testItem}
            };

            string actualValue = Catering.AddToCart(pID, amountOrdered);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
