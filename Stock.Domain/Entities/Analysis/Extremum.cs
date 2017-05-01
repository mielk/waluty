using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services;

namespace Stock.Domain.Entities
{
    public class Extremum
    {
        private const int MinLaterCounter = 3;
        public int ExtremumId { get; set; }
        public AssetTimeframe assetTimeframe { get; set; }
        public string Symbol { get; set; }
        public ExtremumType Type { get; set; }
        public DateTime PriceDate { get; set; }
        public int EarlierCounter { get; set; }
        public double? EarlierAmplitude { get; set; }
        public double? EarlierChange1 { get; set; }
        public double? EarlierChange2 { get; set; }
        public double? EarlierChange3 { get; set; }
        public double? EarlierChange5 { get; set; }
        public double? EarlierChange10 { get; set; }
        public int LaterCounter { get; set; }
        public double? LaterAmplitude { get; set; }
        public double? LaterChange1 { get; set; }
        public double? LaterChange2 { get; set; }
        public double? LaterChange3 { get; set; }
        public double? LaterChange5 { get; set; }
        public double? LaterChange10 { get; set; }
        public double Volatility { get; set; }
        public bool IsOpen { get; set; }
        public bool Cancelled { get; set; }

        private Extremum(){}

        public Extremum(DateTime date, string symbol, bool isPeak, bool byClose)
        {
            this.PriceDate = date;
            this.Symbol = symbol;
            this.Type = Extrema.GetExtremumType(isPeak, byClose);

        }

        public Extremum(DateTime date, AssetTimeframe atf, bool isPeak, bool byClose)
        {
            this.PriceDate = date;
            this.assetTimeframe = atf;
            this.Type = Extrema.GetExtremumType(isPeak, byClose);

        }

        public bool IsConfirmed()
        {
            return LaterCounter > MinLaterCounter;
        }

        public double Evaluate()
        {

            //Timeframe
            var timeframeSymbol = assetTimeframe.GetTimeframe().GetName();// Symbol.GetTimeframeSymbol();
            var timeframeFactor = 1;// timeframeSymbol.GetExtremumEvaluationFactor();

                //public static double GetExtremumEvaluationFactor(this TimeframeSymbol value)
                //{
                //    switch (value)
                //    {
                //        case TimeframeSymbol.M5: return 30d;
                //        case TimeframeSymbol.M15: return 24d;
                //        case TimeframeSymbol.M30: return 12d;
                //        case TimeframeSymbol.H1: return 12d;
                //        case TimeframeSymbol.H4: return 6d;
                //        case TimeframeSymbol.D1: return 2d;
                //        case TimeframeSymbol.W1: return 1d;
                //        case TimeframeSymbol.MN1: return 0.5d;
                //    }

                //    return 1d;

                //}

            if (LaterCounter < MinLaterCounter) return 0d;

            //Range
            var maxRange = (double)Extrema.MaxRange;
            var leftSerie = (double)EarlierCounter / maxRange;
            var rightSerie = (double)LaterCounter / maxRange;
            var rangePoints = Math.Sqrt(leftSerie * rightSerie);

            //Amplitude
            var lowerAmplitude = Math.Min( EarlierAmplitude == null ? 0d : (double) EarlierAmplitude,
                                           LaterAmplitude == null ? (double)EarlierAmplitude : (double)LaterAmplitude) * timeframeFactor;
            var lowerAmplitudePower = Math.Pow(lowerAmplitude, 0.25d);
            var piAmplitude = Math.PI * (lowerAmplitudePower - 0.5);
            var sinPiAmp = Math.Sin(piAmplitude);
            var amplitudePoints = 2 * Math.Pow(sinPiAmp / 2 + 0.5, 2);

            //Volatility
            var volatilityPower = Math.Pow(Volatility, 0.25d);
            var piVolatility = Math.PI * (volatilityPower - 0.5);
            var sinPiVolatility = Math.Sin(piVolatility);
            var volatilityPoints = 2 * Math.Pow(sinPiVolatility / 2 + 0.5, 2);


            //Average
            var avg = (rangePoints + amplitudePoints + volatilityPoints) / 3;

            return avg * 100;

        }

        public static Extremum FromDto(ExtremumDto dto)
        {
            var extremum = new Extremum();
            extremum.ExtremumId = dto.ExtremumId;
            extremum.Symbol = dto.Symbol;
            extremum.Type = (ExtremumType)dto.Type;
            extremum.PriceDate = dto.PriceDate;
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
            extremum.Cancelled = dto.Cancelled;

            return extremum;

        }

        public ExtremumDto ToDto()
        {
            var dto = new ExtremumDto
            {

                ExtremumId = this.ExtremumId,
                Symbol = this.Symbol,
                Type = (int)this.Type,
                PriceDate = this.PriceDate,
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
                IsOpen = this.IsOpen,
                Cancelled = this.Cancelled

            };

            return dto;

        }

    }

}