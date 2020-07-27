using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
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
        public void TaxRatesServiceJSON(string zip, double exprate)
        {
            var expectedrate = (decimal)exprate;

            // Test Calculator JSON
            var taxcalc = new TaxCalculator.Taxjar.CalculatorAPI() as ITaxService;
            var result = taxcalc.GetTaxRate(zip);
            result.Wait();
            var salestaxcalc = result.Result;
            var diff = salestaxcalc - expectedrate;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

            // Test Calculator raw JSON
            taxcalc = new TaxCalculator.Taxjar.CalculatorJSON() as ITaxService;
            result = taxcalc.GetTaxRate(zip);
            result.Wait();
            var salestaxraw = result.Result;
            diff = salestaxraw - expectedrate;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);

            // pass raw json caculator and test as TaxService
            var taxserv = new Services.TaxService(taxcalc);
            result = taxserv.GetTaxRate(zip);
            result.Wait();
            var salestaxserv = result.Result;
            diff = salestaxserv - expectedrate;
            Assert.IsTrue(Math.Abs(diff) < 0.01m);
        }

        [TestMethod]
        public async Task OrderItemSimpleTaxed()
        {
            var order = new Order();
            order.AddLineItem(new OrderItem("taxed", 1m), 2);
            Assert.AreEqual(1, order.LineItems.Count);

            var taxcalc = new TaxCalculator.Taxjar.CalculatorJSON() as ITaxService;
            var salestax = await taxcalc.GetTaxRate("33145");
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
