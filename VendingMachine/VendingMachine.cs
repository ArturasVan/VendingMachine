using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace VendingMachine
{
    public sealed class VendingMachine
    {
        private static VendingMachine instance;

        private readonly Dictionary<Item, int> machineItems;

        private int[] notes = new int[] { 1000, 500, 100, 50, 20, 10, 5, 1 };
        private int[] noteCounter = new int[8];
        private int _userCredit;
        private int machineBank;


        public VendingMachine()
        {
            this.machineItems = new Dictionary<Item, int>();
            this.machineBank = 0;
        }
        public static VendingMachine GetInstance()
        {
            if (instance == null)
                instance = new VendingMachine();
            return instance;
        }
        public int MachineBank
        {
            get
            {
                return this.machineBank;
            }
        }
        public int UserCredit
        {
            get
            {
                return this._userCredit;
            }
        }
        public Dictionary<Item, int> MachineItems
        {
            get
            {
                return this.machineItems;
            }
        }
        public int GetItemStock(Item item)
        {
            if (machineItems.ContainsKey(item))
                return machineItems[item];

            return -1;
        }
        public int GetTotalMachineItems()
        {
            int totalItems = 0;
            foreach (Item item in machineItems.Keys)
                totalItems += machineItems[item];
            return totalItems;
        }
        public void RefillItems()
        {
            machineItems.Clear();
            this.machineItems.Add(ItemCreator.GetItem("Coca Cola 330"), 20);
            this.machineItems.Add(ItemCreator.GetItem("7'up 330"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Fanta 330 ml"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Water 500 ml"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Tuna sandwich"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Cheeseburger"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Svensk Smörgås"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Croissant"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Pringles 50g"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Lay's 50g"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Oreo"), 20);
            this.machineItems.Add(ItemCreator.GetItem("Mini Banana"), 20);
        }

        public void SellItem(Item item, int amountPaid)
        {
            if (item.ItemPrice <= _userCredit)
            {
                machineBank += item.ItemPrice;
                _userCredit -= item.ItemPrice;
                machineItems[item]--;
            }
            else 
            { 
                Console.WriteLine("You dont have suficient money in your credit to buy this item!\n" +
                " Please select option to add more money to your credit"); 
            }     
        }
        public void AddMoney()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;

            Console.WriteLine(" Please put your money to vending machine.\n\n" +
                " Vending machine accepts only fixed denominations:\n" +
                "   1kr, 5kr, 10kr, 20kr, 50kr, 100kr, 500kr and 1000kr\n" +
                " type corresponding numbers and press enter");
           
            string userInput = "a";
            while (userInput != "s")
            {
                Console.WriteLine("\n Please enter your next denomination. \n To STOP adding money press s and ENTER");
                int[] denominations = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };
                    string money = Console.ReadLine();
                    int numMoney;
                    int number;

                if (int.TryParse(money, out number) && denominations.Contains(number))
                {
                    numMoney = number;
                    _userCredit += number;

                }
                else if ( money == "s")
                { 
                    return;
                }
                else
                {
                    numMoney = 0;
                    Console.WriteLine(" You have tried to use wrong denomination banknote. \n Please try again");
                }
                Console.WriteLine(" Your credit: {0}", _userCredit.ToString("C0", swedish));
            }
        }

        public void ReturnMoney()
        {
            // count notes using Greedy approach 
            for (int i = 0; i < 8; i++)
            {
                if (_userCredit >= notes[i])
                {
                    noteCounter[i] = _userCredit / notes[i];
                    _userCredit = _userCredit - noteCounter[i] * notes[i];
                }
            }
            // Print notes 
            Console.WriteLine("Your Change ->");
            for (int i = 0; i < 8; i++)
            {
                if (noteCounter[i] != 0)
                {
                    Console.WriteLine(notes[i] + "kr : "
                        + noteCounter[i] + "quantity");
                }
            }           
        }
        public override string ToString()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format("Unique Items on sale: {0} , Total Items: {1}, Machine Bank: {2}, Buyer Credit: {3} \n",
                machineItems.Count, GetTotalMachineItems(), machineBank.ToString("C0", swedish), UserCredit.ToString("C0",swedish));
        }
    }
}
