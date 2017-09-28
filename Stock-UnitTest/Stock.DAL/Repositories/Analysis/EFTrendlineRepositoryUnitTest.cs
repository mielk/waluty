using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Helpers;
using Stock.Utils;
using Stock.Core;

namespace Stock_UnitTest.Stock.DAL.Repositories.Data
{
    [TestClass]
    public class EFTrendlineRepositoryUnitTest
    {

        private const string UNIT_TEST_DB_NAME = "fx_unittests";
        private const string TRENDLINES_TABLE_NAME = "trendlines";
        private const string TREND_HITS_TABLE_NAME = "trend_hits";


        #region INFRASTRUCTURE

        [ClassInitialize()]
        public static void InitTestSuite(TestContext testContext)
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("recreateDb");
        }

        [ClassCleanup()]
        public static void CleanupTestSuite()
        {
            DbContext context = new OriginalDbContext();
            context.Database.ExecuteSqlCommand("DROP DATABASE fx_unittests");
        }

        private void clearTrendlinesTables()
        {
            DbContext context = new UnitTestsDbContext();
            context.ClearTable(UNIT_TEST_DB_NAME, TRENDLINES_TABLE_NAME);
        }

        private void clearTrendHitsTables()
        {
            DbContext context = new UnitTestsDbContext();
            context.ClearTable(UNIT_TEST_DB_NAME, TREND_HITS_TABLE_NAME);
        }

        #endregion INFRASTRUCTURE



        #region UPDATE_TRENDLINES

        [TestMethod]
        public void UpdateTrendlines_WorksProperly_IfItemsAreOnlyAdded()
        {

            //Arrange
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendlineDto> trendlines = new List<TrendlineDto>();
            TrendlineDto dto1 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 5, StartLevel = 1.2345, EndIndex = 26, EndLevel = 1.3456, Value = 1.234, LastUpdateIndex = 31 };
            TrendlineDto dto2 = new TrendlineDto() { Id = 2, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 6, StartLevel = 1.4567, EndIndex = 23, EndLevel = 1.5678, Value = 1.345, LastUpdateIndex = 29 };
            TrendlineDto dto3 = new TrendlineDto() { Id = 3, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 12, StartLevel = 1.5678, EndIndex = 45, EndLevel = 1.6789, Value = 1.567, LastUpdateIndex = 47 };
            TrendlineDto dto4 = new TrendlineDto() { Id = 4, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 8, StartLevel = 1.6789, EndIndex = 21, EndLevel = 1.7891, Value = 1.678, LastUpdateIndex = 29 };
            trendlines.AddRange(new TrendlineDto[] { dto1, dto2, dto3, dto4 });

            //Act
            clearTrendlinesTables();
            repository.UpdateTrendlines(trendlines);
            IEnumerable<TrendlineDto> actualRecords = repository.GetTrendlines(1, 1, 1);

            //Assert
            Assert.IsTrue(repository.GetTrendlines(1, 1, 1).HasEqualItems(new TrendlineDto[] { dto1, dto2 }));
            Assert.IsTrue(repository.GetTrendlines(1, 1, 2).HasEqualItems(new TrendlineDto[] { dto3, dto4 }));

        }

        [TestMethod]
        public void UpdateTrendlines_WorksProperly_IfItemsAreOnlyUpdated()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendlineDto> trendlines = new List<TrendlineDto>();
            TrendlineDto dto1 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 5, StartLevel = 1.2345, EndIndex = 26, EndLevel = 1.3456, Value = 1.234, LastUpdateIndex = 31 };
            TrendlineDto dto2 = new TrendlineDto() { Id = 2, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 6, StartLevel = 1.4567, EndIndex = 23, EndLevel = 1.5678, Value = 1.345, LastUpdateIndex = 29 };
            TrendlineDto dto3 = new TrendlineDto() { Id = 3, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 12, StartLevel = 1.5678, EndIndex = 45, EndLevel = 1.6789, Value = 1.567, LastUpdateIndex = 47 };
            TrendlineDto dto4 = new TrendlineDto() { Id = 4, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 8, StartLevel = 1.6789, EndIndex = 21, EndLevel = 1.7891, Value = 1.678, LastUpdateIndex = 29 };
            trendlines.AddRange(new TrendlineDto[] { dto1, dto2, dto3, dto4 });
            clearTrendlinesTables();
            repository.UpdateTrendlines(trendlines);

            //Act
            dto1.Value = 2.345;
            dto2.Value = 3.456;
            dto3.Value = 4.567;
            repository.UpdateTrendlines(new TrendlineDto[] { dto1, dto2, dto3, dto4 });

            //Assert
            Assert.IsTrue(repository.GetTrendlines(1, 1, 1).HasEqualItems(new TrendlineDto[] { dto1, dto2 }));
            Assert.IsTrue(repository.GetTrendlines(1, 1, 2).HasEqualItems(new TrendlineDto[] { dto3, dto4 }));

        }

        [TestMethod]
        public void UpdateTrendlines_WorksProperly_IfItemsAreAddedAndUpdated()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendlineDto> trendlines = new List<TrendlineDto>();
            TrendlineDto dto1 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 5, StartLevel = 1.2345, EndIndex = 26, EndLevel = 1.3456, Value = 1.234, LastUpdateIndex = 31 };
            TrendlineDto dto2 = new TrendlineDto() { Id = 2, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 6, StartLevel = 1.4567, EndIndex = 23, EndLevel = 1.5678, Value = 1.345, LastUpdateIndex = 29 };
            TrendlineDto dto3 = new TrendlineDto() { Id = 3, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 12, StartLevel = 1.5678, EndIndex = 45, EndLevel = 1.6789, Value = 1.567, LastUpdateIndex = 47 };
            TrendlineDto dto4 = new TrendlineDto() { Id = 4, AssetId = 1, TimeframeId = 1, SimulationId = 2, StartIndex = 8, StartLevel = 1.6789, EndIndex = 21, EndLevel = 1.7891, Value = 1.678, LastUpdateIndex = 29 };
            trendlines.AddRange(new TrendlineDto[] { dto1, dto2 });
            clearTrendlinesTables();
            repository.UpdateTrendlines(trendlines);

            //Act
            dto1.Value = 2.345;
            dto2.LastUpdateIndex += 2;
            dto2.Value += 0.250;
            dto2.LastUpdateIndex = 32;
            repository.UpdateTrendlines(new TrendlineDto[] { dto1, dto2, dto3, dto4 });

            //Assert
            Assert.IsTrue(repository.GetTrendlines(1, 1, 1).HasEqualItems(new TrendlineDto[] { dto1, dto2 }));
            Assert.IsTrue(repository.GetTrendlines(1, 1, 2).HasEqualItems(new TrendlineDto[] { dto3, dto4 }));

        }

        #endregion UPDATE_TRENDLINES



        #region GET_TRENDLINES

        [TestMethod]
        public void GetTrendlines_returnProperDtoCollection()
        {

            //Arrange
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendlineDto> trendlines = new List<TrendlineDto>();
            TrendlineDto dto1 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 5, StartLevel = 1.2573, EndIndex = 27, EndLevel = 1.2871, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto2 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 2, SimulationId = 1, StartIndex = 7, StartLevel = 1.0123, EndIndex = 52, EndLevel = 1.4865, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto3 = new TrendlineDto() { Id = 1, AssetId = 2, TimeframeId = 1, SimulationId = 1, StartIndex = 7, StartLevel = 1.1234, EndIndex = 60, EndLevel = 1.4564, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto4 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 25, StartLevel = 1.3456, EndIndex = 48, EndLevel = 1.4564, Value = 1.54, LastUpdateIndex = 70 };
            trendlines.AddRange(new TrendlineDto[] { dto1, dto2, dto3, dto4 });
            clearTrendlinesTables(); 
            repository.UpdateTrendlines(trendlines);

            //Act
            IEnumerable<TrendlineDto> dtos = repository.GetTrendlines(1, 1, 1).ToArray();

            //Assert
            IEnumerable<TrendlineDto> expected = new TrendlineDto[] { dto1, dto4 };
            bool areEqualArrays = expected.HasEqualItems(dtos);
            Assert.IsTrue(areEqualArrays);

        }

        [TestMethod]
        public void GetTrendlineById_ReturnsNull_IfThereIsNoTrendlineWithSuchId()
        {

            //Arrange
            EFSimulationRepository repository = new EFSimulationRepository();
            List<SimulationDto> simulations = new List<SimulationDto>();
            SimulationDto dto1 = new SimulationDto() { Id = 1, Name = "a" };
            simulations.AddRange(new SimulationDto[] { dto1 });
            clearTrendlinesTables();
            repository.UpdateSimulations(simulations);

            //Act
            SimulationDto dto = repository.GetSimulationById(2);

            //Assert
            Assert.IsNull(dto);

        }

        [TestMethod]
        public void GetTrendlineById_ReturnsProperTrendlineDto_IfExists()
        {

            //Arrange
            EFSimulationRepository repository = new EFSimulationRepository();
            List<SimulationDto> simulations = new List<SimulationDto>();
            SimulationDto expected = new SimulationDto() { Id = 1, Name = "a" };
            simulations.AddRange(new SimulationDto[] { expected });
            clearTrendlinesTables();
            repository.UpdateSimulations(simulations);

            //Act
            SimulationDto dto = repository.GetSimulationById(expected.Id);

            //Assert
            var areEqual = expected.Equals(dto);
            Assert.IsTrue(areEqual);

        }


        #endregion GET_TRENDLINES




        #region GET_TREND_HITS



        #endregion GET_TREND_HITS



        #region UPDATE_TREND_HITS

        [TestMethod]
        public void UpdateTrendHits_WorksProperly_IfItemsAreOnlyAdded()
        {

            //Arrange
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendHitDto> trendHits = new List<TrendHitDto>();
            TrendHitDto dto1 = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto2 = new TrendHitDto() { Id = 2, Guid = "89BFF378-F310-4A28-B753-00A0FF9A852C", TrendlineId = 1, IndexNumber = 9, ExtremumType = 2, Value = 1.345, DistanceToLine = 0.0007, PreviousRangeGuid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", NextRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58" };
            TrendHitDto dto3 = new TrendHitDto() { Id = 3, Guid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", TrendlineId = 1, IndexNumber = 18, ExtremumType = 2, Value = 1.567, DistanceToLine = 0.0002, PreviousRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C", NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto4 = new TrendHitDto() { Id = 4, Guid = "562BED90-29F8-423E-8D00-DE699C1D14C3", TrendlineId = 2, IndexNumber = 21, ExtremumType = 3, Value = 1.678, DistanceToLine = 0.0001, PreviousRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", NextRangeGuid = null };
            trendHits.AddRange(new TrendHitDto[] { dto1, dto2, dto3, dto4 });

            //Act
            clearTrendHitsTables();
            repository.UpdateTrendHits(trendHits);
            IEnumerable<TrendHitDto> actualRecords = repository.GetTrendHits(1);

            //Assert
            Assert.IsTrue(repository.GetTrendHits(1).HasEqualItems(new TrendHitDto[] { dto1, dto2, dto3 }));
            Assert.IsTrue(repository.GetTrendHits(2).HasEqualItems(new TrendHitDto[] { dto4 }));

        }

        [TestMethod]
        public void UpdateTrendHits_WorksProperly_IfItemsAreOnlyUpdated()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendHitDto> trendHits = new List<TrendHitDto>();
            TrendHitDto dto1 = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto2 = new TrendHitDto() { Id = 2, Guid = "89BFF378-F310-4A28-B753-00A0FF9A852C", TrendlineId = 1, IndexNumber = 9, ExtremumType = 2, Value = 1.345, DistanceToLine = 0.0007, PreviousRangeGuid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", NextRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58" };
            TrendHitDto dto3 = new TrendHitDto() { Id = 3, Guid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", TrendlineId = 1, IndexNumber = 18, ExtremumType = 2, Value = 1.567, DistanceToLine = 0.0002, PreviousRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C", NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto4 = new TrendHitDto() { Id = 4, Guid = "562BED90-29F8-423E-8D00-DE699C1D14C3", TrendlineId = 2, IndexNumber = 21, ExtremumType = 3, Value = 1.678, DistanceToLine = 0.0001, PreviousRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", NextRangeGuid = null };
            trendHits.AddRange(new TrendHitDto[] { dto1, dto2, dto3, dto4 });
            clearTrendHitsTables();
            repository.UpdateTrendHits(trendHits);

            //Act
            dto1.Value = 2.345;
            dto2.Value = 3.456;
            dto3.Value = 4.567;
            repository.UpdateTrendHits(new TrendHitDto[] { dto1, dto2, dto3, dto4 });

            //Assert
            Assert.IsTrue(repository.GetTrendHits(1).HasEqualItems(new TrendHitDto[] { dto1, dto2, dto3 }));
            Assert.IsTrue(repository.GetTrendHits(2).HasEqualItems(new TrendHitDto[] { dto4 }));

        }

        [TestMethod]
        public void UpdateTrendHits_WorksProperly_IfItemsAreAddedAndUpdated()
        {

            //Arrange
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendHitDto> trendHits = new List<TrendHitDto>();
            TrendHitDto dto1 = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto2 = new TrendHitDto() { Id = 2, Guid = "89BFF378-F310-4A28-B753-00A0FF9A852C", TrendlineId = 1, IndexNumber = 9, ExtremumType = 2, Value = 1.345, DistanceToLine = 0.0007, PreviousRangeGuid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", NextRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58" };
            TrendHitDto dto3 = new TrendHitDto() { Id = 3, Guid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", TrendlineId = 1, IndexNumber = 18, ExtremumType = 2, Value = 1.567, DistanceToLine = 0.0002, PreviousRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C", NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto4 = new TrendHitDto() { Id = 4, Guid = "562BED90-29F8-423E-8D00-DE699C1D14C3", TrendlineId = 2, IndexNumber = 21, ExtremumType = 3, Value = 1.678, DistanceToLine = 0.0001, PreviousRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", NextRangeGuid = null };
            trendHits.AddRange(new TrendHitDto[] { dto1, dto2, dto3, dto4 });
            clearTrendHitsTables();
            repository.UpdateTrendHits(trendHits);

            //Act
            dto1.Value = 2.345;
            dto1.DistanceToLine += 0.002;
            dto2.Value += 0.250;
            dto1.DistanceToLine -= 0.002;
            repository.UpdateTrendHits(new TrendHitDto[] { dto1, dto2, dto3, dto4 });

            //Assert
            Assert.IsTrue(repository.GetTrendHits(1).HasEqualItems(new TrendHitDto[] { dto1, dto2, dto3 }));
            Assert.IsTrue(repository.GetTrendHits(2).HasEqualItems(new TrendHitDto[] { dto4 }));

        }

        #endregion UPDATE_TREND_HITS



    }
}

