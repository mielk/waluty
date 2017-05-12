using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.DAL.Repositories
{
    public class EFSimulationRepository : ISimulationRepository
    {

        public void UpdateAnalysisTimestamps(IEnumerable<AnalysisTimestampDto> timestamps)
        {

            using (var db = new SimulationContext())
            {

                foreach (AnalysisTimestampDto dto in timestamps)
                {
                    var record = db.AnalysisTimestamps.SingleOrDefault(s => s.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.AnalysisTimestamps.Add(dto);
                    }
                }
                db.SaveChanges();

            }
        }

        public IEnumerable<AnalysisTimestampDto> GetAnalysisTimestamps()
        {
            IEnumerable<AnalysisTimestampDto> timestamps;
            using (var context = new SimulationContext())
            {
                timestamps = context.AnalysisTimestamps.ToList();
            }
            return timestamps;
        }

        public IEnumerable<AnalysisTimestampDto> GetAnalysisTimestampsForSimulation(int simulationId)
        {
            IEnumerable<AnalysisTimestampDto> timestamps;
            using (var context = new SimulationContext())
            {
                timestamps = context.AnalysisTimestamps.Where(a => a.SimulationId == simulationId).ToList();
            }
            return timestamps;
        }

        public AnalysisTimestampDto GetAnalysisTimestamp(int simulationId, AnalysisType analysisType)
        {
            return GetAnalysisTimestamp(simulationId, (int)analysisType);
        }

        public AnalysisTimestampDto GetAnalysisTimestamp(int simulationId, int analysisTypeId)
        {
            using (var context = new SimulationContext())
            {
                return context.AnalysisTimestamps.SingleOrDefault(a => a.SimulationId == simulationId && a.AnalysisTypeId == analysisTypeId);
            }
        }

        public SimulationDto GetSimulationByNameAssetTimeframe(string name, int assetId, int timeframeId)
        {
            SimulationDto dto;
            using (var context = new SimulationContext())
            {

                dto = context.Simulations.SingleOrDefault(c => c.Name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase) && c.AssetId == assetId && c.TimeframeId == timeframeId);
            }
            return dto;
        }

        public IEnumerable<SimulationDto> GetSimulations()
        {
            IEnumerable<SimulationDto> simulations;
            using (var context = new SimulationContext())
            {
                simulations = context.Simulations.ToList();
            }
            return simulations;
        }

        public void UpdateSimulations(IEnumerable<SimulationDto> simulations)
        {

            using (var db = new SimulationContext())
            {

                foreach (SimulationDto dto in simulations)
                {
                    var record = db.Simulations.SingleOrDefault(s => s.Id == dto.Id);
                    if (record != null)
                    {
                        record.CopyProperties(dto);
                    }
                    else
                    {
                        db.Simulations.Add(dto);
                    }
                }
                db.SaveChanges();

            }

        }

    }
}
