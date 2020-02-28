using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreReceiptLibrary;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class IntegrationTests

    {
      
        
        public Receipt Setup()
        {
            var testItemRepository = new ItemRepository();
            testItemRepository.Add("GroundBeef", 1.99, 1);
            testItemRepository.Add("Milk", 2.49);
            testItemRepository.Add("TomatoSoup", .49);
            testItemRepository.Add("RotiniPasta", 5.95);
            testItemRepository.Add("Onions", 1.39);
            testItemRepository.Add("Salmon", 9.99, 1);
            testItemRepository.Add("PeanutButter", 2.09);

            testItemRepository.Markdown("Milk", 1);
            testItemRepository.Markdown("Salmon", .49);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("TomatoSoup", 2, 1, 1,6);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("RotiniPasta", 1, 1, .25, 2);
            testItemRepository.BuyNumberGetDiscountPercentOnEqualOrLesser("Salmon", 4, 1, .5);
            testItemRepository.BuyGroupAtReducedPrice("PeanutButter", 3, 5m);


            var receiptBeingTested = new Receipt(testItemRepository);
            receiptBeingTested.Buy("Onions"); //+1.39 -- Use Case 1
            receiptBeingTested.Buy("GroundBeef", 1, 3.5); //+6.96  -- Use Case 2 
            receiptBeingTested.Buy("Milk"); // +1.49 -- Use Case 3a
            receiptBeingTested.Buy("Salmon", 1, 3.2); //+25.6 -- Use Case 3b
            receiptBeingTested.Buy("TomatoSoup", 3); //+.98 -- Use Case 4a
            receiptBeingTested.Buy("RotiniPasta", 2); //+10.41 -- Use Case 4b
            receiptBeingTested.Buy("PeanutButter", 3); //+5 -- Use Case 5
            receiptBeingTested.Buy("RotiniPasta"); //+5.95 -- Already Hit Limit, Use Case 6
            receiptBeingTested.Void("Onions"); // -1.39 -- Use Case 7a
            receiptBeingTested.Void("RotiniPasta", 2);  //-5.95 - 4.46 -- Use Case 7b 
            receiptBeingTested.Buy("Salmon", 1, 1.8); //+7.59 for remaining to 4lbs, +4.75 for remaining lbs -- use case 8



            return receiptBeingTested;
            
        }
        [TestMethod]
        public void ReceiptObjectHasCollectionOfGroceryItemsWithATotalWithQuantityAndLastItemAddedIsKnown()
        {
            var receipt = Setup();

            decimal expectedTotal = 58.32m;
            int expectedNumberOfItems = 11; //Ground Beef and Salmon only count as 1 each
            string expectedLastItem = "Salmon";
            
            Assert.AreEqual(expectedTotal, receipt.Total()); //Use Case 1 
            Assert.AreEqual(expectedNumberOfItems, receipt.ItemCount());
            Assert.AreEqual(expectedLastItem, receipt.LastItem().Name);

        }
    }
}
