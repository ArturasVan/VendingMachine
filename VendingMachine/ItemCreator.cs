using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public sealed class ItemCreator
    {
        private static Dictionary<string, Item> cachItems = new Dictionary<string, Item>();
        private static void LoadCachItems()
        {          
            cachItems.Add("Coca Cola 330", new Drink("Coca Cola", 15, "Cold coca cola can", 20));
            cachItems.Add("7'up 330", new Drink("7'up", 13, "Cold 7'up can", 20));
            cachItems.Add("Fanta 330 ml", new Drink("Fanta", 13, "Cold Fanta can", 20));
            cachItems.Add("Water 500 ml", new Drink("Water", 8, "Usual cold water bottle", 20));
            cachItems.Add("Tuna sandwich", new Food("Tuna sandwich", 30, "Tasty tuna sandwich", 20));
            cachItems.Add("Cheeseburger", new Food("Cheeseburger", 25, "Usual cheesburger", 20));
            cachItems.Add("Svensk Smörgås", new Food("Svensk Smörgås", 35, "Heavenly Smörgås", 20));
            cachItems.Add("Croissant", new Food("Croissant", 27, "Fresh French Croissant", 20));
            cachItems.Add("Pringles 50g", new Snack("Pringles Onion", 15, "Crispy onion taste chips", 20));
            cachItems.Add("Lay's 50g", new Snack("Lays tomato", 15, "Crispy tomato taste chips", 20));
            cachItems.Add("Oreo", new Snack("Oreo Cookies", 12, "Original Oreo Cookies", 20));
            cachItems.Add("Mini Banana", new Snack("Mini Bana", 7, "Fresh Organic mini banana", 20));
        }
        public static Item GetItem(string key)
        {
            if (cachItems.Count == 0)
                LoadCachItems();
            return cachItems.ContainsKey(key) ? (Item)cachItems[key].Clone() : null;
        }
    }
}
