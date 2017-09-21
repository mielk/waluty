using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.Utils;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Entities
{
    public class Simulation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private List<AnalysisTimestamp> analysisTimestamps = new List<AnalysisTimestamp>();
        private IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };



        #region SYSTEM.OBJECT


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Simulation)) return false;

            Simulation compared = (Simulation)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if (!areLastUpdatesEqual(compared)) return false;
            return true;

        }

        private bool areLastUpdatesEqual(Simulation simulation)
        {
            return analysisTimestamps.HasEqualItems(simulation.GetAnalysisTimestamps());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }


        #endregion SYSTEM.OBJECT




        #region DTO


        public static Simulation FromDto(SimulationDto dto)
        {
            return new Simulation()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public SimulationDto ToDto()
        {
            return new SimulationDto()
            {
                Id = this.Id,
                Name = this.Name
            };
        }


        #endregion DTO




        #region GETTERS


        public IEnumerable<AnalysisType> GetAnalysisTypes()
        {
            return analysisTypes;
        }


        #endregion GETTERS




        #region ANALYSIS_TIMESTAMPS


        public AnalysisTimestamp GetLastUpdate(int assetId, int timeframeId, AnalysisType type)
        {
            return analysisTimestamps.SingleOrDefault(a => a.AssetId == assetId && a.TimeframeId == timeframeId && a.AnalysisTypeId == (int)type);
        }

        public void AddLastUpdate(AnalysisTimestamp analysisTimestamp)
        {
            analysisTimestamps.RemoveAll(a => a.AssetId == analysisTimestamp.AssetId &&
                                              a.TimeframeId == analysisTimestamp.TimeframeId &&
                                              a.AnalysisTypeId == analysisTimestamp.AnalysisTypeId);
            analysisTimestamps.Add(analysisTimestamp);
        }

        public void AddLastUpdate(AnalysisTimestampDto analysisTimestampDto)
        {
            AnalysisTimestamp timestamp = AnalysisTimestamp.FromDto(analysisTimestampDto);
            AddLastUpdate(timestamp);
        }

        public IEnumerable<AnalysisTimestampDto> GetAnalysisTimestampDtos()
        {
            return analysisTimestamps.Select(a => a.ToDto());
        }
        
        public IEnumerable<AnalysisTimestamp> GetAnalysisTimestamps()
        {
            return analysisTimestamps.ToList();
        }


        #endregion ANALYSIS_TIMESTAMPS



    }

}