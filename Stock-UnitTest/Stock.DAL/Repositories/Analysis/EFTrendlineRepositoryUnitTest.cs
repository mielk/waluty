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

        #endregion INFRASTRUCTURE


        #region UPDATE_SIMULATIONS

        //[TestMethod]
        //public void UpdateSimulations_WorksProperly_IfItemsAreOnlyAdded()
        //{

        //    //Arrange
        //    EFSimulationRepository repository = new EFSimulationRepository();
        //    List<SimulationDto> simulations = new List<SimulationDto>();
        //    SimulationDto dto1 = new SimulationDto() { Id = 1, Name = "a" };
        //    SimulationDto dto2 = new SimulationDto() { Id = 2, Name = "b" };
        //    SimulationDto dto3 = new SimulationDto() { Id = 3, Name = "c" };
        //    SimulationDto dto4 = new SimulationDto() { Id = 4, Name = "d" };
        //    simulations.AddRange(new SimulationDto[] { dto1, dto2, dto3, dto4 });

        //    //Act
        //    clearTrendlinesTables();
        //    repository.UpdateSimulations(simulations);
        //    IEnumerable<SimulationDto> actualRecords = repository.GetSimulations();

        //    //Assert
        //    bool areEqual = simulations.HasEqualItems(actualRecords);
        //    Assert.IsTrue(areEqual);

        //}

        //[TestMethod]
        //public void UpdateSimulations_WorksProperly_IfItemsAreOnlyUpdated()
        //{

        //    //Arrange
        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
        //    EFSimulationRepository repository = new EFSimulationRepository();
        //    List<SimulationDto> simulations = new List<SimulationDto>();
        //    SimulationDto dto1 = new SimulationDto() { Id = 1, Name = "a" };
        //    SimulationDto dto2 = new SimulationDto() { Id = 2, Name = "b" };
        //    SimulationDto dto3 = new SimulationDto() { Id = 3, Name = "c" };
        //    SimulationDto dto4 = new SimulationDto() { Id = 4, Name = "d" };
        //    simulations.AddRange(new SimulationDto[] { dto1, dto2, dto3, dto4 });
        //    clearTrendlinesTables();
        //    repository.UpdateSimulations(simulations);

        //    //Act
        //    dto1.Name = "x";
        //    dto2.Name = "y";
        //    dto3.Name = "z";
        //    repository.UpdateSimulations(new SimulationDto[] { dto1, dto2, dto3 });
        //    IEnumerable<SimulationDto> actualRecords = repository.GetSimulations();

        //    //Assert
        //    bool areEqual = simulations.HasEqualItems(actualRecords);
        //    Assert.IsTrue(areEqual);

        //}

        //[TestMethod]
        //public void UpdateSimulations_WorksProperly_IfItemsAreAddedAndUpdated()
        //{

        //    //Arrange
        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(1, 1);
        //    EFSimulationRepository repository = new EFSimulationRepository();
        //    List<SimulationDto> simulations = new List<SimulationDto>();
        //    SimulationDto dto1 = new SimulationDto() { Id = 1, Name = "a" };
        //    SimulationDto dto2 = new SimulationDto() { Id = 2, Name = "b" };
        //    SimulationDto dto3 = new SimulationDto() { Id = 3, Name = "c" };
        //    SimulationDto dto4 = new SimulationDto() { Id = 4, Name = "d" };
        //    simulations.AddRange(new SimulationDto[] { dto1, dto2, dto3 });
        //    clearTrendlinesTables();
        //    repository.UpdateSimulations(simulations);

        //    //Act
        //    dto1.Name = "x";
        //    dto2.Name += "b";
            
        //    IEnumerable<SimulationDto> expectedRecords = new SimulationDto[] { dto1, dto2, dto3, dto4 };
        //    repository.UpdateSimulations(expectedRecords);
        //    IEnumerable<SimulationDto> actualRecords = repository.GetSimulations();

        //    //Assert
        //    bool areEqual = expectedRecords.HasEqualItems(actualRecords);
        //    Assert.IsTrue(areEqual);

        //}

        #endregion UPDATE_SIMULATIONS


        #region GET_TRENDLINES

        [TestMethod]
        public void GetTrendlines_returnProperDtoCollection()
        {

            //Arrange
            EFTrendlineRepository repository = new EFTrendlineRepository();
            List<TrendlineDto> trendlines = new List<TrendlineDto>();
            TrendlineDto dto1 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 5, StartLevel = 1.2573d, EndIndex = 27, EndLevel = 1.2871d, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto2 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 2, SimulationId = 1, StartIndex = 7, StartLevel = 1.0123, EndIndex = 52, EndLevel = 1.4865d, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto3 = new TrendlineDto() { Id = 1, AssetId = 2, TimeframeId = 1, SimulationId = 1, StartIndex = 7, StartLevel = 1.1234d, EndIndex = 60, EndLevel = 1.4564d, Value = 1.54, LastUpdateIndex = 70 };
            TrendlineDto dto4 = new TrendlineDto() { Id = 1, AssetId = 1, TimeframeId = 1, SimulationId = 1, StartIndex = 25, StartLevel = 1.3456d, EndIndex = 48, EndLevel = 1.4564d, Value = 1.54, LastUpdateIndex = 70 };
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


    }
}

