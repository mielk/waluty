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
using Stock.Domain.Enums;
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
        //TrendRange
        private const int DEFAULT_ID = 1;
        private const string DEFAULT_GUID = "74017f2d-9dfe-494e-bfa0-93c09418cfb7";
        private const int DEFAULT_TRENDLINE_ID = 1;
        private const int DEFAULT_RANGE_START_INDEX = 87;
        private int? DEFAULT_RANGE_END_INDEX = null;
        private const int DEFAULT_QUOTATIONS_COUNTER = 19;
        private const double DEFAULT_TOTAL_DISTANCE = 2.14;
        private const string DEFAULT_PREVIOUS_BREAK_GUID = null;
        private const string DEFAULT_PREVIOUS_HIT_GUID = "45e223ec-cd32-4eca-8d38-0d96d3ee121b";
        private const string DEFAULT_NEXT_BREAK_GUID = "a9139a25-6d38-4c05-bbc7-cc413d6feee9";
        private const string DEFAULT_NEXT_HIT_GUID = null;
        //TrendBreak
        private const int DEFAULT_INDEX_NUMBER = 85;
        private const string DEFAULT_PREVIOUS_RANGE_GUID = "45e223ec-cd32-4eca-8d38-0d96d3ee121b";
        private const string DEFAULT_NEXT_RANGE_GUID = "a9139a25-6d38-4c05-bbc7-cc413d6feee9";



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





        #region GET_TRENDHITS

        [TestMethod]
        public void GetTrendhits_ReturnsProperCollectionOfTrendhits()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendHitDto> dtos = new List<TrendHitDto>();
            TrendHitDto dto1 = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHitDto dto2 = new TrendHitDto() { Id = 2, Guid = "89BFF378-F310-4A28-B753-00A0FF9A852C", TrendlineId = 1, IndexNumber = 9, ExtremumType = 2, Value = 1.345, DistanceToLine = 0.0007, PreviousRangeGuid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", NextRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58" };
            TrendHitDto dto3 = new TrendHitDto() { Id = 3, Guid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", TrendlineId = 1, IndexNumber = 18, ExtremumType = 2, Value = 1.567, DistanceToLine = 0.0002, PreviousRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C", NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            dtos.AddRange(new TrendHitDto[] { dto1, dto2, dto3 });

            mockedRepository.Setup(r => r.GetTrendHits(1)).Returns(dtos);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualSimultations = service.GetTrendHits(1);

            //Assert
            List<TrendHit> expectedTrendhits = new List<TrendHit>();
            TrendHit trendhit1 = new TrendHit(1, 2, ExtremumType.PeakByClose) { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            TrendHit trendhit2 = new TrendHit(1, 9, ExtremumType.PeakByHigh) { Id = 2, Guid = "89BFF378-F310-4A28-B753-00A0FF9A852C", Value = 1.345, DistanceToLine = 0.0007, PreviousRangeGuid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", NextRangeGuid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58" };
            TrendHit trendhit3 = new TrendHit(1, 18, ExtremumType.PeakByHigh) { Id = 3, Guid = "A62DB207-FDDA-45B4-94F6-AE16F4CA9A58", Value = 1.567, DistanceToLine = 0.0002, PreviousRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C", NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            expectedTrendhits.AddRange(new TrendHit[] { trendhit1, trendhit2, trendhit3 });
            bool areEqual = expectedTrendhits.HasEqualItems(actualSimultations);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendhitById_ReturnsNull_IfThereIsNoItemWithGivenCombination()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendHitDto returnedDto = null;
            mockedRepository.Setup(r => r.GetTrendHitById(1)).Returns(returnedDto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendHit baseTrendHit = service.GetTrendHitById(1);
            Assert.IsNull(baseTrendHit);

        }

        [TestMethod]
        public void GetTrendhitById_ReturnsProperItem_IfItemExists()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendHitDto dto = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            mockedRepository.Setup(r => r.GetTrendHitById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualTrendhit = service.GetTrendHitById(1);

            //Assert
            TrendHit expectedTrendhit = new TrendHit(1, 2, ExtremumType.PeakByClose) { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            bool areEqual = expectedTrendhit.Equals(actualTrendhit);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendhitById_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendHitDto> dtos = new List<TrendHitDto>();
            TrendHitDto dto = new TrendHitDto() { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", TrendlineId = 1, IndexNumber = 2, ExtremumType = 1, Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            dtos.AddRange(new TrendHitDto[] { dto });
            mockedRepository.Setup(r => r.GetTrendHits(1)).Returns(dtos);
            mockedRepository.Setup(r => r.GetTrendHitById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendHit baseTrendHit = service.GetTrendHitById(1);
            TrendHit comparedTrendhit = service.GetTrendHitById(1);

            bool areTheSame = (baseTrendHit == comparedTrendhit);
            Assert.IsTrue(areTheSame);

        }

        #endregion GET_TRENDHITS


        #region UPDATE_TRENDHITS

        [TestMethod]
        public void UpdateTrendhits_AllItemsPassedToMethodArePassedToRepository()
        {

            //Arrange
            IEnumerable<TrendHitDto> trendhitDtos = null;
            TrendHit trendhit = new TrendHit(1, 2, ExtremumType.PeakByClose) { Id = 1, Guid = "AC180C9B-E6D2-4138-8E0A-BE31FCE8626D", Value = 1.234, DistanceToLine = 0.0004, PreviousRangeGuid = null, NextRangeGuid = "89BFF378-F310-4A28-B753-00A0FF9A852C" };
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            mockedRepository.
                Setup(r => r.UpdateTrendHits(It.IsAny<IEnumerable<TrendHitDto>>())).
                Callback<IEnumerable<TrendHitDto>>((col) => trendhitDtos = col).Verifiable();

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            service.UpdateTrendHit(trendhit);

            //Assert
            IEnumerable<TrendHitDto> expectedTrendhitDtos = new TrendHitDto[] { trendhit.ToDto() };
            mockedRepository.Verify(r => r.UpdateTrendHits(It.IsAny<IEnumerable<TrendHitDto>>()), Times.Exactly(1));
            Assert.IsTrue(trendhitDtos.HasEqualItems(expectedTrendhitDtos));

        }

        #endregion UPDATE_TRENDHITS




        #region GET_TREND_RANGES

        private TrendRange getDefaultTrendRange()
        {
            var trendRange = new TrendRange(DEFAULT_TRENDLINE_ID, DEFAULT_START_INDEX)
            {
                Id = DEFAULT_ID,
                EndIndex = DEFAULT_RANGE_END_INDEX,
                PreviousBreakGuid = DEFAULT_PREVIOUS_BREAK_GUID,
                PreviousHitGuid = DEFAULT_PREVIOUS_HIT_GUID,
                NextBreakGuid = DEFAULT_NEXT_BREAK_GUID,
                NextHitGuid = DEFAULT_NEXT_HIT_GUID,
                Value = DEFAULT_VALUE
            };
            return trendRange;
        }

        [TestMethod]
        public void GetTrendRanges_ReturnsProperCollectionOfTrendRanges()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendRangeDto> dtos = new List<TrendRangeDto>();
            TrendRange range1 = getDefaultTrendRange();
            TrendRange range2 = getDefaultTrendRange();
            TrendRange range3 = getDefaultTrendRange();
            TrendRangeDto dto1 = range1.ToDto();
            TrendRangeDto dto2 = range2.ToDto();
            TrendRangeDto dto3 = range3.ToDto();
            dtos.AddRange(new TrendRangeDto[] { dto1, dto2, dto3 });

            mockedRepository.Setup(r => r.GetTrendRanges(1)).Returns(dtos);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualSimultations = service.GetTrendRanges(1);

            //Assert
            IEnumerable<TrendRange> expectedTrendranges = new TrendRange[] { range1, range2, range3 };
            bool areEqual = expectedTrendranges.HasEqualItems(actualSimultations);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendRangeById_ReturnsNull_IfThereIsNoItemWithGivenCombination()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendRangeDto returnedDto = null;
            mockedRepository.Setup(r => r.GetTrendRangeById(1)).Returns(returnedDto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendRange baseTrendRange = service.GetTrendRangeById(1);
            Assert.IsNull(baseTrendRange);

        }

        [TestMethod]
        public void GetTrendRangeById_ReturnsProperItem_IfItemExists()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendRange range = getDefaultTrendRange();
            TrendRangeDto dto = range.ToDto();
            mockedRepository.Setup(r => r.GetTrendRangeById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualTrendRange = service.GetTrendRangeById(1);

            //Assert
            bool areEqual = range.Equals(actualTrendRange);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendRangeById_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendRangeDto> dtos = new List<TrendRangeDto>();
            TrendRange range = getDefaultTrendRange();
            TrendRangeDto dto = range.ToDto();
            dtos.AddRange(new TrendRangeDto[] { dto });
            mockedRepository.Setup(r => r.GetTrendRanges(1)).Returns(dtos);
            mockedRepository.Setup(r => r.GetTrendRangeById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendRange baseTrendRange = service.GetTrendRangeById(1);
            TrendRange comparedTrendRange = service.GetTrendRangeById(1);

            bool areTheSame = (baseTrendRange == comparedTrendRange);
            Assert.IsTrue(areTheSame);

        }

        #endregion GET_TREND_RANGES


        #region UPDATE_TREND_RANGES

        [TestMethod]
        public void UpdateTrendRanges_AllItemsPassedToMethodArePassedToRepository()
        {

            //Arrange
            List<TrendRangeDto> trendRangeDtos = new List<TrendRangeDto>();
            TrendRange trendRange = getDefaultTrendRange();
            TrendRange trendRange2 = getDefaultTrendRange();
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            mockedRepository.
                Setup(r => r.UpdateTrendRanges(It.IsAny<IEnumerable<TrendRangeDto>>())).
                Callback<IEnumerable<TrendRangeDto>>((col) => trendRangeDtos.AddRange(col)).Verifiable();

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            service.UpdateTrendRange(trendRange);
            service.UpdateTrendRange(trendRange2);

            //Assert
            IEnumerable<TrendRangeDto> expectedTrendRangeDtos = new TrendRangeDto[] { trendRange.ToDto(), trendRange2.ToDto() };
            mockedRepository.Verify(r => r.UpdateTrendRanges(It.IsAny<IEnumerable<TrendRangeDto>>()), Times.Exactly(2));
            Assert.IsTrue(trendRangeDtos.HasEqualItems(expectedTrendRangeDtos));

        }

        #endregion UPDATE_TREND_RANGES




        #region GET_TREND_BREAKS

        private TrendBreak getDefaultTrendBreak()
        {
            var trendBreak = new TrendBreak(DEFAULT_TRENDLINE_ID, DEFAULT_INDEX_NUMBER)
            {
                Id = DEFAULT_ID,
                TrendlineId = DEFAULT_TRENDLINE_ID,
                IndexNumber = DEFAULT_INDEX_NUMBER,
                PreviousRangeGuid = DEFAULT_PREVIOUS_RANGE_GUID,
                NextRangeGuid = DEFAULT_NEXT_RANGE_GUID
            };
            return trendBreak;
        }

        [TestMethod]
        public void GetTrendBreaks_ReturnsProperCollectionOfTrendBreaks()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendBreakDto> dtos = new List<TrendBreakDto>();
            TrendBreak break1 = getDefaultTrendBreak();
            TrendBreak break2 = getDefaultTrendBreak();
            TrendBreak break3 = getDefaultTrendBreak();
            TrendBreakDto dto1 = break1.ToDto();
            TrendBreakDto dto2 = break2.ToDto();
            TrendBreakDto dto3 = break3.ToDto();
            dtos.AddRange(new TrendBreakDto[] { dto1, dto2, dto3 });

            mockedRepository.Setup(r => r.GetTrendBreaks(1)).Returns(dtos);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualSimultations = service.GetTrendBreaks(1);

            //Assert
            IEnumerable<TrendBreak> expectedTrendBreaks = new TrendBreak[] { break1, break2, break3 };
            bool areEqual = expectedTrendBreaks.HasEqualItems(actualSimultations);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendBreakById_ReturnsNull_IfThereIsNoItemWithGivenCombination()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendBreakDto returnedDto = null;
            mockedRepository.Setup(r => r.GetTrendBreakById(1)).Returns(returnedDto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendBreak baseTrendRange = service.GetTrendBreakById(1);
            Assert.IsNull(baseTrendRange);

        }

        [TestMethod]
        public void GetTrendBreakById_ReturnsProperItem_IfItemExists()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            TrendBreak trendBreak = getDefaultTrendBreak();
            TrendBreakDto dto = trendBreak.ToDto();
            mockedRepository.Setup(r => r.GetTrendBreakById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            var actualTrendBreak = service.GetTrendBreakById(1);

            //Assert
            bool areEqual = trendBreak.Equals(actualTrendBreak);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void GetTrendBreakById_ReturnsAlwaysTheSameInstance()
        {

            //Arrange
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            List<TrendBreakDto> dtos = new List<TrendBreakDto>();
            TrendBreak trendBreak = getDefaultTrendBreak();
            TrendBreakDto dto = trendBreak.ToDto();
            dtos.AddRange(new TrendBreakDto[] { dto });
            mockedRepository.Setup(r => r.GetTrendBreaks(1)).Returns(dtos);
            mockedRepository.Setup(r => r.GetTrendBreakById(1)).Returns(dto);

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);

            //Assert
            TrendBreak baseTrendBreak = service.GetTrendBreakById(1);
            TrendBreak comparedTrendBreak = service.GetTrendBreakById(1);

            bool areTheSame = (baseTrendBreak == comparedTrendBreak);
            Assert.IsTrue(areTheSame);

        }

        #endregion GET_TREND_BREAKS


        #region UPDATE_TREND_BREAKS

        [TestMethod]
        public void UpdateTrendBreaks_AllItemsPassedToMethodArePassedToRepository()
        {

            //Arrange
            List<TrendBreakDto> trendBreakDtos = new List<TrendBreakDto>();
            TrendBreak trendBreak = getDefaultTrendBreak();
            TrendBreak trendBreak2 = getDefaultTrendBreak();
            Mock<ITrendlineRepository> mockedRepository = new Mock<ITrendlineRepository>();
            mockedRepository.
                Setup(r => r.UpdateTrendBreaks(It.IsAny<IEnumerable<TrendBreakDto>>())).
                Callback<IEnumerable<TrendBreakDto>>((col) => trendBreakDtos.AddRange(col)).Verifiable();

            //Act
            TrendlineService service = new TrendlineService(mockedRepository.Object);
            service.UpdateTrendBreak(trendBreak);
            service.UpdateTrendBreak(trendBreak2);

            //Assert
            IEnumerable<TrendBreakDto> expectedTrendBreakDtos = new TrendBreakDto[] { trendBreak.ToDto(), trendBreak2.ToDto() };
            mockedRepository.Verify(r => r.UpdateTrendBreaks(It.IsAny<IEnumerable<TrendBreakDto>>()), Times.Exactly(2));
            Assert.IsTrue(trendBreakDtos.HasEqualItems(expectedTrendBreakDtos));

        }

        #endregion UPDATE_TREND_BREAKS





    }

}
