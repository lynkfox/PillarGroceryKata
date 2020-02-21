using GroceryStoreReceiptLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class ReceiptUnitTests
    {
        public Receipt SetupReceipt()
        {
            var testItemRepository = new ItemRepository();
            testItemRepository.Add("Milk", 2.49);
            testItemRepository.Add("Rotini", 5.49);
            testItemRepository.Add("TomatoSoup", .49);

            return new Receipt(testItemRepository);
        }
        [TestMethod]
        public void BuyIncreasesTheNumberOfItemsOnAReceipt()
        {
            var testReceipt = SetupReceipt();
            
            int expectedTotalItems = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotalItems, testReceipt.NumberOfItems);

        }

        [TestMethod]
        public void BuyIncreasesTotalByPriceOfItemOnReceipt()
        {
            var testReceipt = SetupReceipt();

            decimal expectedTotal = 2.49m;

            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotal, testReceipt.Total);
        }

        [TestMethod]
        public void BuyWithMultipleItemsOfDifferentPriceGivesProperTotalAndItemCount()
        {
            var testReceipt = SetupReceipt();

            decimal expectedTotal = 7.98m;
            int expectedItemCount = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");

            Assert.AreEqual(expectedTotal, testReceipt.Total);
            Assert.AreEqual(expectedItemCount, testReceipt.NumberOfItems);

        }

        [TestMethod]
        public void BuyWithQuantityProperlyAddsNumberOfItemsToReceiptAndProperTotal()
        {
            var testReceipt = SetupReceipt();
            decimal expectedTotal = 7.47m;
            int expectedItemCount = 3;

            testReceipt.Buy("Milk", 3);

            Assert.AreEqual(expectedTotal, testReceipt.Total);
            Assert.AreEqual(expectedItemCount, testReceipt.NumberOfItems);
        }

        [TestMethod]
        public void BuyAnItemThatDoesntExistReturnsAnItemNotFoundException()
        {
            var testReceipt = SetupReceipt();

            Assert.ThrowsException<ItemNotFound>(() => testReceipt.Buy("JunkFood"));
        }

        [TestMethod]
        public void LastItemReturnsTheNameAndPriceOftheLastItemAdded()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("TomatoSoup");

            string expectedLastItem = "TomatoSoup";
            decimal expectedLastPrice = .49m;

            Assert.AreEqual(expectedLastItem, testReceipt.LastItem().Name);
            Assert.AreEqual(expectedLastPrice, testReceipt.LastItem().Price);
        }
    }
}
