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
            decimal price = 1.99m;
            var testItem = new Item("GroundBeef", price);

            string expectedName = "GroundBeef";
            decimal expectedPrice = 1.99m;

            Assert.AreEqual(expectedName, testItem.Name);
            Assert.AreEqual(expectedPrice, testItem.Price);
        }


    }
}
