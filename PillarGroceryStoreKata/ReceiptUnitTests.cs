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
            var testReceipt = new Receipt();
            var itemRepository = new ItemRepository();
            itemRepository.Add("Milk", 2.49);
            int expectedTotalItems = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotalItems, testReceipt.NumberOfItems);

        }
    }
}
