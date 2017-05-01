using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class DataItem
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public TimeframeOld Timeframe { get; set; }
        public DateTime Date { get; set; }
        public Quotation Quotation { get; set; }
        public Price Price { get; set; }
        public Macd Macd { get; set; }
        public Adx Adx { get; set; }
        

        public int Index { get; set; }


        /* Covered with unit tests. */
        public double GetValue(bool isPeak, bool byClose)
        {

            if (byClose)
            {
                return Quotation.Close;
            }
            else
            {
                return isPeak ? Quotation.High: Quotation.Low;
            }

        }


        public int Distance(DataItem item)
        {
            return this.Index - item.Index;
        }

        public bool IsExtremum(TrendlineType type)
        {
            if (this.Price != null)
            {
                return this.Price.IsExtremum(type);
            }

            return false;

        }

        public double TrendlineAnalysisStep()
        {
            var sign = (Math.Max(Price.PeakByClose, Price.PeakByHigh) > 
                                                                Math.Max(Price.TroughByClose, Price.TroughByLow)) 
                                                                ? 1 : -1;
            return sign * (Quotation.High - Quotation.Close) / 10d;
        }

        public Extremum Extremum(ExtremumType type)
        {
            if (Price == null) return null;
            Extremum extremum = Price.GetExtremumObject(type);
            if (extremum == null || extremum.Cancelled) return null;
            return extremum;
        }

        /* Covered with unit tests. */
        public double GetOpenOrClosePrice(TrendlineType type)
        {
            return (type == TrendlineType.Support) ? Math.Min(Quotation.Open, Quotation.Close) : Math.Max(Quotation.Open, Quotation.Close);
        }

        /* Covered with unit tests. */
        public double GetHighOrLowPrice(TrendlineType type)
        {
            return (type == TrendlineType.Support) ? Quotation.Low : Quotation.High;
        }

        /* Covered with unit tests. */
        public double GetClosestDistance(TrendlineType type, double level)
        {

            double closeDistance = Math.Abs(GetOpenOrClosePrice(type) - level);
            double exDistance = Math.Abs(GetHighOrLowPrice(type) - level);
            return Math.Min(closeDistance, exDistance);

        }



        public static DataItem FromDto(DataItemDto dto)
        {
            var item = new DataItem();
            //item.AssetId = dto.AssetId;
            item.Asset = Asset.ById(dto.AssetId);
            item.Date = dto.PriceDate;
            item.Timeframe = TimeframeOld.GetTimeframeByShortName(dto.Timeframe);

            if (dto.QuotationId > 0)
            {
                item.Quotation = new Quotation
                {
                    AssetId = dto.AssetId,
                    Date = dto.PriceDate,
                    Open = dto.OpenPrice,
                    Low = dto.LowPrice,
                    High = dto.HighPrice,
                    Close = dto.ClosePrice,
                    Volume = dto.Volume ?? 0,
                    Id = dto.QuotationId
                };
            }


            if (dto.Price != null)
            {
                item.Price = Price.FromDto(dto.Price);
            }

            if (dto.Macd != null)
            {
                item.Macd = Macd.FromDto(dto.Macd);
            }

            if (dto.Adx != null)
            {
                item.Adx = Adx.FromDto(dto.Adx);
            }

            if (dto.Candlestick != null)
            {
                
            }

            return item;

        }


        public bool IsInRange(double value)
        {
            return value <= this.Quotation.High && value >= this.Quotation.Low;
        }

        public double GetMinDifference(double price)
        {
            return Math.Min(Math.Abs(price - Quotation.High), Math.Abs(price - Quotation.Low));
        }

        public double GetMaxDifference(double price)
        {
            return Math.Max(Math.Abs(price - Quotation.High), Math.Abs(price - Quotation.Low));
        }

        public double GetProperOpenOrClose()
        {

            if (Math.Max(Price.PeakByClose, Price.PeakByHigh) > Math.Max(Price.TroughByClose, Price.TroughByLow))
            {
                return Math.Max(Quotation.Open, Quotation.Close);
            }
            else
            {
                return Math.Min(Quotation.Open, Quotation.Close);
            }

        }

        public double GetProperHighOrLow()
        {

            if (Math.Max(Price.PeakByClose, Price.PeakByHigh) > Math.Max(Price.TroughByClose, Price.TroughByLow))
            {
                return Math.Max(Quotation.High, Quotation.Low);
            }
            else
            {
                return Math.Min(Quotation.High, Quotation.Low);
            }
        }

        public bool IsTrendlineBroken(double level, TrendlineType type)
        {

            if (type == TrendlineType.Resistance)
            {
                return Quotation.Close > level;
            }
            else if (type == TrendlineType.Support)
            {
                return Quotation.Close < level;
            }

            return false;

        }

    }
}
