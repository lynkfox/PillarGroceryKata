using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreReceiptLibrary
{
    public class Receipt
    {
        public double Total { get; set; }
        public int NumberOfItems { get; set; }

        public void Buy(string itemName)
        {
            throw new NotImplementedException();
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
