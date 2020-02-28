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
        [TestMethod]
        public void ItemCanHaveAPerUnitDesignationOfPricePerUnit()
        {
            decimal price = 1.99m;
            int unit = 1;
            var testItem = new Item("GroundBeef", price, unit);

            string expectedName = "GroundBeef";
            decimal expectedPrice = 1.99m;
            bool expectedPerUnit = true;

            Assert.AreEqual(expectedName, testItem.Name);
            Assert.AreEqual(expectedPrice, testItem.Price);
            Assert.AreEqual(expectedPerUnit, testItem.PriceIsPerWeight);

        }
    }
}
