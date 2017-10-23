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
    public class PriceProcessorUnitTests
    {

        private double MAX_DOUBLE_COMPARISON_DIFFERENCE = 0.00000000001;
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_SIMULATION_ID = 1;
        private DateTime DEFAULT_BASE_DATE = new DateTime(2017, 5, 1, 12, 0, 0);



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


        private DataSet getDataSet(int indexNumber)
        {
            return getDataSet(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private DataSet getDataSet(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            DataSet ds = new DataSet(assetId, timeframeId, date, indexNumber);
            return ds;
        }


        private Quotation getQuotation(DataSet ds)
        {
            return new Quotation(ds)
            {
                Id = ds.IndexNumber,
                Open = 1.09191,
                High = 1.09218,
                Low = 1.09186,
                Close = 1.09194,
                Volume = 1411
            };
        }

        private Quotation getQuotation(int indexNumber)
        {
            return getQuotation(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private Quotation getQuotation(int assetId, int timeframeId, int indexNumber)
        {
            DataSet ds = getDataSet(assetId, timeframeId, indexNumber);
            return new Quotation(ds)
            {
                Id = indexNumber,
                Open = 1.09191,
                High = 1.09218,
                Low = 1.09186,
                Close = 1.09194,
                Volume = 1411
            };
        }

        private QuotationDto getQuotationDto(int indexNumber)
        {
            return getQuotationDto(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private QuotationDto getQuotationDto(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            return new QuotationDto()
            {
                PriceDate = date,
                AssetId = assetId,
                TimeframeId = timeframeId,
                OpenPrice = 1.09191,
                HighPrice = 1.09218,
                LowPrice = 1.09186,
                ClosePrice = 1.09194,
                Volume = 1411
            };
        }



        private Price getPrice(DataSet ds)
        {
            return getPrice(ds);
        }

        private Price getPrice(int indexNumber)
        {
            return getPrice(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private Price getPrice(int assetId, int timeframeId, int indexNumber)
        {
            DataSet ds = getDataSet(assetId, timeframeId, indexNumber);
            return new Price(ds)
            {
                Id = indexNumber,
                CloseDelta = 1.05,
                Direction2D = 1,
                Direction3D = 0,
                PriceGap = 1.23,
                CloseRatio = 1.23,
                ExtremumRatio = 2.34
            };
        }

        private PriceDto getPriceDto(int indexNumber)
        {
            return getPriceDto(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, indexNumber);
        }

        private PriceDto getPriceDto(int assetId, int timeframeId, int indexNumber)
        {
            var timeframe = Timeframe.ById(timeframeId);
            DateTime date = timeframe.AddTimeUnits(DEFAULT_BASE_DATE, indexNumber);
            return new PriceDto()
            {
                Id = indexNumber,
                PriceDate = date,
                AssetId = assetId,
                TimeframeId = timeframeId,
                IndexNumber = indexNumber,
                DeltaClosePrice = 1.04,
                PriceDirection2D = 1,
                PriceDirection3D = 1,
                PriceGap = 0.05,
                CloseRatio = 0.23,
                ExtremumRatio = 1
            };
        }

        #endregion INFRASTRUCTURE




        #region SETTING_NEW_PRICE_OBJECT

        [TestMethod]
        public void AfterProcessing_PriceObjectIsNotSet_IfQuotationIsNull()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();

            DataSet dataSet1 = getDataSet(1);
            DataSet dataSet2 = getDataSet(2);
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet2);

            //Assert
            Assert.IsNull(dataSet2.GetPrice());

        }

        [TestMethod]
        public void AfterProcessing_PriceObjectIsSet_IfQuotationIsNotNull()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = getDataSet(1);
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet2);

            //Assert
            Assert.IsNotNull(dataSet2.GetPrice());

        }

        #endregion SETTING_NEW_PRICE_OBJECT


        #region DELTA_CLOSE

        [TestMethod]
        public void AfterProcessing_DeltaCloseIsZero_IfPreviousCloseWasTheSameAsCurrentOne()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            DataSet dataSet3 = getDataSetWithQuotation(3, 1.09193, 1.09256, 1.09165, 1.09194, 1819);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet3);

            //Assert
            var expected = 0;
            var actual = dataSet3.GetPrice().CloseDelta;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void AfterProcessing_DeltaCloseIsZero_IfThisIsItemIndexOne()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = getDataSetWithQuotation(1, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09193, 1.09256, 1.09165, 1.09177, 1819);
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet1);

            //Assert
            var expected = 0;
            var actual = dataSet1.GetPrice().CloseDelta;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void AfterProcessing_DeltaCloseIsZero_IfPreviousQuotationWasNull()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = getDataSet(1);
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09193, 1.09256, 1.09165, 1.09177, 1819);
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet2);

            //Assert
            var expected = 0;
            var actual = dataSet2.GetPrice().CloseDelta;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void AfterProcessing_DeltaCloseIsProperlyCalculated_IfPreviousCloseWasLower()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            DataSet dataSet3 = getDataSetWithQuotation(3, 1.09193, 1.09256, 1.09165, 1.09177, 1819);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet3);

            //Assert
            var expected = -0.00017;
            var actual = dataSet3.GetPrice().CloseDelta;
            var difference = Math.Abs(actual - expected);
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_DeltaCloseIsProperlyCalculated_IfPreviousCloseWasHigher()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet2 = getDataSetWithQuotation(2, 1.09191, 1.09218, 1.09186, 1.09194, 1411);
            DataSet dataSet3 = getDataSetWithQuotation(3, 1.09193, 1.09256, 1.09165, 1.09213, 1819);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.Process(dataSet3);

            //Assert
            var expected = 0.00019;
            var actual = dataSet3.GetPrice().CloseDelta;
            var difference = Math.Abs(actual - expected);
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        #endregion GET_SIMULATIONS


        #region DIRECTION_2D
        #endregion DIRECTION_2D


        #region DIRECTION_3D
        #endregion DIRECTION_3D


        #region PRICE_GAP
        #endregion PRICE_GAP


        #region CLOSE_RATIO
        #endregion CLOSE_RATIO


        #region EXTREMUM_RATIO
        #endregion EXTREMUM_RATIO


        #region CREATING_EXTREMA_OBJECT

        [TestMethod]
        public void AfterProcessing_PriceHasNoPeakByClose_IfExtremaProcessorReturnsFalseForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();

            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.PeakByClose)).Returns(false);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNull(price.GetExtremum(ExtremumType.PeakByClose));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasPeakByClose_IfExtremaProcessorReturnsTrueForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.PeakByClose)).Returns(true);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNotNull(price.GetExtremum(ExtremumType.PeakByClose));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasNoPeakByHigh_IfExtremaProcessorReturnsFalseForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.PeakByHigh)).Returns(false);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNull(price.GetExtremum(ExtremumType.PeakByHigh));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasPeakByHigh_IfExtremaProcessorReturnsTrueForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.PeakByHigh)).Returns(true);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNotNull(price.GetExtremum(ExtremumType.PeakByHigh));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasNoTroughByClose_IfExtremaProcessorReturnsFalseForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByClose)).Returns(false);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNull(price.GetExtremum(ExtremumType.TroughByClose));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasTroughByClose_IfExtremaProcessorReturnsTrueForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByClose)).Returns(true);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNotNull(price.GetExtremum(ExtremumType.TroughByClose));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasNoTroughByLow_IfExtremaProcessorReturnsFalseForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(false);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNull(price.GetExtremum(ExtremumType.TroughByLow));

        }

        [TestMethod]
        public void AfterProcessing_PriceHasTroughByLow_IfExtremaProcessorReturnsTrueForIsExtrema()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Assert.IsNotNull(price.GetExtremum(ExtremumType.TroughByLow));

        }

        [TestMethod]
        public void AfterProcessing_IfThereIsNewPeakByCloseExtremum_ItHasIsUpdatedFlagSetToTrue()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.PeakByClose)).Returns(true);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.PeakByClose);
            Assert.IsTrue(extremum.Updated);

        }

        //Jeżeli tworzony jest nowy obiekt Extremum - ma flagę isNew = true;
        //Jeżeli istniał już wcześniej obiekt Extremum - ma flagę isNew = false;

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierCounter()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierCounter(It.IsAny<Extremum>())).Returns(4);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            int expectedValue = 4;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            Assert.AreEqual(expectedValue, extremum.EarlierCounter);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierAmplitude()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierAmplitude(It.IsAny<Extremum>())).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierAmplitude - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierChange1()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierChange(It.IsAny<Extremum>(), 1)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierChange1 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierChange2()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierChange(It.IsAny<Extremum>(), 2)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierChange2 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierChange3()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierChange(It.IsAny<Extremum>(), 3)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierChange3 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierChange5()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierChange(It.IsAny<Extremum>(), 5)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierChange5 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedEarlierChange10()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateEarlierChange(It.IsAny<Extremum>(), 10)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.EarlierChange10 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }



        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterCounter()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterCounter(It.IsAny<Extremum>())).Returns(4);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            int expectedValue = 4;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            Assert.AreEqual(expectedValue, extremum.LaterCounter);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterAmplitude()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterAmplitude(It.IsAny<Extremum>())).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterAmplitude - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterChange1()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterChange(It.IsAny<Extremum>(), 1)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterChange1 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterChange2()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterChange(It.IsAny<Extremum>(), 2)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterChange2 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterChange3()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterChange(It.IsAny<Extremum>(), 3)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterChange3 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterChange5()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterChange(It.IsAny<Extremum>(), 5)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterChange5 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void AfterProcessing_ExtremumObjectHasProperlyAssignedLaterChange10()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            Mock<IExtremumProcessor> mockedProcessor = new Mock<IExtremumProcessor>();
            DataSet dataSet4 = getDataSetWithQuotation(4, 1.0915, 1.0916, 1.09111, 1.09112, 1392);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedProcessor.Setup(p => p.IsExtremum(dataSet4, ExtremumType.TroughByLow)).Returns(true);
            mockedProcessor.Setup(p => p.CalculateLaterChange(It.IsAny<Extremum>(), 10)).Returns(1.23);

            //Act
            PriceProcessor processor = new PriceProcessor(mockedManager.Object);
            processor.InjectExtremumProcessor(mockedProcessor.Object);
            processor.Process(dataSet4);

            //Assert
            double expectedValue = 1.23;
            Price price = dataSet4.GetPrice();
            Extremum extremum = price.GetExtremum(ExtremumType.TroughByLow);
            var difference = (extremum.LaterChange10 - expectedValue);
            Assert.IsTrue(Math.Abs(difference) < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }


        #endregion CREATING_EXTREMA_OBJECT



    }
}
