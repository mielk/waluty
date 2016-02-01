using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities
{
    public class Extrema
    {
        public const int MinRange = 3;
        public const int MaxRange = 360;
        public static ExtremumType GetExtremumType(bool isPeak, bool byClose)
        {
            if (isPeak)
            {
                return byClose ? ExtremumType.PeakByClose : ExtremumType.PeakByHigh;
            }
            else
            {
                return byClose ? ExtremumType.TroughByClose : ExtremumType.TroughByLow;
            }            
        }

    }
}
