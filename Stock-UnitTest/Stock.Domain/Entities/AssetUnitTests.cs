using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using System.Collections.Generic;
using Stock.DAL.TransferObjects;
using System.Linq;
using Stock.Domain.Services;
using Moq;

namespace Stock_UnitTest.Stock.Domain.Entities
{
    [TestClass]
    public class AssetUnitTests
    {

        //Arrange.
        private Market getMarket(int id)
        {
            IEnumerable<Market> markets = marketsCollection();
            Market market = markets.SingleOrDefault(m => m.Id == id);
            return market;
        }

        private IEnumerable<Market> marketsCollection()
        {
            List<Market> markets = new List<Market>();
            markets.Add(new Market(1, "Forex") { ShortName = "FX" });
            markets.Add(new Market(2, "United Kingdom") { ShortName = "UK" });
            markets.Add(new Market(3, "United States") { ShortName = "US" });
            return markets;
        }


        #region constructor
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

            //Arrange.
            Mock<IMarketService> mockService = new Mock<IMarketService>();
            mockService.Setup(c => c.GetMarketById(It.IsAny<int>())).Returns((int a) => getMarket(a));
            Market.injectService(mockService.Object);
            AssetDto dto = new AssetDto { Id = 1, IdMarket = 1, Name = "EURUSD", Symbol = "EURUSD" };

            //Act.
            Asset asset = Asset.FromDto(dto);

            //Assert.
            Assert.AreEqual(1, asset.Id);
            Assert.AreEqual("EURUSD", asset.Name);
            Assert.AreEqual("EURUSD", asset.ShortName);

        }

        #endregion constructor





    }
}
