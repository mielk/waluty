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
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        private Dictionary<AnalysisType, DateTime?> lastUpdates = new Dictionary<AnalysisType, DateTime?>();


        public void AddLastUpdate(AnalysisType type, DateTime? datetime)
        {
            if (lastUpdates.ContainsKey(type))
            {
                lastUpdates.Remove(type);
            }
            lastUpdates.Add(type, datetime);
        }

        public DateTime? GetLastUpdate(AnalysisType type)
        {
            DateTime? date = null;
            if (lastUpdates.ContainsKey(type))
            {
                lastUpdates.TryGetValue(type, out date);
            }
            return date;
        }

        public IEnumerable<AnalysisType> GetAnalysisTypes()
        {
            return lastUpdates.Keys;
        }

        public static Simulation FromDto(SimulationDto dto)
        {
            return new Simulation()
            {
                Id = dto.Id,
                Name = dto.Name,
                AssetId = dto.AssetId,
                TimeframeId = dto.TimeframeId
            };
        }

        public SimulationDto ToDto()
        {
            return new SimulationDto()
            {
                Id = this.Id,
                Name = this.Name,
                AssetId = this.AssetId,
                TimeframeId = this.TimeframeId
            };
        }

        public IEnumerable<AnalysisTimestampDto> GetAnalysisTimestampDtos()
        {
            List<AnalysisTimestampDto> dtos = new List<AnalysisTimestampDto>();
            IEnumerable<AnalysisType> types = GetAnalysisTypes();
            foreach (var type in types)
            {
                DateTime? datetime = GetLastUpdate(type);
                AnalysisTimestampDto dto = new AnalysisTimestampDto()
                {
                    AnalysisTypeId = (int) type,
                    LastAnalysedItem = datetime,
                    SimulationId = this.Id
                };
                dtos.Add(dto);
            }
            return dtos;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Simulation)) return false;

            Simulation compared = (Simulation)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if (!areLastUpdatesEqual(compared)) return false;
            return true;

        }

        private bool areLastUpdatesEqual(Simulation simulation)
        {
            if (GetAnalysisTypes().HasEqualItems(simulation.GetAnalysisTypes()))
            {
                foreach (var key in lastUpdates.Keys)
                {
                    DateTime? date;
                    DateTime? comparedDate = simulation.GetLastUpdate(key);
                    lastUpdates.TryGetValue(key, out date);

                    if (!date.IsEqual(comparedDate))
                    {
                        return false;
                    }

                }

                return true;

            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name + " | " + AssetId + " | " + TimeframeId;
        }

    }

}
