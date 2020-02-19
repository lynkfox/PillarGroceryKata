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
            double expectedPrice = .49;

            itemRepository.Add("TomatoSoup", .49);

            Assert.AreEqual(expectedPrice, itemRepository.PriceCheck("TomatoSoup"));

            
        }
    }
}
