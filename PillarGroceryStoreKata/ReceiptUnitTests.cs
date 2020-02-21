using GroceryStoreReceiptLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class ReceiptUnitTests
    {
        [TestMethod]
        public void BuyIncreasesTheNumberOfItemsOnAReceipt()
        {
            
            var itemRepository = new ItemRepository();
            itemRepository.Add("Milk", 2.49);

            var testReceipt = new Receipt(itemRepository);
            int expectedTotalItems = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotalItems, testReceipt.NumberOfItems);

        }

        [TestMethod]
        public void BuyIncreasesTotalByPriceOfItemOnReceipt()
        {
            var itemRepository = new ItemRepository();
            itemRepository.Add("Milk", 2.49);
            var testReceipt = new Receipt(itemRepository);

            double expectedTotal = 2.49;

            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotal, testReceipt.Total);
        }
    }
}
