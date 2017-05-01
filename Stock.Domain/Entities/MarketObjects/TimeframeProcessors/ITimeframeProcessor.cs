using Stock.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Stock.Domain.Entities
{
    public interface ITimeframeProcessor
    {
        TimeframeUnit GetTimeframeUnit();
        DateTime GetProperDateTime(DateTime baseDate, int periodLength);
        int CountTimeUnits(DateTime baseDate, DateTime comparedDate, int periodLength);
        DateTime AddTimeUnits(DateTime baseDate, int periodLength, int units);
        DateTime GetNext(DateTime baseDate, int periodLength);
    }

}
