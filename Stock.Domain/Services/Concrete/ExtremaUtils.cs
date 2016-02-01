using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Services.Concrete
{
    public class ExtremaUtils
    {

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
