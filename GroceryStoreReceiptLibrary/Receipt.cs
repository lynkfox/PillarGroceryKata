using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GroceryStoreReceiptLibrary
{
    public class Receipt
    {
        private ItemRepository PriceList;

        public Receipt(ItemRepository itemRepository)
        {
            PriceList = itemRepository;
        }

        public decimal Total { get; set; }
        public int NumberOfItems { get; set; }



        public void Buy(string itemName)
        {
            if(!PriceList.DoesItemExist(itemName))
            {
                throw new ItemNotFound();
            }

            NumberOfItems++;
            Total += Math.Round(PriceList.PriceCheck(itemName),2);
        }
        public void Buy(string itemName, int itemQuantity)
        {
            for(int i=0; i < itemQuantity; i++)
            {
                Buy(itemName);
            }
        }

        public void Void(string itemName)
        {
            throw new NotImplementedException();
        }

        

        public void Void(string itemName, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public Item LastItem()
        {
            throw new NotImplementedException();
        }
    }
}
