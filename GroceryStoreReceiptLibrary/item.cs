using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreReceiptLibrary
{
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public double PriceMarkDown { get; set; }

        public Item(string itemName, double itemPrice)
        {
            Name = itemName;
            Price = itemPrice;
        }
    }
}
