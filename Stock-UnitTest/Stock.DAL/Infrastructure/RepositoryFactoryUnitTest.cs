using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;

namespace Stock_UnitTest.Stock.DAL.Infrastructure
{
    [TestClass]
    public class RepositoryFactoryUnitTest
    {

        [TestMethod]
        public void GetMarketRepository_alwaysReturnsSingletonInstance()
        {
            IMarketRepository repository = RepositoryFactory.GetMarketRepository();
            IMarketRepository repository2 = RepositoryFactory.GetMarketRepository();
            Assert.AreSame(repository, repository2);
        }

        [TestMethod]
        public void GetCurrencyRepository_alwaysReturnsSingletonInstance()
        {
            ICurrencyRepository repository = RepositoryFactory.GetCurrencyRepository();
            ICurrencyRepository repository2 = RepositoryFactory.GetCurrencyRepository();
            Assert.AreSame(repository, repository2);
        }

        [TestMethod]
        public void GetDataRepository_alwaysReturnsSingletonInstance()
        {
            IDataRepository repository = RepositoryFactory.GetDataRepository();
            IDataRepository repository2 = RepositoryFactory.GetDataRepository();
            Assert.AreSame(repository, repository2);
        }

        [TestMethod]
        public void GetTimeframeRepository_alwaysReturnsSingletonInstance()
        {
            ITimeframeRepository repository = RepositoryFactory.GetTimeframeRepository();
            ITimeframeRepository repository2 = RepositoryFactory.GetTimeframeRepository();
            Assert.AreSame(repository, repository2);
        }


    }
}