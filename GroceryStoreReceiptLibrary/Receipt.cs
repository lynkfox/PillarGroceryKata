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


        //Information Methods
       
        public int ItemCount()
        {
            return ItemsOnReceipt.Count();
        }

        public decimal Total()
        {
            return ItemsOnReceipt.Select(x => x.Price).Sum();
        }

        public Item LastItem()
        {
            return ItemsOnReceipt.Last();
        }


        //Add and Remove Items Methods

        public void Buy(string itemName)
        {

            Buy(itemName, 1);
            
        }
        public void Buy(string itemName, int itemQuantity)
        {
            Buy(itemName, itemQuantity, 1);
        }

        public void Buy(string itemName, int itemQuantity, double weight)
        {
            if (!PriceList.DoesItemExist(itemName))
            {
                throw new ItemNotFound();
            }
            
            decimal weightInDecimal = Convert.ToDecimal(weight);

            for(int i = 0; i < itemQuantity; i++)
            {
                decimal costOfItemPerWeight = AdjustPriceForVariousSaleTypes(PriceList.CheckSaleInfo(itemName)) * weightInDecimal;
                decimal roundedMoneyPriceOfItem = Decimal.Round(costOfItemPerWeight, 2, MidpointRounding.ToEven);
                ItemsOnReceipt.Add(new Item(itemName, roundedMoneyPriceOfItem));
            }
            
        }


        public void Void(string itemName, int itemQuantity)
        {
            for (int i = 0; i < itemQuantity; i++)
            {
                Void(itemName);
            }
        }
        public void Void(string itemName)
        {
            var itemToBeRemoved = ItemsOnReceipt.Where(x => x.Name == itemName).LastOrDefault();
            if (itemToBeRemoved is null)
            {
                throw new ItemNotFound();
            }

            ItemsOnReceipt.Remove(itemToBeRemoved);

        }

        public void Void()
        {
            Void(ItemsOnReceipt.Last().Name);
        }

        

        


        // Internal Price Adjustment Methods

        private decimal AdjustPriceForVariousSaleTypes(Item itemToBeBought)
        {
            if (itemToBeBought.RequiredToGetDiscount != 0)
            {
                return AdjustPriceForBuySomeGetDiscountSale(itemToBeBought);
            }
            else if (itemToBeBought.GroupBuyingRequiredNumber != 0)
            {
                return AdjustPriceForGroupBuying(itemToBeBought);
            }
            else
            {
                return itemToBeBought.Price - itemToBeBought.PriceMarkDown;
            }

        }

        private decimal AdjustPriceForGroupBuying(Item itemToBeBought)
        {
            int numberOfItemsAlreadyPurchased = ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Count();
            decimal costOfAlreadyAddedToReceiptItems = ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Select(x => x.Price).Sum();

            //Check to see if when buying the final item in a group if the cost comes out to the advertised price.
            if (numberOfItemsAlreadyPurchased+1 == itemToBeBought.GroupBuyingRequiredNumber && costOfAlreadyAddedToReceiptItems+itemToBeBought.ReducedGroupItemCost != itemToBeBought.GroupBuyGroupPrice)
            {
                return itemToBeBought.GroupBuyGroupPrice - costOfAlreadyAddedToReceiptItems;
            }
            return itemToBeBought.ReducedGroupItemCost;
        }


        private decimal AdjustPriceForBuySomeGetDiscountSale(Item itemToBeBought)
        {
            int numberOfItemsAlreadyPurchased = ItemsOnReceipt.Where(x => x.Name == itemToBeBought.Name).Count();
            int itemsThatNeedToBePurchasedBeforeNewSetToResetSale = itemToBeBought.RequiredToGetDiscount + itemToBeBought.ToReceiveDiscount;

            if (numberOfItemsAlreadyPurchased >= itemToBeBought.RequiredToGetDiscount && numberOfItemsAlreadyPurchased < itemToBeBought.DiscountLimit)
            {
                if(numberOfItemsAlreadyPurchased%itemToBeBought.RequiredToGetDiscount < itemToBeBought.ToReceiveDiscount)
                {
                    return itemToBeBought.Price - (itemToBeBought.Price * itemToBeBought.DiscountPercentage) ;
                }
                else
                {
                    return itemToBeBought.Price;
                }
            }
            else //if BuySomeGetDiscount Items Have Not Yet Reached Required Purchase Amount
            {
                return itemToBeBought.Price;
            }
        }

        

        

       
    }
}
