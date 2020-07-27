using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService.Interfaces;
using TaxService.Services;

namespace TaxService.Tests
{
    [TestClass]
    public class TestOrder
    {
        [DataTestMethod]
        [DataRow("33145", 0.07)]
        [DataRow("02118", 0.07)]
        [DataRow("91714", 0.1025)]
        public void TaxRatesService(string zip, double exprate)
        {
            // Test Calculator
            var taxcalc = new TaxCalculator.Taxjar.Calculator() as ITaxService;
            var salestaxcalc = taxcalc.GetTaxRate(zip);
            var diff = salestaxcalc - (decimal)exprate;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

            // pass and tess as TaxService
            var taxserv = new Services.TaxService(taxcalc);
            var salestaxserv = taxserv.GetTaxRate(zip);
            diff = salestaxserv - (decimal)exprate;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
        }

        [TestMethod]
        public void OrderItemSimpleTaxed()
        {
            var order = new Order();
            order.AddLineItem(new OrderItem("taxed", 1m), 2);
            Assert.AreEqual(1, order.LineItems.Count);

            var taxcalc = new TaxCalculator.Taxjar.Calculator() as ITaxService;
            var salestax = taxcalc.GetTaxRate("33145");
            var diff = salestax - 0.07m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

            taxcalc.CalculateFromOrder(order, salestax);

            diff = order.TotalPreTax - 2m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            diff = order.Tax - 0.14m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            diff = order.Total - 2.14m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

        }


        [TestMethod]
        public void OrderItemEmpty()
        {
            var order = new Order();
            Assert.AreEqual(0, order.LineItems.Count);
            var diff = order.TotalPreTax - 0m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            diff = order.Tax - 0m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
            diff = order.Total - 0m;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
        }
    }
}
