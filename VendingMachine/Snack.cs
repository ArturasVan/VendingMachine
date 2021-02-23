using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VendingMachine
{
    class Snack : Item
    {
        public Snack(string itemName, int itemPrice, string itemDescription, int itemQuantity)
            : base(itemName, itemPrice, itemDescription, itemQuantity)
        {
        }
        public Snack(Snack Snack) :
            base(Snack)
        {
        }
        public override string UseItem()
        {
            return string.Format("You have chewed tasty {0}", this.itemName);
        }
        public override Object Clone()
        {
            return new Snack(this);
        }
        public override string ToString()
        {
            CultureInfo swedish = new CultureInfo("sv-SE");
            swedish = (CultureInfo)swedish.Clone();
            swedish.NumberFormat.CurrencyPositivePattern = 3;
            swedish.NumberFormat.CurrencyNegativePattern = 3;
            return string.Format("[SNACK] ID: {0}, Name: {1}, Price: {2}, Description: {3}, Quantity: {4}",
                this.itemID, this.itemName, this.itemPrice.ToString("C0", swedish), this.itemDescription, this.itemQuantity);
        }
    }
}
