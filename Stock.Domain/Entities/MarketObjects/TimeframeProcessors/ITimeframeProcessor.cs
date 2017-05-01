using Stock.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Stock.Domain.Entities
{
    public interface ITimeframeProcessor
    {
        TimeframeUnit GetTimeframeUnit();
        DateTime GetProperDateTime(DateTime baseDate, int periodLength);
        int GetDifferenceBetweenDates(DateTime baseDate, DateTime comparedDate);
        DateTime AddTimeUnits(DateTime baseDate, int units);
        DateTime GetNext(DateTime baseDate, int periodLength);
    }

}
