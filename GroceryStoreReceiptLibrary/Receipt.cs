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

        public double Total { get; set; }
        public int NumberOfItems { get; set; }



        public void Buy(string itemName)
        {
            NumberOfItems++;
            Total += PriceList.PriceCheck(itemName);
        }
        public void Buy(string itemName, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public void Void(string itemName)
        {
            throw new NotImplementedException();
        }

        

        public void Void(string itemName, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public string LastItem()
        {
            throw new NotImplementedException();
        }
    }
}
