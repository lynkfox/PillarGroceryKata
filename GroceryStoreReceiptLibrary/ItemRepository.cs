using System;
using System.Collections.Generic;

namespace GroceryStoreReceiptLibrary
{
    public static class ItemRepository
    {
        public static int Count { get; set; }


        
        public static void Add(string itemName, double price, int perUnit)
        {
            throw new NotImplementedException();
        }

        public static void Add(string itemName, double price)
        {
            Count++;
        }

        public static void Markdown(string itemName, double priceToMarkdownInDollars)
        {
            throw new NotImplementedException();
        }

        public static void BuyNumberGetNumberFreeLimitNumber(string itemName, int numberNeedToBuy, int numberReceivedFree, int LimitOnDealTotalItems)
        {
            throw new NotImplementedException();
        }

        public static void BuyNumberGetNumberAtDiscountPercentLimitNumber(string itemName, int numberNeedToBuy, int numberToReceiveDiscount, double discountInPercentage, int LimitOnDealTotalItems)
        {
            throw new NotImplementedException();
        }

        public static void BuyNumberGetDiscountPercentOnEqualOrLesser(string itemName, int numberNeedToBuy, double discountPercentageOnNextItem )
        {
            throw new NotImplementedException();
        }
    }
}
