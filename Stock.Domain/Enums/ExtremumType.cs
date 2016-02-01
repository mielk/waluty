using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Enums
{
    public enum ExtremumType
    {
        PeakByClose = 1,
        PeakByHigh = 2,
        TroughByClose = 3,
        TroughByLow = 4
    }
}
