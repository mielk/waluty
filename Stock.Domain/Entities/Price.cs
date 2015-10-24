using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public double CloseDelta { get; set; }
        public int Direction2D { get; set; }
        public int Direction3D { get; set; }
        public double PeakByClose { get; set; }
        public double PeakByHigh { get; set; }
        public double TroughByClose { get; set; }
        public double TroughByLow { get; set; }
        public double PriceGap { get; set; }


        public static Price FromDto(PriceDto dto)
        {

            var price = new Price();
            price.Id = dto.Id;
            price.AssetId = dto.AssetId;
            price.Date = dto.ItemDate;
            price.CloseDelta = dto.DeltaClosePrice;
            price.Direction2D = dto.PriceDirection2D;
            price.Direction3D = dto.PriceDirection3D;
            price.PeakByClose = dto.PeakByCloseEvaluation;
            price.PeakByHigh = dto.PeakByHighEvaluation;
            price.TroughByClose = dto.TroughByCloseEvaluation;
            price.TroughByLow = dto.TroughByLowEvaluation;

            return price;

        }

        public PriceDto ToDto()
        {
            var dto = new PriceDto
            {
                AssetId = this.AssetId
                , DeltaClosePrice = this.CloseDelta
                , Id = this.Id
                , ItemDate = this.Date
                , PeakByCloseEvaluation = this.PeakByClose
                , PeakByHighEvaluation = this.PeakByHigh
                , PriceDirection2D = this.Direction2D
                , PriceDirection3D = this.Direction3D
                , TroughByCloseEvaluation = this.TroughByClose
                , TroughByLowEvaluation = this.TroughByLow
                , TimebandId = 1
            };

            return dto;

        }


        public bool IsExtremum()
        {
            return (PeakByClose > 0 || PeakByHigh > 0 || TroughByClose > 0 || TroughByLow > 0 || PriceGap > 0);
        }

        public bool IsExtremum(TrendlineType type)
        {
            return type == TrendlineType.Resistance ? IsPeak() : IsTrough();
        }

        public bool IsPeak()
        {
            return (PeakByHigh > 0 || PeakByClose > 0);
        }

        public bool IsTrough()
        {
            return (TroughByLow > 0 || TroughByClose > 0);
        }

        public bool IsGap()
        {
            return PriceGap != 0;
        }

        public double ExtremumEvaluation(TrendlineType type)
        {

            var value = 0d;

            if (type == TrendlineType.Support)
            {
                value = Math.Max(TroughByClose, TroughByLow);
            }
            else if (type == TrendlineType.Resistance)
            {
                value = Math.Max(PeakByClose, PeakByHigh);
            }


            return Math.Max(value, PriceGap);

        }

        public TrendlineType toTrendlineType()
        {
            if (PeakByClose > 0 || PeakByHigh > 0)
            {
                return TrendlineType.Resistance;
            }
            else if (TroughByClose > 0 || TroughByLow > 0)
            {
                return TrendlineType.Support;
            }
            else
            {
                return TrendlineType.None;
            }
        }

        public bool IsMatch(TrendlineType type)
        {

            if (type == TrendlineType.Resistance)
            {
                return (PeakByClose > 0 || PeakByHigh > 0);
            }
            else if (type == TrendlineType.Support)
            {
                return (TroughByClose > 0 || TroughByLow > 0);
            }
            else
            {
                return true;
            }

        }

    }

}
