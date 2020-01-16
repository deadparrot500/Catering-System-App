using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        public string PID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string DisplayName
        {
            get
            {
                switch (ProductType)
                {
                    case "A":
                        return "Appetizer";
                    case "B":
                        return "Beverage";
                    case "E":
                        return "Entree";
                    case "D":
                        return "Dessert";
                    default:
                        return "UnKnown";
                }
            }

        }
        public int InventoryAmount { get; set; }
        public int AmountSold { get; set; }

        public CateringItem(string pID, string name, decimal price, string productType)
        {
            PID = pID;
            Name = name;
            Price = price;
            ProductType = productType;
            InventoryAmount = 50;
            AmountSold = 0;
        }

        public CateringItem(string name, int amountSold)
        {
            Name = name;
            AmountSold = amountSold;
        }

    }
}
