using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere
        // All instance of Console.ReadLine and Console.WriteLine should be in this class.
        // This class is not testable.
        public UserInterface()
        {
            FileAccess.LoadInventory();
        }
        
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                int input = 0;
                try
                {
                    input = int.Parse(Console.ReadLine());
                } 
                catch(FormatException ex)
                {
                    Console.WriteLine("Please enter in a valid integer for selection.");
                    continue;
                }
                

                switch (input)
                {
                    case 1:
                        DisplayCateringItems();
                        break;

                    case 2:
                        OrderMenu();
                        break;

                    case 3:
                        done = true;
                        FileAccess.GenerateSalesReport();
                        continue;
                    default:
                        Console.WriteLine("Please enter a valid response.");
                        continue;                      

                }
                               
            }
            return;
        }

        public void DisplayCateringItems()
        {
            Console.WriteLine(String.Format("{0, -5} | {1, -15} | {2, -35} | {3, -10} | {4, -10 }",
                    "PID", "Catagory", "Item", "Price", "In Stock"));
            foreach (KeyValuePair< string, CateringItem> kvp in Catering.Inventory)
            {
                Console.WriteLine(String.Format("{0, -5} | {1, -15} | {2, -35} | {3, -10} | {4, -5 }",
                    kvp.Value.PID, kvp.Value.DisplayName, kvp.Value.Name, kvp.Value.Price, kvp.Value.InventoryAmount));
            }
        }

        public void OrderMenu()
        {

            Console.WriteLine("(1) Add Money");
            Console.WriteLine("(2) Select Products");
            Console.WriteLine("(3) Complete Transaction");
            Console.WriteLine($"Current Account Balance: ${CustomerAccount.Balance}");

            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());
            } catch(FormatException ex)
            {
                Console.WriteLine("Please enter in a valid integer for selection.");
                return;
            }
            

            switch (input)
            {
                case 1:
                    Console.WriteLine("How much would you like to add?");
                    decimal amountToAdd;
                    try
                    {
                        amountToAdd = decimal.Parse(Console.ReadLine());
                    } catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid input for amount. Please enter a valid number.");
                        return;
                    }
                    
                    if (!Transaction.AddMoney(amountToAdd))
                    {
                        Console.WriteLine("You are unable to have more than $5000.00 in your account.");
                    };
                    Console.WriteLine($"New Account Balance: {CustomerAccount.Balance}");

                    break;

                case 2:
                    Console.WriteLine("Enter the PID of the Item you would like to order:");
                    string PID = Console.ReadLine();
                    Console.WriteLine("How many would you like to order:");
                    int amountOrdered;
                    try
                    {
                        amountOrdered = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid input for amount. Please enter a valid number.");
                        return;
                    }
                    
                    Console.WriteLine(Catering.AddToCart(PID, amountOrdered));
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    break;

                case 3:
                    Console.WriteLine(Transaction.MakeChange());
                    decimal totalAmount = 0.00M;

                    foreach (KeyValuePair<string, CateringItem> kvp in Catering.Cart)
                    {
                        decimal totalItemPrice = kvp.Value.AmountSold * kvp.Value.Price;
                        Console.WriteLine(String.Format("{0, -5} | {1, -15} | {2, -35} | {3, -10} | {4, -5 }",
                            kvp.Value.PID, kvp.Value.DisplayName, kvp.Value.Name, kvp.Value.Price, totalItemPrice));
                        totalAmount += totalItemPrice;
                    }
                    Console.WriteLine("\n $Total: "+ totalAmount);
                    Console.WriteLine("---------------------------------------------------------");
                    break;

                default:
                    Console.WriteLine("Please enter a valid response.");
                    break;

            }



        }


    }
}
