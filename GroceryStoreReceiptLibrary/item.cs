using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreReceiptLibrary
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal PriceMarkDown { get; set; }

        public Item(string itemName, decimal itemPrice)
        {
            Name = itemName;
            Price = itemPrice;
        }
    }
}
