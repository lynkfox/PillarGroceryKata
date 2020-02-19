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
            ItemRepository.Add("TomatoSoup", .49);

            Assert.AreEqual(1, ItemRepository.Count);
        }
    }
}
