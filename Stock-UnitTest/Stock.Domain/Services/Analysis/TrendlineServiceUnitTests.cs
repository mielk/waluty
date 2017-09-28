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
    public class TrendlineServiceUnitTests
    {
        private const int DEFAULT_ASSET_ID = 1;
        private const int DEFAULT_TIMEFRAME_ID = 1;
        private const int DEFAULT_SIMULATION_ID = 1;
        private const int DEFAULT_START_INDEX = 87;
        private const double DEFAULT_START_LEVEL = 1.1654;
        private const int DEFAULT_END_INDEX = 100;
        private const double DEFAULT_END_LEVEL = 1.1754;
        private const double DEFAULT_VALUE = 1.234;
        private const int DEFAULT_LAST_UPDATE_INDEX = 104;



        #region GET_TRENDLINES

        [TestMethod]
        public void GetTrendlines_ReturnsProperCollectionOfTrendlines()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendlineDto> dtos = new List<TrendlineDto>();
            TrendlineDto trendlineDto1 = new TrendlineDto() { Id = 1, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID, SimulationId = DEFAULT_SIMULATION_ID, StartIndex = 50, StartLevel = 1.1234, EndLevel = 1.1412, EndIndex = 72, LastUpdateIndex = 77, Value = 1.23 };
            TrendlineDto trendlineDto2 = new TrendlineDto() { Id = 2, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID, SimulationId = DEFAULT_SIMULATION_ID, StartIndex = 64, StartLevel = 1.1276, EndLevel = 1.1412, EndIndex = 72, LastUpdateIndex = 77, Value = 1.49 };
            TrendlineDto trendlineDto3 = new TrendlineDto() { Id = 3, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID, SimulationId = DEFAULT_SIMULATION_ID, StartIndex = 81, StartLevel = 1.1315, EndLevel = 1.1412, EndIndex = 72, LastUpdateIndex = 77, Value = 2.16 };
            dtos.AddRange(new TrendlineDto[] { trendlineDto1, trendlineDto2, trendlineDto3 });

            mockedRepository.Setup(r => r.GetTrendlines(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID)).Returns(dtos);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualSimultations = service.GetTrendlines(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID);

            //Assert
            List<Trendline> expectedTrendlines = new List<Trendline>();
            Trendline trendline1 = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, 50, 1.1234, 72, 1.1412) { Id = 1, LastUpdateIndex = 77, Value = 1.23 };
            Trendline trendline2 = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, 64, 1.1276, 72, 1.1412) { Id = 2, LastUpdateIndex = 77, Value = 1.49 };
            Trendline trendline3 = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, 81, 1.1315, 72, 1.1412) { Id = 3, LastUpdateIndex = 77, Value = 2.16 };
            expectedTrendlines.AddRange(new Trendline[] { trendline1, trendline2, trendline3 });
            bool areEqual = expectedTrendlines.HasEqualItems(actualSimultations);
            Assert.IsTrue(areEqual);
            
        }

        [TestMethod]
        public void GetTrendlineById_ReturnsNull_IfThereIsNoItemWithGivenCombination()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendlineDto returnedDto = null;
            mockedRepository.Setup(r => r.GetTrendlineById(1)).Returns(returnedDto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            Trendline baseTrendline = service.GetTrendlineById(1);
            Assert.IsNull(baseTrendline);

        }

        [TestMethod]
        public void GetTrendlineById_ReturnsProperItem_IfItemExists()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendlineDto trendlineDto = new TrendlineDto() { Id = 1, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID, SimulationId = DEFAULT_SIMULATION_ID, StartIndex = 50, StartLevel = 1.1234, EndLevel = 1.1412, EndIndex = 72, LastUpdateIndex = 77, Value = 1.23 };
            mockedRepository.Setup(r => r.GetTrendlineById(1)).Returns(trendlineDto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualTrendline = service.GetTrendlineById(1);

            //Assert
            Trendline expectedTrendline = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, 50, 1.1234, 72, 1.1412) { Id = 1, LastUpdateIndex = 77, Value = 1.23 };
            bool areEqual = expectedTrendline.Equals(actualTrendline);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendlineById_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendlineDto> dtos = new List<TrendlineDto>();
            TrendlineDto trendlineDto1 = new TrendlineDto() { Id = 1, AssetId = DEFAULT_ASSET_ID, TimeframeId = DEFAULT_TIMEFRAME_ID, SimulationId = DEFAULT_SIMULATION_ID, StartIndex = 50, StartLevel = 1.1234, EndLevel = 1.1412, EndIndex = 72, LastUpdateIndex = 77, Value = 1.23 };
            dtos.AddRange(new TrendlineDto[] { trendlineDto1 });
            mockedRepository.Setup(r => r.GetTrendlines(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID)).Returns(dtos);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            Trendline baseTrendline = service.GetTrendlineById(1);
            Trendline comparedTrendline = service.GetTrendlineById(1);

            bool areTheSame = (baseTrendline == comparedTrendline);
            Assert.IsTrue(areTheSame);

        }

        #endregion GET_TRENDLINES


        #region UPDATE_TRENDLINES

        [TestMethod]
        public void UpdateTrendlines_AllItemsPassedToMethodArePassedToRepository()
        {

            //Arrange
            IEnumerable<TrendlineDto> trendlineDtos = null;
            Trendline trendline = new Trendline(DEFAULT_ASSET_ID, DEFAULT_TIMEFRAME_ID, DEFAULT_SIMULATION_ID, DEFAULT_START_INDEX, DEFAULT_START_LEVEL, DEFAULT_END_INDEX, DEFAULT_END_LEVEL) 
            { 
                Id = 1, 
                Value = 1.2345,
                LastUpdateIndex = 51
            };
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            mockedRepository.
                Setup(r => r.UpdateTrendlines(It.IsAny<IEnumerable<TrendlineDto>>())).
                Callback<IEnumerable<TrendlineDto>>((col) => trendlineDtos = col).Verifiable();

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            service.UpdateTrendline(trendline);

            //Assert
            IEnumerable<TrendlineDto> expectedTrendlineDtos = new TrendlineDto[] { trendline.ToDto() };
            mockedRepository.Verify(r => r.UpdateTrendlines(It.IsAny<IEnumerable<TrendlineDto>>()), Times.Exactly(1));
            Assert.IsTrue(trendlineDtos.HasEqualItems(expectedTrendlineDtos));

        }

        #endregion UPDATE_TRENDLINES


    }

}
