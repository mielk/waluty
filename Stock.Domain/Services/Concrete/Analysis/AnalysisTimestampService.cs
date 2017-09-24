using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{
    public class AnalysisTimestampService : IAnalysisTimestampService
    {

        private ISimulationRepository repository;

        public AnalysisTimestampService(ISimulationRepository repository)
        {
            this.repository = repository;
        }

        public Dictionary<AnalysisType, int?> GetLastAnalyzedIndexes(int assetId, int timeframeId, int simulationId)
        {
            Dictionary<AnalysisType, int?> result = new Dictionary<AnalysisType, int?>();
            IEnumerable<AnalysisTimestampDto> dtos = repository.GetAnalysisTimestampsForSimulation(simulationId);
            foreach (var dto in dtos)
            {
                if (dto.AssetId == assetId && dto.TimeframeId == timeframeId)
                {
                    result.Add((AnalysisType)dto.AnalysisTypeId, dto.LastAnalysedIndex);
                }
            }
            return result;
        }

    }

}
