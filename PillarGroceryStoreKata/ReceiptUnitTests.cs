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
            testItemRepository.Add("PeanutButter", 1.25);
            testItemRepository.Add("Bread", 2.50);
            testItemRepository.Add("GroundBeef", 1.99, 1);
            testItemRepository.Add("Salmon", 9.99, 1);
            testItemRepository.Add("Soda", 5.69);
            testItemRepository.Add("OysterCrackers", 2.09);

            testItemRepository.Markdown("Salmon", 1.99);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("PeanutButter", 2, 1, 1, 3);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Soda", 2, 1, 1, 6);
            testItemRepository.BuyNumberGetNumberAtDiscountPercentLimitNumber("Bread", 4, 2, .50, 12);
            testItemRepository.BuyGroupAtReducedPrice("OysterCrackers", 3, 5);

            return new Receipt(testItemRepository);
        }
        [TestMethod]
        public void BuyRoundsToNearest2DecimalPlacesUpfromPoint5()
        {
            var testItemRepository = new ItemRepository();
            testItemRepository.Add("RoundUp", 2.455);
            var testReceipt = new Receipt(testItemRepository);

            decimal expectedRoundUp = 2.46m;

            testReceipt.Buy("RoundUp");
            Assert.AreEqual(expectedRoundUp, testReceipt.Total());
        }
        [TestMethod]
        public void BuyRoundsToNearest2DecimalPlacesDownFromPoint4Below()
        {
            var testItemRepository = new ItemRepository();
            testItemRepository.Add("RoundDown", 2.454);
            var testReceipt = new Receipt(testItemRepository);

            decimal expectedRoundDown = 2.45m;

            testReceipt.Buy("RoundDown");
            Assert.AreEqual(expectedRoundDown, testReceipt.Total());
        }
        [TestMethod]
        public void BuyIncreasesTheNumberOfItemsOnAReceipt()
        {
            var testReceipt = SetupReceipt();
            
            int expectedTotalItems = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotalItems, testReceipt.ItemCount());

        }

        [TestMethod]
        public void BuyIncreasesTotalByPriceOfItemOnReceipt()
        {
            var testReceipt = SetupReceipt();

            decimal expectedTotal = 2.49m;

            testReceipt.Buy("Milk");

            Assert.AreEqual(expectedTotal, testReceipt.Total());
        }

        [TestMethod]
        public void BuyWithMultipleItemsOfDifferentPriceGivesProperTotalAndItemCount()
        {
            var testReceipt = SetupReceipt();

            decimal expectedTotal = 7.98m;
            int expectedItemCount = 2;

            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());

        }

        [TestMethod]
        public void BuyWithQuantityProperlyAddsNumberOfItemsToReceiptAndProperTotal()
        {
            var testReceipt = SetupReceipt();
            decimal expectedTotal = 7.47m;
            int expectedItemCount = 3;

            testReceipt.Buy("Milk", 3);

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
        [TestMethod]
        public void BuyAnItemThatIsSoldByWeightProperlyAddsToReceiptAndTotal()
        {
            var testReceipt = SetupReceipt();
            decimal expectedTotal = 6.96m;
            int expectedItemCount = 1;

            testReceipt.Buy("GroundBeef", 1, 3.5);

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }

        [TestMethod]
        public void BuyWithQuantityOfItemThatDoesNotExistThrowsException()
        {
            var testReceipt = SetupReceipt();

            Assert.ThrowsException<ItemNotFound>(() => testReceipt.Buy("JunkFood", 3));
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

        [TestMethod]
        public void VoidRemovesTheLastItemAddedFromTheReceipt()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("TomatoSoup");

            string expectedLastItem = "Rotini";
            decimal expectedLastPrice = 5.49m;

            testReceipt.Void();

            Assert.AreEqual(expectedLastItem, testReceipt.LastItem().Name);
            Assert.AreEqual(expectedLastPrice, testReceipt.LastItem().Price);

        }

        [TestMethod]
        public void ParamterlessVoidAdjustsPriceAndTotalItems()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("TomatoSoup");

            decimal expectedTotal = 7.98m;
            int expectedItemCount = 2;

            testReceipt.Void();

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }

        [TestMethod]
        public void VoidWithAnItemNameRemovesTheLastEntryOfThatItemInReceipt()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("Milk");

            string expectedLastItem = "Milk";

            testReceipt.Void("Rotini");

            Assert.AreEqual(expectedLastItem, testReceipt.LastItem().Name);
        }

        [TestMethod]
        public void VoidWithAnItemNameAdjustsPriceAndTotalItemCount()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("Milk");

            int expectedItemCount = 2;
            decimal expectedTotal = 4.98m;

            testReceipt.Void("Rotini");

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());

        }

        [TestMethod]
        public void VoidThrowsItemNotFoundExceptionIfGivenAnItemNotOnReceipt()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk");
            testReceipt.Buy("Rotini");
            testReceipt.Buy("Milk");

            Assert.ThrowsException<ItemNotFound>(() => testReceipt.Void("JunkFood"));
        }

        [TestMethod]
        public void VoidForItemWithQuantityRemovesTheAppropriateNumberOfItemsFromReceipt()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Milk", 5);
            int expectedNumberOfItems = 3;

            testReceipt.Void("Milk", 2);

            Assert.AreEqual(expectedNumberOfItems, testReceipt.ItemCount());
        }

        [TestMethod]
        public void BuyItemsWithABOGOTypeSaleWillAppropriatelyAddFreeItemsToReceipt()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("PeanutButter"); //1.25 each, Buy 2 get 1 free
            testReceipt.Buy("PeanutButter");
            testReceipt.Buy("PeanutButter");

            decimal expectedTotal = 2.50m;
            int expectedItemCount = 3;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());

        }

        [TestMethod]
        public void BuyItemsWithABOGOTypeSaleAndBuyingMoreThanTheLimitDoesNotProduceExtraFreeItems()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("PeanutButter", 4); //1.25 each, Buy 2 get 1 free, limit 3
            

            decimal expectedTotal = 3.75m;
            int expectedItemCount = 4;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
        [TestMethod]
        public void BuyItemsWithABOGOTypeSaleWithALimitOfMoreThanJustOneSetOfBOGOWillProduceProperAmountOfFreeItems()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Soda", 7); //5.69 each, buy 2 get 1 free, limit 6. 5@5.69+2Free

            decimal expectedTotal = 28.45m;
            int expectedItemCount = 7;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }

        [TestMethod]
        public void BuyItemsWithDiscountTypeSaleAddsProperCostToTotal()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Bread", 6); //4 at 2.50 Each, 2 at 50% (1.25)


            decimal expectedTotal = 12.50m;
            int expectedItemCount = 6;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
        [TestMethod]
        public void BuyItemsWithDiscountTypeSaleAddsProperCostToTotalWhileRespectingLimitOnSale()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Bread", 13); //8 at 2.50 Each, 4 at 50% (1.25), 1 at 2.50

            decimal expectedTotal = 27.50m;
            int expectedItemCount = 13;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }

        [TestMethod]
        public void BuyItemsPerWeightWithAMarkDownProperlyMarksDownEachPound()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("Salmon", 1, 3.2);

            decimal expectedTotal = 25.6m;
            int expectedItemCount = 1;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
       

        [TestMethod]
        public void BuyGroupPriceItemsGetsDiscountedWithDifferentAmountThanGroupNumber()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("OysterCrackers", 4);

            decimal expectedTotal = 6.68m;
            int expectedItemCount = 4;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
        [TestMethod]
        public void BuyGroupPriceItemsGetsDiscountedPriceAtGroupNumber()
        {
            var testReceipt = SetupReceipt();
            testReceipt.Buy("OysterCrackers", 3);

            decimal expectedTotal = 5;
            int expectedItemCount = 3;

            Assert.AreEqual(expectedTotal, testReceipt.Total());
            Assert.AreEqual(expectedItemCount, testReceipt.ItemCount());
        }
    }
}
