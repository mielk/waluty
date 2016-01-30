using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class AdxDto : IDateItem
    {

        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }
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
        public double Adx { get; set; }
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
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }

        [NotMapped]
        public int TimebandId { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }

    }
}
