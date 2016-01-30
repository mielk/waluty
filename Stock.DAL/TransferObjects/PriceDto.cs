using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class PriceDto : IDateItem
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime PriceDate { get; set; }
        public double DeltaClosePrice { get; set; }
        public int PriceDirection2D { get; set; }
        public int PriceDirection3D { get; set; }
        public double PeakByCloseEvaluation { get; set; }
        public double PeakByHighEvaluation { get; set; }
        public double TroughByCloseEvaluation { get; set; }
        public double TroughByLowEvaluation { get; set; }

        [NotMapped]
        public int TimebandId { get; set; }

        public DateTime GetDate()
        {
            return PriceDate;
        }

    }
}