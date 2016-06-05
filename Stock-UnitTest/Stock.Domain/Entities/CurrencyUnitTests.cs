using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class CurrencyUnitTests
    {

        [TestMethod]
        public void constructor_new_instance_has_proper_id_and_name()
        {
            var id = 2;
            var name = "name";
            var currency = new Currency(id, name);

            Assert.AreEqual(id, currency.Id);
            Assert.AreEqual(name, currency.Name);

        }


        [TestMethod]
        [TestCategory("FetchCurrencyFromDb")]
        public void fetchCurrencyFromDb_return_instance_with_proper_id_and_name()
        {
            Assert.Fail("Not implemented yet");
        }


        [TestMethod]
        [TestCategory("FetchCurrencyFromDb")]
        public void fetchCurrencyFromDb_return_doesnt_create_new_instance_of_the_same_currency()
        {
            Assert.Fail("Not implemented yet");
        }


    }
}
