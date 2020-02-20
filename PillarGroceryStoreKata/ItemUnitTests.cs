using GroceryStoreReceiptLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PillarGroceryStoreKata
{
    [TestClass]
    public class ItemUnitTests
    {
        [TestMethod]
        public void AnItemHasANameAndAPrice()
        {
            var testItem = new Item("GroundBeef", 1.99);

            string expectedName = "GroundBeef";
            double expectedPrice = 1.99;

            Assert.AreEqual(expectedName, testItem.Name);
            Assert.AreEqual(expectedPrice, testItem.Price);
        }
    }
}
