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

namespace Stock_UnitTest.Stock.Domain.Services
{
    [TestClass]
    public class SimulationServiceUnitTests
    {

        #region GET_SIMULATIONS

        [TestMethod]
        public void GetSimulations_ReturnsProperCollectionOfSimulations_IncludingLastUpdateTimestamps()
        {

            //Arrange
            Mock<ISimulationRepository> mockedRepository = new Mock<ISimulationRepository>();
            List<SimulationDto> dtos = new List<SimulationDto>();
            SimulationDto simulationDto1 = new SimulationDto() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };
            SimulationDto simulationDto2 = new SimulationDto() { Id = 2, Name = "a", AssetId = 1, TimeframeId = 2 };
            SimulationDto simulationDto3 = new SimulationDto() { Id = 3, Name = "b", AssetId = 2, TimeframeId = 1 };
            dtos.AddRange(new SimulationDto[] { simulationDto1, simulationDto2, simulationDto3 });

            List<AnalysisTimestampDto> timestamps = new List<AnalysisTimestampDto>();
            AnalysisTimestampDto timestamp1 = new AnalysisTimestampDto() { Id = 1, SimulationId = 1, AnalysisTypeId = 2, LastAnalysedItem = new DateTime(2016, 5, 2, 12, 0, 0) };
            AnalysisTimestampDto timestamp2 = new AnalysisTimestampDto() { Id = 2, SimulationId = 1, AnalysisTypeId = 3, LastAnalysedItem = new DateTime(2016, 5, 2, 13, 0, 0) };
            AnalysisTimestampDto timestamp3 = new AnalysisTimestampDto() { Id = 3, SimulationId = 1, AnalysisTypeId = 4, LastAnalysedItem = new DateTime(2016, 5, 2, 14, 0, 0) };
            AnalysisTimestampDto timestamp4 = new AnalysisTimestampDto() { Id = 4, SimulationId = 2, AnalysisTypeId = 2, LastAnalysedItem = new DateTime(2016, 5, 2, 12, 0, 0) };
            AnalysisTimestampDto timestamp5 = new AnalysisTimestampDto() { Id = 5, SimulationId = 2, AnalysisTypeId = 3, LastAnalysedItem = new DateTime(2016, 5, 2, 13, 0, 0) };
            AnalysisTimestampDto timestamp6 = new AnalysisTimestampDto() { Id = 6, SimulationId = 2, AnalysisTypeId = 4, LastAnalysedItem = new DateTime(2016, 5, 2, 14, 0, 0) };
            timestamps.AddRange(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3, timestamp4, timestamp5, timestamp6 });

            mockedRepository.Setup(r => r.GetSimulations()).Returns(dtos);
            mockedRepository.Setup(r => r.GetAnalysisTimestampsForSimulation(1)).Returns(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 });
            mockedRepository.Setup(r => r.GetAnalysisTimestampsForSimulation(2)).Returns(new AnalysisTimestampDto[] { timestamp4, timestamp5, timestamp6 });

            //Act
            SimulationService service = new SimulationService(mockedRepository.Object);
            var actualSimultations = service.GetSimulations();

            //Assert
            List<Simulation> expectedSimulations = new List<Simulation>();
            Simulation simulation1 = new Simulation() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };
            simulation1.AddLastUpdate((AnalysisType)2, new DateTime(2016, 5, 2, 12, 0, 0));
            simulation1.AddLastUpdate((AnalysisType)3, new DateTime(2016, 5, 2, 13, 0, 0));
            simulation1.AddLastUpdate((AnalysisType)4, new DateTime(2016, 5, 2, 14, 0, 0));
            simulation1.AddLastUpdate(AnalysisType.Prices, new DateTime(2016, 5, 2, 12, 0, 0));
            Simulation simulation2 = new Simulation() { Id = 2, Name = "a", AssetId = 1, TimeframeId = 2 };
            simulation2.AddLastUpdate((AnalysisType)2, new DateTime(2016, 5, 2, 12, 0, 0));
            simulation2.AddLastUpdate((AnalysisType)3, new DateTime(2016, 5, 2, 13, 0, 0));
            simulation2.AddLastUpdate((AnalysisType)4, new DateTime(2016, 5, 2, 14, 0, 0));
            Simulation simulation3 = new Simulation() { Id = 3, Name = "b", AssetId = 2, TimeframeId = 1 };
            expectedSimulations.AddRange(new Simulation[] { simulation1, simulation2, simulation3 });
            bool areEqual = expectedSimulations.HasEqualItems(actualSimultations);
            Assert.IsTrue(areEqual);
            
        }

        [TestMethod]
        public void GetSimulationByNameAssetTimeframe_ReturnsNull_IfThereIsNoItemWithGivenCombination()
        {

            //Arrange
            Mock<ISimulationRepository> mockedRepository = new Mock<ISimulationRepository>();
            SimulationDto returnedDto = null;
            mockedRepository.Setup(r => r.GetSimulationByNameAssetTimeframe("a", 1, 1)).Returns(returnedDto);

            //Act
            SimulationService service = new SimulationService(mockedRepository.Object);

            //Assert
            Simulation baseSimulation = service.GetSimulationByNameAssetTimeframe("a", 1, 1);
            Assert.IsNull(baseSimulation);

        }

        [TestMethod]
        public void GetSimulationByNameAssetTimeframe_ReturnsProperItem_IfItemExists()
        {

            //Arrange
            Mock<ISimulationRepository> mockedRepository = new Mock<ISimulationRepository>();
            SimulationDto simulationDto = new SimulationDto() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };

            List<AnalysisTimestampDto> timestamps = new List<AnalysisTimestampDto>();
            AnalysisTimestampDto timestamp1 = new AnalysisTimestampDto() { Id = 1, SimulationId = 1, AnalysisTypeId = 2, LastAnalysedItem = new DateTime(2016, 5, 2, 12, 0, 0) };
            AnalysisTimestampDto timestamp2 = new AnalysisTimestampDto() { Id = 2, SimulationId = 1, AnalysisTypeId = 3, LastAnalysedItem = new DateTime(2016, 5, 2, 13, 0, 0) };
            AnalysisTimestampDto timestamp3 = new AnalysisTimestampDto() { Id = 3, SimulationId = 1, AnalysisTypeId = 4, LastAnalysedItem = new DateTime(2016, 5, 2, 14, 0, 0) };
            timestamps.AddRange(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 });

            mockedRepository.Setup(r => r.GetSimulationByNameAssetTimeframe("a", 1, 1)).Returns(simulationDto);
            mockedRepository.Setup(r => r.GetAnalysisTimestampsForSimulation(1)).Returns(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 });

            //Act
            SimulationService service = new SimulationService(mockedRepository.Object);
            var actualSimulation = service.GetSimulationByNameAssetTimeframe("a", 1, 1);

            //Assert
            Simulation expectedSimulation = new Simulation() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };
            expectedSimulation.AddLastUpdate((AnalysisType)2, new DateTime(2016, 5, 2, 12, 0, 0));
            expectedSimulation.AddLastUpdate((AnalysisType)3, new DateTime(2016, 5, 2, 13, 0, 0));
            expectedSimulation.AddLastUpdate((AnalysisType)4, new DateTime(2016, 5, 2, 14, 0, 0));
            bool areEqual = expectedSimulation.Equals(actualSimulation);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetSimulationByNameAssetTimeframe_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ISimulationRepository> mockedRepository = new Mock<ISimulationRepository>();
            List<SimulationDto> dtos = new List<SimulationDto>();
            SimulationDto simulationDto1 = new SimulationDto() { Id = 1, Name = "a", AssetId = 1, TimeframeId = 1 };
            dtos.AddRange(new SimulationDto[] { simulationDto1 });

            List<AnalysisTimestampDto> timestamps = new List<AnalysisTimestampDto>();
            AnalysisTimestampDto timestamp1 = new AnalysisTimestampDto() { Id = 1, SimulationId = 1, AnalysisTypeId = 2, LastAnalysedItem = new DateTime(2016, 5, 2, 12, 0, 0) };
            AnalysisTimestampDto timestamp2 = new AnalysisTimestampDto() { Id = 2, SimulationId = 1, AnalysisTypeId = 3, LastAnalysedItem = new DateTime(2016, 5, 2, 13, 0, 0) };
            AnalysisTimestampDto timestamp3 = new AnalysisTimestampDto() { Id = 3, SimulationId = 1, AnalysisTypeId = 4, LastAnalysedItem = new DateTime(2016, 5, 2, 14, 0, 0) };
            timestamps.AddRange(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 });

            mockedRepository.Setup(r => r.GetSimulations()).Returns(dtos);
            mockedRepository.Setup(r => r.GetAnalysisTimestampsForSimulation(1)).Returns(new AnalysisTimestampDto[] { timestamp1, timestamp2, timestamp3 });

            //Act
            SimulationService service = new SimulationService(mockedRepository.Object);

            //Assert
            Simulation baseSimulation = service.GetSimulationByNameAssetTimeframe("a", 1, 1);
            Simulation comparedSimulation = service.GetSimulationByNameAssetTimeframe("a", 1, 1);

            bool areTheSame = (baseSimulation == comparedSimulation);
            Assert.IsTrue(areTheSame);

        }


        [TestMethod]
        public void GetSimulationById_TODO()
        {
            Assert.Fail("Not implemented yet");
        }


        #endregion GET_SIMULATIONS


    }
}
