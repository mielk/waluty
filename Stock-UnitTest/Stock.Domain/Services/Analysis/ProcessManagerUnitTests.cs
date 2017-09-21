using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Stock.Domain.Services;
using Stock.Domain.Entities;
using Stock.Core;

namespace Stock_UnitTest.Stock.Domain.Services.Analysis
{
    [TestClass]
    public class ProcessManagerUnitTests
    {


        #region HELPER_METHODS

        #endregion HELPER_METHODS


        #region GET_ANALYSIS_LAST_UPDATED_INDEX

        [TestMethod]
        public void GetAnalysisLastUpdatedIndex_ReturnsNull_IfThereIsNoEntryForThisAnalysisType()
        {

            //Arrange
            Mock<IAnalysisTimestampService> mockedTimestampService = new Mock<IAnalysisTimestampService>();
            Dictionary<AnalysisType, int?> indexes = new Dictionary<AnalysisType, int?>();
            mockedTimestampService.Setup(s => s.GetLastAnalyzedIndexes(It.IsAny<int>())).Returns(indexes);
            
            //Act
            ProcessManager manager = new ProcessManager();
            manager.InjectTimestampService(mockedTimestampService.Object);

            //Assert
            int? result = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations);
            Assert.IsNull(result);

        }

        [TestMethod]
        public void GetAnalysisLastUpdatedIndex_ReturnsNull_IfNullIsAssignedToThisAnalysisType()
        {

            //Arrange
            Mock<IAnalysisTimestampService> mockedTimestampService = new Mock<IAnalysisTimestampService>();
            Dictionary<AnalysisType, int?> indexes = new Dictionary<AnalysisType, int?>();
            indexes.Add(AnalysisType.Prices, null);
            mockedTimestampService.Setup(s => s.GetLastAnalyzedIndexes(It.IsAny<int>())).Returns(indexes);

            //Act
            ProcessManager manager = new ProcessManager();
            manager.InjectTimestampService(mockedTimestampService.Object);

            //Assert
            int? result = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Prices);
            Assert.IsNull(result);

        }

        [TestMethod]
        public void GetAnalysisLastUpdatedIndex_ReturnsPropertValue_IfNotNullIsAssignedToThisAnalysisType()
        {

            int? expected = 5;

            //Arrange
            Mock<IAnalysisTimestampService> mockedTimestampService = new Mock<IAnalysisTimestampService>();
            Dictionary<AnalysisType, int?> indexes = new Dictionary<AnalysisType, int?>();
            indexes.Add(AnalysisType.Prices, expected);
            mockedTimestampService.Setup(s => s.GetLastAnalyzedIndexes(It.IsAny<int>())).Returns(indexes);

            //Act
            ProcessManager manager = new ProcessManager();
            manager.InjectTimestampService(mockedTimestampService.Object);

            //Assert
            int? result = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Prices);
            Assert.AreEqual(expected, result);

        }

        #endregion GET_ANALYSIS_LAST_UPDATED_INDEX


    }

}
