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
using Stock.Domain.Services;
using Stock.Utils;
using Stock.Core;   

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class PriceServiceUnitTests
    {


        #region GET_PRICES

        [TestMethod]
        public void GetPrices_ReturnsProperCollection_IfAssetAndTimeframeAreGiven()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();
            List<PriceDto> dtos = new List<PriceDto>();
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.45, PeakByHighEvaluation = 1.67, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            dtos.AddRange(new PriceDto[] { dto1, dto2, dto3, dto4 });
            mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(dtos);

            //Act
            IPriceService service = PriceService.Instance();
            service.InjectRepository(mockedRepository.Object);
            var actualPrices = service.GetPrices(queryDef);

            //Assert
            List<Price> expectedPrices = new List<Price>();
            Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.45, PeakByHigh = 1.67, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            expectedPrices.AddRange(new Price[] { price1, price2, price3, price4 });
            bool areEqual = expectedPrices.HasEqualItems(actualPrices);
            Assert.IsTrue(areEqual);
            
        }

        [TestMethod]
        public void GetPrices_ReturnsProperCollection_IfQueryDefIsGiven()
        {

            //Arrange
            Mock<IPriceRepository> mockedRepository = new Mock<IPriceRepository>();
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.45, PeakByHighEvaluation = 1.67, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            mockedRepository.Setup(r => r.GetPrices(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new PriceDto[] { dto1, dto2, dto3, dto4 });

            //Act
            IPriceService service = PriceService.Instance();
            service.InjectRepository(mockedRepository.Object);
            var actualPrices = service.GetPrices(new AnalysisDataQueryDefinition(1, 1));

            //Assert
            List<Price> expectedPrices = new List<Price>();
            Price price1 = new Price() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, CloseDelta = 1.05, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price2 = new Price() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, CloseDelta = 1.06, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price3 = new Price() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, CloseDelta = 1.07, Direction2D = 1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.23, PeakByHigh = 1.25, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            Price price4 = new Price() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, CloseDelta = 1.08, Direction2D = -1, Direction3D = 0, PriceGap = 1.23, PeakByClose = 1.45, PeakByHigh = 1.67, TroughByClose = 0, TroughByLow = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
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
            PriceDto dto1 = new PriceDto() { Id = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), IndexNumber = 1, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.05, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto2 = new PriceDto() { Id = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), IndexNumber = 2, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.06, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto3 = new PriceDto() { Id = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), IndexNumber = 3, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.07, PriceDirection2D = 1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.23, PeakByHighEvaluation = 1.25, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            PriceDto dto4 = new PriceDto() { Id = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), IndexNumber = 4, AssetId = 1, TimeframeId = 1, DeltaClosePrice = 1.08, PriceDirection2D = -1, PriceDirection3D = 0, PriceGap = 1.23, PeakByCloseEvaluation = 1.45, PeakByHighEvaluation = 1.67, TroughByCloseEvaluation = 0, TroughByLowEvaluation = 0, CloseRatio = 1.23, ExtremumRatio = 2.34 };
            mockedRepository.Setup(r => r.GetPrices(queryDef)).Returns(new PriceDto[] { dto1, dto2, dto3 });

            //Act
            IPriceService service = PriceService.Instance();
            service.InjectRepository(mockedRepository.Object);
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


    }
}
