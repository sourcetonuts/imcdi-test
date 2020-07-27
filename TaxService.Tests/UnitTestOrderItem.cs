using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService.Interfaces;
using TaxService.Services;

namespace TaxService.Tests
{
    [TestClass]
    public class UnitTestLineItem
    {

        [TestMethod]
        public void OrderItemDefault()
        {
            var item = new OrderItem();
            Assert.AreEqual(0, item.Price);
            Assert.IsTrue(item.IsTaxible);
            Assert.AreEqual("undefined", item.Description);
        }

        [TestMethod]
        public void OrderItemTaxible()
        {
            var item = new OrderItem( "taxible", 1);
            Assert.AreEqual(1, item.Price);
            Assert.IsTrue(item.IsTaxible);
            Assert.AreEqual("taxible", item.Description);
        }

        [TestMethod]
        public void LineItemDefault()
        {
            var item = new OrderItem();
            var lineitem = new LineItem(item) as ILineItem;
            Assert.AreEqual(0, lineitem.Price);
            Assert.AreEqual("undefined", lineitem.Description);
        }

        [TestMethod]
        public void LineItemBasic()
        {
            var item = new OrderItem("taxible", 0.99m);
            var lineitem = new LineItem(item) as ILineItem;
            Assert.AreEqual(0.99m, lineitem.Price);
            Assert.AreEqual("taxible", lineitem.Description);
        }

        [TestMethod]
        public void LineItemBasicChangeQuantity()
        {
            var item = new OrderItem("taxible", 0.99m);
            var lineitem = new LineItem(item) as ILineItem;
            var diff = lineitem.Price - 0.99m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            Assert.AreEqual("taxible", lineitem.Description);

            lineitem.Quantity = 2;
            diff = lineitem.Price - 1.98m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            Assert.AreEqual("taxible", lineitem.Description);

            lineitem.Quantity = 0;
            diff = lineitem.Price - 0m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

            lineitem.Quantity = 100;
            diff = lineitem.Price - 99.0m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
        }
    }
}
