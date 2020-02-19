using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreReceiptLibrary;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class IntegrationTests

    {
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            ItemRepository groceryStoreItems = new ItemRepository();
            groceryStoreItems.Add("GroundBeef", 1.99, 1);
            groceryStoreItems.Add("Milk", 2.49);
            groceryStoreItems.Add("TomatoSoup", .49);
            groceryStoreItems.Add("RotiniPasta", 5.95);
            groceryStoreItems.Add("Onions", 1.39);
            groceryStoreItems.Add("Salmon", 9.99, 1);

            groceryStoreItems.Markdown("Milk", .49);
            groceryStoreItems.BuyNumberGetNumberFreeLimitNumber("TomatoSoup", 2, 1, 6);
            groceryStoreItems.BuyNumberGetNumberAtDiscountPercentLimitNumber("Milk", 1, 1, .25, 2);
            groceryStoreItems.BuyNumberGetDiscountPercentOnEqualOrLesser("GroundBeef", 2, .5);
        }
        
        public Receipt Setup()
        {
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
