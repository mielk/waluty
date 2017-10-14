﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Stock.Domain.Entities;
using Stock.Domain.Services;
using Stock.Utils;
using Stock.Core;
using Stock.Domain.Enums;

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class PriceServiceUnitTests
    {


        #region GET_PRICES
        
        [TestMethod]
        public void GetPrices_ReturnsProperCollection_IfQueryDefIsGiven()
        {

            Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();

            //Arrange:Prices
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            mockedRepository.Setup(r => r.GetPrices(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new PriceDto[] { dto1, dto2, dto3, dto4 });

            //Arrange:Extrema
            ExtremumDto extremumDto1 = new ExtremumDto() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 1, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto2 = new ExtremumDto() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 2, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto3 = new ExtremumDto() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 3, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto4 = new ExtremumDto() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 4, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            mockedRepository.Setup(r => r.GetExtrema(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new ExtremumDto[] { extremumDto1, extremumDto2, extremumDto3, extremumDto4 });

            //Act
            IPriceService service = new PriceService(mockedRepository.Object);
            var actualPrices = service.GetPrices(new AnalysisDataQueryDefinition(1, 1));

            //Assert
            Extremum extremum1 = new Extremum(1, 1, ExtremumType.PeakByClose, 1) { ExtremumId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum2 = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { ExtremumId = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum3 = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { ExtremumId = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum4 = new Extremum(1, 1, ExtremumType.TroughByLow, 4) { ExtremumId = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };

            List<Price> expectedPrices = new List<Price>();
            Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByClose = extremum1 };
            Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByHigh = extremum2 };
            Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 , TroughByClose = extremum3, TroughByLow = extremum4 };

            expectedPrices.AddRange(new Price[] { price1, price2, price3, price4 });
            bool areEqual = expectedPrices.HasEqualItems(actualPrices);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetPrices_ForAlreadyExistingInstances_ThoseInstancesAreReturned()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(new PriceDto[] { dto1, dto2, dto3 });

            //Act
            IPriceService service = new PriceService(mockedRepository.Object);
            var actualPrices = service.GetPrices(queryDef);
            Price basePrice = actualPrices.SingleOrDefault(p => p.IndexNumber == 3);

            //Change mocking.
            mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(new PriceDto[] { dto1, dto2, dto3, dto4 });
            actualPrices = service.GetPrices(queryDef);
            Price comparedPrice = actualPrices.SingleOrDefault(p => p.IndexNumber == 3);

            //Assert
            bool areTheSameObject = (basePrice == comparedPrice);
            Assert.IsTrue(areTheSameObject);

        }

        #endregion GET_PRICES



        #region UPDATE_PRICES

        class PriceUpdateTester : IPriceRepository
        {
            List<PriceDto> priceDtos = new List<PriceDto>();
            List<ExtremumDto> extremumDtos = new List<ExtremumDto>();

            public void UpdatePrices(IEnumerable<PriceDto> prices)
            {
                priceDtos.AddRange(prices);
            }

            public void UpdateExtrema(IEnumerable<ExtremumDto> extrema)
            {
                extremumDtos.AddRange(extrema);
            }

            public IEnumerable<PriceDto> GetPriceDtosPassedAsParameters()
            {
                return priceDtos;
            }

            public IEnumerable<ExtremumDto> GetExtremumDtosPassedAsParameters()
            {
                return extremumDtos;
            }

            public IEnumerable<PriceDto> GetPrices(AnalysisDataQueryDefinition queryDef)
            {
                return null;
            }

            public IEnumerable<ExtremumDto> GetExtrema(AnalysisDataQueryDefinition queryDef)
            {
                return null;
            }

        }

        [TestMethod]
        public void UpdatePrices_OnlyItemsFlaggedAsUpdatedAreSendToRepository()
        {
            //Arrange
            PriceUpdateTester mockedRepository = new PriceUpdateTester();

            //Arrange:Extrema
            ExtremumDto extremumDto1 = new ExtremumDto() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 1, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto2 = new ExtremumDto() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 2, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto3 = new ExtremumDto() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 3, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };
            ExtremumDto extremumDto4 = new ExtremumDto() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), ExtremumType = 4, Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Timestamp = DateTime.Now, Value = 123.42 };

            Extremum extremum1 = new Extremum(1, 1, ExtremumType.PeakByClose, 1) { ExtremumId = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum2 = new Extremum(1, 1, ExtremumType.PeakByHigh, 2) { ExtremumId = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum3 = new Extremum(1, 1, ExtremumType.TroughByClose, 3) { ExtremumId = 3, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };
            Extremum extremum4 = new Extremum(1, 1, ExtremumType.TroughByLow, 4) { ExtremumId = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), LastCheckedDateTime = new DateTime(2017, 3, 5, 12, 0, 0), Volatility = 1.23, EarlierCounter = 15, EarlierAmplitude = 7.45, EarlierChange1 = 1.12, EarlierChange2 = 2.21, EarlierChange3 = 3.12, EarlierChange5 = 4.56, EarlierChange10 = 5.28, LaterCounter = 16, LaterAmplitude = 1.23, LaterChange1 = 0.72, LaterChange2 = 0.54, LaterChange3 = 1.57, LaterChange5 = 2.41, LaterChange10 = 3.15, IsOpen = true, Value = 123.42 };

            //Arrange:Prices
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };

            Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByClose = extremum1 };
            Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, PeakByHigh = extremum2 };
            Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, CloseRatio = 1.23, ExtremumRatio = 2.34, TroughByClose = extremum3, TroughByLow = extremum4 };

            //Act
            price1.IsUpdated = true;
            price3.IsNew = true;
            price4.IsUpdated = true;
            extremum2.IsUpdated = true;
            extremum3.IsUpdated = true;
            IPriceService service = new PriceService(mockedRepository);
            service.UpdatePrices(new Price[] { price1, price2, price3, price4} );
            IEnumerable<PriceDto> passedPriceDtos = mockedRepository.GetPriceDtosPassedAsParameters();
            IEnumerable<ExtremumDto> passedExtremumDtos = mockedRepository.GetExtremumDtosPassedAsParameters();

            //Assert
            IEnumerable<PriceDto> expectedPriceDtos = new PriceDto[] { dto1, dto3, dto4 };
            IEnumerable<ExtremumDto> expectedExtremumDtos = new ExtremumDto[] { extremumDto2, extremumDto3 };
            bool arePriceDtosEqual = expectedPriceDtos.HasEqualItems(passedPriceDtos);
            bool areExtremumDtosEqual = expectedExtremumDtos.HasEqualItems(passedExtremumDtos);
            bool areEqual = arePriceDtosEqual && areExtremumDtosEqual;
            Assert.IsTrue(areEqual);
        }

        #endregion UPDATE_PRICES


    }

}
