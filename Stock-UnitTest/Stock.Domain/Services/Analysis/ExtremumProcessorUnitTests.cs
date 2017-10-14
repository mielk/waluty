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
    public class ExtremumProcessorUnitTests
    {

        private double MAX_DOUBLE_COMPARISON_DIFFERENCE = 0.00000001;


        #region ISUPDATED_FLAG

        [Ignore]
        [TestMethod]
        public void AfterBeingProcessed_ItemExtremumHasItsUpdatedFlagSetToTrue()
        {
            Assert.Fail("Zastanowić się nad tym testem, czy powinien być w tym miejscu.");
            Assert.Fail("Na pewno trzeba to sprawdzić, ale chyba z poziomu klasy wywołującej");
            Assert.Fail("i z bardziej skomplikowanymi parametrami wejściowymi.");
            Assert.Fail("Not implemented yet");
        }

        #endregion ISUPDATED_FLAG


        #region PEAK_BY_CLOSE.IS_EXTREMUM

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByClose_IfProcessedDataSetIndexIsLessOrEqualToMinimumRequiredLowerQuotations()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet3, ExtremumType.PeakByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByClose_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet123 = new DataSet(new Quotation() { Id = 123, Date = new DateTime(2016, 1, 18, 8, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08965, High = 1.08971, Low = 1.08889, Close = 1.08933, Volume = 845, IndexNumber = 123 });
            DataSet dataSet124 = new DataSet(new Quotation() { Id = 124, Date = new DateTime(2016, 1, 18, 8, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08964, Low = 1.08885, Close = 1.089, Volume = 993, IndexNumber = 124 });
            DataSet dataSet125 = new DataSet(new Quotation() { Id = 125, Date = new DateTime(2016, 1, 18, 8, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08897, High = 1.08921, Low = 1.08862, Close = 1.08889, Volume = 681, IndexNumber = 125 });
            DataSet dataSet126 = new DataSet(new Quotation() { Id = 126, Date = new DateTime(2016, 1, 18, 8, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08889, High = 1.08903, Low = 1.0886, Close = 1.08889, Volume = 876, IndexNumber = 126 });
            DataSet dataSet127 = new DataSet(new Quotation() { Id = 127, Date = new DateTime(2016, 1, 18, 8, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08891, High = 1.08922, Low = 1.0886, Close = 1.08894, Volume = 923, IndexNumber = 127 });
            DataSet dataSet128 = new DataSet(new Quotation() { Id = 128, Date = new DateTime(2016, 1, 18, 9, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08893, Low = 1.08771, Close = 1.08805, Volume = 1743, IndexNumber = 128 });
            DataSet dataSet129 = new DataSet(new Quotation() { Id = 129, Date = new DateTime(2016, 1, 18, 9, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08807, High = 1.08845, Low = 1.08749, Close = 1.08765, Volume = 1291, IndexNumber = 129 });
            DataSet dataSet130 = new DataSet(new Quotation() { Id = 130, Date = new DateTime(2016, 1, 18, 9, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08769, High = 1.0881, Low = 1.08731, Close = 1.08752, Volume = 1385, IndexNumber = 130 });
            DataSet dataSet131 = new DataSet(new Quotation() { Id = 131, Date = new DateTime(2016, 1, 18, 9, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08752, High = 1.08829, Low = 1.08747, Close = 1.08825, Volume = 1337, IndexNumber = 131 });
            DataSet dataSet132 = new DataSet(new Quotation() { Id = 132, Date = new DateTime(2016, 1, 18, 9, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08824, High = 1.08849, Low = 1.0881, Close = 1.08829, Volume = 1084, IndexNumber = 132 });
            DataSet dataSet133 = new DataSet(new Quotation() { Id = 133, Date = new DateTime(2016, 1, 18, 9, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08831, High = 1.08872, Low = 1.08817, Close = 1.08853, Volume = 980, IndexNumber = 133 });
            mockedManager.Setup(m => m.GetDataSet(123)).Returns(dataSet123);
            mockedManager.Setup(m => m.GetDataSet(124)).Returns(dataSet124);
            mockedManager.Setup(m => m.GetDataSet(125)).Returns(dataSet125);
            mockedManager.Setup(m => m.GetDataSet(126)).Returns(dataSet126);
            mockedManager.Setup(m => m.GetDataSet(127)).Returns(dataSet127);
            mockedManager.Setup(m => m.GetDataSet(128)).Returns(dataSet128);
            mockedManager.Setup(m => m.GetDataSet(129)).Returns(dataSet129);
            mockedManager.Setup(m => m.GetDataSet(130)).Returns(dataSet130);
            mockedManager.Setup(m => m.GetDataSet(131)).Returns(dataSet131);
            mockedManager.Setup(m => m.GetDataSet(132)).Returns(dataSet132);
            mockedManager.Setup(m => m.GetDataSet(133)).Returns(dataSet133);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet127, ExtremumType.PeakByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByClose_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet123 = new DataSet(new Quotation() { Id = 123, Date = new DateTime(2016, 1, 18, 8, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08965, High = 1.08971, Low = 1.08889, Close = 1.08933, Volume = 845, IndexNumber = 123 });
            DataSet dataSet124 = new DataSet(new Quotation() { Id = 124, Date = new DateTime(2016, 1, 18, 8, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08964, Low = 1.08885, Close = 1.08894, Volume = 993, IndexNumber = 124 });
            DataSet dataSet125 = new DataSet(new Quotation() { Id = 125, Date = new DateTime(2016, 1, 18, 8, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08897, High = 1.08921, Low = 1.08862, Close = 1.08889, Volume = 681, IndexNumber = 125 });
            DataSet dataSet126 = new DataSet(new Quotation() { Id = 126, Date = new DateTime(2016, 1, 18, 8, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08889, High = 1.08903, Low = 1.0886, Close = 1.08889, Volume = 876, IndexNumber = 126 });
            DataSet dataSet127 = new DataSet(new Quotation() { Id = 127, Date = new DateTime(2016, 1, 18, 8, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08891, High = 1.08922, Low = 1.0886, Close = 1.08894, Volume = 923, IndexNumber = 127 });
            DataSet dataSet128 = new DataSet(new Quotation() { Id = 128, Date = new DateTime(2016, 1, 18, 9, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08893, Low = 1.08771, Close = 1.08805, Volume = 1743, IndexNumber = 128 });
            DataSet dataSet129 = new DataSet(new Quotation() { Id = 129, Date = new DateTime(2016, 1, 18, 9, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08807, High = 1.08845, Low = 1.08749, Close = 1.08765, Volume = 1291, IndexNumber = 129 });
            DataSet dataSet130 = new DataSet(new Quotation() { Id = 130, Date = new DateTime(2016, 1, 18, 9, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08769, High = 1.0881, Low = 1.08731, Close = 1.08752, Volume = 1385, IndexNumber = 130 });
            DataSet dataSet131 = new DataSet(new Quotation() { Id = 131, Date = new DateTime(2016, 1, 18, 9, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08752, High = 1.08829, Low = 1.08747, Close = 1.08825, Volume = 1337, IndexNumber = 131 });
            DataSet dataSet132 = new DataSet(new Quotation() { Id = 132, Date = new DateTime(2016, 1, 18, 9, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08824, High = 1.08849, Low = 1.0881, Close = 1.08829, Volume = 1084, IndexNumber = 132 });
            DataSet dataSet133 = new DataSet(new Quotation() { Id = 133, Date = new DateTime(2016, 1, 18, 9, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08831, High = 1.08872, Low = 1.08817, Close = 1.08853, Volume = 980, IndexNumber = 133 });
            mockedManager.Setup(m => m.GetDataSet(123)).Returns(dataSet123);
            mockedManager.Setup(m => m.GetDataSet(124)).Returns(dataSet124);
            mockedManager.Setup(m => m.GetDataSet(125)).Returns(dataSet125);
            mockedManager.Setup(m => m.GetDataSet(126)).Returns(dataSet126);
            mockedManager.Setup(m => m.GetDataSet(127)).Returns(dataSet127);
            mockedManager.Setup(m => m.GetDataSet(128)).Returns(dataSet128);
            mockedManager.Setup(m => m.GetDataSet(129)).Returns(dataSet129);
            mockedManager.Setup(m => m.GetDataSet(130)).Returns(dataSet130);
            mockedManager.Setup(m => m.GetDataSet(131)).Returns(dataSet131);
            mockedManager.Setup(m => m.GetDataSet(132)).Returns(dataSet132);
            mockedManager.Setup(m => m.GetDataSet(133)).Returns(dataSet133);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet127, ExtremumType.PeakByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByClose_IfAtLeastOneClosePriceInTheNextThreeQuotationsIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet142 = new DataSet(new Quotation() { Id = 142, Date = new DateTime(2016, 1, 18, 10, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08852, High = 1.08856, Low = 1.08798, Close = 1.08825, Volume = 2227, IndexNumber = 142 });
            DataSet dataSet143 = new DataSet(new Quotation() { Id = 143, Date = new DateTime(2016, 1, 18, 10, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08825, High = 1.0885, Low = 1.08795, Close = 1.08809, Volume = 1904, IndexNumber = 143 });
            DataSet dataSet144 = new DataSet(new Quotation() { Id = 144, Date = new DateTime(2016, 1, 18, 10, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08806, High = 1.08827, Low = 1.0879, Close = 1.08816, Volume = 1484, IndexNumber = 144 });
            DataSet dataSet145 = new DataSet(new Quotation() { Id = 145, Date = new DateTime(2016, 1, 18, 10, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08816, High = 1.08843, Low = 1.08738, Close = 1.08756, Volume = 1690, IndexNumber = 145 });
            DataSet dataSet146 = new DataSet(new Quotation() { Id = 146, Date = new DateTime(2016, 1, 18, 10, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08759, High = 1.08849, Low = 1.08754, Close = 1.08836, Volume = 1813, IndexNumber = 146 });
            DataSet dataSet147 = new DataSet(new Quotation() { Id = 147, Date = new DateTime(2016, 1, 18, 10, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08838, High = 1.08843, Low = 1.08797, Close = 1.0883, Volume = 1487, IndexNumber = 147 });
            DataSet dataSet148 = new DataSet(new Quotation() { Id = 148, Date = new DateTime(2016, 1, 18, 10, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08829, High = 1.08848, Low = 1.08788, Close = 1.08836, Volume = 1635, IndexNumber = 148 });
            DataSet dataSet149 = new DataSet(new Quotation() { Id = 149, Date = new DateTime(2016, 1, 18, 10, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08838, High = 1.08872, Low = 1.08829, Close = 1.08865, Volume = 1337, IndexNumber = 149 });
            DataSet dataSet150 = new DataSet(new Quotation() { Id = 150, Date = new DateTime(2016, 1, 18, 10, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08865, High = 1.08906, Low = 1.08848, Close = 1.08906, Volume = 1383, IndexNumber = 150 });
            DataSet dataSet151 = new DataSet(new Quotation() { Id = 151, Date = new DateTime(2016, 1, 18, 10, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08905, High = 1.08935, Low = 1.08875, Close = 1.08887, Volume = 1410, IndexNumber = 151 });
            mockedManager.Setup(m => m.GetDataSet(142)).Returns(dataSet142);
            mockedManager.Setup(m => m.GetDataSet(143)).Returns(dataSet143);
            mockedManager.Setup(m => m.GetDataSet(144)).Returns(dataSet144);
            mockedManager.Setup(m => m.GetDataSet(145)).Returns(dataSet145);
            mockedManager.Setup(m => m.GetDataSet(146)).Returns(dataSet146);
            mockedManager.Setup(m => m.GetDataSet(147)).Returns(dataSet147);
            mockedManager.Setup(m => m.GetDataSet(148)).Returns(dataSet148);
            mockedManager.Setup(m => m.GetDataSet(149)).Returns(dataSet149);
            mockedManager.Setup(m => m.GetDataSet(150)).Returns(dataSet150);
            mockedManager.Setup(m => m.GetDataSet(151)).Returns(dataSet151);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet146, ExtremumType.PeakByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForPeakByClose_EvenIsSomeClosePricesInTheNextThreeQuotationsAreEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet142 = new DataSet(new Quotation() { Id = 142, Date = new DateTime(2016, 1, 18, 10, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08852, High = 1.08856, Low = 1.08798, Close = 1.08825, Volume = 2227, IndexNumber = 142 });
            DataSet dataSet143 = new DataSet(new Quotation() { Id = 143, Date = new DateTime(2016, 1, 18, 10, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08825, High = 1.0885, Low = 1.08795, Close = 1.08809, Volume = 1904, IndexNumber = 143 });
            DataSet dataSet144 = new DataSet(new Quotation() { Id = 144, Date = new DateTime(2016, 1, 18, 10, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08806, High = 1.08827, Low = 1.0879, Close = 1.08816, Volume = 1484, IndexNumber = 144 });
            DataSet dataSet145 = new DataSet(new Quotation() { Id = 145, Date = new DateTime(2016, 1, 18, 10, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08816, High = 1.08843, Low = 1.08738, Close = 1.08756, Volume = 1690, IndexNumber = 145 });
            DataSet dataSet146 = new DataSet(new Quotation() { Id = 146, Date = new DateTime(2016, 1, 18, 10, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08759, High = 1.08849, Low = 1.08754, Close = 1.08836, Volume = 1813, IndexNumber = 146 });
            DataSet dataSet147 = new DataSet(new Quotation() { Id = 147, Date = new DateTime(2016, 1, 18, 10, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08838, High = 1.08843, Low = 1.08797, Close = 1.0883, Volume = 1487, IndexNumber = 147 });
            DataSet dataSet148 = new DataSet(new Quotation() { Id = 148, Date = new DateTime(2016, 1, 18, 10, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08829, High = 1.08848, Low = 1.08788, Close = 1.08836, Volume = 1635, IndexNumber = 148 });
            DataSet dataSet149 = new DataSet(new Quotation() { Id = 149, Date = new DateTime(2016, 1, 18, 10, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08838, High = 1.08872, Low = 1.08829, Close = 1.08825, Volume = 1337, IndexNumber = 149 });
            DataSet dataSet150 = new DataSet(new Quotation() { Id = 150, Date = new DateTime(2016, 1, 18, 10, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08865, High = 1.08906, Low = 1.08848, Close = 1.08906, Volume = 1383, IndexNumber = 150 });
            DataSet dataSet151 = new DataSet(new Quotation() { Id = 151, Date = new DateTime(2016, 1, 18, 10, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08905, High = 1.08935, Low = 1.08875, Close = 1.08887, Volume = 1410, IndexNumber = 151 });
            mockedManager.Setup(m => m.GetDataSet(142)).Returns(dataSet142);
            mockedManager.Setup(m => m.GetDataSet(143)).Returns(dataSet143);
            mockedManager.Setup(m => m.GetDataSet(144)).Returns(dataSet144);
            mockedManager.Setup(m => m.GetDataSet(145)).Returns(dataSet145);
            mockedManager.Setup(m => m.GetDataSet(146)).Returns(dataSet146);
            mockedManager.Setup(m => m.GetDataSet(147)).Returns(dataSet147);
            mockedManager.Setup(m => m.GetDataSet(148)).Returns(dataSet148);
            mockedManager.Setup(m => m.GetDataSet(149)).Returns(dataSet149);
            mockedManager.Setup(m => m.GetDataSet(150)).Returns(dataSet150);
            mockedManager.Setup(m => m.GetDataSet(151)).Returns(dataSet151);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet146, ExtremumType.PeakByClose);
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForPeakByClose_IfAtLeastThreeEarlierAndThreeLaterQuotationsHaveLowerClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet8, ExtremumType.PeakByClose);
            Assert.IsTrue(result);

        }

        #endregion PEAK_BY_CLOSE.IS_EXTREMUM


        #region PEAK_BY_HIGH.IS_EXTREMUM

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByHigh_IfProcessedDataSetIndexIsLessOrEqualToMinimumRequiredLowerQuotations()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet3, ExtremumType.PeakByHigh);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByHigh_IfAtLeastOneHighPriceInThePreviousThreeQuotationsIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09181, High = 1.09188, Low = 1.09176, Close = 1.09184, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0919, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09147, Low = 1.09114, Close = 1.09145, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09144, High = 1.09149, Low = 1.0915, Close = 1.09147, Volume = 1009, IndexNumber = 8 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet4, ExtremumType.PeakByHigh);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByHigh_IfAtLeastOneHighPriceInThePreviousThreeQuotationsIsEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09181, High = 1.09188, Low = 1.09176, Close = 1.09184, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.0919, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0919, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09147, Low = 1.09114, Close = 1.09145, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09144, High = 1.09149, Low = 1.0915, Close = 1.09147, Volume = 1009, IndexNumber = 8 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);



            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet3, ExtremumType.PeakByHigh);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForPeakByHigh_IfAtLeastOneHighPriceInTheNextThreeQuotationsIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09196, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet9, ExtremumType.PeakByHigh);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForPeakByHigh_EvenIsSomeHighPricesInTheNextThreeQuotationsAreEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09189, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet9, ExtremumType.PeakByHigh);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForPeakByHigh_IfAtLeastThreeEarlierAndThreeLaterQuotationsHaveLowerHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet9, ExtremumType.PeakByHigh);
            Assert.IsTrue(result);

        }

        #endregion PEAK_BY_HIGH.IS_EXTREMUM


        #region TROUGH_BY_CLOSE.IS_EXTREMUM

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByClose_IfProcessedDataSetIndexIsLessOrEqualToMinimumRequiredLowerQuotations()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet3, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByClose_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet269 = new DataSet(new Quotation() { Id = 269, Date = new DateTime(2016, 1, 18, 20, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08883, High = 1.08906, Low = 1.08881, Close = 1.08892, Volume = 362, IndexNumber = 269 });
            DataSet dataSet270 = new DataSet(new Quotation() { Id = 270, Date = new DateTime(2016, 1, 18, 20, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08891, High = 1.0894, Low = 1.0889, Close = 1.08924, Volume = 414, IndexNumber = 270 });
            DataSet dataSet271 = new DataSet(new Quotation() { Id = 271, Date = new DateTime(2016, 1, 18, 20, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.0896, Low = 1.08916, Close = 1.08952, Volume = 419, IndexNumber = 271 });
            DataSet dataSet272 = new DataSet(new Quotation() { Id = 272, Date = new DateTime(2016, 1, 18, 21, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08952, High = 1.08973, Low = 1.0895, Close = 1.08951, Volume = 1090, IndexNumber = 272 });
            DataSet dataSet273 = new DataSet(new Quotation() { Id = 273, Date = new DateTime(2016, 1, 18, 21, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08951, High = 1.08953, Low = 1.08925, Close = 1.08929, Volume = 869, IndexNumber = 273 });
            DataSet dataSet274 = new DataSet(new Quotation() { Id = 274, Date = new DateTime(2016, 1, 18, 21, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08936, Low = 1.08926, Close = 1.08936, Volume = 151, IndexNumber = 274 });
            DataSet dataSet275 = new DataSet(new Quotation() { Id = 275, Date = new DateTime(2016, 1, 18, 21, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08927, High = 1.08945, Low = 1.08926, Close = 1.08936, Volume = 155, IndexNumber = 275 });
            DataSet dataSet276 = new DataSet(new Quotation() { Id = 276, Date = new DateTime(2016, 1, 18, 21, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08938, High = 1.0894, Low = 1.08932, Close = 1.08935, Volume = 223, IndexNumber = 276 });
            DataSet dataSet277 = new DataSet(new Quotation() { Id = 277, Date = new DateTime(2016, 1, 18, 21, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08937, High = 1.08939, Low = 1.08928, Close = 1.08938, Volume = 237, IndexNumber = 277 });
            mockedManager.Setup(m => m.GetDataSet(269)).Returns(dataSet269);
            mockedManager.Setup(m => m.GetDataSet(270)).Returns(dataSet270);
            mockedManager.Setup(m => m.GetDataSet(271)).Returns(dataSet271);
            mockedManager.Setup(m => m.GetDataSet(272)).Returns(dataSet272);
            mockedManager.Setup(m => m.GetDataSet(273)).Returns(dataSet273);
            mockedManager.Setup(m => m.GetDataSet(274)).Returns(dataSet274);
            mockedManager.Setup(m => m.GetDataSet(275)).Returns(dataSet275);
            mockedManager.Setup(m => m.GetDataSet(276)).Returns(dataSet276);
            mockedManager.Setup(m => m.GetDataSet(277)).Returns(dataSet277);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet273, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByClose_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet269 = new DataSet(new Quotation() { Id = 269, Date = new DateTime(2016, 1, 18, 20, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08883, High = 1.08906, Low = 1.08881, Close = 1.0895, Volume = 362, IndexNumber = 269 });
            DataSet dataSet270 = new DataSet(new Quotation() { Id = 270, Date = new DateTime(2016, 1, 18, 20, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08891, High = 1.0894, Low = 1.0889, Close = 1.08929, Volume = 414, IndexNumber = 270 });
            DataSet dataSet271 = new DataSet(new Quotation() { Id = 271, Date = new DateTime(2016, 1, 18, 20, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.0896, Low = 1.08916, Close = 1.08952, Volume = 419, IndexNumber = 271 });
            DataSet dataSet272 = new DataSet(new Quotation() { Id = 272, Date = new DateTime(2016, 1, 18, 21, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08952, High = 1.08973, Low = 1.0895, Close = 1.08951, Volume = 1090, IndexNumber = 272 });
            DataSet dataSet273 = new DataSet(new Quotation() { Id = 273, Date = new DateTime(2016, 1, 18, 21, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08951, High = 1.08953, Low = 1.08925, Close = 1.08929, Volume = 869, IndexNumber = 273 });
            DataSet dataSet274 = new DataSet(new Quotation() { Id = 274, Date = new DateTime(2016, 1, 18, 21, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08936, Low = 1.08926, Close = 1.08936, Volume = 151, IndexNumber = 274 });
            DataSet dataSet275 = new DataSet(new Quotation() { Id = 275, Date = new DateTime(2016, 1, 18, 21, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08927, High = 1.08945, Low = 1.08926, Close = 1.08936, Volume = 155, IndexNumber = 275 });
            DataSet dataSet276 = new DataSet(new Quotation() { Id = 276, Date = new DateTime(2016, 1, 18, 21, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08938, High = 1.0894, Low = 1.08932, Close = 1.08935, Volume = 223, IndexNumber = 276 });
            DataSet dataSet277 = new DataSet(new Quotation() { Id = 277, Date = new DateTime(2016, 1, 18, 21, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08937, High = 1.08939, Low = 1.08928, Close = 1.08938, Volume = 237, IndexNumber = 277 });
            mockedManager.Setup(m => m.GetDataSet(269)).Returns(dataSet269);
            mockedManager.Setup(m => m.GetDataSet(270)).Returns(dataSet270);
            mockedManager.Setup(m => m.GetDataSet(271)).Returns(dataSet271);
            mockedManager.Setup(m => m.GetDataSet(272)).Returns(dataSet272);
            mockedManager.Setup(m => m.GetDataSet(273)).Returns(dataSet273);
            mockedManager.Setup(m => m.GetDataSet(274)).Returns(dataSet274);
            mockedManager.Setup(m => m.GetDataSet(275)).Returns(dataSet275);
            mockedManager.Setup(m => m.GetDataSet(276)).Returns(dataSet276);
            mockedManager.Setup(m => m.GetDataSet(277)).Returns(dataSet277);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet273, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByClose_IfAtLeastOneClosePriceInTheNextThreeQuotationsIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0905, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet16, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForTroughByClose_EvenIsSomeClosePricesInTheNextThreeQuotationsAreEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.0906, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet16, ExtremumType.TroughByClose);
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForTroughByClose_IfAtLeastThreeEarlierAndThreeLaterQuotationsHaveHigherClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet16, ExtremumType.TroughByClose);
            Assert.IsTrue(result);

        }

        #endregion TROUGH_BY_CLOSE.IS_EXTREMUM
        

        #region TROUGH_BY_LOW.IS_EXTREMUM

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByLow_IfProcessedDataSetIndexIsLessOrEqualToMinimumRequiredLowerQuotations()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09091, Close = 1.09112, Volume = 1392, IndexNumber = 4 });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09111, Close = 1.091, Volume = 1154, IndexNumber = 5 });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet3, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByLow_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet202 = new DataSet(new Quotation() { Id = 202, Date = new DateTime(2016, 1, 18, 15, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08963, Low = 1.089, Close = 1.089, Volume = 1444, IndexNumber = 202 });
            DataSet dataSet203 = new DataSet(new Quotation() { Id = 203, Date = new DateTime(2016, 1, 18, 15, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08901, High = 1.08958, Low = 1.08896, Close = 1.08954, Volume = 1189, IndexNumber = 203 });
            DataSet dataSet204 = new DataSet(new Quotation() { Id = 204, Date = new DateTime(2016, 1, 18, 15, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08954, High = 1.08975, Low = 1.0893, Close = 1.08932, Volume = 1027, IndexNumber = 204 });
            DataSet dataSet205 = new DataSet(new Quotation() { Id = 205, Date = new DateTime(2016, 1, 18, 15, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08929, High = 1.08938, Low = 1.08912, Close = 1.08921, Volume = 959, IndexNumber = 205 });
            DataSet dataSet206 = new DataSet(new Quotation() { Id = 206, Date = new DateTime(2016, 1, 18, 15, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0892, High = 1.08943, Low = 1.08901, Close = 1.08939, Volume = 1284, IndexNumber = 206 });
            DataSet dataSet207 = new DataSet(new Quotation() { Id = 207, Date = new DateTime(2016, 1, 18, 15, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08938, High = 1.08955, Low = 1.08922, Close = 1.08946, Volume = 1217, IndexNumber = 207 });
            DataSet dataSet208 = new DataSet(new Quotation() { Id = 208, Date = new DateTime(2016, 1, 18, 15, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08948, High = 1.08949, Low = 1.08921, Close = 1.08937, Volume = 1082, IndexNumber = 208 });
            DataSet dataSet209 = new DataSet(new Quotation() { Id = 209, Date = new DateTime(2016, 1, 18, 15, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08969, Low = 1.08919, Close = 1.08951, Volume = 1142, IndexNumber = 209 });
            DataSet dataSet210 = new DataSet(new Quotation() { Id = 210, Date = new DateTime(2016, 1, 18, 15, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0895, High = 1.08953, Low = 1.08929, Close = 1.08946, Volume = 850, IndexNumber = 210 });
            DataSet dataSet211 = new DataSet(new Quotation() { Id = 211, Date = new DateTime(2016, 1, 18, 15, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08964, Low = 1.08922, Close = 1.08928, Volume = 1177, IndexNumber = 211 });

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet206, ExtremumType.TroughByLow);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByLow_IfAtLeastOneClosePriceInThePreviousThreeQuotationsIsEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet202 = new DataSet(new Quotation() { Id = 202, Date = new DateTime(2016, 1, 18, 15, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08963, Low = 1.0891, Close = 1.089, Volume = 1444, IndexNumber = 202 });
            DataSet dataSet203 = new DataSet(new Quotation() { Id = 203, Date = new DateTime(2016, 1, 18, 15, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08901, High = 1.08958, Low = 1.08901, Close = 1.08954, Volume = 1189, IndexNumber = 203 });
            DataSet dataSet204 = new DataSet(new Quotation() { Id = 204, Date = new DateTime(2016, 1, 18, 15, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08954, High = 1.08975, Low = 1.0893, Close = 1.08932, Volume = 1027, IndexNumber = 204 });
            DataSet dataSet205 = new DataSet(new Quotation() { Id = 205, Date = new DateTime(2016, 1, 18, 15, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08929, High = 1.08938, Low = 1.08912, Close = 1.08921, Volume = 959, IndexNumber = 205 });
            DataSet dataSet206 = new DataSet(new Quotation() { Id = 206, Date = new DateTime(2016, 1, 18, 15, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0892, High = 1.08943, Low = 1.08901, Close = 1.08939, Volume = 1284, IndexNumber = 206 });
            DataSet dataSet207 = new DataSet(new Quotation() { Id = 207, Date = new DateTime(2016, 1, 18, 15, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08938, High = 1.08955, Low = 1.08922, Close = 1.08946, Volume = 1217, IndexNumber = 207 });
            DataSet dataSet208 = new DataSet(new Quotation() { Id = 208, Date = new DateTime(2016, 1, 18, 15, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08948, High = 1.08949, Low = 1.08921, Close = 1.08937, Volume = 1082, IndexNumber = 208 });
            DataSet dataSet209 = new DataSet(new Quotation() { Id = 209, Date = new DateTime(2016, 1, 18, 15, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08969, Low = 1.08919, Close = 1.08951, Volume = 1142, IndexNumber = 209 });
            DataSet dataSet210 = new DataSet(new Quotation() { Id = 210, Date = new DateTime(2016, 1, 18, 15, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0895, High = 1.08953, Low = 1.08929, Close = 1.08946, Volume = 850, IndexNumber = 210 });
            DataSet dataSet211 = new DataSet(new Quotation() { Id = 211, Date = new DateTime(2016, 1, 18, 15, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08964, Low = 1.08922, Close = 1.08928, Volume = 1177, IndexNumber = 211 });

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet206, ExtremumType.TroughByLow);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsFalseForTroughByLow_IfAtLeastOneClosePriceInTheNextThreeQuotationsIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09038, Close = 1.09142, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet17, ExtremumType.TroughByClose);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForTroughByLow_EvenIsSomeClosePricesInTheNextThreeQuotationsAreEqual()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09041, Close = 1.09142, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet16, ExtremumType.TroughByClose);
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsExtremum_ReturnsTrueForTroughByLow_IfAtLeastThreeEarlierAndThreeLaterQuotationsHaveHigherClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 });
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.IsExtremum(dataSet16, ExtremumType.TroughByClose);
            Assert.IsTrue(result);

        }

        #endregion TROUGH_BY_LOW.IS_EXTREMUM


        #region CALCULATE_EARLIER_AMPLITUDE

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByClose_IfThereIsNoHigherClosePriceEarlier()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09085, Close = 1.09177, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum);
            double expectedResult = 0.00086;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByClose_IfThereIsHigherClosePriceEarlierAndLowPriceAtThisQuotationIsLowerThanProcessedClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09039, Close = 1.09177, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09169, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09167, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum20 = new Extremum(1, 1, ExtremumType.PeakByClose, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum20);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum20);
            double expectedResult = 0.0013;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByClose_WhenLookingForLastHigherBeforeIgnoresQuotationsWithHigherHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09039, Close = 1.09177, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum20 = new Extremum(1, 1, ExtremumType.PeakByClose, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum20);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum20);
            double expectedResult = 0.0013;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByClose_IfExtremumHasMoreEarlierMinorsThanMaxSerie()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            DataSet dataSet57 = new DataSet(new Quotation() { Id = 57, Date = new DateTime(2016, 1, 18, 3, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09092, High = 1.09092, Low = 1.09031, Close = 1.09032, Volume = 1155, IndexNumber = 57 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 5, 0) });
            DataSet dataSet58 = new DataSet(new Quotation() { Id = 58, Date = new DateTime(2016, 1, 18, 3, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09034, High = 1.09055, Low = 1.09019, Close = 1.0905, Volume = 1304, IndexNumber = 58 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 10, 0) });
            DataSet dataSet59 = new DataSet(new Quotation() { Id = 59, Date = new DateTime(2016, 1, 18, 3, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09048, High = 1.09055, Low = 1.08928, Close = 1.08961, Volume = 2252, IndexNumber = 59 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 15, 0) });
            DataSet dataSet60 = new DataSet(new Quotation() { Id = 60, Date = new DateTime(2016, 1, 18, 3, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08963, High = 1.09008, Low = 1.08958, Close = 1.08977, Volume = 1971, IndexNumber = 60 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 20, 0) });
            DataSet dataSet61 = new DataSet(new Quotation() { Id = 61, Date = new DateTime(2016, 1, 18, 3, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08978, High = 1.09055, Low = 1.08974, Close = 1.09047, Volume = 2171, IndexNumber = 61 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 25, 0) });
            DataSet dataSet62 = new DataSet(new Quotation() { Id = 62, Date = new DateTime(2016, 1, 18, 3, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09047, High = 1.09075, Low = 1.09023, Close = 1.09062, Volume = 1654, IndexNumber = 62 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 30, 0) });
            DataSet dataSet63 = new DataSet(new Quotation() { Id = 63, Date = new DateTime(2016, 1, 18, 3, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09063, High = 1.09072, Low = 1.09024, Close = 1.09056, Volume = 1589, IndexNumber = 63 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 35, 0) });
            DataSet dataSet64 = new DataSet(new Quotation() { Id = 64, Date = new DateTime(2016, 1, 18, 3, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09055, High = 1.09055, Low = 1.08983, Close = 1.09001, Volume = 1299, IndexNumber = 64 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 40, 0) });
            DataSet dataSet65 = new DataSet(new Quotation() { Id = 65, Date = new DateTime(2016, 1, 18, 3, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08997, High = 1.09017, Low = 1.08951, Close = 1.08984, Volume = 1636, IndexNumber = 65 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 45, 0) });
            DataSet dataSet66 = new DataSet(new Quotation() { Id = 66, Date = new DateTime(2016, 1, 18, 3, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08984, High = 1.09011, Low = 1.08976, Close = 1.0898, Volume = 1355, IndexNumber = 66 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 50, 0) });
            DataSet dataSet67 = new DataSet(new Quotation() { Id = 67, Date = new DateTime(2016, 1, 18, 3, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08981, High = 1.09002, Low = 1.08956, Close = 1.08977, Volume = 1205, IndexNumber = 67 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 55, 0) });
            DataSet dataSet68 = new DataSet(new Quotation() { Id = 68, Date = new DateTime(2016, 1, 18, 4, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08978, High = 1.09008, Low = 1.08968, Close = 1.08982, Volume = 1155, IndexNumber = 68 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 0, 0) });
            DataSet dataSet69 = new DataSet(new Quotation() { Id = 69, Date = new DateTime(2016, 1, 18, 4, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0898, High = 1.09017, Low = 1.08974, Close = 1.09008, Volume = 893, IndexNumber = 69 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 5, 0) });
            DataSet dataSet70 = new DataSet(new Quotation() { Id = 70, Date = new DateTime(2016, 1, 18, 4, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09009, High = 1.09022, Low = 1.08996, Close = 1.08996, Volume = 1013, IndexNumber = 70 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 10, 0) });
            DataSet dataSet71 = new DataSet(new Quotation() { Id = 71, Date = new DateTime(2016, 1, 18, 4, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08998, High = 1.09022, Low = 1.08984, Close = 1.09015, Volume = 1077, IndexNumber = 71 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 15, 0) });
            DataSet dataSet72 = new DataSet(new Quotation() { Id = 72, Date = new DateTime(2016, 1, 18, 4, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09011, High = 1.09037, Low = 1.09009, Close = 1.0903, Volume = 814, IndexNumber = 72 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 20, 0) });
            DataSet dataSet73 = new DataSet(new Quotation() { Id = 73, Date = new DateTime(2016, 1, 18, 4, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09031, High = 1.09037, Low = 1.0901, Close = 1.09031, Volume = 905, IndexNumber = 73 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 25, 0) });
            DataSet dataSet74 = new DataSet(new Quotation() { Id = 74, Date = new DateTime(2016, 1, 18, 4, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09031, High = 1.09069, Low = 1.0902, Close = 1.09069, Volume = 901, IndexNumber = 74 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);
            mockedManager.Setup(m => m.GetDataSet(57)).Returns(dataSet57);
            mockedManager.Setup(m => m.GetDataSet(58)).Returns(dataSet58);
            mockedManager.Setup(m => m.GetDataSet(59)).Returns(dataSet59);
            mockedManager.Setup(m => m.GetDataSet(60)).Returns(dataSet60);
            mockedManager.Setup(m => m.GetDataSet(61)).Returns(dataSet61);
            mockedManager.Setup(m => m.GetDataSet(62)).Returns(dataSet62);
            mockedManager.Setup(m => m.GetDataSet(63)).Returns(dataSet63);
            mockedManager.Setup(m => m.GetDataSet(64)).Returns(dataSet64);
            mockedManager.Setup(m => m.GetDataSet(65)).Returns(dataSet65);
            mockedManager.Setup(m => m.GetDataSet(66)).Returns(dataSet66);
            mockedManager.Setup(m => m.GetDataSet(67)).Returns(dataSet67);
            mockedManager.Setup(m => m.GetDataSet(68)).Returns(dataSet68);
            mockedManager.Setup(m => m.GetDataSet(69)).Returns(dataSet69);
            mockedManager.Setup(m => m.GetDataSet(70)).Returns(dataSet70);
            mockedManager.Setup(m => m.GetDataSet(71)).Returns(dataSet71);
            mockedManager.Setup(m => m.GetDataSet(72)).Returns(dataSet72);
            mockedManager.Setup(m => m.GetDataSet(73)).Returns(dataSet73);
            mockedManager.Setup(m => m.GetDataSet(74)).Returns(dataSet74);



            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 74) { Date = new DateTime(2016, 1, 18, 4, 30, 0) };
            dataSet74.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum);
            double expectedResult = 0.00118;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }


        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByHigh_IfThereIsNoHigherClosePriceEarlier()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09176, High = 1.09182, Low = 1.09142, Close = 1.09151, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum);
            double expectedResult = 0.00098;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForPeakByHigh_IfThereIsHigherHighPriceEarlierAndLowPriceAtThisQuotationIsLowerThanProcessedHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            DataSet dataSet89 = new DataSet(new Quotation() { Id = 89, Date = new DateTime(2016, 1, 18, 5, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08899, Low = 1.08863, Close = 1.08892, Volume = 1133, IndexNumber = 89 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 45, 0) });
            DataSet dataSet90 = new DataSet(new Quotation() { Id = 90, Date = new DateTime(2016, 1, 18, 5, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08896, High = 1.08933, Low = 1.08893, Close = 1.08926, Volume = 685, IndexNumber = 90 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 50, 0) });
            DataSet dataSet91 = new DataSet(new Quotation() { Id = 91, Date = new DateTime(2016, 1, 18, 5, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08945, Low = 1.08916, Close = 1.08932, Volume = 774, IndexNumber = 91 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 55, 0) });
            DataSet dataSet92 = new DataSet(new Quotation() { Id = 92, Date = new DateTime(2016, 1, 18, 6, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.0893, High = 1.08939, Low = 1.08923, Close = 1.08932, Volume = 441, IndexNumber = 92 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 0, 0) });
            DataSet dataSet93 = new DataSet(new Quotation() { Id = 93, Date = new DateTime(2016, 1, 18, 6, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08944, Low = 1.08924, Close = 1.08932, Volume = 764, IndexNumber = 93 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 5, 0) });
            DataSet dataSet94 = new DataSet(new Quotation() { Id = 94, Date = new DateTime(2016, 1, 18, 6, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08942, Low = 1.08908, Close = 1.08913, Volume = 827, IndexNumber = 94 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 10, 0) });
            DataSet dataSet95 = new DataSet(new Quotation() { Id = 95, Date = new DateTime(2016, 1, 18, 6, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08912, High = 1.08918, Low = 1.08878, Close = 1.0888, Volume = 805, IndexNumber = 95 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 15, 0) });
            DataSet dataSet96 = new DataSet(new Quotation() { Id = 96, Date = new DateTime(2016, 1, 18, 6, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0888, High = 1.08966, Low = 1.08859, Close = 1.08904, Volume = 905, IndexNumber = 96 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 20, 0) });
            DataSet dataSet97 = new DataSet(new Quotation() { Id = 97, Date = new DateTime(2016, 1, 18, 6, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08904, High = 1.08923, Low = 1.08895, Close = 1.08916, Volume = 767, IndexNumber = 97 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 25, 0) });
            DataSet dataSet98 = new DataSet(new Quotation() { Id = 98, Date = new DateTime(2016, 1, 18, 6, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08915, High = 1.08928, Low = 1.08902, Close = 1.08921, Volume = 691, IndexNumber = 98 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 30, 0) });
            DataSet dataSet99 = new DataSet(new Quotation() { Id = 99, Date = new DateTime(2016, 1, 18, 6, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.08926, Low = 1.08911, Close = 1.08925, Volume = 675, IndexNumber = 99 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 35, 0) });
            DataSet dataSet100 = new DataSet(new Quotation() { Id = 100, Date = new DateTime(2016, 1, 18, 6, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08924, High = 1.08959, Low = 1.08916, Close = 1.08956, Volume = 809, IndexNumber = 100 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);
            mockedManager.Setup(m => m.GetDataSet(89)).Returns(dataSet89);
            mockedManager.Setup(m => m.GetDataSet(90)).Returns(dataSet90);
            mockedManager.Setup(m => m.GetDataSet(91)).Returns(dataSet91);
            mockedManager.Setup(m => m.GetDataSet(92)).Returns(dataSet92);
            mockedManager.Setup(m => m.GetDataSet(93)).Returns(dataSet93);
            mockedManager.Setup(m => m.GetDataSet(94)).Returns(dataSet94);
            mockedManager.Setup(m => m.GetDataSet(95)).Returns(dataSet95);
            mockedManager.Setup(m => m.GetDataSet(96)).Returns(dataSet96);
            mockedManager.Setup(m => m.GetDataSet(97)).Returns(dataSet97);
            mockedManager.Setup(m => m.GetDataSet(98)).Returns(dataSet98);
            mockedManager.Setup(m => m.GetDataSet(99)).Returns(dataSet99);
            mockedManager.Setup(m => m.GetDataSet(100)).Returns(dataSet100);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum100 = new Extremum(1, 1, ExtremumType.PeakByHigh, 100) { Date = new DateTime(2016, 1, 18, 6, 40, 0) };
            dataSet100.GetPrice().SetExtremum(extremum100);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum100);
            double expectedResult = 0.00064;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }


        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForTroughByClose_IfThereIsNoLowerClosePriceEarlier()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            
            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum);
            double expectedResult = 0.00118;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForTroughByClose_IfThereIsLowerClosePriceEarlierAndHighPriceAtThisQuotationIsHigherThanProcessedClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            DataSet dataSet39 = new DataSet(new Quotation() { Id = 39, Date = new DateTime(2016, 1, 18, 1, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09182, Low = 1.09149, Close = 1.09178, Volume = 690, IndexNumber = 39 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 35, 0) });
            DataSet dataSet40 = new DataSet(new Quotation() { Id = 40, Date = new DateTime(2016, 1, 18, 1, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09175, High = 1.09201, Low = 1.09175, Close = 1.09192, Volume = 546, IndexNumber = 40 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 40, 0) });
            DataSet dataSet41 = new DataSet(new Quotation() { Id = 41, Date = new DateTime(2016, 1, 18, 1, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09194, High = 1.092, Low = 1.09178, Close = 1.09179, Volume = 604, IndexNumber = 41 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 45, 0) });
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);
            mockedManager.Setup(m => m.GetDataSet(39)).Returns(dataSet39);
            mockedManager.Setup(m => m.GetDataSet(40)).Returns(dataSet40);
            mockedManager.Setup(m => m.GetDataSet(41)).Returns(dataSet41);
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum43 = new Extremum(1, 1, ExtremumType.TroughByClose, 43) { Date = new DateTime(2016, 1, 18, 1, 55, 0) };
            dataSet43.GetPrice().SetExtremum(extremum43);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum43);
            double expectedResult = 0.00032;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForTroughByClose_WhenLookingForLastHigherBeforeIgnoresQuotationsWithHigherHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
    

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum16 = new Extremum(1, 1, ExtremumType.TroughByClose, 16) { Date = new DateTime(2016, 1, 15, 23, 40, 0) };
            dataSet16.GetPrice().SetExtremum(extremum16);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum16);
            double expectedResult = 0.00158;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }


        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForTroughByLow_IfThereIsNoLowerLowPriceEarlier()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum);
            double expectedResult = 0.00127;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateEarlierAmplitude_ReturnsProperValueForTroughByLow_IfThereIsLowerLowPriceEarlierAndHighPriceAtThisQuotationIsHigherThanProcessedLowPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            DataSet dataSet39 = new DataSet(new Quotation() { Id = 39, Date = new DateTime(2016, 1, 18, 1, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09182, Low = 1.09144, Close = 1.09178, Volume = 690, IndexNumber = 39 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 35, 0) });
            DataSet dataSet40 = new DataSet(new Quotation() { Id = 40, Date = new DateTime(2016, 1, 18, 1, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09175, High = 1.09201, Low = 1.09175, Close = 1.09192, Volume = 546, IndexNumber = 40 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 40, 0) });
            DataSet dataSet41 = new DataSet(new Quotation() { Id = 41, Date = new DateTime(2016, 1, 18, 1, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09194, High = 1.092, Low = 1.09178, Close = 1.09179, Volume = 604, IndexNumber = 41 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 45, 0) });
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);
            mockedManager.Setup(m => m.GetDataSet(39)).Returns(dataSet39);
            mockedManager.Setup(m => m.GetDataSet(40)).Returns(dataSet40);
            mockedManager.Setup(m => m.GetDataSet(41)).Returns(dataSet41);
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum45 = new Extremum(1, 1, ExtremumType.TroughByLow, 45) { Date = new DateTime(2016, 1, 18, 2, 5, 0) };
            dataSet45.GetPrice().SetExtremum(extremum45);

            //Assert
            var result = processor.CalculateEarlierAmplitude(extremum45);
            double expectedResult = 0.00056;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }



        #endregion CALCULATE_EARLIER_AMPLITUDE


        #region CALCULATE_EARLIER_COUNTER

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForPeakByClose_IfThereArePreviousHigherValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForPeakByClose_IfThereIsNoPreviousHigherValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09277, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 7;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForPeakByHigh_IfThereArePreviousHigherValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09189, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForPeakByHigh_IfThereIsNoPreviousHigherValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 8;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForTroughByClose_IfThereArePreviousLowerValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 38) { Date = new DateTime(2016, 1, 18, 1, 30, 0) };
            dataSet38.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForTroughByClose_IfThereIsNoPreviousLowerValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForTroughByLow_IfThereArePreviousLowerValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 23) { Date = new DateTime(2016, 1, 18, 0, 15, 0) };
            dataSet23.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValueForTroughByLow_IfThereIsNoPreviousLowerValues()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierCounter_ReturnsProperValue_IfPreviousMinorIsEarlierThanMaxSeriesCount()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 17) { Date = new DateTime(2016, 1, 15, 23, 45, 0) };
            dataSet17.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierCounter(extremum);
            int expectedResult = 10;
            Assert.AreEqual(expectedResult, result);

        }

        #endregion CALCULATE_EARLIER_COUNTER


        #region CALCULATE_EARLIER_CHANGE

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 2);
            double expectedResult = 0.00042151;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 5);
            double expectedResult = -0.00015569;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 3);
            double expectedResult = 0.00016494;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 2);
            double expectedResult = -0.00014657;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 3);
            double expectedResult = -0.00016494;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 2);
            double expectedResult = 0.00014657;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }


        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 17) { Date = new DateTime(2016, 1, 15, 23, 45, 0) };
            dataSet17.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 3);
            double expectedResult = -0.00011001;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 17) { Date = new DateTime(2016, 1, 15, 23, 45, 0) };
            dataSet17.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 5);
            double expectedResult = 0.00010082;
            double difference = (Math.Abs(expectedResult - result));
            Assert.IsTrue(difference < MAX_DOUBLE_COMPARISON_DIFFERENCE);

        }

        [TestMethod]
        public void CalculateEarlierChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        #endregion CALCULATE_EARLIER_CHANGE



        #region CALCULATE_LATER_AMPLITUDE

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByClose_IfThereIsNoHigherClosePriceLater()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum);
            double expectedResult = 0.00128;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByClose_IfThereIsHigherClosePriceLaterAndLowPriceAtThisQuotationIsLowerThanProcessedClosePrice()
        {
            
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09168, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09161, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09161, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09162, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum20 = new Extremum(1, 1, ExtremumType.PeakByClose, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum20);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum20);
            double expectedResult = 0.00071;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByClose_WhenLookingForFirstHigherAfterIgnoresQuotationsWithHigherHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum20 = new Extremum(1, 1, ExtremumType.PeakByClose, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum20);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum20);
            double expectedResult = 0.00071;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByClose_IfExtremumHasMoreLaterMinorsThanMaxSerie()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09038, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum);
            double expectedResult = 0.00136;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByHigh_IfThereIsNoHigherClosePriceLater()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum);
            double expectedResult = 0.00178;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForPeakByHigh_IfThereIsHigherHighPriceLaterAndLowPriceAtThisQuotationIsLowerThanProcessedHighPrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum9 = new Extremum(1, 1, ExtremumType.PeakByHigh, 9) { Date = new DateTime(2016, 1, 15, 23, 5, 0) };
            dataSet9.GetPrice().SetExtremum(extremum9);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum9);
            double expectedResult = 0.00178;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForTroughByClose_IfThereIsNoLowerClosePriceLater()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum);
            double expectedResult = 0.00119;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        //[TestMethod]
        //public void CalculateLaterAmplitude_ForTroughByClose_IfThereIsLowerClosePriceLaterAndHighPriceAtThisQuotationIsTheHighestSoFar_ThisValueIsUsedAsAmplitude()
        //{
        //    //Arrange
        //    Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
        //    DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
        //    DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
        //    DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
        //    DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
        //    DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
        //    DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
        //    DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
        //    DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
        //    DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
        //    DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
        //    DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
        //    DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
        //    DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
        //    DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
        //    DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
        //    DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
        //    mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
        //    mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
        //    mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
        //    mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
        //    mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
        //    mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
        //    mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
        //    mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
        //    mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
        //    mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
        //    mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
        //    mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
        //    mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
        //    mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
        //    mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
        //    mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);


        //    //Act
        //    ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
        //    Extremum extremum5 = new Extremum(1, 1, ExtremumType.TroughByClose, new DateTime(2016, 1, 15, 22, 45, 0)) { IndexNumber = 5 };
        //    dataSet5.GetPrice().SetExtremum(extremum5);

        //    //Assert
        //    var result = processor.CalculateLaterAmplitude(extremum5);
        //    double expectedResult = 0.00032;
        //    var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
        //    Assert.IsTrue(areEqual);

        //}

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForTroughByClose_WhenLookingForLastHigherBeforeIgnoresQuotationsWithLowerLowPriceButHigherClosePrice()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09075, Close = 1.0912, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09318, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum5 = new Extremum(1, 1, ExtremumType.TroughByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum5);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum5);
            double expectedResult = 0.00218;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void CalculateLaterAmplitude_ReturnsProperValueForTroughByLow_IfThereIsNoLowerLowPriceLater()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterAmplitude(extremum);
            double expectedResult = 0.00128;
            var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
            Assert.IsTrue(areEqual);

        }

        //[TestMethod]
        //public void CalculateLaterAmplitude_ReturnsProperValueForTroughByLow_IfThereIsLowerLowPriceLaterAndHighPriceAtThisQuotationIsHigherThanProcessedLowPrice()
        //{
        //    //Arrange
        //    Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
        //    DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
        //    DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
        //    DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
        //    DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
        //    DataSet dataSet39 = new DataSet(new Quotation() { Id = 39, Date = new DateTime(2016, 1, 18, 1, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09182, Low = 1.09144, Close = 1.09178, Volume = 690, IndexNumber = 39 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 35, 0) });
        //    DataSet dataSet40 = new DataSet(new Quotation() { Id = 40, Date = new DateTime(2016, 1, 18, 1, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09175, High = 1.09201, Low = 1.09175, Close = 1.09192, Volume = 546, IndexNumber = 40 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 40, 0) });
        //    DataSet dataSet41 = new DataSet(new Quotation() { Id = 41, Date = new DateTime(2016, 1, 18, 1, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09194, High = 1.092, Low = 1.09178, Close = 1.09179, Volume = 604, IndexNumber = 41 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 45, 0) });
        //    DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
        //    DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
        //    DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
        //    DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
        //    mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
        //    mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
        //    mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
        //    mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);
        //    mockedManager.Setup(m => m.GetDataSet(39)).Returns(dataSet39);
        //    mockedManager.Setup(m => m.GetDataSet(40)).Returns(dataSet40);
        //    mockedManager.Setup(m => m.GetDataSet(41)).Returns(dataSet41);
        //    mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
        //    mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
        //    mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
        //    mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);

        //    //Act
        //    ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
        //    Extremum extremum45 = new Extremum(1, 1, ExtremumType.TroughByLow, new DateTime(2016, 1, 18, 2, 5, 0)) { IndexNumber = 45 };
        //    dataSet45.GetPrice().SetExtremum(extremum45);

        //    //Assert
        //    var result = processor.CalculateLaterAmplitude(extremum45);
        //    double expectedResult = 0.00056;
        //    var areEqual = Math.Abs(expectedResult - result) < MAX_DOUBLE_COMPARISON_DIFFERENCE;
        //    Assert.IsTrue(areEqual);

        //}


        #endregion CALCULATE_LATER_AMPLITUDE

        
        #region CALCULATE_LATER_COUNTER

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByClose_IfThereAreHigherValuesLater()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 25) { Date = new DateTime(2016, 1, 18, 0, 25, 0) };
            dataSet25.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 8;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByClose_IfThereIsHigherValueLaterButOutOfMaxSerieRange()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 5;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 25) { Date = new DateTime(2016, 1, 18, 0, 25, 0) };
            dataSet25.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByClose_IfThereIsNoHigherValue()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            DataSet dataSet39 = new DataSet(new Quotation() { Id = 39, Date = new DateTime(2016, 1, 18, 1, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09182, Low = 1.09144, Close = 1.09178, Volume = 690, IndexNumber = 39 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 35, 0) });
            DataSet dataSet40 = new DataSet(new Quotation() { Id = 40, Date = new DateTime(2016, 1, 18, 1, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09175, High = 1.09201, Low = 1.09175, Close = 1.09192, Volume = 546, IndexNumber = 40 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 40, 0) });
            DataSet dataSet41 = new DataSet(new Quotation() { Id = 41, Date = new DateTime(2016, 1, 18, 1, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09194, High = 1.092, Low = 1.09178, Close = 1.09179, Volume = 604, IndexNumber = 41 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 45, 0) });
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            DataSet dataSet52 = new DataSet(new Quotation() { Id = 52, Date = new DateTime(2016, 1, 18, 2, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09222, Low = 1.09158, Close = 1.0917, Volume = 855, IndexNumber = 52 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 40, 0) });
            DataSet dataSet53 = new DataSet(new Quotation() { Id = 53, Date = new DateTime(2016, 1, 18, 2, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09174, High = 1.09178, Low = 1.09143, Close = 1.09163, Volume = 768, IndexNumber = 53 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 45, 0) });
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            DataSet dataSet57 = new DataSet(new Quotation() { Id = 57, Date = new DateTime(2016, 1, 18, 3, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09092, High = 1.09092, Low = 1.09031, Close = 1.09032, Volume = 1155, IndexNumber = 57 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 5, 0) });
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);
            mockedManager.Setup(m => m.GetDataSet(39)).Returns(dataSet39);
            mockedManager.Setup(m => m.GetDataSet(40)).Returns(dataSet40);
            mockedManager.Setup(m => m.GetDataSet(41)).Returns(dataSet41);
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);
            mockedManager.Setup(m => m.GetDataSet(52)).Returns(dataSet52);
            mockedManager.Setup(m => m.GetDataSet(53)).Returns(dataSet53);
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);
            mockedManager.Setup(m => m.GetDataSet(57)).Returns(dataSet57);

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 20;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 35) { Date = new DateTime(2016, 1, 18, 1, 15, 0) };
            dataSet35.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 20;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByClose_IfThereIsHigherValueOnTheLastQuotationAllowedByMaxSerie()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 8;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 25) { Date = new DateTime(2016, 1, 18, 0, 25, 0) };
            dataSet25.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 8;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByClose_IfThereIsHigherValueOnTheFirstPositionAfterMaxSerie()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 7;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 25) { Date = new DateTime(2016, 1, 18, 0, 25, 0) };
            dataSet25.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 7;
            Assert.AreEqual(expectedResult, result);

        }





        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByHigh_IfThereAreHigherValuesLater()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 27) { Date = new DateTime(2016, 1, 18, 0, 35, 0) };
            dataSet27.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 7;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByHigh_IfThereIsHigherValueLaterButOutOfMaxSerieRange()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 5;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 27) { Date = new DateTime(2016, 1, 18, 0, 35, 0) };
            dataSet27.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByHigh_IfThereIsNoHigherValue()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0918, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            DataSet dataSet39 = new DataSet(new Quotation() { Id = 39, Date = new DateTime(2016, 1, 18, 1, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09182, Low = 1.09144, Close = 1.09178, Volume = 690, IndexNumber = 39 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 35, 0) });
            DataSet dataSet40 = new DataSet(new Quotation() { Id = 40, Date = new DateTime(2016, 1, 18, 1, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09175, High = 1.09201, Low = 1.09175, Close = 1.09192, Volume = 546, IndexNumber = 40 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 40, 0) });
            DataSet dataSet41 = new DataSet(new Quotation() { Id = 41, Date = new DateTime(2016, 1, 18, 1, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09194, High = 1.092, Low = 1.09178, Close = 1.09179, Volume = 604, IndexNumber = 41 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 45, 0) });
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);
            mockedManager.Setup(m => m.GetDataSet(39)).Returns(dataSet39);
            mockedManager.Setup(m => m.GetDataSet(40)).Returns(dataSet40);
            mockedManager.Setup(m => m.GetDataSet(41)).Returns(dataSet41);
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 35) { Date = new DateTime(2016, 1, 18, 1, 15, 0) };
            dataSet35.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 10;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByHigh_IfThereIsHigherValueOnTheLastQuotationAllowedByMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 7;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 27) { Date = new DateTime(2016, 1, 18, 0, 35, 0) };
            dataSet27.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 7;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForPeakByHigh_IfThereIsHigherValueOnTheFirstPositionAfterMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            DataSet dataSet31 = new DataSet(new Quotation() { Id = 31, Date = new DateTime(2016, 1, 18, 0, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09104, High = 1.09131, Low = 1.09064, Close = 1.09101, Volume = 761, IndexNumber = 31 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 55, 0) });
            DataSet dataSet32 = new DataSet(new Quotation() { Id = 32, Date = new DateTime(2016, 1, 18, 1, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09091, High = 1.09181, Low = 1.09091, Close = 1.09166, Volume = 1697, IndexNumber = 32 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 0, 0) });
            DataSet dataSet33 = new DataSet(new Quotation() { Id = 33, Date = new DateTime(2016, 1, 18, 1, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09175, Low = 1.0916, Close = 1.09165, Volume = 754, IndexNumber = 33 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 5, 0) });
            DataSet dataSet34 = new DataSet(new Quotation() { Id = 34, Date = new DateTime(2016, 1, 18, 1, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09208, Low = 1.09156, Close = 1.09198, Volume = 703, IndexNumber = 34 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 10, 0) });
            DataSet dataSet35 = new DataSet(new Quotation() { Id = 35, Date = new DateTime(2016, 1, 18, 1, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09202, High = 1.09261, Low = 1.09198, Close = 1.0923, Volume = 964, IndexNumber = 35 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 15, 0) });
            DataSet dataSet36 = new DataSet(new Quotation() { Id = 36, Date = new DateTime(2016, 1, 18, 1, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09232, High = 1.09232, Low = 1.09175, Close = 1.09189, Volume = 559, IndexNumber = 36 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 20, 0) });
            DataSet dataSet37 = new DataSet(new Quotation() { Id = 37, Date = new DateTime(2016, 1, 18, 1, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09211, Low = 1.09177, Close = 1.09185, Volume = 673, IndexNumber = 37 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 25, 0) });
            DataSet dataSet38 = new DataSet(new Quotation() { Id = 38, Date = new DateTime(2016, 1, 18, 1, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09189, Low = 1.0915, Close = 1.09155, Volume = 640, IndexNumber = 38 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);
            mockedManager.Setup(m => m.GetDataSet(31)).Returns(dataSet31);
            mockedManager.Setup(m => m.GetDataSet(32)).Returns(dataSet32);
            mockedManager.Setup(m => m.GetDataSet(33)).Returns(dataSet33);
            mockedManager.Setup(m => m.GetDataSet(34)).Returns(dataSet34);
            mockedManager.Setup(m => m.GetDataSet(35)).Returns(dataSet35);
            mockedManager.Setup(m => m.GetDataSet(36)).Returns(dataSet36);
            mockedManager.Setup(m => m.GetDataSet(37)).Returns(dataSet37);
            mockedManager.Setup(m => m.GetDataSet(38)).Returns(dataSet38);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 6;
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 27) { Date = new DateTime(2016, 1, 18, 0, 35, 0) };
            dataSet27.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, result);

        }





        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByClose_IfThereAreLowerValuesLater()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            DataSet dataSet52 = new DataSet(new Quotation() { Id = 52, Date = new DateTime(2016, 1, 18, 2, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09222, Low = 1.09158, Close = 1.0917, Volume = 855, IndexNumber = 52 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 40, 0) });
            DataSet dataSet53 = new DataSet(new Quotation() { Id = 53, Date = new DateTime(2016, 1, 18, 2, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09174, High = 1.09178, Low = 1.09143, Close = 1.09163, Volume = 768, IndexNumber = 53 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 45, 0) });
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);
            mockedManager.Setup(m => m.GetDataSet(52)).Returns(dataSet52);
            mockedManager.Setup(m => m.GetDataSet(53)).Returns(dataSet53);
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 43) { Date = new DateTime(2016, 1, 18, 1, 55, 0) };
            dataSet43.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 9;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByClose_IfThereIsLowerValueLaterButOutOfMaxSerieRange()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            DataSet dataSet52 = new DataSet(new Quotation() { Id = 52, Date = new DateTime(2016, 1, 18, 2, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09222, Low = 1.09158, Close = 1.0917, Volume = 855, IndexNumber = 52 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 40, 0) });
            DataSet dataSet53 = new DataSet(new Quotation() { Id = 53, Date = new DateTime(2016, 1, 18, 2, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09174, High = 1.09178, Low = 1.09143, Close = 1.09163, Volume = 768, IndexNumber = 53 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 45, 0) });
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);
            mockedManager.Setup(m => m.GetDataSet(52)).Returns(dataSet52);
            mockedManager.Setup(m => m.GetDataSet(53)).Returns(dataSet53);
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 5;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 43) { Date = new DateTime(2016, 1, 18, 1, 55, 0) };
            dataSet43.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByClose_IfThereIsNoLowerValue()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 16) { Date = new DateTime(2016, 1, 15, 23, 40, 0) };
            dataSet16.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 10;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByClose_IfThereIsLowerValueOnTheLastQuotationAllowedByMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            DataSet dataSet52 = new DataSet(new Quotation() { Id = 52, Date = new DateTime(2016, 1, 18, 2, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09222, Low = 1.09158, Close = 1.0917, Volume = 855, IndexNumber = 52 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 40, 0) });
            DataSet dataSet53 = new DataSet(new Quotation() { Id = 53, Date = new DateTime(2016, 1, 18, 2, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09174, High = 1.09178, Low = 1.09143, Close = 1.09163, Volume = 768, IndexNumber = 53 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 45, 0) });
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);
            mockedManager.Setup(m => m.GetDataSet(52)).Returns(dataSet52);
            mockedManager.Setup(m => m.GetDataSet(53)).Returns(dataSet53);
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 9;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 43) { Date = new DateTime(2016, 1, 18, 1, 55, 0) };
            dataSet43.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 9;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByClose_IfThereIsLowerValueOnTheFirstPositionAfterMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet42 = new DataSet(new Quotation() { Id = 42, Date = new DateTime(2016, 1, 18, 1, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09192, Low = 1.09168, Close = 1.09189, Volume = 485, IndexNumber = 42 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 50, 0) });
            DataSet dataSet43 = new DataSet(new Quotation() { Id = 43, Date = new DateTime(2016, 1, 18, 1, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09188, High = 1.09189, Low = 1.09158, Close = 1.09169, Volume = 371, IndexNumber = 43 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 1, 55, 0) });
            DataSet dataSet44 = new DataSet(new Quotation() { Id = 44, Date = new DateTime(2016, 1, 18, 2, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09167, High = 1.09186, Low = 1.0915, Close = 1.09179, Volume = 1327, IndexNumber = 44 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 0, 0) });
            DataSet dataSet45 = new DataSet(new Quotation() { Id = 45, Date = new DateTime(2016, 1, 18, 2, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.0918, High = 1.09181, Low = 1.09145, Close = 1.0917, Volume = 1421, IndexNumber = 45 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 5, 0) });
            DataSet dataSet46 = new DataSet(new Quotation() { Id = 46, Date = new DateTime(2016, 1, 18, 2, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09169, High = 1.09189, Low = 1.09162, Close = 1.09184, Volume = 1097, IndexNumber = 46 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 10, 0) });
            DataSet dataSet47 = new DataSet(new Quotation() { Id = 47, Date = new DateTime(2016, 1, 18, 2, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09183, High = 1.09216, Low = 1.09181, Close = 1.0921, Volume = 816, IndexNumber = 47 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 15, 0) });
            DataSet dataSet48 = new DataSet(new Quotation() { Id = 48, Date = new DateTime(2016, 1, 18, 2, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0921, High = 1.09215, Low = 1.09192, Close = 1.09202, Volume = 684, IndexNumber = 48 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 20, 0) });
            DataSet dataSet49 = new DataSet(new Quotation() { Id = 49, Date = new DateTime(2016, 1, 18, 2, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09226, Low = 1.09201, Close = 1.09214, Volume = 691, IndexNumber = 49 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 25, 0) });
            DataSet dataSet50 = new DataSet(new Quotation() { Id = 50, Date = new DateTime(2016, 1, 18, 2, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09215, High = 1.09232, Low = 1.09183, Close = 1.09185, Volume = 996, IndexNumber = 50 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 30, 0) });
            DataSet dataSet51 = new DataSet(new Quotation() { Id = 51, Date = new DateTime(2016, 1, 18, 2, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09185, High = 1.09212, Low = 1.0918, Close = 1.092, Volume = 678, IndexNumber = 51 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 35, 0) });
            DataSet dataSet52 = new DataSet(new Quotation() { Id = 52, Date = new DateTime(2016, 1, 18, 2, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09201, High = 1.09222, Low = 1.09158, Close = 1.0917, Volume = 855, IndexNumber = 52 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 40, 0) });
            DataSet dataSet53 = new DataSet(new Quotation() { Id = 53, Date = new DateTime(2016, 1, 18, 2, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09174, High = 1.09178, Low = 1.09143, Close = 1.09163, Volume = 768, IndexNumber = 53 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 45, 0) });
            DataSet dataSet54 = new DataSet(new Quotation() { Id = 54, Date = new DateTime(2016, 1, 18, 2, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09162, High = 1.09178, Low = 1.09148, Close = 1.09153, Volume = 981, IndexNumber = 54 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 50, 0) });
            DataSet dataSet55 = new DataSet(new Quotation() { Id = 55, Date = new DateTime(2016, 1, 18, 2, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09152, Low = 1.09094, Close = 1.09114, Volume = 1151, IndexNumber = 55 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 2, 55, 0) });
            DataSet dataSet56 = new DataSet(new Quotation() { Id = 56, Date = new DateTime(2016, 1, 18, 3, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09113, High = 1.09121, Low = 1.09069, Close = 1.09086, Volume = 1219, IndexNumber = 56 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 3, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(42)).Returns(dataSet42);
            mockedManager.Setup(m => m.GetDataSet(43)).Returns(dataSet43);
            mockedManager.Setup(m => m.GetDataSet(44)).Returns(dataSet44);
            mockedManager.Setup(m => m.GetDataSet(45)).Returns(dataSet45);
            mockedManager.Setup(m => m.GetDataSet(46)).Returns(dataSet46);
            mockedManager.Setup(m => m.GetDataSet(47)).Returns(dataSet47);
            mockedManager.Setup(m => m.GetDataSet(48)).Returns(dataSet48);
            mockedManager.Setup(m => m.GetDataSet(49)).Returns(dataSet49);
            mockedManager.Setup(m => m.GetDataSet(50)).Returns(dataSet50);
            mockedManager.Setup(m => m.GetDataSet(51)).Returns(dataSet51);
            mockedManager.Setup(m => m.GetDataSet(52)).Returns(dataSet52);
            mockedManager.Setup(m => m.GetDataSet(53)).Returns(dataSet53);
            mockedManager.Setup(m => m.GetDataSet(54)).Returns(dataSet54);
            mockedManager.Setup(m => m.GetDataSet(55)).Returns(dataSet55);
            mockedManager.Setup(m => m.GetDataSet(56)).Returns(dataSet56);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 8;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 43) { Date = new DateTime(2016, 1, 18, 1, 55, 0) };
            dataSet43.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 8;
            Assert.AreEqual(expectedResult, result);

        }





        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByLow_IfThereAreLowerValuesLater()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            DataSet dataSet89 = new DataSet(new Quotation() { Id = 89, Date = new DateTime(2016, 1, 18, 5, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08899, Low = 1.08863, Close = 1.08892, Volume = 1133, IndexNumber = 89 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 45, 0) });
            DataSet dataSet90 = new DataSet(new Quotation() { Id = 90, Date = new DateTime(2016, 1, 18, 5, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08896, High = 1.08933, Low = 1.08893, Close = 1.08926, Volume = 685, IndexNumber = 90 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 50, 0) });
            DataSet dataSet91 = new DataSet(new Quotation() { Id = 91, Date = new DateTime(2016, 1, 18, 5, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08945, Low = 1.08916, Close = 1.08932, Volume = 774, IndexNumber = 91 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 55, 0) });
            DataSet dataSet92 = new DataSet(new Quotation() { Id = 92, Date = new DateTime(2016, 1, 18, 6, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.0893, High = 1.08939, Low = 1.08923, Close = 1.08932, Volume = 441, IndexNumber = 92 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 0, 0) });
            DataSet dataSet93 = new DataSet(new Quotation() { Id = 93, Date = new DateTime(2016, 1, 18, 6, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08944, Low = 1.08924, Close = 1.08932, Volume = 764, IndexNumber = 93 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 5, 0) });
            DataSet dataSet94 = new DataSet(new Quotation() { Id = 94, Date = new DateTime(2016, 1, 18, 6, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08942, Low = 1.08908, Close = 1.08913, Volume = 827, IndexNumber = 94 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 10, 0) });
            DataSet dataSet95 = new DataSet(new Quotation() { Id = 95, Date = new DateTime(2016, 1, 18, 6, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08912, High = 1.08918, Low = 1.08878, Close = 1.0888, Volume = 805, IndexNumber = 95 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 15, 0) });
            DataSet dataSet96 = new DataSet(new Quotation() { Id = 96, Date = new DateTime(2016, 1, 18, 6, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0888, High = 1.08966, Low = 1.08859, Close = 1.08904, Volume = 905, IndexNumber = 96 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 20, 0) });
            DataSet dataSet97 = new DataSet(new Quotation() { Id = 97, Date = new DateTime(2016, 1, 18, 6, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08904, High = 1.08923, Low = 1.08895, Close = 1.08916, Volume = 767, IndexNumber = 97 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 25, 0) });
            DataSet dataSet98 = new DataSet(new Quotation() { Id = 98, Date = new DateTime(2016, 1, 18, 6, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08915, High = 1.08928, Low = 1.08902, Close = 1.08921, Volume = 691, IndexNumber = 98 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 30, 0) });
            DataSet dataSet99 = new DataSet(new Quotation() { Id = 99, Date = new DateTime(2016, 1, 18, 6, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.08926, Low = 1.08911, Close = 1.08925, Volume = 675, IndexNumber = 99 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 35, 0) });
            DataSet dataSet100 = new DataSet(new Quotation() { Id = 100, Date = new DateTime(2016, 1, 18, 6, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08924, High = 1.08959, Low = 1.08916, Close = 1.08956, Volume = 809, IndexNumber = 100 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 40, 0) });
            DataSet dataSet101 = new DataSet(new Quotation() { Id = 101, Date = new DateTime(2016, 1, 18, 6, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08955, High = 1.08955, Low = 1.08901, Close = 1.0895, Volume = 1153, IndexNumber = 101 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 45, 0) });
            DataSet dataSet102 = new DataSet(new Quotation() { Id = 102, Date = new DateTime(2016, 1, 18, 6, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08953, Low = 1.08907, Close = 1.0891, Volume = 807, IndexNumber = 102 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 50, 0) });
            DataSet dataSet103 = new DataSet(new Quotation() { Id = 103, Date = new DateTime(2016, 1, 18, 6, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08911, High = 1.08955, Low = 1.08906, Close = 1.08955, Volume = 822, IndexNumber = 103 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);
            mockedManager.Setup(m => m.GetDataSet(89)).Returns(dataSet89);
            mockedManager.Setup(m => m.GetDataSet(90)).Returns(dataSet90);
            mockedManager.Setup(m => m.GetDataSet(91)).Returns(dataSet91);
            mockedManager.Setup(m => m.GetDataSet(92)).Returns(dataSet92);
            mockedManager.Setup(m => m.GetDataSet(93)).Returns(dataSet93);
            mockedManager.Setup(m => m.GetDataSet(94)).Returns(dataSet94);
            mockedManager.Setup(m => m.GetDataSet(95)).Returns(dataSet95);
            mockedManager.Setup(m => m.GetDataSet(96)).Returns(dataSet96);
            mockedManager.Setup(m => m.GetDataSet(97)).Returns(dataSet97);
            mockedManager.Setup(m => m.GetDataSet(98)).Returns(dataSet98);
            mockedManager.Setup(m => m.GetDataSet(99)).Returns(dataSet99);
            mockedManager.Setup(m => m.GetDataSet(100)).Returns(dataSet100);
            mockedManager.Setup(m => m.GetDataSet(101)).Returns(dataSet101);
            mockedManager.Setup(m => m.GetDataSet(102)).Returns(dataSet102);
            mockedManager.Setup(m => m.GetDataSet(103)).Returns(dataSet103);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 89) { Date = new DateTime(2016, 1, 18, 5, 45, 0) };
            dataSet89.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByLow_IfThereIsLowerValueLaterButOutOfMaxSerieRange()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            DataSet dataSet89 = new DataSet(new Quotation() { Id = 89, Date = new DateTime(2016, 1, 18, 5, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08899, Low = 1.08863, Close = 1.08892, Volume = 1133, IndexNumber = 89 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 45, 0) });
            DataSet dataSet90 = new DataSet(new Quotation() { Id = 90, Date = new DateTime(2016, 1, 18, 5, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08896, High = 1.08933, Low = 1.08893, Close = 1.08926, Volume = 685, IndexNumber = 90 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 50, 0) });
            DataSet dataSet91 = new DataSet(new Quotation() { Id = 91, Date = new DateTime(2016, 1, 18, 5, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08945, Low = 1.08916, Close = 1.08932, Volume = 774, IndexNumber = 91 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 55, 0) });
            DataSet dataSet92 = new DataSet(new Quotation() { Id = 92, Date = new DateTime(2016, 1, 18, 6, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.0893, High = 1.08939, Low = 1.08923, Close = 1.08932, Volume = 441, IndexNumber = 92 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 0, 0) });
            DataSet dataSet93 = new DataSet(new Quotation() { Id = 93, Date = new DateTime(2016, 1, 18, 6, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08944, Low = 1.08924, Close = 1.08932, Volume = 764, IndexNumber = 93 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 5, 0) });
            DataSet dataSet94 = new DataSet(new Quotation() { Id = 94, Date = new DateTime(2016, 1, 18, 6, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08942, Low = 1.08908, Close = 1.08913, Volume = 827, IndexNumber = 94 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 10, 0) });
            DataSet dataSet95 = new DataSet(new Quotation() { Id = 95, Date = new DateTime(2016, 1, 18, 6, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08912, High = 1.08918, Low = 1.08878, Close = 1.0888, Volume = 805, IndexNumber = 95 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 15, 0) });
            DataSet dataSet96 = new DataSet(new Quotation() { Id = 96, Date = new DateTime(2016, 1, 18, 6, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0888, High = 1.08966, Low = 1.08859, Close = 1.08904, Volume = 905, IndexNumber = 96 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 20, 0) });
            DataSet dataSet97 = new DataSet(new Quotation() { Id = 97, Date = new DateTime(2016, 1, 18, 6, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08904, High = 1.08923, Low = 1.08895, Close = 1.08916, Volume = 767, IndexNumber = 97 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 25, 0) });
            DataSet dataSet98 = new DataSet(new Quotation() { Id = 98, Date = new DateTime(2016, 1, 18, 6, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08915, High = 1.08928, Low = 1.08902, Close = 1.08921, Volume = 691, IndexNumber = 98 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 30, 0) });
            DataSet dataSet99 = new DataSet(new Quotation() { Id = 99, Date = new DateTime(2016, 1, 18, 6, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.08926, Low = 1.08911, Close = 1.08925, Volume = 675, IndexNumber = 99 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 35, 0) });
            DataSet dataSet100 = new DataSet(new Quotation() { Id = 100, Date = new DateTime(2016, 1, 18, 6, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08924, High = 1.08959, Low = 1.08916, Close = 1.08956, Volume = 809, IndexNumber = 100 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 40, 0) });
            DataSet dataSet101 = new DataSet(new Quotation() { Id = 101, Date = new DateTime(2016, 1, 18, 6, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08955, High = 1.08955, Low = 1.08901, Close = 1.0895, Volume = 1153, IndexNumber = 101 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 45, 0) });
            DataSet dataSet102 = new DataSet(new Quotation() { Id = 102, Date = new DateTime(2016, 1, 18, 6, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08953, Low = 1.08907, Close = 1.0891, Volume = 807, IndexNumber = 102 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 50, 0) });
            DataSet dataSet103 = new DataSet(new Quotation() { Id = 103, Date = new DateTime(2016, 1, 18, 6, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08911, High = 1.08955, Low = 1.08906, Close = 1.08955, Volume = 822, IndexNumber = 103 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);
            mockedManager.Setup(m => m.GetDataSet(89)).Returns(dataSet89);
            mockedManager.Setup(m => m.GetDataSet(90)).Returns(dataSet90);
            mockedManager.Setup(m => m.GetDataSet(91)).Returns(dataSet91);
            mockedManager.Setup(m => m.GetDataSet(92)).Returns(dataSet92);
            mockedManager.Setup(m => m.GetDataSet(93)).Returns(dataSet93);
            mockedManager.Setup(m => m.GetDataSet(94)).Returns(dataSet94);
            mockedManager.Setup(m => m.GetDataSet(95)).Returns(dataSet95);
            mockedManager.Setup(m => m.GetDataSet(96)).Returns(dataSet96);
            mockedManager.Setup(m => m.GetDataSet(97)).Returns(dataSet97);
            mockedManager.Setup(m => m.GetDataSet(98)).Returns(dataSet98);
            mockedManager.Setup(m => m.GetDataSet(99)).Returns(dataSet99);
            mockedManager.Setup(m => m.GetDataSet(100)).Returns(dataSet100);
            mockedManager.Setup(m => m.GetDataSet(101)).Returns(dataSet101);
            mockedManager.Setup(m => m.GetDataSet(102)).Returns(dataSet102);
            mockedManager.Setup(m => m.GetDataSet(103)).Returns(dataSet103);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 4;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 89) { Date = new DateTime(2016, 1, 18, 5, 45, 0) };
            dataSet89.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByLow_IfThereIsNoLowerValue()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            DataSet dataSet15 = new DataSet(new Quotation() { Id = 15, Date = new DateTime(2016, 1, 15, 23, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09086, High = 1.0909, Low = 1.09076, Close = 1.09082, Volume = 303, IndexNumber = 15 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 35, 0) });
            DataSet dataSet16 = new DataSet(new Quotation() { Id = 16, Date = new DateTime(2016, 1, 15, 23, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09081, High = 1.09089, Low = 1.09059, Close = 1.0906, Volume = 450, IndexNumber = 16 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 40, 0) });
            DataSet dataSet17 = new DataSet(new Quotation() { Id = 17, Date = new DateTime(2016, 1, 15, 23, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09061, High = 1.09099, Low = 1.09041, Close = 1.09097, Volume = 660, IndexNumber = 17 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 45, 0) });
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            DataSet dataSet28 = new DataSet(new Quotation() { Id = 28, Date = new DateTime(2016, 1, 18, 0, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09182, High = 1.09182, Low = 1.09057, Close = 1.09103, Volume = 1090, IndexNumber = 28 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 40, 0) });
            DataSet dataSet29 = new DataSet(new Quotation() { Id = 29, Date = new DateTime(2016, 1, 18, 0, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09084, High = 1.09124, Low = 1.09055, Close = 1.09107, Volume = 1845, IndexNumber = 29 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 45, 0) });
            DataSet dataSet30 = new DataSet(new Quotation() { Id = 30, Date = new DateTime(2016, 1, 18, 0, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09147, Low = 1.0909, Close = 1.09117, Volume = 1318, IndexNumber = 30 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 50, 0) });
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);
            mockedManager.Setup(m => m.GetDataSet(15)).Returns(dataSet15);
            mockedManager.Setup(m => m.GetDataSet(16)).Returns(dataSet16);
            mockedManager.Setup(m => m.GetDataSet(17)).Returns(dataSet17);
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);
            mockedManager.Setup(m => m.GetDataSet(28)).Returns(dataSet28);
            mockedManager.Setup(m => m.GetDataSet(29)).Returns(dataSet29);
            mockedManager.Setup(m => m.GetDataSet(30)).Returns(dataSet30);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 10;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 17) { Date = new DateTime(2016, 1, 15, 23, 45, 0) };
            dataSet17.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 10;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByLow_IfThereIsLowerValueOnTheLastQuotationAllowedByMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            DataSet dataSet89 = new DataSet(new Quotation() { Id = 89, Date = new DateTime(2016, 1, 18, 5, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08899, Low = 1.08863, Close = 1.08892, Volume = 1133, IndexNumber = 89 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 45, 0) });
            DataSet dataSet90 = new DataSet(new Quotation() { Id = 90, Date = new DateTime(2016, 1, 18, 5, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08896, High = 1.08933, Low = 1.08893, Close = 1.08926, Volume = 685, IndexNumber = 90 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 50, 0) });
            DataSet dataSet91 = new DataSet(new Quotation() { Id = 91, Date = new DateTime(2016, 1, 18, 5, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08945, Low = 1.08916, Close = 1.08932, Volume = 774, IndexNumber = 91 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 55, 0) });
            DataSet dataSet92 = new DataSet(new Quotation() { Id = 92, Date = new DateTime(2016, 1, 18, 6, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.0893, High = 1.08939, Low = 1.08923, Close = 1.08932, Volume = 441, IndexNumber = 92 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 0, 0) });
            DataSet dataSet93 = new DataSet(new Quotation() { Id = 93, Date = new DateTime(2016, 1, 18, 6, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08944, Low = 1.08924, Close = 1.08932, Volume = 764, IndexNumber = 93 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 5, 0) });
            DataSet dataSet94 = new DataSet(new Quotation() { Id = 94, Date = new DateTime(2016, 1, 18, 6, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08942, Low = 1.08908, Close = 1.08913, Volume = 827, IndexNumber = 94 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 10, 0) });
            DataSet dataSet95 = new DataSet(new Quotation() { Id = 95, Date = new DateTime(2016, 1, 18, 6, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08912, High = 1.08918, Low = 1.08878, Close = 1.0888, Volume = 805, IndexNumber = 95 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 15, 0) });
            DataSet dataSet96 = new DataSet(new Quotation() { Id = 96, Date = new DateTime(2016, 1, 18, 6, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0888, High = 1.08966, Low = 1.08859, Close = 1.08904, Volume = 905, IndexNumber = 96 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 20, 0) });
            DataSet dataSet97 = new DataSet(new Quotation() { Id = 97, Date = new DateTime(2016, 1, 18, 6, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08904, High = 1.08923, Low = 1.08895, Close = 1.08916, Volume = 767, IndexNumber = 97 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 25, 0) });
            DataSet dataSet98 = new DataSet(new Quotation() { Id = 98, Date = new DateTime(2016, 1, 18, 6, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08915, High = 1.08928, Low = 1.08902, Close = 1.08921, Volume = 691, IndexNumber = 98 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 30, 0) });
            DataSet dataSet99 = new DataSet(new Quotation() { Id = 99, Date = new DateTime(2016, 1, 18, 6, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.08926, Low = 1.08911, Close = 1.08925, Volume = 675, IndexNumber = 99 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 35, 0) });
            DataSet dataSet100 = new DataSet(new Quotation() { Id = 100, Date = new DateTime(2016, 1, 18, 6, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08924, High = 1.08959, Low = 1.08916, Close = 1.08956, Volume = 809, IndexNumber = 100 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 40, 0) });
            DataSet dataSet101 = new DataSet(new Quotation() { Id = 101, Date = new DateTime(2016, 1, 18, 6, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08955, High = 1.08955, Low = 1.08901, Close = 1.0895, Volume = 1153, IndexNumber = 101 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 45, 0) });
            DataSet dataSet102 = new DataSet(new Quotation() { Id = 102, Date = new DateTime(2016, 1, 18, 6, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08953, Low = 1.08907, Close = 1.0891, Volume = 807, IndexNumber = 102 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 50, 0) });
            DataSet dataSet103 = new DataSet(new Quotation() { Id = 103, Date = new DateTime(2016, 1, 18, 6, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08911, High = 1.08955, Low = 1.08906, Close = 1.08955, Volume = 822, IndexNumber = 103 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);
            mockedManager.Setup(m => m.GetDataSet(89)).Returns(dataSet89);
            mockedManager.Setup(m => m.GetDataSet(90)).Returns(dataSet90);
            mockedManager.Setup(m => m.GetDataSet(91)).Returns(dataSet91);
            mockedManager.Setup(m => m.GetDataSet(92)).Returns(dataSet92);
            mockedManager.Setup(m => m.GetDataSet(93)).Returns(dataSet93);
            mockedManager.Setup(m => m.GetDataSet(94)).Returns(dataSet94);
            mockedManager.Setup(m => m.GetDataSet(95)).Returns(dataSet95);
            mockedManager.Setup(m => m.GetDataSet(96)).Returns(dataSet96);
            mockedManager.Setup(m => m.GetDataSet(97)).Returns(dataSet97);
            mockedManager.Setup(m => m.GetDataSet(98)).Returns(dataSet98);
            mockedManager.Setup(m => m.GetDataSet(99)).Returns(dataSet99);
            mockedManager.Setup(m => m.GetDataSet(100)).Returns(dataSet100);
            mockedManager.Setup(m => m.GetDataSet(101)).Returns(dataSet101);
            mockedManager.Setup(m => m.GetDataSet(102)).Returns(dataSet102);
            mockedManager.Setup(m => m.GetDataSet(103)).Returns(dataSet103);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 6;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 89) { Date = new DateTime(2016, 1, 18, 5, 45, 0) };
            dataSet89.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 6;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterCounter_ReturnsProperValueForTroughByLow_IfThereIsLowerValueOnTheFirstPositionAfterMaxSerie()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            DataSet dataSet89 = new DataSet(new Quotation() { Id = 89, Date = new DateTime(2016, 1, 18, 5, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08899, Low = 1.08863, Close = 1.08892, Volume = 1133, IndexNumber = 89 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 45, 0) });
            DataSet dataSet90 = new DataSet(new Quotation() { Id = 90, Date = new DateTime(2016, 1, 18, 5, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08896, High = 1.08933, Low = 1.08893, Close = 1.08926, Volume = 685, IndexNumber = 90 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 50, 0) });
            DataSet dataSet91 = new DataSet(new Quotation() { Id = 91, Date = new DateTime(2016, 1, 18, 5, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08945, Low = 1.08916, Close = 1.08932, Volume = 774, IndexNumber = 91 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 55, 0) });
            DataSet dataSet92 = new DataSet(new Quotation() { Id = 92, Date = new DateTime(2016, 1, 18, 6, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.0893, High = 1.08939, Low = 1.08923, Close = 1.08932, Volume = 441, IndexNumber = 92 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 0, 0) });
            DataSet dataSet93 = new DataSet(new Quotation() { Id = 93, Date = new DateTime(2016, 1, 18, 6, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08935, High = 1.08944, Low = 1.08924, Close = 1.08932, Volume = 764, IndexNumber = 93 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 5, 0) });
            DataSet dataSet94 = new DataSet(new Quotation() { Id = 94, Date = new DateTime(2016, 1, 18, 6, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08942, Low = 1.08908, Close = 1.08913, Volume = 827, IndexNumber = 94 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 10, 0) });
            DataSet dataSet95 = new DataSet(new Quotation() { Id = 95, Date = new DateTime(2016, 1, 18, 6, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08912, High = 1.08918, Low = 1.08878, Close = 1.0888, Volume = 805, IndexNumber = 95 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 15, 0) });
            DataSet dataSet96 = new DataSet(new Quotation() { Id = 96, Date = new DateTime(2016, 1, 18, 6, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0888, High = 1.08966, Low = 1.08859, Close = 1.08904, Volume = 905, IndexNumber = 96 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 20, 0) });
            DataSet dataSet97 = new DataSet(new Quotation() { Id = 97, Date = new DateTime(2016, 1, 18, 6, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08904, High = 1.08923, Low = 1.08895, Close = 1.08916, Volume = 767, IndexNumber = 97 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 25, 0) });
            DataSet dataSet98 = new DataSet(new Quotation() { Id = 98, Date = new DateTime(2016, 1, 18, 6, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08915, High = 1.08928, Low = 1.08902, Close = 1.08921, Volume = 691, IndexNumber = 98 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 30, 0) });
            DataSet dataSet99 = new DataSet(new Quotation() { Id = 99, Date = new DateTime(2016, 1, 18, 6, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08922, High = 1.08926, Low = 1.08911, Close = 1.08925, Volume = 675, IndexNumber = 99 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 35, 0) });
            DataSet dataSet100 = new DataSet(new Quotation() { Id = 100, Date = new DateTime(2016, 1, 18, 6, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08924, High = 1.08959, Low = 1.08916, Close = 1.08956, Volume = 809, IndexNumber = 100 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 40, 0) });
            DataSet dataSet101 = new DataSet(new Quotation() { Id = 101, Date = new DateTime(2016, 1, 18, 6, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.08955, High = 1.08955, Low = 1.08901, Close = 1.0895, Volume = 1153, IndexNumber = 101 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 45, 0) });
            DataSet dataSet102 = new DataSet(new Quotation() { Id = 102, Date = new DateTime(2016, 1, 18, 6, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.08947, High = 1.08953, Low = 1.08907, Close = 1.0891, Volume = 807, IndexNumber = 102 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 50, 0) });
            DataSet dataSet103 = new DataSet(new Quotation() { Id = 103, Date = new DateTime(2016, 1, 18, 6, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08911, High = 1.08955, Low = 1.08906, Close = 1.08955, Volume = 822, IndexNumber = 103 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 6, 55, 0) });
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);
            mockedManager.Setup(m => m.GetDataSet(89)).Returns(dataSet89);
            mockedManager.Setup(m => m.GetDataSet(90)).Returns(dataSet90);
            mockedManager.Setup(m => m.GetDataSet(91)).Returns(dataSet91);
            mockedManager.Setup(m => m.GetDataSet(92)).Returns(dataSet92);
            mockedManager.Setup(m => m.GetDataSet(93)).Returns(dataSet93);
            mockedManager.Setup(m => m.GetDataSet(94)).Returns(dataSet94);
            mockedManager.Setup(m => m.GetDataSet(95)).Returns(dataSet95);
            mockedManager.Setup(m => m.GetDataSet(96)).Returns(dataSet96);
            mockedManager.Setup(m => m.GetDataSet(97)).Returns(dataSet97);
            mockedManager.Setup(m => m.GetDataSet(98)).Returns(dataSet98);
            mockedManager.Setup(m => m.GetDataSet(99)).Returns(dataSet99);
            mockedManager.Setup(m => m.GetDataSet(100)).Returns(dataSet100);
            mockedManager.Setup(m => m.GetDataSet(101)).Returns(dataSet101);
            mockedManager.Setup(m => m.GetDataSet(102)).Returns(dataSet102);
            mockedManager.Setup(m => m.GetDataSet(103)).Returns(dataSet103);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            processor.MaxSerieCount = 5;
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 89) { Date = new DateTime(2016, 1, 18, 5, 45, 0) };
            dataSet89.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterCounter(extremum);
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);

        }





        #endregion CALCULATE_LATER_COUNTER


        #region CALCULATE_LATER_CHANGE

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 2);
            double expectedResult = (1.09177d - 1.09148d) / 1.09177d;
            double difference = (Math.Abs(expectedResult - result));
            Assert.AreEqual(result, expectedResult);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum);
            
            //Assert
            var result = processor.CalculateLaterChange(extremum, 5);
            double expectedResult = (1.09171d - 1.09189d) / 1.09171d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByClose_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 5);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            DataSet dataSet9 = new DataSet(new Quotation() { Id = 9, Date = new DateTime(2016, 1, 15, 23, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09178, High = 1.09219, Low = 1.09143, Close = 1.09149, Volume = 657, IndexNumber = 9 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 5, 0) });
            DataSet dataSet10 = new DataSet(new Quotation() { Id = 10, Date = new DateTime(2016, 1, 15, 23, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.09164, Low = 1.09144, Close = 1.09148, Volume = 414, IndexNumber = 10 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 10, 0) });
            DataSet dataSet11 = new DataSet(new Quotation() { Id = 11, Date = new DateTime(2016, 1, 15, 23, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09149, High = 1.09156, Low = 1.09095, Close = 1.091, Volume = 419, IndexNumber = 11 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 15, 0) });
            DataSet dataSet12 = new DataSet(new Quotation() { Id = 12, Date = new DateTime(2016, 1, 15, 23, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.09098, High = 1.09118, Low = 1.09091, Close = 1.09108, Volume = 341, IndexNumber = 12 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 20, 0) });
            DataSet dataSet13 = new DataSet(new Quotation() { Id = 13, Date = new DateTime(2016, 1, 15, 23, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09109, High = 1.09112, Low = 1.09066, Close = 1.09068, Volume = 326, IndexNumber = 13 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 25, 0) });
            DataSet dataSet14 = new DataSet(new Quotation() { Id = 14, Date = new DateTime(2016, 1, 15, 23, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09066, High = 1.09088, Low = 1.09052, Close = 1.09085, Volume = 476, IndexNumber = 14 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 30, 0) });
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);
            mockedManager.Setup(m => m.GetDataSet(9)).Returns(dataSet9);
            mockedManager.Setup(m => m.GetDataSet(10)).Returns(dataSet10);
            mockedManager.Setup(m => m.GetDataSet(11)).Returns(dataSet11);
            mockedManager.Setup(m => m.GetDataSet(12)).Returns(dataSet12);
            mockedManager.Setup(m => m.GetDataSet(13)).Returns(dataSet13);
            mockedManager.Setup(m => m.GetDataSet(14)).Returns(dataSet14);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 2);
            double expectedResult = (1.09177d - 1.09148d) / 1.09177d;
            Assert.AreEqual(result, expectedResult);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet18 = new DataSet(new Quotation() { Id = 18, Date = new DateTime(2016, 1, 15, 23, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09099, High = 1.09129, Low = 1.09092, Close = 1.0911, Volume = 745, IndexNumber = 18 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 50, 0) });
            DataSet dataSet19 = new DataSet(new Quotation() { Id = 19, Date = new DateTime(2016, 1, 15, 23, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09197, Low = 1.09088, Close = 1.09142, Volume = 1140, IndexNumber = 19 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 55, 0) });
            DataSet dataSet20 = new DataSet(new Quotation() { Id = 20, Date = new DateTime(2016, 1, 18, 0, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09151, High = 1.09257, Low = 1.09138, Close = 1.09171, Volume = 417, IndexNumber = 20 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 0, 0) });
            DataSet dataSet21 = new DataSet(new Quotation() { Id = 21, Date = new DateTime(2016, 1, 18, 0, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.09165, High = 1.09188, Low = 1.0913, Close = 1.09154, Volume = 398, IndexNumber = 21 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 5, 0) });
            DataSet dataSet22 = new DataSet(new Quotation() { Id = 22, Date = new DateTime(2016, 1, 18, 0, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.09152, High = 1.09181, Low = 1.09129, Close = 1.09155, Volume = 518, IndexNumber = 22 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 10, 0) });
            DataSet dataSet23 = new DataSet(new Quotation() { Id = 23, Date = new DateTime(2016, 1, 18, 0, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.09153, High = 1.09171, Low = 1.091, Close = 1.09142, Volume = 438, IndexNumber = 23 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 15, 0) });
            DataSet dataSet24 = new DataSet(new Quotation() { Id = 24, Date = new DateTime(2016, 1, 18, 0, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.0912, High = 1.09192, Low = 1.0911, Close = 1.09162, Volume = 532, IndexNumber = 24 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 20, 0) });
            DataSet dataSet25 = new DataSet(new Quotation() { Id = 25, Date = new DateTime(2016, 1, 18, 0, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.09199, Low = 1.0915, Close = 1.09189, Volume = 681, IndexNumber = 25 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 25, 0) });
            DataSet dataSet26 = new DataSet(new Quotation() { Id = 26, Date = new DateTime(2016, 1, 18, 0, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.0919, High = 1.09209, Low = 1.09171, Close = 1.09179, Volume = 387, IndexNumber = 26 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 30, 0) });
            DataSet dataSet27 = new DataSet(new Quotation() { Id = 27, Date = new DateTime(2016, 1, 18, 0, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09173, High = 1.09211, Low = 1.09148, Close = 1.09181, Volume = 792, IndexNumber = 27 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 0, 35, 0) });
            mockedManager.Setup(m => m.GetDataSet(18)).Returns(dataSet18);
            mockedManager.Setup(m => m.GetDataSet(19)).Returns(dataSet19);
            mockedManager.Setup(m => m.GetDataSet(20)).Returns(dataSet20);
            mockedManager.Setup(m => m.GetDataSet(21)).Returns(dataSet21);
            mockedManager.Setup(m => m.GetDataSet(22)).Returns(dataSet22);
            mockedManager.Setup(m => m.GetDataSet(23)).Returns(dataSet23);
            mockedManager.Setup(m => m.GetDataSet(24)).Returns(dataSet24);
            mockedManager.Setup(m => m.GetDataSet(25)).Returns(dataSet25);
            mockedManager.Setup(m => m.GetDataSet(26)).Returns(dataSet26);
            mockedManager.Setup(m => m.GetDataSet(27)).Returns(dataSet27);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 20) { Date = new DateTime(2016, 1, 18, 0, 0, 0) };
            dataSet20.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 5);
            double expectedResult = (1.09171d - 1.09189d) / 1.09171d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForPeakByHigh_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.PeakByHigh, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet78 = new DataSet(new Quotation() { Id = 78, Date = new DateTime(2016, 1, 18, 4, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09003, High = 1.09013, Low = 1.08976, Close = 1.08985, Volume = 905, IndexNumber = 78 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 50, 0) });
            DataSet dataSet79 = new DataSet(new Quotation() { Id = 79, Date = new DateTime(2016, 1, 18, 4, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08986, High = 1.08995, Low = 1.08936, Close = 1.0894, Volume = 1168, IndexNumber = 79 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 55, 0) });
            DataSet dataSet80 = new DataSet(new Quotation() { Id = 80, Date = new DateTime(2016, 1, 18, 5, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08941, High = 1.08954, Low = 1.08915, Close = 1.08926, Volume = 1583, IndexNumber = 80 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 0, 0) });
            DataSet dataSet81 = new DataSet(new Quotation() { Id = 81, Date = new DateTime(2016, 1, 18, 5, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08949, Low = 1.08912, Close = 1.08934, Volume = 1369, IndexNumber = 81 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 5, 0) });
            DataSet dataSet82 = new DataSet(new Quotation() { Id = 82, Date = new DateTime(2016, 1, 18, 5, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08966, Low = 1.08926, Close = 1.08954, Volume = 1208, IndexNumber = 82 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 10, 0) });
            DataSet dataSet83 = new DataSet(new Quotation() { Id = 83, Date = new DateTime(2016, 1, 18, 5, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08953, High = 1.08983, Low = 1.0895, Close = 1.08977, Volume = 827, IndexNumber = 83 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 15, 0) });
            DataSet dataSet84 = new DataSet(new Quotation() { Id = 84, Date = new DateTime(2016, 1, 18, 5, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08977, High = 1.08982, Low = 1.08947, Close = 1.08948, Volume = 689, IndexNumber = 84 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 20, 0) });
            DataSet dataSet85 = new DataSet(new Quotation() { Id = 85, Date = new DateTime(2016, 1, 18, 5, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08945, High = 1.08948, Low = 1.08925, Close = 1.08937, Volume = 1129, IndexNumber = 85 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 25, 0) });
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(78)).Returns(dataSet78);
            mockedManager.Setup(m => m.GetDataSet(79)).Returns(dataSet79);
            mockedManager.Setup(m => m.GetDataSet(80)).Returns(dataSet80);
            mockedManager.Setup(m => m.GetDataSet(81)).Returns(dataSet81);
            mockedManager.Setup(m => m.GetDataSet(82)).Returns(dataSet82);
            mockedManager.Setup(m => m.GetDataSet(83)).Returns(dataSet83);
            mockedManager.Setup(m => m.GetDataSet(84)).Returns(dataSet84);
            mockedManager.Setup(m => m.GetDataSet(85)).Returns(dataSet85);
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 80) { Date = new DateTime(2016, 1, 18, 5, 0, 0) };
            dataSet80.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 6);
            double expectedResult = (1.08913d - 1.08926d) / 1.08926d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet78 = new DataSet(new Quotation() { Id = 78, Date = new DateTime(2016, 1, 18, 4, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09003, High = 1.09013, Low = 1.08976, Close = 1.08985, Volume = 905, IndexNumber = 78 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 50, 0) });
            DataSet dataSet79 = new DataSet(new Quotation() { Id = 79, Date = new DateTime(2016, 1, 18, 4, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08986, High = 1.08995, Low = 1.08936, Close = 1.0894, Volume = 1168, IndexNumber = 79 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 55, 0) });
            DataSet dataSet80 = new DataSet(new Quotation() { Id = 80, Date = new DateTime(2016, 1, 18, 5, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08941, High = 1.08954, Low = 1.08915, Close = 1.08926, Volume = 1583, IndexNumber = 80 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 0, 0) });
            DataSet dataSet81 = new DataSet(new Quotation() { Id = 81, Date = new DateTime(2016, 1, 18, 5, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08949, Low = 1.08912, Close = 1.08934, Volume = 1369, IndexNumber = 81 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 5, 0) });
            DataSet dataSet82 = new DataSet(new Quotation() { Id = 82, Date = new DateTime(2016, 1, 18, 5, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08966, Low = 1.08926, Close = 1.08954, Volume = 1208, IndexNumber = 82 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 10, 0) });
            DataSet dataSet83 = new DataSet(new Quotation() { Id = 83, Date = new DateTime(2016, 1, 18, 5, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08953, High = 1.08983, Low = 1.0895, Close = 1.08977, Volume = 827, IndexNumber = 83 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 15, 0) });
            DataSet dataSet84 = new DataSet(new Quotation() { Id = 84, Date = new DateTime(2016, 1, 18, 5, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08977, High = 1.08982, Low = 1.08947, Close = 1.08948, Volume = 689, IndexNumber = 84 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 20, 0) });
            DataSet dataSet85 = new DataSet(new Quotation() { Id = 85, Date = new DateTime(2016, 1, 18, 5, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08945, High = 1.08948, Low = 1.08925, Close = 1.08937, Volume = 1129, IndexNumber = 85 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 25, 0) });
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(78)).Returns(dataSet78);
            mockedManager.Setup(m => m.GetDataSet(79)).Returns(dataSet79);
            mockedManager.Setup(m => m.GetDataSet(80)).Returns(dataSet80);
            mockedManager.Setup(m => m.GetDataSet(81)).Returns(dataSet81);
            mockedManager.Setup(m => m.GetDataSet(82)).Returns(dataSet82);
            mockedManager.Setup(m => m.GetDataSet(83)).Returns(dataSet83);
            mockedManager.Setup(m => m.GetDataSet(84)).Returns(dataSet84);
            mockedManager.Setup(m => m.GetDataSet(85)).Returns(dataSet85);
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 80) { Date = new DateTime(2016, 1, 18, 5, 0, 0) };
            dataSet80.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 2);
            double expectedResult = (1.08954d - 1.08926d) / 1.08926d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByClose_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByClose, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }


        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsLower()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet78 = new DataSet(new Quotation() { Id = 78, Date = new DateTime(2016, 1, 18, 4, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09003, High = 1.09013, Low = 1.08976, Close = 1.08985, Volume = 905, IndexNumber = 78 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 50, 0) });
            DataSet dataSet79 = new DataSet(new Quotation() { Id = 79, Date = new DateTime(2016, 1, 18, 4, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08986, High = 1.08995, Low = 1.08936, Close = 1.0894, Volume = 1168, IndexNumber = 79 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 55, 0) });
            DataSet dataSet80 = new DataSet(new Quotation() { Id = 80, Date = new DateTime(2016, 1, 18, 5, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08941, High = 1.08954, Low = 1.08915, Close = 1.08926, Volume = 1583, IndexNumber = 80 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 0, 0) });
            DataSet dataSet81 = new DataSet(new Quotation() { Id = 81, Date = new DateTime(2016, 1, 18, 5, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08949, Low = 1.08912, Close = 1.08934, Volume = 1369, IndexNumber = 81 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 5, 0) });
            DataSet dataSet82 = new DataSet(new Quotation() { Id = 82, Date = new DateTime(2016, 1, 18, 5, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08966, Low = 1.08926, Close = 1.08954, Volume = 1208, IndexNumber = 82 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 10, 0) });
            DataSet dataSet83 = new DataSet(new Quotation() { Id = 83, Date = new DateTime(2016, 1, 18, 5, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08953, High = 1.08983, Low = 1.0895, Close = 1.08977, Volume = 827, IndexNumber = 83 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 15, 0) });
            DataSet dataSet84 = new DataSet(new Quotation() { Id = 84, Date = new DateTime(2016, 1, 18, 5, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08977, High = 1.08982, Low = 1.08947, Close = 1.08948, Volume = 689, IndexNumber = 84 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 20, 0) });
            DataSet dataSet85 = new DataSet(new Quotation() { Id = 85, Date = new DateTime(2016, 1, 18, 5, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08945, High = 1.08948, Low = 1.08925, Close = 1.08937, Volume = 1129, IndexNumber = 85 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 25, 0) });
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(78)).Returns(dataSet78);
            mockedManager.Setup(m => m.GetDataSet(79)).Returns(dataSet79);
            mockedManager.Setup(m => m.GetDataSet(80)).Returns(dataSet80);
            mockedManager.Setup(m => m.GetDataSet(81)).Returns(dataSet81);
            mockedManager.Setup(m => m.GetDataSet(82)).Returns(dataSet82);
            mockedManager.Setup(m => m.GetDataSet(83)).Returns(dataSet83);
            mockedManager.Setup(m => m.GetDataSet(84)).Returns(dataSet84);
            mockedManager.Setup(m => m.GetDataSet(85)).Returns(dataSet85);
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 80) { Date = new DateTime(2016, 1, 18, 5, 0, 0) };
            dataSet80.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 6);
            double expectedResult = (1.08913d - 1.08926d) / 1.08926d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexExistsAndComparedClosePriceIsHigher()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet78 = new DataSet(new Quotation() { Id = 78, Date = new DateTime(2016, 1, 18, 4, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09003, High = 1.09013, Low = 1.08976, Close = 1.08985, Volume = 905, IndexNumber = 78 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 50, 0) });
            DataSet dataSet79 = new DataSet(new Quotation() { Id = 79, Date = new DateTime(2016, 1, 18, 4, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.08986, High = 1.08995, Low = 1.08936, Close = 1.0894, Volume = 1168, IndexNumber = 79 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 4, 55, 0) });
            DataSet dataSet80 = new DataSet(new Quotation() { Id = 80, Date = new DateTime(2016, 1, 18, 5, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.08941, High = 1.08954, Low = 1.08915, Close = 1.08926, Volume = 1583, IndexNumber = 80 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 0, 0) });
            DataSet dataSet81 = new DataSet(new Quotation() { Id = 81, Date = new DateTime(2016, 1, 18, 5, 5, 0), AssetId = 1, TimeframeId = 1, Open = 1.08928, High = 1.08949, Low = 1.08912, Close = 1.08934, Volume = 1369, IndexNumber = 81 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 5, 0) });
            DataSet dataSet82 = new DataSet(new Quotation() { Id = 82, Date = new DateTime(2016, 1, 18, 5, 10, 0), AssetId = 1, TimeframeId = 1, Open = 1.08932, High = 1.08966, Low = 1.08926, Close = 1.08954, Volume = 1208, IndexNumber = 82 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 10, 0) });
            DataSet dataSet83 = new DataSet(new Quotation() { Id = 83, Date = new DateTime(2016, 1, 18, 5, 15, 0), AssetId = 1, TimeframeId = 1, Open = 1.08953, High = 1.08983, Low = 1.0895, Close = 1.08977, Volume = 827, IndexNumber = 83 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 15, 0) });
            DataSet dataSet84 = new DataSet(new Quotation() { Id = 84, Date = new DateTime(2016, 1, 18, 5, 20, 0), AssetId = 1, TimeframeId = 1, Open = 1.08977, High = 1.08982, Low = 1.08947, Close = 1.08948, Volume = 689, IndexNumber = 84 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 20, 0) });
            DataSet dataSet85 = new DataSet(new Quotation() { Id = 85, Date = new DateTime(2016, 1, 18, 5, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.08945, High = 1.08948, Low = 1.08925, Close = 1.08937, Volume = 1129, IndexNumber = 85 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 25, 0) });
            DataSet dataSet86 = new DataSet(new Quotation() { Id = 86, Date = new DateTime(2016, 1, 18, 5, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.08936, High = 1.08938, Low = 1.08908, Close = 1.08913, Volume = 848, IndexNumber = 86 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 30, 0) });
            DataSet dataSet87 = new DataSet(new Quotation() { Id = 87, Date = new DateTime(2016, 1, 18, 5, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.08914, High = 1.08919, Low = 1.08882, Close = 1.0889, Volume = 748, IndexNumber = 87 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 35, 0) });
            DataSet dataSet88 = new DataSet(new Quotation() { Id = 88, Date = new DateTime(2016, 1, 18, 5, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.08893, High = 1.08916, Low = 1.08884, Close = 1.08894, Volume = 1299, IndexNumber = 88 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 18, 5, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(78)).Returns(dataSet78);
            mockedManager.Setup(m => m.GetDataSet(79)).Returns(dataSet79);
            mockedManager.Setup(m => m.GetDataSet(80)).Returns(dataSet80);
            mockedManager.Setup(m => m.GetDataSet(81)).Returns(dataSet81);
            mockedManager.Setup(m => m.GetDataSet(82)).Returns(dataSet82);
            mockedManager.Setup(m => m.GetDataSet(83)).Returns(dataSet83);
            mockedManager.Setup(m => m.GetDataSet(84)).Returns(dataSet84);
            mockedManager.Setup(m => m.GetDataSet(85)).Returns(dataSet85);
            mockedManager.Setup(m => m.GetDataSet(86)).Returns(dataSet86);
            mockedManager.Setup(m => m.GetDataSet(87)).Returns(dataSet87);
            mockedManager.Setup(m => m.GetDataSet(88)).Returns(dataSet88);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 80) { Date = new DateTime(2016, 1, 18, 5, 0, 0) };
            dataSet80.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateLaterChange(extremum, 2);
            double expectedResult = (1.08954d - 1.08926d) / 1.08926d;
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateLaterChange_ReturnsProperValueForTroughByLow_IfQuotationsForComparedIndexDoesntExist()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.09111, High = 1.09124, Low = 1.09091, Close = 1.091, Volume = 1154, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 45, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.09101, High = 1.09132, Low = 1.09097, Close = 1.09131, Volume = 933, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 50, 0) });
            DataSet dataSet7 = new DataSet(new Quotation() { Id = 7, Date = new DateTime(2016, 1, 15, 22, 55, 0), AssetId = 1, TimeframeId = 1, Open = 1.09131, High = 1.09167, Low = 1.09114, Close = 1.09165, Volume = 1079, IndexNumber = 7 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 55, 0) });
            DataSet dataSet8 = new DataSet(new Quotation() { Id = 8, Date = new DateTime(2016, 1, 15, 23, 0, 0), AssetId = 1, TimeframeId = 1, Open = 1.09164, High = 1.09183, Low = 1.0915, Close = 1.09177, Volume = 1009, IndexNumber = 8 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 23, 0, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);
            mockedManager.Setup(m => m.GetDataSet(7)).Returns(dataSet7);
            mockedManager.Setup(m => m.GetDataSet(8)).Returns(dataSet8);


            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);
            Extremum extremum = new Extremum(1, 1, ExtremumType.TroughByLow, 8) { Date = new DateTime(2016, 1, 15, 23, 0, 0) };
            dataSet8.GetPrice().SetExtremum(extremum);

            //Assert
            var result = processor.CalculateEarlierChange(extremum, 10);
            double expectedResult = 0d;
            Assert.AreEqual(expectedResult, result);

        }

        #endregion CALCULATE_LATER_CHANGE



        #region EXTRACT_EXTREMUM_GROUPS

        [TestMethod]
        public void ExtractExtremumGroups_IfThereIsNoExtremumByCloseInSourceCollection_ReturnsNothing()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForPeakByCloseAndHighAtTheSameQuotation()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            Extremum peakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            Extremum peakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(peakByClose);
            dataSet3.GetPrice().SetExtremum(peakByHigh);
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);
            
            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsTrue(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == peakByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == peakByClose);

        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForPeakByCloseAndHighAtPreviousQuotation()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09222, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum peakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { Date = new DateTime(2016, 1, 15, 22, 30, 0) };
            dataSet2.GetPrice().SetExtremum(peakByHigh);            
            Extremum peakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(peakByClose);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsTrue(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == peakByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == peakByHigh);

        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForPeakByCloseAndHighAtNextQuotation()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0922, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum peakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(peakByClose);
            Extremum peakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 4) { Date = new DateTime(2016, 1, 15, 22, 40, 0) };
            dataSet4.GetPrice().SetExtremum(peakByHigh);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsTrue(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == peakByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == peakByHigh);

        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForTroughByCloseAndLowAtTheSameQuotation()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09188, Low = 1.09126, Close = 1.09134, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09151, Close = 1.09162, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum troughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(troughByClose);
            Extremum troughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(troughByLow);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsFalse(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == troughByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == troughByClose);

        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForTroughByCloseAndLowAtPreviousQuotation()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09115, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09188, Low = 1.09126, Close = 1.09134, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09151, Close = 1.09162, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum troughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 2) { Date = new DateTime(2016, 1, 15, 22, 30, 0) };
            dataSet2.GetPrice().SetExtremum(troughByLow);
            Extremum troughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(troughByClose);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsFalse(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == troughByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == troughByLow);

        }

        [TestMethod]
        public void ExtractExtremumGroups_ReturnsProperExtremumGroupForTroughByCloseAndLowAtNextQuotation()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09135, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09188, Low = 1.09126, Close = 1.09134, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09111, Close = 1.09162, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum troughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 4) { Date = new DateTime(2016, 1, 15, 22, 40, 0) };
            dataSet4.GetPrice().SetExtremum(troughByLow);
            Extremum troughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(troughByClose);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup group = result.ElementAt(0);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsNotNull(group);
            Assert.IsFalse(group.IsPeak);
            Assert.IsTrue(group.MasterExtremum == troughByClose);
            Assert.IsNotNull(group.SecondExtremum);
            Assert.IsTrue(group.SecondExtremum == troughByLow);

        }

        [TestMethod]
        public void ExtractExtremumGroups_IfThereIsOrphanedByHighExtremum_ItIsReturnedAsExtremumGroup()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09115, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09208, Low = 1.09126, Close = 1.09134, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09151, Close = 1.09132, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09151, Close = 1.09182, Volume = 1392, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09151, Close = 1.09172, Volume = 1392, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum peakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(peakByHigh);
            Extremum peakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(peakByClose);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4, dataSet5, dataSet6 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup groupHigh = result.ElementAt(0);
            ExtremumGroup groupClose = result.ElementAt(1);

            Assert.IsTrue(result.Count() == 2);

            Assert.IsNotNull(groupHigh);
            Assert.IsTrue(groupHigh.IsPeak);
            Assert.IsNotNull(groupHigh.MasterExtremum);
            Assert.IsNotNull(groupHigh.SecondExtremum);
            Assert.IsTrue(groupHigh.MasterExtremum == peakByHigh);
            Assert.IsTrue(groupHigh.SecondExtremum == peakByHigh);

            Assert.IsNotNull(groupClose);
            Assert.IsTrue(groupClose.IsPeak);
            Assert.IsNotNull(groupClose.MasterExtremum);
            Assert.IsNotNull(groupClose.SecondExtremum);
            Assert.IsTrue(groupClose.MasterExtremum == peakByClose);
            Assert.IsTrue(groupClose.SecondExtremum == peakByClose);

        }

        [TestMethod]
        public void ExtractExtremumGroups_IfThereIsOrphanedByLowExtremum_ItIsReturnedAsExtremumGroup()
        {

            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09192, Low = 1.09115, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.0918, Low = 1.09096, Close = 1.09134, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09101, Close = 1.09132, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet5 = new DataSet(new Quotation() { Id = 5, Date = new DateTime(2016, 1, 15, 22, 45, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09101, Close = 1.09112, Volume = 1392, IndexNumber = 5 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            DataSet dataSet6 = new DataSet(new Quotation() { Id = 6, Date = new DateTime(2016, 1, 15, 22, 50, 0), AssetId = 1, TimeframeId = 1, Open = 1.0916, High = 1.0917, Low = 1.09101, Close = 1.09172, Volume = 1392, IndexNumber = 6 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });

            Extremum troughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 3) { Date = new DateTime(2016, 1, 15, 22, 35, 0) };
            dataSet3.GetPrice().SetExtremum(troughByLow);
            Extremum troughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 5) { Date = new DateTime(2016, 1, 15, 22, 45, 0) };
            dataSet5.GetPrice().SetExtremum(troughByClose);

            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);
            mockedManager.Setup(m => m.GetDataSet(5)).Returns(dataSet5);
            mockedManager.Setup(m => m.GetDataSet(6)).Returns(dataSet6);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4, dataSet5, dataSet6 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            ExtremumGroup groupLow = result.ElementAt(0);
            ExtremumGroup groupClose = result.ElementAt(1);

            Assert.IsTrue(result.Count() == 2);

            Assert.IsNotNull(groupLow);
            Assert.IsFalse(groupLow.IsPeak);
            Assert.IsNotNull(groupLow.MasterExtremum);
            Assert.IsNotNull(groupLow.SecondExtremum);
            Assert.IsTrue(groupLow.SecondExtremum == troughByLow);
            Assert.IsTrue(groupLow.MasterExtremum == troughByLow);

            Assert.IsNotNull(groupClose);
            Assert.IsFalse(groupClose.IsPeak);
            Assert.IsNotNull(groupClose.MasterExtremum);
            Assert.IsNotNull(groupClose.SecondExtremum);
            Assert.IsTrue(groupClose.MasterExtremum == troughByClose);
            Assert.IsTrue(groupClose.SecondExtremum == troughByClose);

        }

        [TestMethod]
        public void ExtractExtremumGroups_IfThereIsPeakByCloseExtremumAtFirstQuotation_ItIsSkipped()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            Extremum peakByClose = new Extremum(1, 1, ExtremumType.PeakByClose, 1) { Date = new DateTime(2016, 1, 15, 22, 25, 0) };
            Extremum peakByHigh = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { Date = new DateTime(2016, 1, 15, 22, 30, 0) };
            dataSet1.GetPrice().SetExtremum(peakByClose);
            dataSet2.GetPrice().SetExtremum(peakByHigh);
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            Assert.IsTrue(result.Count() == 0);

        }

        [TestMethod]
        public void ExtractExtremumGroups_IfThereIsTroughByCloseExtremumAtFirstQuotation_ItIsSkipped()
        {
            //Arrange
            Mock<IProcessManager> mockedManager = new Mock<IProcessManager>();
            DataSet dataSet1 = new DataSet(new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09187, Low = 1.09162, Close = 1.09177, Volume = 1411, IndexNumber = 1 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0) });
            DataSet dataSet2 = new DataSet(new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09177, High = 1.09182, Low = 1.09165, Close = 1.09174, Volume = 1819, IndexNumber = 2 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 30, 0) });
            DataSet dataSet3 = new DataSet(new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1359, IndexNumber = 3 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 35, 0) });
            Extremum troughByClose = new Extremum(1, 1, ExtremumType.TroughByClose, 1) { Date = new DateTime(2016, 1, 15, 22, 25, 0) };
            Extremum troughByLow = new Extremum(1, 1, ExtremumType.TroughByLow, 2) { Date = new DateTime(2016, 1, 15, 22, 30, 0) };
            dataSet1.GetPrice().SetExtremum(troughByClose);
            dataSet2.GetPrice().SetExtremum(troughByLow);
            DataSet dataSet4 = new DataSet(new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.0915, High = 1.0916, Low = 1.09111, Close = 1.09112, Volume = 1392, IndexNumber = 4 }).SetPrice(new Price() { AssetId = 1, TimeframeId = 1, SimulationId = 1, Date = new DateTime(2016, 1, 15, 22, 40, 0) });
            mockedManager.Setup(m => m.GetDataSet(1)).Returns(dataSet1);
            mockedManager.Setup(m => m.GetDataSet(2)).Returns(dataSet2);
            mockedManager.Setup(m => m.GetDataSet(3)).Returns(dataSet3);
            mockedManager.Setup(m => m.GetDataSet(4)).Returns(dataSet4);

            DataSet[] dataSets = new DataSet[] { dataSet1, dataSet2, dataSet3, dataSet4 };

            //Act
            ExtremumProcessor processor = new ExtremumProcessor(mockedManager.Object);

            //Assert
            var result = processor.ExtractExtremumGroups(dataSets);
            Assert.IsTrue(result.Count() == 0);

        }

        #endregion EXTRACT_EXTREMUM_GROUPS

    }

}
