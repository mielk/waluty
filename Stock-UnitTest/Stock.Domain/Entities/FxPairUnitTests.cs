using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class FxPairUnitTests
    {


        [TestMethod]
        public void constructor_new_instance_has_proper_id_and_name()
        {
            var id = 2;
            var name = "name";
            var baseCurrency = new Currency(1, "USD");
            var quoteCurrency = new Currency(2, "EUR");
            var pair = new FxPair(id, name, baseCurrency, quoteCurrency);

            Assert.AreEqual(id, pair.Id);
            Assert.AreEqual(name, pair.Name);

        }


        [TestMethod]
        public void constructor_new_instance_empty_list_of_assetTimeframes_is_created()
        {
            var baseCurrency = new Currency(1, "USD");
            var quoteCurrency = new Currency(2, "EUR");
            var pair = new FxPair(1, "x", baseCurrency, quoteCurrency);
            Assert.IsNotNull(pair.AssetTimeframes);
            Assert.IsInstanceOfType(pair.AssetTimeframes, typeof(IEnumerable<AssetTimeframe>));
        }


        [TestMethod]
        public void constructor_throw_exception_if_the_same_currency_ids()
        {
            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void constructor_throw_exception_if_given_currency_doesnt_exist()
        {
            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void constructor_throw_exception_if_the_same_currencies_given()
        {
            Assert.Fail("Not implemented");
        }


    }
}
