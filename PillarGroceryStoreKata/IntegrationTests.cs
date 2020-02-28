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
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("TomatoSoup", 2, 1, 1, 6);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("RotiniPasta", 1, 1, .25, 2);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Salmon", 4, 1, .5, 5);
            testItemRepository.SetEqualOrLesserAmount("Salmon");
            testItemRepository.BuyGroupAtReducedPrice("PeanutButter", 3, 5m);


            var receiptBeingTested = new Receipt(testItemRepository);
            receiptBeingTested.Buy("Onions"); //Use Case 1
            receiptBeingTested.Buy("GroundBeef", 1, 3.5); // Use Case 2 
            receiptBeingTested.Buy("Milk"); //Use Case 3a
            receiptBeingTested.Buy("Salmon", 1, 4); //Use Case 3b
            receiptBeingTested.Buy("TomatoSoup", 3); //Use Case 4a
            receiptBeingTested.Buy("RotiniPasta", 2); //Use Case 4b
            receiptBeingTested.Buy("PeanutButter", 3); //Use Case 5
            receiptBeingTested.Buy("RotiniPasta"); //Already Hit Limit, Use Case 6
            receiptBeingTested.Void("Onions"); //Use Case 7a
            receiptBeingTested.Void("RotiniPasta", 2);  //Use Case 7b 
            receiptBeingTested.Buy("Salmon", 1, 1); //Use Case 8

            return receiptBeingTested;
            
        }
        [TestMethod]
        public void ReceiptObjectHasCollectionOfGroceryItemsWithATotalWithQuantityAndLastItemAddedIsKnown()
        {
            var receipt = Setup();

            decimal expectedTotal = 65.34m;
            int expectedNumberOfItems = 11; //Ground Beef and Salmon only count as 1 each
            string expectedLastItem = "Salmon";
            
            Assert.AreEqual(expectedTotal, receipt.Total()); //Use Case 1 
            Assert.AreEqual(expectedNumberOfItems, receipt.ItemCount());
            Assert.AreEqual(expectedLastItem, receipt.LastItem().Name);

        }
    }
}
