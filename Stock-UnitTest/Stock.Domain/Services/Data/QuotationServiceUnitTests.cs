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
    public class QuotationServiceUnitTests
    {


        #region GET_QUOTATIONS

        [TestMethod]
        public void GetQuotations_ReturnsProperCollection_IfQueryDefIsGiven()
        {

            //Arrange
            Mock<IQuotationRepository> mockedRepository = new Mock<IQuotationRepository>();
            QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            mockedRepository.Setup(r => r.GetQuotations(It.IsAny<AnalysisDataQueryDefinition>())).Returns(new QuotationDto[] { dto1, dto2, dto3, dto4 });

            //Act
            IQuotationService service = new QuotationService(mockedRepository.Object);
            var actualQuotations = service.GetQuotations(new AnalysisDataQueryDefinition(1, 1));

            //Assert
            List<Quotation> expectedQuotations = new List<Quotation>();
            Quotation quotation1 = new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 3 };
            Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 4 };
            Quotation quotation4 = new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 5 };
            expectedQuotations.AddRange(new Quotation[] { quotation1, quotation2, quotation3, quotation4 });
            bool areEqual = expectedQuotations.HasEqualItems(actualQuotations);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetQuotations_ForAlreadyExistingInstances_ThoseInstancesAreReturned()
        {

            //Arrange
            Mock<IQuotationRepository> mockedRepository = new Mock<IQuotationRepository>();
            List<QuotationDto> dtos = new List<QuotationDto>();
            QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            mockedRepository.Setup(r => r.GetQuotations(queryDef)).Returns(new QuotationDto[] { dto1, dto2, dto3 });

            //Act
            IQuotationService service = new QuotationService(mockedRepository.Object);
            var actualQuotations = service.GetQuotations(queryDef);
            Quotation baseQuotation = actualQuotations.SingleOrDefault(q => q.IndexNumber == 3);

            //Change mocking.
            mockedRepository.Setup(r => r.GetQuotations(queryDef)).Returns(new QuotationDto[] { dto1, dto2, dto3, dto4 });
            actualQuotations = service.GetQuotations(queryDef);
            Quotation comparedQuotation = actualQuotations.SingleOrDefault(q => q.IndexNumber == 3);

            //Assert
            bool areTheSameObject = (baseQuotation == comparedQuotation);
            Assert.IsTrue(areTheSameObject);

        }

        #endregion GET_QUOTATIONS


        #region UPDATE_QUOTATIONS

        class QuotationUpdateTester : IQuotationRepository
        {
            List<QuotationDto> parameterDtos = new List<QuotationDto>();

            public void UpdateQuotations(IEnumerable<QuotationDto> quotations)
            {
                parameterDtos.AddRange(quotations);
            }
            public IEnumerable<QuotationDto> GetDtosPassedAsParameters()
            {
                return parameterDtos;
            }
            public IEnumerable<QuotationDto> GetQuotations(AnalysisDataQueryDefinition queryDef)
            {
                return null;
            }
        }

        [TestMethod]
        public void UpdateQuotations_OnlyItemsFlaggedAsUpdatedAreSendToRepository() 
        {
            //Arrange
            QuotationUpdateTester mockedRepository = new QuotationUpdateTester();
            List<Quotation> quotations = new List<Quotation>();
            Quotation quotation1 = new Quotation() { Id = 1, Date = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, Open = 1.09191, High = 1.09218, Low = 1.09186, Close = 1.09194, Volume = 1411, IndexNumber = 2 };
            Quotation quotation2 = new Quotation() { Id = 2, Date = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 3 };
            Quotation quotation3 = new Quotation() { Id = 3, Date = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 4 };
            Quotation quotation4 = new Quotation() { Id = 4, Date = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, Open = 1.09193, High = 1.09256, Low = 1.09165, Close = 1.09177, Volume = 1819, IndexNumber = 5 };
            quotations.AddRange(new Quotation[] { quotation1, quotation2, quotation3, quotation4 });

            //Act
            quotation1.IsUpdated = true;
            quotation3.IsUpdated = true;
            quotation4.IsNew = true;
            IQuotationService service = new QuotationService(mockedRepository);
            service.UpdateQuotations(quotations);

            //Assert
            IEnumerable<QuotationDto> passedDtos = mockedRepository.GetDtosPassedAsParameters();
            List<QuotationDto> expectedDtos = new List<QuotationDto>();
            QuotationDto dto1 = new QuotationDto() { QuotationId = 1, PriceDate = new DateTime(2016, 1, 15, 22, 25, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09191, HighPrice = 1.09218, LowPrice = 1.09186, ClosePrice = 1.09194, Volume = 1411, IndexNumber = 2 };
            QuotationDto dto2 = new QuotationDto() { QuotationId = 2, PriceDate = new DateTime(2016, 1, 15, 22, 30, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 3 };
            QuotationDto dto3 = new QuotationDto() { QuotationId = 3, PriceDate = new DateTime(2016, 1, 15, 22, 35, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 4 };
            QuotationDto dto4 = new QuotationDto() { QuotationId = 4, PriceDate = new DateTime(2016, 1, 15, 22, 40, 0), AssetId = 1, TimeframeId = 1, OpenPrice = 1.09193, HighPrice = 1.09256, LowPrice = 1.09165, ClosePrice = 1.09177, Volume = 1819, IndexNumber = 5 };
            expectedDtos.AddRange(new QuotationDto[] { dto1, dto3, dto4 });
            bool areEqual = expectedDtos.HasEqualItems(passedDtos);
            Assert.IsTrue(areEqual);
        }


        #endregion UPDATE_QUOTATIONS    


    }
}
