using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace VendingMachine
{
    public abstract class Item
    {
        public int itemID { get; private set; }
        protected readonly string itemName;
        protected readonly int itemPrice;
        public int itemQuantity { get; set; }
        protected string itemDescription { get; set; }
        private static int totalItemCount = 0;

        public Item(string itemName, int itemPrice, string itemDescription, int itemQuantity)
        {
            this.itemID = totalItemCount++;
            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.itemDescription = itemDescription;
            this.itemQuantity = itemQuantity;
        }
        public Item(Item item)
        {
            this.itemID = item.itemID;
            this.itemName = item.itemName;
            this.itemPrice = item.itemPrice;
            this.itemDescription = item.itemDescription;
            this.itemQuantity = item.itemQuantity;
        }
        public int ItemID
        {
            get
            {
                return this.itemID;
            }
        }
        public string ItemName
        {
            get
            {
                return this.itemName;
            }
        }
       
        public int ItemPrice
        {
            get
            {
                return this.itemPrice;
            }
        }
        public string ItemDescription
        {
            get
            {
                return this.itemDescription;
            }
        }
        public virtual string UseItem()
        {
            throw new NotImplementedException();
        }
        public string ExamineItem()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format("[Item Details] ID: {0}, Name: {1}, Price: {2}, Description: {3}",
                this.itemID, this.itemName, itemPrice.ToString("C0", swedish), this.itemDescription);
        }
        public void PurchaseItem()
        {
            if (this.CheckAvailability())
            {
                this.itemQuantity -= 1;
            }
            else
            {
                Console.WriteLine("Sorry but you cant buy this item");
            }
        }
        public bool CheckAvailability()
        {
            return this.itemQuantity > 0;
        }
        public static int TotalItems()
        {
            return totalItemCount;
        }
        public override string ToString()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format("[Item Details] ID: {0}, Name: {1}, Price: {2}, Description: {3}",
                this.itemID, this.itemName, itemPrice.ToString("C0", swedish), this.itemDescription);
        }
        public abstract Object Clone();
    }
}
