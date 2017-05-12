﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;
using Stock.Utils;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public class Extremum : IDataUnit
    {
        private const AnalysisType analysisType = AnalysisType.Prices;
        public int ExtremumId { get; set; }
        public int SimulationId { get; set; }
        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public ExtremumType Type { get; set; }
        public DateTime Date { get; set; }
        public int IndexNumber { get; set; }
        public double Value { get; set; }
        public DateTime LastCheckedDateTime { get; set; }
        public int EarlierCounter { get; set; }
        public double EarlierAmplitude { get; set; }
        public double EarlierChange1 { get; set; }
        public double EarlierChange2 { get; set; }
        public double EarlierChange3 { get; set; }
        public double EarlierChange5 { get; set; }
        public double EarlierChange10 { get; set; }
        public int LaterCounter { get; set; }
        public double LaterAmplitude { get; set; }
        public double LaterChange1 { get; set; }
        public double LaterChange2 { get; set; }
        public double LaterChange3 { get; set; }
        public double LaterChange5 { get; set; }
        public double LaterChange10 { get; set; }
        public double Volatility { get; set; }
        public bool IsOpen { get; set; }


        #region CONSTRUCTORS

        public Extremum(int assetId, int timeframeId, ExtremumType type, DateTime date)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.Type = type;
            this.Date = date;
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public DateTime GetDate()
        {
            return Date;
        }

        public int GetIndexNumber()
        {
            return IndexNumber;
        }

        public int GetAssetId()
        {
            return AssetId;
        }

        public int GetTimeframeId()
        {
            return TimeframeId;
        }

        public AnalysisType GetAnalysisType()
        {
            return AnalysisType.DataSet;
        }

        #endregion GETTERS


        #region FROM/TO DTO

        public static Extremum FromDto(ExtremumDto dto)
        {
            var extremum = new Extremum(dto.AssetId, dto.TimeframeId, (ExtremumType)dto.ExtremumType, dto.Date);
            extremum.SimulationId = dto.SimulationId;
            extremum.IndexNumber = dto.IndexNumber;
            extremum.ExtremumId = dto.Id;
            extremum.EarlierCounter = dto.EarlierCounter;
            extremum.EarlierAmplitude = dto.EarlierAmplitude;
            extremum.EarlierChange1 = dto.EarlierChange1;
            extremum.EarlierChange2 = dto.EarlierChange2;
            extremum.EarlierChange3 = dto.EarlierChange3;
            extremum.EarlierChange5 = dto.EarlierChange5;
            extremum.EarlierChange10 = dto.EarlierChange10;
            extremum.LaterCounter = dto.LaterCounter;
            extremum.LaterAmplitude = dto.LaterAmplitude;
            extremum.LaterChange1 = dto.LaterChange1;
            extremum.LaterChange2 = dto.LaterChange2;
            extremum.LaterChange3 = dto.LaterChange3;
            extremum.LaterChange5 = dto.LaterChange5;
            extremum.LaterChange10 = dto.LaterChange10;
            extremum.Volatility = dto.Volatility;
            extremum.IsOpen = dto.IsOpen;
            extremum.Value = dto.Value;
            extremum.LastCheckedDateTime = dto.LastCheckedDateTime;
            return extremum;
        }

        public ExtremumDto ToDto()
        {
            var dto = new ExtremumDto()
            {
                Id = this.ExtremumId,
                AssetId = this.AssetId,
                TimeframeId = this.TimeframeId,
                SimulationId = this.SimulationId,
                Date = this.Date,
                Value = this.Value,
                ExtremumType = (int)this.Type,
                IndexNumber = this.IndexNumber,
                EarlierCounter = this.EarlierCounter,
                EarlierAmplitude = this.EarlierAmplitude,
                EarlierChange1 = this.EarlierChange1,
                EarlierChange2 = this.EarlierChange2,
                EarlierChange3 = this.EarlierChange3,
                EarlierChange5 = this.EarlierChange5,
                EarlierChange10 = this.EarlierChange10,
                LaterCounter = this.LaterCounter,
                LaterAmplitude = this.LaterAmplitude,
                LaterChange1 = this.LaterChange1,
                LaterChange2 = this.LaterChange2,
                LaterChange3 = this.LaterChange3,
                LaterChange5 = this.LaterChange5,
                LaterChange10 = this.LaterChange10,
                Volatility = this.Volatility,
                IsOpen = this.IsOpen
            };

            return dto;

        }

        #endregion FROM/TO DTO



        //public double Evaluate()
        //{

        //    //Timeframe
        //    var timeframeSymbol = assetTimeframe.GetTimeframe().GetName();// Symbol.GetTimeframeSymbol();
        //    var timeframeFactor = 1;// timeframeSymbol.GetExtremumEvaluationFactor();

        //        //public static double GetExtremumEvaluationFactor(this TimeframeSymbol value)
        //        //{
        //        //    switch (value)
        //        //    {
        //        //        case TimeframeSymbol.M5: return 30d;
        //        //        case TimeframeSymbol.M15: return 24d;
        //        //        case TimeframeSymbol.M30: return 12d;
        //        //        case TimeframeSymbol.H1: return 12d;
        //        //        case TimeframeSymbol.H4: return 6d;
        //        //        case TimeframeSymbol.D1: return 2d;
        //        //        case TimeframeSymbol.W1: return 1d;
        //        //        case TimeframeSymbol.MN1: return 0.5d;
        //        //    }

        //        //    return 1d;

        //        //}

        //    if (LaterCounter < MinLaterCounter) return 0d;

        //    //Range
        //    var maxRange = (double)Extrema.MaxRange;
        //    var leftSerie = (double)EarlierCounter / maxRange;
        //    var rightSerie = (double)LaterCounter / maxRange;
        //    var rangePoints = Math.Sqrt(leftSerie * rightSerie);

        //    //Amplitude
        //    var lowerAmplitude = Math.Min( EarlierAmplitude == null ? 0d : (double) EarlierAmplitude,
        //                                   LaterAmplitude == null ? (double)EarlierAmplitude : (double)LaterAmplitude) * timeframeFactor;
        //    var lowerAmplitudePower = Math.Pow(lowerAmplitude, 0.25d);
        //    var piAmplitude = Math.PI * (lowerAmplitudePower - 0.5);
        //    var sinPiAmp = Math.Sin(piAmplitude);
        //    var amplitudePoints = 2 * Math.Pow(sinPiAmp / 2 + 0.5, 2);

        //    //Volatility
        //    var volatilityPower = Math.Pow(Volatility, 0.25d);
        //    var piVolatility = Math.PI * (volatilityPower - 0.5);
        //    var sinPiVolatility = Math.Sin(piVolatility);
        //    var volatilityPoints = 2 * Math.Pow(sinPiVolatility / 2 + 0.5, 2);


        //    //Average
        //    var avg = (rangePoints + amplitudePoints + volatilityPoints) / 3;

        //    return avg * 100;

        //}

        

        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            const double MAX_VALUE_DIFFERENCE = 0.0001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(Extremum)) return false;

            Extremum compared = (Extremum)obj;
            if ((compared.SimulationId) != SimulationId) return false;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.Date.CompareTo(Date) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if ((compared.Type) != this.Type) return false;
            if (!compared.Volatility.CompareForTest(Volatility, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.EarlierCounter) != EarlierCounter) return false;
            if (!compared.EarlierAmplitude.CompareForTest(EarlierAmplitude, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange1.CompareForTest(EarlierChange1, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange2.CompareForTest(EarlierChange2, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange3.CompareForTest(EarlierChange3, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange5.CompareForTest(EarlierChange5, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.EarlierChange10.CompareForTest(EarlierChange10, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.LaterCounter) != LaterCounter) return false;
            if (!compared.LaterAmplitude.CompareForTest(LaterAmplitude, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange1.CompareForTest(LaterChange1, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange2.CompareForTest(LaterChange2, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange3.CompareForTest(LaterChange3, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange5.CompareForTest(LaterChange5, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.LaterChange10.CompareForTest(LaterChange10, MAX_VALUE_DIFFERENCE)) return false;
            if (!compared.Value.CompareForTest(Value, MAX_VALUE_DIFFERENCE)) return false;
            if ((compared.IsOpen) != IsOpen) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Date.ToString() + " | " + TimeframeId + " | " + AssetId;
        }

        #endregion SYSTEM.OBJECT


    }

}