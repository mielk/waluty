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
            var id = 2;
            var name = "name";
            var asset = new Asset(id, name);

            Assert.AreEqual(id, asset.Id);
            Assert.AreEqual(name, asset.Name);

        }

        [TestMethod]
        public void constructor_new_instance_empty_list_of_assetTimeframes_is_created()
        {
            var asset = new Asset(1, "x");
            Assert.IsNotNull(asset.AssetTimeframes);
            Assert.IsInstanceOfType(asset.AssetTimeframes, typeof(IEnumerable<AssetTimeframe>));
        }

    }
}
