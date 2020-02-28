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
            decimal precisePrice = Convert.ToDecimal(price);
            if (!Items.Any(x => x.Name == itemName))
            {
                Items.Add(new Item(itemName, precisePrice, perUnit));
                Count = Items.Count;
            }
            else
            {
                Items.Where(x => x.Name == itemName).FirstOrDefault().Price = precisePrice;
            }
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

        public void BuyNumberGetNumberAtDiscountPercentLimitNumber(string itemName, int numberNeedToBuy, int numberToReceiveDiscount, double discountInPercentage, int LimitOnDealTotalItems)
        {
            Items.Where(x => x.Name == itemName).First().RequiredToGetDiscount = numberNeedToBuy;
            Items.Where(x => x.Name == itemName).First().ToReceiveDiscount = numberToReceiveDiscount;
            Items.Where(x => x.Name == itemName).First().DiscountPercentage = Convert.ToDecimal(discountInPercentage);
            Items.Where(x => x.Name == itemName).First().DiscountLimit = LimitOnDealTotalItems;
        }

        public void BuyNumberGetDiscountPercentOnEqualOrLesser(string itemName, double numberNeedToBuy, double numberToGetDiscount, double discountPercentageOnNextItem )
        {
            throw new NotImplementedException();
        }

        public void BuyGroupAtReducedPrice(string itemName, int groupAmount, decimal costForEntireGroup)
        {
            Items.Where(x => x.Name == itemName).First().GroupBuyingRequiredNumber = groupAmount;
            Items.Where(x => x.Name == itemName).First().GroupBuyGroupPrice = costForEntireGroup;
            Items.Where(x => x.Name == itemName).First().ReducedGroupItemCost = costForEntireGroup / groupAmount;
        }
    }
}
