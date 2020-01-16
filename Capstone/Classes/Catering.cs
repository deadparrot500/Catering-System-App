using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public static class Catering
    {
        // This class should contain all the "work" for catering


        public static Dictionary<string, CateringItem> Inventory { get; set; }

        public static Dictionary<string, CateringItem> Cart { get; set; } = new Dictionary<string, CateringItem>();

        public static string AddToCart(string PID, int amountOrdered)
        {
            if (!Inventory.ContainsKey(PID))
            {
                return "No item with that ID exists.";
            }
            int amountInInventory = Inventory[PID].InventoryAmount;

            if (amountInInventory < amountOrdered)
            {
                return "Not enough items in stock";
            }
            decimal totalPrice = Inventory[PID].Price * amountOrdered;
            if (!Transaction.RemoveMoney(totalPrice))
            {
                return "Not enough money in account.";
            }

            Inventory[PID].InventoryAmount -= amountOrdered;
            if (!Cart.ContainsKey(PID))
            {
                Cart.Add(PID, Inventory[PID]);
            }
            Cart[PID].AmountSold += amountOrdered;
            
            FileAccess.LogItem($"{amountOrdered} {Inventory[PID].Name} {PID} ${totalPrice} ${CustomerAccount.Balance} ");

            return "Item added";

        }

    }
}
