using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Globalization;

namespace VendingMachine
{
    class Program
    {
        static VendingMachine vendingMachine;
        static void Main(string[] args)
        {
            vendingMachine = VendingMachine.GetInstance();

            PrintColored("- Initialized Vending Machine.", ConsoleColor.Cyan);
            RequestUserAction();
        }
        static void RequestUserAction()
        {
            Console.WriteLine(" What action would you like to use?\n   1 - Print Machine and Buyer credit Info, \n   2 - Put money to Buyer Credit, \n   3 - Refill Machine, \n   4 - Purchase a Product\n HINT: Type EXIT to disconnect from the system and get your change.");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    PrintColored(vendingMachine.ToString(), ConsoleColor.Green);
                    break;
                case "2":
                    vendingMachine.AddMoney();
                    break;
                case "3":
                    vendingMachine.RefillItems();
                    break;
                case "4":
                    ProductPurchase();
                    break;
                case "EXIT":

                    vendingMachine.ReturnMoney();
                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();

                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
            RequestUserAction();
        }
        static void ProductPurchase()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;
            Item requestedItem = null;
            string userInputString = string.Empty;
            int userInputInt;
            int userInputInt2;

            if (vendingMachine.MachineItems.Count == 0)
            {
                PrintColored("[ERROR] This vending machine does not have any items in stock.\n" +
                    " Please use option to refill machine\n", ConsoleColor.Red);
                return;
            }
            PrintColored("   Available Machine Items:\n", ConsoleColor.Cyan);
            foreach (Item item in vendingMachine.MachineItems.Keys)
            {
                PrintColored(string.Format("{0} [IN STOCK: {1}]", item.ToString(), vendingMachine.MachineItems[item]),
                    ConsoleColor.DarkGreen);
            }
            PrintColored("\n   Select the item you would like to purchase (Item ID).\n", ConsoleColor.Cyan);
            Console.WriteLine("HINT: Type EXIT in order to return to the previous menu.");

            userInputString = Console.ReadLine();

            if (userInputString == "EXIT")
                return;

            if (!int.TryParse(userInputString, out userInputInt))
            {
                PrintColored("[ERROR] Invalid input.", ConsoleColor.Red);
                ProductPurchase();
                return;
            }

            foreach (Item item in vendingMachine.MachineItems.Keys)
            {
                if (item.ItemID == userInputInt)
                {
                    requestedItem = item;
                    break;
                }
            }

            if (requestedItem == null)
            {
                PrintColored("[ERROR] No such item in the vending machine. Please try again.", ConsoleColor.Red);
                ProductPurchase();
                return;
            }

            if (vendingMachine.GetItemStock(requestedItem) == 0)
            {
                PrintColored("[ERROR] Item is out of stock.", ConsoleColor.Red);
                ProductPurchase();
                return;
            }

            Console.WriteLine(requestedItem);
            Console.WriteLine(" How much money are you paying from your credit?");
            Console.WriteLine("   Your credit: {0} kr", vendingMachine.UserCredit);
            userInputString = Console.ReadLine();

            if (!int.TryParse(userInputString, out userInputInt2))
            {
                PrintColored("[ERROR] Invalid input.", ConsoleColor.Red);
                ProductPurchase();
                return;
            }
            if (userInputInt2 < requestedItem.ItemPrice)
            {
                PrintColored("[ERROR] Item price is higher than the paid amount.", ConsoleColor.Red);
                ProductPurchase();
                return;
            }

            vendingMachine.SellItem(requestedItem, userInputInt2);

            string recordMessage = string.Format(" [Vending Machine]: Item has been successfully sold (Paid: {0}, Returned to user credit: {1}), Total user credit: {2} .\n",
                    userInputInt2.ToString("C0"), (userInputInt2 - requestedItem.ItemPrice).ToString("C0", swedish), vendingMachine.UserCredit);

            PrintColored(recordMessage, ConsoleColor.Green);

            
            Console.WriteLine("\n If you want you can examine or use bought item\n" +
                "   Press e to EXAMINE bought item\n" +
                "   Press u to USE bought item\n" +
                " to exit press s and enter");

            bool examined = false;
            bool used = false;
            string userInput = "a";
            while (userInput != "s")
            {
                
                string test = Console.ReadLine();
                 
                if (test == "e" && !examined)
                {
                    PrintColored(requestedItem.ExamineItem(), ConsoleColor.Green);
                    examined = true;
                    Console.WriteLine(" Item was examined please use it by presing u and enter\n" +
                        " exit to menu by presing s and enter");
                }
                else if (test == "e" && used)
                {
                    PrintColored(" You cant examine used item", ConsoleColor.Red);
                    examined = true;
                }
                if (test == "u" && !used)
                {
                    PrintColored(requestedItem.UseItem(), ConsoleColor.Green);
                    used = true;
                    Console.WriteLine(" Item was used\n" +
                        " exit to menu by presing s and enter");
                }
                else if (test == "u" && used)
                {
                    PrintColored(" Item was used press s and enter to return to menu", ConsoleColor.Red);
                }
                if(test == "s")
                {
                   return;
                }

            }
                        
            Console.ReadLine();

            
        }
        static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
