using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GroceryStoreReceiptLibrary
{
    public class Receipt
    {
        private readonly ItemRepository PriceList;
        private Stack<Item> ItemsOnReceipt = new Stack<Item>();

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
            var price = Math.Round(PriceList.PriceCheck(itemName), 2);

            
            Total += price;
            ItemsOnReceipt.Push(new Item(itemName, price));
            NumberOfItems = ItemsOnReceipt.Count();
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
            return ItemsOnReceipt.Peek();
        }

        public void Void()
        {
            throw new NotImplementedException();
        }
    }
}
