using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class Adx
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public double Tr { get; set; }
        public double Dm1Pos { get; set; }
        public double Dm1Neg { get; set; }
        public double Tr14 { get; set; }
        public double Dm14Pos { get; set; }
        public double Dm14Neg { get; set; }
        public double Di14Pos { get; set; }
        public double Di14Neg { get; set; }
        public double Di14Diff { get; set; }
        public double Di14Sum { get; set; }
        public double Dx { get; set; }
        public double AdxValue { get; set; }
        public int DaysUnder20 { get; set; }
        public int DaysUnder15 { get; set; }
        public double Cross20 { get; set; }
        public double DeltaDiPos { get; set; }
        public double DeltaDiNeg { get; set; }
        public double DeltaAdx { get; set; }
        public int DiPosDirection3D { get; set; }
        public int DiPosDirection2D { get; set; }
        public int DiNegDirection3D { get; set; }
        public int DiNegDirection2D { get; set; }
        public int AdxDirection3D { get; set; }
        public int AdxDirection2D { get; set; }
        public int DiPosDirectionChanged { get; set; }
        public int DiNegDirectionChanged { get; set; }
        public int AdxDirectionChanged { get; set; }
        public double DiDifference { get; set; }
        public int DiLinesCrossing { get; set; }


        public static Adx FromDto(AdxDto dto)
        {
            var adx = new Adx();
            adx.Id = dto.Id;
            adx.AssetId = dto.AssetId;
            adx.Date = dto.PriceDate;
            adx.Tr = dto.Tr;
            adx.Dm1Pos = dto.Dm1Pos;
            adx.Dm1Neg = dto.Dm1Neg;
            adx.Tr14 = dto.Tr14;
            adx.Dm14Pos = dto.Dm14Pos;
            adx.Dm14Neg = dto.Dm14Neg;
            adx.Di14Pos = dto.Di14Pos;
            adx.Di14Neg = dto.Di14Neg;
            adx.Di14Diff = dto.Di14Diff;
            adx.Di14Sum = dto.Di14Sum;
            adx.Dx = dto.Dx;
            adx.AdxValue = dto.Adx;
            adx.DaysUnder20 = dto.DaysUnder20;
            adx.DaysUnder15 = dto.DaysUnder15;
            adx.Cross20 = dto.Cross20;
            adx.DeltaDiPos = dto.DeltaDiPos;
            adx.DeltaDiNeg = dto.DeltaDiNeg;
            adx.DeltaAdx = dto.DeltaAdx;
            adx.DiPosDirection3D = dto.DiPosDirection3D;
            adx.DiPosDirection2D = dto.DiPosDirection2D;
            adx.DiNegDirection3D = dto.DiNegDirection3D;
            adx.DiNegDirection2D = dto.DiNegDirection2D;
            adx.AdxDirection3D = dto.AdxDirection3D;
            adx.AdxDirection2D = dto.AdxDirection2D;
            adx.DiPosDirectionChanged = dto.DiPosDirectionChanged;
            adx.DiNegDirectionChanged = dto.DiNegDirectionChanged;
            adx.AdxDirectionChanged = dto.AdxDirectionChanged;
            adx.DiDifference = dto.DiDifference;
            adx.DiLinesCrossing = dto.DiLinesCrossing;

            return adx;

        }

        public AdxDto ToDto()
        {
            var dto = new AdxDto
            {
                Id = this.Id,
                AssetId = this.AssetId,
                PriceDate = this.Date,
                Tr = this.Tr,
                Dm1Pos = this.Dm1Pos,
                Dm1Neg = this.Dm1Neg,
                Tr14 = this.Tr14,
                Dm14Pos = this.Dm14Pos,
                Dm14Neg = this.Dm14Neg,
                Di14Pos = this.Di14Pos,
                Di14Neg = this.Di14Neg,
                Di14Diff = this.Di14Diff,
                Di14Sum = this.Di14Sum,
                Dx = this.Dx,
                Adx = this.AdxValue,
                DaysUnder20 = this.DaysUnder20,
                DaysUnder15 = this.DaysUnder15,
                Cross20 = this.Cross20,
                DeltaDiPos = this.DeltaDiPos,
                DeltaDiNeg = this.DeltaDiNeg,
                DeltaAdx = this.DeltaAdx,
                DiPosDirection3D = this.DiPosDirection3D,
                DiPosDirection2D = this.DiPosDirection2D,
                DiNegDirection3D = this.DiNegDirection3D,
                DiNegDirection2D = this.DiNegDirection2D,
                AdxDirection3D = this.AdxDirection3D,
                AdxDirection2D = this.AdxDirection2D,
                DiPosDirectionChanged = this.DiPosDirectionChanged,
                DiNegDirectionChanged = this.DiNegDirectionChanged,
                AdxDirectionChanged = this.AdxDirectionChanged,
                DiDifference = this.DiDifference,
                DiLinesCrossing = this.DiLinesCrossing, 
                TimeframeId = 1
            };

            return dto;

        }

    }
}
