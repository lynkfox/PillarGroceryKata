using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreReceiptLibrary
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BOGOLimit { get; set; }
        public int BOGOPurchasedNumber { get; set; }
        public int BOGOFreeReceivedNumber { get; set; }

        public int RequiredToGetDiscount { get; set; }
        public int ToReceiveDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int DiscountLimit { get; set; }


        public decimal PriceMarkDown { get; set; }

        public Item(string itemName, decimal itemPrice)
        {
            Name = itemName;
            Price = itemPrice;
        }

    }
}
