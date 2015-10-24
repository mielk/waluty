using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IPriceTrendComparer
    {
        void Compare(DataItem item, double level, TrendlineType type, DataItem previousItem);
        bool IsHit();
        TrendHit GetHit();
        bool IsBreak();
        TrendBreak GetBreak();
        double GetPriceOverBreak();
    }
}
