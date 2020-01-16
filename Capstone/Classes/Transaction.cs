using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone.Classes
{
    public static class Transaction
    {
        public static List<KeyValuePair<decimal, string>> Denominations { get; } = new List<KeyValuePair<decimal, string>>()
        {
            new KeyValuePair<decimal, string> (100.00M, "hundred(s)"),
            new KeyValuePair<decimal, string>(50.00M, "fifty(s)" ),
            new KeyValuePair<decimal, string>(20.00M, "twenty(s)" ),
            new KeyValuePair<decimal, string>(10.00M, "ten(s)" ),
            new KeyValuePair<decimal, string>(5.00M, "five(s)" ),
            new KeyValuePair<decimal, string>(1.00M, "one(s)" ),
            new KeyValuePair<decimal, string>(.25M, "quarter(s)" ),
            new KeyValuePair<decimal, string>(.10M, "dime(s)" ),
            new KeyValuePair<decimal, string> (.05M, "nickel(s)" )
        };





        public static bool AddMoney(decimal amountToAdd)
        {
            bool addMoney = true;
            if ((CustomerAccount.Balance + amountToAdd) > 5000.00M ||
                amountToAdd < 0)
            {
                addMoney = false;
            }
            else
            {
                addMoney = true;
                CustomerAccount.Balance += amountToAdd;
            }
            
            FileAccess.LogItem($"ADD MONEY: ${amountToAdd} ${CustomerAccount.Balance}");
            return addMoney;
        }

        public static bool RemoveMoney(decimal amountToRemove)
        {
            bool removeMoney = true;
            if ((CustomerAccount.Balance - amountToRemove) < 00.00M ||
                amountToRemove < 0)
            {
                removeMoney = false;
            }
            else
            {
                removeMoney = true;
                CustomerAccount.Balance -= amountToRemove;
            }
            
            return removeMoney;
        }

        public static string MakeChange()
        {
            StringBuilder changeToPrint = new StringBuilder();
            changeToPrint.AppendLine("---------------------------------------------------------");
            decimal remainingBalance = CustomerAccount.Balance;
            

            Dictionary<string, int> counter = new Dictionary<string, int>();

            while (remainingBalance > .04M)
            {

                foreach (KeyValuePair<decimal, string> kvp in Denominations)
                {
                    if (remainingBalance / kvp.Key >= 1)
                    {
                        remainingBalance -= kvp.Key;
                        if (!counter.ContainsKey(kvp.Value))
                        {
                            counter.Add(kvp.Value, 0);
                        }
                        counter[kvp.Value]+=1;
                        break;
                    }
                }
            }
            foreach(KeyValuePair<string, int> kvp in counter)
            {
                changeToPrint.AppendLine(String.Format("{0,-10} | {1,-20}", kvp.Value, kvp.Key));
            }
            FileAccess.LogItem($"GIVE CHANGE: ${CustomerAccount.Balance} ${remainingBalance}");
            CustomerAccount.Balance = 0.00M;
            return changeToPrint.ToString();
        }
    }
}
