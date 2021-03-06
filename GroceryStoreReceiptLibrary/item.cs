﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreReceiptLibrary
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool PriceIsPerWeight { get; set; }
        public double Weight { get; set; }
        public int PricePerUnit { get; set; }

        public int RequiredToGetDiscount { get; set; }
        public int ToReceiveDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int DiscountLimit { get; set; }

        public bool EqualOrLesserValueRequired { get; set; }
        public int GroupBuyingRequiredNumber { get; set; }
        public decimal GroupBuyGroupPrice { get; set; }
        public decimal ReducedGroupItemCost { get; set; }


        public decimal PriceMarkDown { get; set; }

        public Item(string itemName, decimal itemPrice, int unit)
        {
            Name = itemName;
            Price = itemPrice;
            PriceIsPerWeight = true;
            EqualOrLesserValueRequired = false;
            PricePerUnit = unit;
        }
        public Item(string itemName, decimal itemPrice)
        {
            Name = itemName;
            Price = itemPrice;
            PricePerUnit = 1;
            PriceIsPerWeight = false;
            EqualOrLesserValueRequired = false;
        }

    }
}
