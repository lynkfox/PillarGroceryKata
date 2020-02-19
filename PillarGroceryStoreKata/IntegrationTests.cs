using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreReceiptLibrary;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class IntegrationTests

    {
      
        
        public Receipt Setup()
        {
            var ItemRepository = new ItemRepository();
            ItemRepository.Add("GroundBeef", 1.99, 1);
            ItemRepository.Add("Milk", 2.49);
            ItemRepository.Add("TomatoSoup", .49);
            ItemRepository.Add("RotiniPasta", 5.95);
            ItemRepository.Add("Onions", 1.39);
            ItemRepository.Add("Salmon", 9.99, 1);

            ItemRepository.Markdown("Milk", .49);
            ItemRepository.BuyNumberGetNumberFreeLimitNumber("TomatoSoup", 2, 1, 6);
            ItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Milk", 1, 1, .25, 2);
            ItemRepository.BuyNumberGetDiscountPercentOnEqualOrLesser("GroundBeef", 2, .5);


            var receiptBeingTested = new Receipt();
            receiptBeingTested.Buy("RotiniPasta", 1); //+5.95
            receiptBeingTested.Buy("TomatoSoup"); //+.49
            receiptBeingTested.Buy("TomatoSoup"); //+.49
            receiptBeingTested.Buy("TomatoSoup"); //+0
            receiptBeingTested.Buy("TomatoSoup", 4); // +.49+.49+.0+.49 (1.47)
            receiptBeingTested.Buy("GroundBeef", 3); //+5.97
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

            double expectedTotal = 44.62;
            int expectedNumberOfItems = 11; //Ground Beef and Salmon only count as 1 each
            string expectedLastItem = "Milk";
            
            Assert.AreEqual(expectedTotal, receipt.Total);
            Assert.AreEqual(expectedNumberOfItems, receipt.NumberOfItems);
            Assert.AreEqual(expectedLastItem, receipt.LastItem());

        }
    }
}
