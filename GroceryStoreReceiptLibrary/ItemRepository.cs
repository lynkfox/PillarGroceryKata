using System;

namespace GroceryStoreReceiptLibrary
{
    public class ItemRepository
    {
        public void Add(string itemName, double price, int perUnit)
        {
            throw new NotImplementedException();
        }

        public void Add(string itemName, double price)
        {
            throw new NotImplementedException();
        }

        public void Markdown(string itemName, double priceToMarkdownInDollars)
        {
            throw new NotImplementedException();
        }

        public void BuyNumberGetNumberFreeLimitNumber(string itemName, int numberNeedToBuy, int numberReceivedFree, int LimitOnDealTotalItems)
        {
            throw new NotImplementedException();
        }

        public void BuyNumberGetNumberAtDiscountPercentLimitNumber(string itemName, int numberNeedToBuy, int numberToReceiveDiscount, double discountInPercentage, int LimitOnDealTotalItems)
        {
            throw new NotImplementedException();
        }

        public void BuyNumberGetDiscountPercentOnEqualOrLesser(string itemName, int numberNeedToBuy, double discountPercentageOnNextItem )
        {
            throw new NotImplementedException();
        }
    }
}
