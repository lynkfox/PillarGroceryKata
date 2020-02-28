using GroceryStoreReceiptLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class ItemRepositoryUnitTests
    {
        [TestMethod]
        public void WhenItemAddedToRepositoryCollectionGrowsBy1()
        {
            var itemRepository = new ItemRepository();

            itemRepository.Add("TomatoSoup", .49);

            Assert.AreEqual(1, itemRepository.Count);
        }

        [TestMethod]
        public void ItemAddedToRepositoryCanBeRecalledForPriceCheck()
        {
            var itemRepository = new ItemRepository();
            decimal expectedPrice = .49m;

            itemRepository.Add("TomatoSoup", .49);

            Assert.AreEqual(expectedPrice, itemRepository.PriceCheck("TomatoSoup"));
        }

        [TestMethod]
        public void ItemAddedTwiceUpdatesToNewPriceAndDoesNotAddASecondItem()
        {
            var itemRepository = new ItemRepository();
            decimal expectedPrice = 1.49m;
            int expectedItemCount = 1;

            itemRepository.Add("TomatoSoup", .49);
            itemRepository.Add("TomatoSoup", 1.49);

            Assert.AreEqual(expectedPrice, itemRepository.PriceCheck("TomatoSoup"));
            Assert.AreEqual(expectedItemCount, itemRepository.Count);
        }
        [TestMethod]
        public void ItemAddedWithPricePerUnitQualificationProperlyAddsItem()
        {
            var itemRepository = new ItemRepository();
            decimal expectedPrice = 1.99m;

            itemRepository.Add("GroundBeef", 1.99, 1);

            Assert.AreEqual(expectedPrice, itemRepository.PriceCheck("GroundBeef"));
        }

        [TestMethod]
        public void MarkdownCausesPriceCheckToReturnPriceAsChangedByMarkdownAmount()
        {
            var itemRepository = new ItemRepository();
            decimal expectedPrice = 2;

            itemRepository.Add("Milk", 2.49);
            itemRepository.Markdown("Milk", .49);

            Assert.AreEqual(expectedPrice, itemRepository.PriceCheck("Milk"));
        }

        [TestMethod]
        public void CheckSaleInformationReturnsAnItemOBject()
        {
            var itemRepository = new ItemRepository();
            itemRepository.Add("Milk", 2.49);

            Assert.IsInstanceOfType(itemRepository.CheckSaleInfo("Milk"), typeof(Item));
        }


        [TestMethod]
        public void BuyNumberGetNumberFreeLimitNumberSetsUpTheNeededNumbers()
        {
            var itemRepository = new ItemRepository();
            itemRepository.Add("Milk", 2.49);
            itemRepository.BuyNumberGetNumberFreeLimitNumber("Milk", 2, 1, 3);

            int expectedLimit = 3;
            int expectedRequiredToBuy = 2;
            int expectedGainedFree = 1;

            Item testItem = itemRepository.CheckSaleInfo("Milk");

            Assert.AreEqual(expectedLimit, testItem.BOGOLimit);
            Assert.AreEqual(expectedRequiredToBuy, testItem.BOGOPurchasedNumber);
            Assert.AreEqual(expectedGainedFree, testItem.BOGOFreeReceivedNumber);

        }

        [TestMethod]
        public void BuyNumberGetNumberAtDiscountPercentLimitNumberSetsUpProperInformationInItem()
        {
            var itemRepository = new ItemRepository();
            itemRepository.Add("Bread", 1.49);
            itemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Bread", 4, 2, .50, 6);

            int expectedRequiredToGetDiscount = 4;
            int expectedToReceiveDiscount = 2;
            decimal expectedDiscountPercentage = .50m;
            int expectedDiscountLimit = 6;


            Item testItem = itemRepository.CheckSaleInfo("Bread");

            Assert.AreEqual(expectedRequiredToGetDiscount, testItem.RequiredToGetDiscount);
            Assert.AreEqual(expectedToReceiveDiscount, testItem.ToReceiveDiscount);
            Assert.AreEqual(expectedDiscountPercentage, testItem.DiscountPercentage);
            Assert.AreEqual(expectedDiscountLimit, testItem.DiscountLimit);

        }


    }
}
