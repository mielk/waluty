using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;
using Stock.Core;
using Stock.Utils;
using System.Collections.Generic;
using System.Linq;
using Stock_UnitTest.Helpers;

namespace Stock_UnitTest.Stock.Domain
{
    [TestClass]
    public class ExtremumGroupUnitTests
    {

        private UTFactory utf = new UTFactory();




        #region CONSTRUCTOR

        [TestMethod]
        public void ExtremumGroup_Constructor_IfOnlyMasterPassed_ProperObjectIsCreated()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex = new Extremum(p, ExtremumType.PeakByClose);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex, null);

            //Assert
            Assert.IsNotNull(eg);
            Assert.AreEqual(ex, eg.MasterExtremum);
            Assert.AreEqual(ex, eg.SecondExtremum);

        }

        [TestMethod]
        public void ExtremumGroup_Constructor_IfOnlySlavePassed_ProperObjectIsCreated()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex = new Extremum(p, ExtremumType.PeakByHigh);

            //Act
            ExtremumGroup eg = new ExtremumGroup(null, ex);

            //Assert
            Assert.IsNotNull(eg);
            Assert.AreEqual(ex, eg.MasterExtremum);
            Assert.AreEqual(ex, eg.SecondExtremum);

        }

        [TestMethod]
        public void ExtremumGroup_Constructor_IfMasterAndSlavedPassed_ProperObjectIsCreated()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex1 = new Extremum(p, ExtremumType.PeakByClose);
            Extremum ex2 = new Extremum(p, ExtremumType.PeakByHigh);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex1, ex2);

            //Assert
            Assert.IsNotNull(eg);
            Assert.AreEqual(ex1, eg.MasterExtremum);
            Assert.AreEqual(ex2, eg.SecondExtremum);

        }

        [TestMethod]
        public void ExtremumGroup_Constructor_ForPeakExtremum_IsPeakPropertyIsCorrectlySet()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex1 = new Extremum(p, ExtremumType.PeakByClose);
            Extremum ex2 = new Extremum(p, ExtremumType.PeakByHigh);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex1, ex2);

            //Assert
            Assert.AreEqual(true, eg.IsPeak);

        }

        [TestMethod]
        public void ExtremumGroup_Constructor_ForTroughExtremum_IsPeakPropertyIsCorrectlySet()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex1 = new Extremum(p, ExtremumType.TroughByClose);
            Extremum ex2 = new Extremum(p, ExtremumType.TroughByLow);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex1, ex2);

            //Assert
            Assert.AreEqual(false, eg.IsPeak);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExtremumGroup_Constructor_IfPeakMasterAndTroughSlaveIsGiven_ExceptionIsThrown()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex1 = new Extremum(p, ExtremumType.PeakByClose);
            Extremum ex2 = new Extremum(p, ExtremumType.TroughByLow);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex1, ex2);

            //Assert
            Assert.AreEqual(false, eg.IsPeak);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExtremumGroup_Constructor_IfTroughMasterAndPeakSlaveIsGiven_ExceptionIsThrown()
        {

            //Arrange
            Price p = utf.getPrice(10);
            Extremum ex1 = new Extremum(p, ExtremumType.TroughByClose);
            Extremum ex2 = new Extremum(p, ExtremumType.PeakByHigh);

            //Act
            ExtremumGroup eg = new ExtremumGroup(ex1, ex2);

        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Both extrema cannot be Null")]
        public void ExtremumGroup_Constructor_IfNoneExtremumIsGiven_ExceptionIsThrown()
        {
           
            //Act
            ExtremumGroup eg = new ExtremumGroup(null, null);

        }


        #endregion CONSTRUCTOR




        #region GET_LATE_INDEX_NUMBER

        [TestMethod]
        public void GetLateIndexNumber_IfThereIsOnlyMasterExtremum_MasterExtremumIndexIsReturned()
        {
            
            //Arrange
            DataSet ds = utf.getDataSet(50);
            Price price = utf.getPrice(ds);
            Quotation quotation = new Quotation(ds);
            Extremum master = new Extremum(price, ExtremumType.PeakByClose);
            ExtremumGroup group = new ExtremumGroup(master, null);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 50;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfThereIsOnlySlaveExtremum_SlaveExtremumIndexIsReturned()
        {

            //Arrange
            DataSet ds = utf.getDataSet(51);
            Price price = utf.getPrice(ds);
            Quotation quotation = new Quotation(ds);
            Extremum slave = new Extremum(price, ExtremumType.PeakByHigh);
            ExtremumGroup group = new ExtremumGroup(null, slave);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfSlaveExtremumIsEarlier_MasterExtremumIndexIsReturned()
        {

            //Arrange
            Price price50 = utf.getPrice(50);
            Price price51 = utf.getPrice(51);

            Extremum slave = new Extremum(price50, ExtremumType.PeakByHigh);
            Extremum master = new Extremum(price51, ExtremumType.PeakByClose);
            ExtremumGroup group = new ExtremumGroup(master, slave);

            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();

            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        [TestMethod]
        public void GetLateIndexNumber_IfMasterExtremumIsEarlier_SlaveExtremumIndexIsReturned()
        {

            //Arrange
            Price price50 = utf.getPrice(50);
            Price price51 = utf.getPrice(51);

            Extremum master = new Extremum(price50, ExtremumType.PeakByClose);
            Extremum slave = new Extremum(price51, ExtremumType.PeakByHigh);
            ExtremumGroup group = new ExtremumGroup(master, slave);
            
            //Assert
            int lateIndexNumber = group.GetLateIndexNumber();
            
            //Act
            int expected = 51;
            Assert.AreEqual(expected, lateIndexNumber);

        }

        #endregion GET_LATE_INDEX_NUMBER




        #region GET_INDEX_NUMBER_FOR_QUOTATION

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereIsOnlyMasterQuotation_ReturnsMasterQuotationIndexNumber()
        {
            
            //Arrange


            //Act

            //Assert

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereIsOnlySlaveQuotation_ReturnsSlaveQuotationIndexNumber()
        {

            //Arrange

            //Act

            //Assert

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereAreBothQuotationsAndLevelLowerThanMasterHighPrice_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForPeak_IfThereAreBothQuotationsAndLevelHigherThanMasterHighPrice_ReturnsSlaveQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereIsOnlyMasterQuotation_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereIsOnlySlaveQuotation_ReturnsSlaveQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereAreBothQuotationsAndLevelHigherThanMasterLowPrice_ReturnsMasterQuotationIndexNumber()
        {

        }

        [TestMethod]
        public void GetIndexNumberForQuotationForTrough_IfThereAreBothQuotationsAndLevelLowerThanMasterLowPrice_ReturnsSlaveQuotationIndexNumber()
        {

        }

        #endregion GET_INDEX_NUMBER_FOR_QUOTATION




    }
}
