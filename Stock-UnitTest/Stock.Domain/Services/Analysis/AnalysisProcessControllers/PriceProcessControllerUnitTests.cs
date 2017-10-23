using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services;
using Stock.Utils;
using Stock.Core;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class PriceProcessControllerUnitTests
    {

        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_SIMULATION_ID = 1;
        private DateTime DEFAULT_BASE_DATE = new DateTime(2016, 1, 15, 22, 25, 0);



        #region INFRASTRUCTURE

        private DataSet getDataSetWithQuotation(int indexNumber, double open, double high, double low, double close, double volume)
        {
            return getDataSetWithQuotation(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber, open, high, low, close, volume);
        }

        private DataSet getDataSetWithQuotation(int assetId, int timeframeId, int indexNumber, double open, double high, double low, double close, double volume)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            Quotation q = new Quotation(ds) { Open = open, High = high, Low = low, Close = close, Volume = volume };
            return ds;

        }


        private DataSet getDataSetWithQuotationAndPrice(int indexNumber, double open, double high, double low, double close, double volume)
        {
            return getDataSetWithQuotationAndPrice(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber, open, high, low, close, volume);
        }

        private DataSet getDataSetWithQuotationAndPrice(int assetId, int timeframeId, int indexNumber, double open, double high, double low, double close, double volume)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            Quotation q = new Quotation(ds) { Open = open, High = high, Low = low, Close = close, Volume = volume };
            Price p = new Price(ds);
            return ds;

        }


        #endregion INFRASTRUCTURE


        [TestMethod]
        public void Run_AllItemsFromLastUpdatedMinusFixedNumberOfQuotationsAreChecked_IfThereIsNotEnoughItems()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet[] dataSets = new DataSet[13];
            dataSets[1] = getDataSetWithQuotation(1, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            dataSets[2] = getDataSetWithQuotation(2, 1.09193, 1.09256, 1.09165, 1.09177, 1819);
            dataSets[3] = getDataSetWithQuotation(3, 1.09176, 1.09182, 1.09142, 1.09151, 1359);
            dataSets[4] = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            dataSets[5] = getDataSetWithQuotation(5, 1.09111, 1.09124, 1.09091, 1.091, 1154);
            dataSets[6] = getDataSetWithQuotation(6, 1.09101, 1.09132, 1.09097, 1.09131, 933);
            dataSets[7] = getDataSetWithQuotation(7, 1.09131, 1.09167, 1.09114, 1.09165, 1079);
            dataSets[8] = getDataSetWithQuotation(8, 1.09164, 1.09183, 1.0915, 1.09177, 1009);
            dataSets[9] = getDataSetWithQuotation(9, 1.09178, 1.09189, 1.09143, 1.09149, 657);
            dataSets[10] = getDataSetWithQuotation(10, 1.0915, 1.09164, 1.09144, 1.09148, 414);
            dataSets[11] = getDataSetWithQuotation(11, 1.09149, 1.09156, 1.09095, 1.091, 419);
            dataSets[12] = getDataSetWithQuotation(12, 1.09098, 1.09118, 1.09091, 1.09108, 341);
            for (int i = 1; i <= 12; i++)
            {
                mockedManager.Setup(m => m.GetDataSet(i)).Returns(dataSets[i]);
            }

            mockedManager.Setup(m => m.GetAnalysisLastUpdatedIndex(AnalysisType.Prices)).Returns(4);
            mockedManager.Setup(m => m.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations)).Returns(12);

            Mock<IPriceProcessor> mockedProcessor = new Mock<IPriceProcessor>();
            mockedProcessor.Setup(m => m.Process(It.IsAny<DataSet>())).Verifiable();

            //Act
            PriceProcessController controller = new PriceProcessController();
            controller.InjectPriceProcessor(mockedProcessor.Object);
            controller.HowManyItemsBeforeInclude = 5;
            controller.Run(mockedManager.Object);

            //Assert
            mockedProcessor.Verify(m => m.Process(It.IsAny<DataSet>()), Times.Exactly(12));
            for (var i = 1; i <= 12; i++)
            {
                DataSet ds = dataSets[i];
                mockedProcessor.Verify(m => m.Process(ds), Times.Once());
            }

        }


        [TestMethod]
        public void Run_ItemsBeforeLastUpdatedMinusFixedNumberOfQuotationsAreNotChecked()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet[] dataSets = new DataSet[13];
            dataSets[1] = getDataSetWithQuotation(1, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            dataSets[2] = getDataSetWithQuotation(2, 1.09193, 1.09256, 1.09165, 1.09177, 1819);
            dataSets[3] = getDataSetWithQuotation(3, 1.09176, 1.09182, 1.09142, 1.09151, 1359);
            dataSets[4] = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            dataSets[5] = getDataSetWithQuotation(5, 1.09111, 1.09124, 1.09091, 1.091, 1154);
            dataSets[6] = getDataSetWithQuotation(6, 1.09101, 1.09132, 1.09097, 1.09131, 933);
            dataSets[7] = getDataSetWithQuotation(7, 1.09131, 1.09167, 1.09114, 1.09165, 1079);
            dataSets[8] = getDataSetWithQuotation(8, 1.09164, 1.09183, 1.0915, 1.09177, 1009);
            dataSets[9] = getDataSetWithQuotation(9, 1.09178, 1.09189, 1.09143, 1.09149, 657);
            dataSets[10] = getDataSetWithQuotation(10, 1.0915, 1.09164, 1.09144, 1.09148, 414);
            dataSets[11] = getDataSetWithQuotation(11, 1.09149, 1.09156, 1.09095, 1.091, 419);
            dataSets[12] = getDataSetWithQuotation(12, 1.09098, 1.09118, 1.09091, 1.09108, 341);
            for (int i = 1; i <= 12; i++)
            {
                mockedManager.Setup(m => m.GetDataSet(i)).Returns(dataSets[i]);
            }


            mockedManager.Setup(m => m.GetAnalysisLastUpdatedIndex(AnalysisType.Prices)).Returns(10);
            mockedManager.Setup(m => m.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations)).Returns(12);

            Mock<IPriceProcessor> mockedProcessor = new Mock<IPriceProcessor>();
            mockedProcessor.Setup(m => m.Process(It.IsAny<DataSet>())).Verifiable();

            //Act
            PriceProcessController controller = new PriceProcessController();
            controller.InjectPriceProcessor(mockedProcessor.Object);
            controller.HowManyItemsBeforeInclude = 5;
            controller.Run(mockedManager.Object);

            //Assert
            mockedProcessor.Verify(m => m.Process(It.IsAny<DataSet>()), Times.Exactly(8));
            for (var i = 0; i <= 4; i++)
            {
                DataSet ds = dataSets[i];
                mockedProcessor.Verify(m => m.Process(ds), Times.Never());
            }

            for (var i = 5; i <= 12; i++)
            {
                DataSet ds = dataSets[i];
                mockedProcessor.Verify(m => m.Process(ds), Times.Once());
            }

        }



    }
}
