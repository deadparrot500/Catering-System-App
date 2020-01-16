using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class FileAccessTests
    {

        [TestMethod]
        public void LoadInventoryTest()
        {
            Assert.IsTrue(FileAccess.LoadInventory());
        }

        [TestMethod]
        public void LogItemTest()
        {
            Assert.IsTrue(FileAccess.LogItem("This is a test"));
        }

        [TestMethod]
        public void GenerateSalesReportTest()
        {
            Assert.IsTrue(FileAccess.GenerateSalesReport());
        }
    }
}
