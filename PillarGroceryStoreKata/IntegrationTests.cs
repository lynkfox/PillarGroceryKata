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

            testItemRepository.Markdown("Milk", .49);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("TomatoSoup", 2, 1, 1,6);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Milk", 1, 1, .25, 2);
            testItemRepository.BuyNumberGetDiscountPercentOnEqualOrLesser("GroundBeef", 2, .5);


            var receiptBeingTested = new Receipt(testItemRepository);
            receiptBeingTested.Buy("RotiniPasta", 1); //+5.95
            receiptBeingTested.Buy("TomatoSoup"); //+.49
            receiptBeingTested.Buy("TomatoSoup"); //+.49
            receiptBeingTested.Buy("TomatoSoup"); //+0
            receiptBeingTested.Buy("TomatoSoup", 4); // +.49+.49+.0+.49 (1.47)
            receiptBeingTested.Buy("GroundBeef", 1, 3.5); //+6.97
            receiptBeingTested.Buy("Salmon", 3); //+29.97
            receiptBeingTested.Buy("Milk", 3); //+8.72
            receiptBeingTested.Void("RotiniPasta"); //-5.95
            receiptBeingTested.Void("Milk", 1); // -2.49

            return receiptBeingTested;
            
        }
        [TestMethod]
        public void ReceiptObjectHasCollectionOfGroceryItemsWithATotalWithQuantityAndLastItemAddedIsKnown()
        {
            var receipt = Setup();

            decimal expectedTotal = 44.62m;
            int expectedNumberOfItems = 11; //Ground Beef and Salmon only count as 1 each
            string expectedLastItem = "Milk";
            
            Assert.AreEqual(expectedTotal, receipt.Total());
            Assert.AreEqual(expectedNumberOfItems, receipt.ItemCount());
            Assert.AreEqual(expectedLastItem, receipt.LastItem().Name);

        }
    }
}
