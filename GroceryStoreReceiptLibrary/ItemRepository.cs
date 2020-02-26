using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreReceiptLibrary
{
    public class ItemRepository
    {
        public int Count { get; set; }


        private List<Item> Items = new List<Item>();


        //Building and Removing Item Methods

        public void Add(string itemName, double price, int perUnit)
        {
            throw new NotImplementedException();
        }

        public void Add(string itemName, double price)
        {
            decimal precisePrice = Convert.ToDecimal(price);
            if(!Items.Any(x=>x.Name == itemName))
            {
                Items.Add(new Item(itemName, precisePrice));
                Count = Items.Count;
            }else
            {
                Items.Where(x => x.Name == itemName).FirstOrDefault().Price = precisePrice;
            }
            
        }

        //Item Information Gathering Methods

        internal bool DoesItemExist(string itemName)
        {
            return Items.Any(x => x.Name == itemName);
        }

        public decimal PriceCheck(string itemName)
        {
            return Items.Where(x => x.Name == itemName).Select(x => x.Price-x.PriceMarkDown).FirstOrDefault();
        }

        public Item CheckSaleInfo(string itemName)
        {
            return Items.Where(x => x.Name == itemName).First();
        }


        //Sale Methods

        public void Markdown(string itemName, double priceToMarkdownInDollars)
        {
            Items.Where(x => x.Name == itemName).FirstOrDefault().PriceMarkDown = Convert.ToDecimal(priceToMarkdownInDollars);
        }

        public void BuyNumberGetNumberFreeLimitNumber(string itemName, int numberNeedToBuy, int numberReceivedFree, int LimitOnDealTotalItems)
        {
            Items.Where(x => x.Name == itemName).First().BOGOFreeReceivedNumber = numberReceivedFree;
            Items.Where(x => x.Name == itemName).First().BOGOPurchasedNumber = numberNeedToBuy;
            Items.Where(x => x.Name == itemName).First().BOGOLimit = LimitOnDealTotalItems;
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
