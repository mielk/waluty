using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public interface ISimulationRepository
    {
        void UpdateAnalysisTimestamps(IEnumerable<AnalysisTimestampDto> timestamps);
        IEnumerable<AnalysisTimestampDto> GetAnalysisTimestamps();
        IEnumerable<AnalysisTimestampDto> GetAnalysisTimestampsForSimulation(int simulationId);
        AnalysisTimestampDto GetAnalysisTimestamp(int simulationId, int assetId, int timeframeId, AnalysisType analysisType);
        AnalysisTimestampDto GetAnalysisTimestamp(int simulationId, int assetId, int timeframeId, int analysisTypeId);
        void UpdateSimulations(IEnumerable<SimulationDto> simulations);
        IEnumerable<SimulationDto> GetSimulations();
        SimulationDto GetSimulationById(int id);
    }
}