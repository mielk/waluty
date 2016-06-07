using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class AssetUnitTests
    {

        [TestMethod]
        public void constructor_new_instance_has_proper_id_and_name()
        {
            const int ID = 2;
            const string NAME = "name";
            var asset = new Asset(ID, NAME);

            Assert.AreEqual(ID, asset.Id);
            Assert.AreEqual(NAME, asset.Name);

        }

        [TestMethod]
        public void constructor_new_instance_empty_list_of_assetTimeframes_is_created()
        {
            const int ID = 2;
            const string NAME = "name";
            var asset = new Asset(ID, NAME);
            Assert.IsNotNull(asset.AssetTimeframes);
            Assert.IsInstanceOfType(asset.AssetTimeframes, typeof(IEnumerable<AssetTimeframe>));
        }



        [TestMethod]
        public void assetFromDto_has_the_same_properties_as_dto()
        {
            Assert.Fail("Not implemented");
        }

    }
}
