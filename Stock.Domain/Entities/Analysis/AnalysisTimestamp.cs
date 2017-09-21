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
    public class AnalysisTimestamp
    {
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int AnalysisTypeId { get; set; }
        public DateTime? LastAnalysedItem { get; set; }
        public int? LastAnalysedIndex { get; set; }



        #region SYSTEM.OBJECT


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AnalysisTimestamp)) return false;

            AnalysisTimestamp compared = (AnalysisTimestamp)obj;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.AnalysisTypeId) != AnalysisTypeId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((LastAnalysedItem == null && compared.LastAnalysedItem != null) || (LastAnalysedItem != null && ((DateTime)LastAnalysedItem).CompareTo(compared.LastAnalysedItem) != 0)) return false;
            if ((LastAnalysedIndex == null && compared.LastAnalysedIndex != null) || (LastAnalysedIndex != null && ((int)LastAnalysedIndex).CompareTo(compared.LastAnalysedIndex) != 0)) return false;

            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Id + "|" + SimulationId + "|" + AssetId + "|" + TimeframeId + "|" + AnalysisTypeId + ": " + (LastAnalysedItem == null ? "null" : LastAnalysedItem.ToString());
        }


        #endregion SYSTEM.OBJECT



        #region DTO

        public static AnalysisTimestamp FromDto(AnalysisTimestampDto dto)
        {
            return new AnalysisTimestamp()
            {
                Id = dto.Id,
                SimulationId = dto.SimulationId,
                AssetId = dto.AssetId,
                TimeframeId = dto.TimeframeId,
                AnalysisTypeId = dto.AnalysisTypeId,
                LastAnalysedIndex = dto.LastAnalysedIndex,
                LastAnalysedItem = dto.LastAnalysedItem
            };
        }

        public AnalysisTimestampDto ToDto()
        {
            return new AnalysisTimestampDto()
            {
                Id = this.Id,
                SimulationId = this.SimulationId,
                AssetId = this.AssetId,
                TimeframeId = this.TimeframeId,
                AnalysisTypeId = this.AnalysisTypeId,
                LastAnalysedIndex = this.LastAnalysedIndex,
                LastAnalysedItem = this.LastAnalysedItem
            };
        }

        #endregion DTO


    }

}