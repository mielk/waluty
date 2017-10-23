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





        #region GET_LATE_INDEX_NUMBER

        [TestMethod]
        public void GetLateIndexNumber_IfThereIsOnlyMasterExtremum_MasterExtremumIndexIsReturned()
        {
            
            //Arrange
            DataSet ds = utf.getDataSet(50);
            Price price = utf.getPrice(ds);
            Quotation quotation = new Quotation(ds);
            Extremum master = new Extremum(price, ExtremumType.PeakByClose);
            ExtremumGroup group = new ExtremumGroup(master, null, true);

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
            ExtremumGroup group = new ExtremumGroup(null, slave, true);

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
            ExtremumGroup group = new ExtremumGroup(master, slave, true);

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
            ExtremumGroup group = new ExtremumGroup(master, slave, true);
            
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
