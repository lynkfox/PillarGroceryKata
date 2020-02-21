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
            ItemsOnReceipt.Add(new Item(itemName, price));
            NumberOfItems = ItemsOnReceipt.Count();
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
            ItemsOnReceipt.Remove(ItemsOnReceipt.Last());
        }

        public void Void(string itemName)
        {
            var itemToBeRemoved = ItemsOnReceipt.Where(x => x.Name == itemName).Last();
            Total -= itemToBeRemoved.Price;
            ItemsOnReceipt.Remove(itemToBeRemoved);
            NumberOfItems = ItemsOnReceipt.Count();

        }

        

        public void Void(string itemName, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public Item LastItem()
        {
            return ItemsOnReceipt.Last();
        }

       
    }
}
