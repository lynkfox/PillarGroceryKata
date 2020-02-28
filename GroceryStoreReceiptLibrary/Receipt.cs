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
            
            ItemsOnReceipt.Add(new Item(itemName, AdjustPriceForVariousSaleTypes(PriceList.CheckSaleInfo(itemName))));
        }

        private decimal AdjustPriceForVariousSaleTypes(Item itemToBeBought)
        {
            if (itemToBeBought.BOGOPurchasedNumber != 0)
            {
                return AdjustPriceForBOGOSale(itemToBeBought);
            }
            else if (itemToBeBought.RequiredToGetDiscount != 0)
            {
                return AdjustPriceForBOGDiscountSale(itemToBeBought);
            }
            else
            {
                return itemToBeBought.Price;
            }

        }

        private decimal AdjustPriceForBOGOSale(Item itemToBeBought)
        {
            if (ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Count() >= itemToBeBought.BOGOPurchasedNumber && ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Count() < itemToBeBought.BOGOLimit)
            {
                return 0;
            }
            else //if BOGO Items Have Not Yet Reached Required Purchase Amount
            {
                return itemToBeBought.Price;
            }
        }

        private decimal AdjustPriceForBOGDiscountSale(Item itemToBeBought)
        {
            int numberOfItemsAlreadyPurchased = ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Count();
            int itemsThatNeedToBePurchasedBeforeNewSetToResetSale = itemToBeBought.RequiredToGetDiscount + itemToBeBought.ToReceiveDiscount;

            if (numberOfItemsAlreadyPurchased >= itemToBeBought.RequiredToGetDiscount && numberOfItemsAlreadyPurchased < itemToBeBought.DiscountLimit)
            {
                if(numberOfItemsAlreadyPurchased%itemToBeBought.RequiredToGetDiscount < itemToBeBought.ToReceiveDiscount)
                {
                    return itemToBeBought.Price * itemToBeBought.DiscountPercentage;
                }
                else
                {
                    return itemToBeBought.Price;
                }
            }
            else //if BOGetDiscount Items Have Not Yet Reached Required Purchase Amount
            {
                return itemToBeBought.Price;
            }
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
