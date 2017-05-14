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


        #region SETTING_NEW_PRICE_OBJECT

        [TestMethod]
        public void AfterProcessing_PriceObjectIsNotSet_IfQuotationIsNull()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(1, 1, new DateTime(2017, 5, 1, 12, 0, 0), 1);
            DataSet dataSet2 = new DataSet(1, 1, new DateTime(2017, 5, 1, 12, 5, 0), 2);
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
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            DataSet dataSet1 = new DataSet(1, 1, new DateTime(2017, 5, 1, 12, 0, 0), 1);
            DataSet dataSet2 = new DataSet(quotation2);
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
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09194, Volume = 1819, IndexNumber = 3 };
            DataSet dataSet2 = new DataSet(quotation2);
            DataSet dataSet3 = new DataSet(quotation3);
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
            Quotation quotation1 = new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 };
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 };
            DataSet dataSet1 = new DataSet(quotation1);
            DataSet dataSet2 = new DataSet(quotation2);
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
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 };
            DataSet dataSet1 = new DataSet(1, 1, new DateTime(2016, 1, 15, 22, 25, 0), 1);
            DataSet dataSet2 = new DataSet(quotation2);
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
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 3 };
            DataSet dataSet2 = new DataSet(quotation2);
            DataSet dataSet3 = new DataSet(quotation3);
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
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09213, Volume = 1819, IndexNumber = 3 };
            DataSet dataSet2 = new DataSet(quotation2);
            DataSet dataSet3 = new DataSet(quotation3);
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
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

        #endregion CREATING_EXTREMA_OBJECT



    }
}
