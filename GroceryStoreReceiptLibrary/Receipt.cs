using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GroceryStoreReceiptLibrary
{
    public class Receipt
    {
        private readonly ItemRepository PriceList;
        private List<Item> ItemsOnReceipt = new List<Item>();

        public Receipt(ItemRepository itemRepository)
        {
            PriceList = itemRepository;
        }


       
        public int ItemCount()
        {
            return ItemsOnReceipt.Count();
        }

        public decimal Total()
        {
            return ItemsOnReceipt.Select(x => x.Price).Sum();
        }


        public void Buy(string itemName)
        {
            if(!PriceList.DoesItemExist(itemName))
            {
                throw new ItemNotFound();
            }

            decimal priceAdjustedForSale = 0;
            var itemToBeBought = PriceList.CheckSaleInfo(itemName);
            if (itemToBeBought.BOGOPurchasedNumber == 0)
            {
                priceAdjustedForSale = Math.Round(PriceList.PriceCheck(itemName), 2);

            }else if(ItemsOnReceipt.Where(x => x.Name == itemName).Count() >= itemToBeBought.BOGOPurchasedNumber && ItemsOnReceipt.Where(x => x.Name == itemName).Count() < itemToBeBought.LimitNumber)
            {
                priceAdjustedForSale = 0;
            } else //if BOGO is not 0 but Items Have Not Yet Reached Required Purchase Amount
            {
                priceAdjustedForSale = Math.Round(PriceList.PriceCheck(itemName), 2);
            }


            ItemsOnReceipt.Add(new Item(itemName, priceAdjustedForSale));
        }
        public void Buy(string itemName, int itemQuantity)
        {
            for(int i=0; i < itemQuantity; i++)
            {
                Buy(itemName);
            }
        }

        public void Void()
        {
            Void(ItemsOnReceipt.Last().Name);
        }

        public void Void(string itemName)
        {
            var itemToBeRemoved = ItemsOnReceipt.Where(x => x.Name == itemName).LastOrDefault();
            if(itemToBeRemoved is null)
            {
                throw new ItemNotFound();
            }

            ItemsOnReceipt.Remove(itemToBeRemoved);

        }

        

        public void Void(string itemName, int itemQuantity)
        {
            for(int i=0; i<itemQuantity; i++)
            {
                Void(itemName);
            }
        }

        public Item LastItem()
        {
            return ItemsOnReceipt.Last();
        }

       
    }
}
