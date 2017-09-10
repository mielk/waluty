using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class AnalysisTimestampDto
    {

        [Key]
        public int Id { get; set; }
        public int SimulationId { get; set; }
        public int AnalysisTypeId { get; set; }
        [Column("LastDate")]
        public DateTime? LastAnalysedItem { get; set; }
        [Column("LastIndex")]
        public int? LastAnalysedIndex { get; set; }


        public void CopyProperties(AnalysisTimestampDto dto)
        {
            Id = dto.Id;
            SimulationId = dto.SimulationId;
            AnalysisTypeId = dto.AnalysisTypeId;
            LastAnalysedItem = dto.LastAnalysedItem;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AnalysisTimestampDto)) return false;

            AnalysisTimestampDto compared = (AnalysisTimestampDto)obj;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.AnalysisTypeId) != AnalysisTypeId) return false;
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
            return SimulationId + " | " + AnalysisTypeId;
        }

    }

}