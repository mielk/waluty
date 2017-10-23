using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public class Quotation : IDataUnit
    {
        private const AnalysisType analysisType = AnalysisType.Quotations;
        public int Id { get; set; }
        public DataSet DataSet { get; set; }
        // [Data]
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        // [Status]
        public bool Updated { get; set; }
        public bool New { get; set; }



        #region CONSTRUCTOR

        public Quotation(DataSet ds)
        {
            this.DataSet = ds;
            ds.SetQuotation(this);
        }

        #endregion CONSTRUCTOR



        #region DTO

        public static Quotation FromDto(DataSet ds, QuotationDto dto)
        {

            var quotation = new Quotation(ds);
            quotation.Id = dto.QuotationId;
            quotation.Open = dto.OpenPrice;
            quotation.High = dto.HighPrice;
            quotation.Low = dto.LowPrice;
            quotation.Close = dto.ClosePrice;
            quotation.Volume = dto.Volume ?? 0;
            return quotation;
        }

        public QuotationDto ToDto()
        {
            var dto = new QuotationDto
            {
                QuotationId = this.Id,
                AssetId = DataSet.AssetId,
                IndexNumber = DataSet.IndexNumber,
                TimeframeId = DataSet.TimeframeId,
                PriceDate = DataSet.Date,
                OpenPrice = this.Open,
                HighPrice = this.High,
                LowPrice = this.Low,
                ClosePrice = this.Close,
                Volume = this.Volume
            };

            return dto;

        }

        #endregion DTO



        #region GETTERS

        public bool IsUpdated()
        {
            return Updated;
        }

        public bool IsNew()
        {
            return New;
        }

        public DateTime GetDate()
        {
            return DataSet.Date;
        }

        public int GetIndexNumber()
        {
            return DataSet.IndexNumber;
        }

        public int GetAssetId()
        {
            return DataSet.AssetId;
        }

        public int GetTimeframeId()
        {
            return DataSet.TimeframeId;
        }

        public AnalysisType GetAnalysisType()
        {
            return analysisType;
        }

        public object GetJson()
        {
            return new
            {
                id = Id,
                assetId = GetAssetId(),
                timeframeId = GetTimeframeId(),
                date = GetDate(),
                indexNumber = GetIndexNumber(),
                analysisType = (int)analysisType,
                open = Open,
                high = High,
                low = Low,
                close = Close,
                volume = Volume,
                isUpdated = Updated,
                isNew = New
            };
        }

        #endregion GETTERS

        

        #region EXTREMA

        public double GetProperValue(ExtremumType type)
        {
            switch (type)
            {
                case ExtremumType.PeakByClose:
                    return Close;
                case ExtremumType.TroughByClose:
                    return Close;
                case ExtremumType.PeakByHigh:
                    return High;
                case ExtremumType.TroughByLow:
                    return Low;
                default:
                    return Close;
            }
        }

        public double GetOppositeValue(ExtremumType type)
        {
            switch (type)
            {
                case ExtremumType.PeakByClose:
                    return Low;
                case ExtremumType.TroughByClose:
                    return High;
                case ExtremumType.PeakByHigh:
                    return Low;
                case ExtremumType.TroughByLow:
                    return High;
                default:
                    return Close;
            }
        }

        #endregion EXTREMA



        #region CALCULATIONS

        public double GetProperValue(TrendlineType type)
        {
            if (type == TrendlineType.Resistance)
            {
                return High;
            }
            else
            {
                return Low;
            }
        }

        public double GetVolatility()
        {
            return (High - Low) / Open;
        }

        #endregion CALCULATIONS



        #region OBJECT METHODS

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Quotation)) return false;

            Quotation compared = (Quotation)obj;
            if ((compared.Id) != Id) return false;
            if ((compared.GetIndexNumber()) != GetIndexNumber()) return false;
            if (compared.GetDate().CompareTo(GetDate()) != 0) return false;
            if ((compared.GetAssetId()) != GetAssetId()) return false;
            if ((compared.GetTimeframeId()) != GetTimeframeId()) return false;
            if (!compared.Open.IsEqual(Open)) return false;
            if (!compared.High.IsEqual(High)) return false;
            if (!compared.Low.IsEqual(Low)) return false;
            if (!compared.Close.IsEqual(Close)) return false;
            if (!((double)compared.Volume).IsEqual((double)Volume)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return GetDate().ToString() + " | " + GetTimeframeId() + " | " + GetAssetId();
        }

        #endregion OBJECT METHODS


    }
}
