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
    public class Trendline
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int SimulationId { get; set; }
        public int StartIndex { get; set; }
        public double StartLevel { get; set; }
        public int EndIndex { get; set; }
        public double EndLevel { get; set; }
        public double Value { get; set; }
        public int LastUpdateIndex { get; set; }



        #region CONSTRUCTOR

        public Trendline(int assetId, int timeframeId, int simulationId, int startIndex, double startLevel, int endIndex, double endLevel)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.SimulationId = simulationId;
            this.StartIndex = startIndex;
            this.StartLevel = startLevel;
            this.EndIndex = endIndex;
            this.EndLevel = endLevel;
        }

        #endregion CONSTRUCTOR


        #region SYSTEM.OBJECT


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Trendline)) return false;

            Trendline compared = (Trendline)obj;
            if (compared.Id != Id) return false;
            if (compared.TimeframeId != TimeframeId) return false;
            if (compared.SimulationId != SimulationId) return false;
            if (compared.AssetId != AssetId) return false;
            if (compared.StartIndex != StartIndex) return false;
            if (compared.StartLevel != StartLevel) return false;
            if (compared.EndIndex != EndIndex) return false;
            if (compared.EndLevel != EndLevel) return false;
            if (compared.Value != Value) return false;
            if (compared.LastUpdateIndex != LastUpdateIndex) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Id + "): " + StartIndex.ToString() + " - " + EndIndex.ToString();
        }


        #endregion SYSTEM.OBJECT




        #region DTO


        public static Trendline FromDto(TrendlineDto dto)
        {
            return new Trendline(dto.AssetId, dto.TimeframeId, dto.SimulationId, dto.StartIndex, dto.StartLevel, dto.EndIndex, dto.EndLevel)
            {
                Id = dto.Id,
                Value = dto.Value,
                LastUpdateIndex = dto.LastUpdateIndex
            };
        }

        public TrendlineDto ToDto()
        {
            return new TrendlineDto()
            {
                Id = this.Id,
                AssetId = this.AssetId,
                TimeframeId = this.TimeframeId, 
                SimulationId = this.SimulationId, 
                StartIndex = this.StartIndex, 
                StartLevel = this.StartLevel, 
                EndIndex = this.EndIndex, 
                EndLevel = this.EndLevel,
                Value = this.Value,
                LastUpdateIndex = this.LastUpdateIndex
            };
        }


        #endregion DTO




        #region GETTERS

        
        #endregion GETTERS



    }

}