using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Entities
{
    public class AnalysisInfo
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public double? MinLevel { get; set; }
        public double? MaxLevel { get; set; }
        public int Counter { get; set; }


        #region CONSTRUCTORS

        public static AnalysisInfo FromDto(AnalysisInfoDto dto)
        {
            var info = new AnalysisInfo();
            info.StartDate = dto.StartDate;
            info.EndDate = dto.EndDate;
            info.StartIndex = dto.StartIndex;
            info.EndIndex = dto.EndIndex;
            info.MinLevel = dto.MinLevel;
            info.MaxLevel = dto.MaxLevel;
            info.Counter = dto.Counter;
            return info;
        }

        public AnalysisInfoDto ToDto()
        {
            var dto = new AnalysisInfoDto
            {
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                StartIndex = this.StartIndex,
                EndIndex = this.EndIndex,
                MinLevel = this.MinLevel,
                MaxLevel = this.MaxLevel,
                Counter = this.Counter
            };
            return dto;
        }

        #endregion CONSTRUCTORS


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AnalysisInfo)) return false;

            AnalysisInfo compared = (AnalysisInfo)obj;
            if (!compared.StartDate.IsEqual(StartDate)) return false;
            if (!compared.EndDate.IsEqual(EndDate)) return false;
            if (!compared.MinLevel.IsEqual(MinLevel)) return false;
            if (!compared.MaxLevel.IsEqual(MaxLevel)) return false;
            if (compared.Counter != Counter) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.EndIndex != EndIndex) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion SYSTEM.OBJECT


    }
}
