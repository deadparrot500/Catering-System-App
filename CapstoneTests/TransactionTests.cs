using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class TransactionTests
    {
        [DataTestMethod]
        [DataRow("2500", "2500", true)]
        [DataRow("6000", "0", false)]
        [DataRow("-1", "0", false)]
        [TestMethod]
        public void AddMoneyTest(string amountAdded, string balanceExpected, bool expectedReturn)
        {
            CustomerAccount.Balance = 0.00M;

            bool actualReturn = Transaction.AddMoney(decimal.Parse(amountAdded));
            if(CustomerAccount.Balance != decimal.Parse(balanceExpected))
            {
                Assert.Fail();
            }
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [DataTestMethod]
        [DataRow("2500", "2500", true)]
        [DataRow("6000", "5000", false)]
        [DataRow("-1", "5000", false)]
        [TestMethod]
        public void RemoveMoneyTest (string amountRemoved, string balanceExpected, bool expectedReturn)
        {
            CustomerAccount.Balance = 5000.00M;

            bool actualReturn = Transaction.RemoveMoney(decimal.Parse(amountRemoved));
            if (CustomerAccount.Balance != decimal.Parse(balanceExpected))
            {
                Assert.Fail();
            }
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void MakeChangeTest()
        {
            string expectedValue =
            "1          | hundred(s)          \r\n" +
            "1          | fifty(s)            \r\n" +
            "1          | twenty(s)           \r\n" +
            "1          | ten(s)              \r\n" +
            "1          | five(s)             \r\n" +
            "1          | one(s)              \r\n" +
            "1          | quarter(s)          \r\n" +
            "1          | dime(s)             \r\n" +
            "1          | nickel(s)           \r\n";
            CustomerAccount.Balance = 186.40M;

            string actualValue = Transaction.MakeChange();

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
