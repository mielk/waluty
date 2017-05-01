using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities.MarketObjects.TimeframeProcessors
{
    public class TimeframeProcessorFactory
    {

        private static List<ITimeframeProcessor> processors = new List<ITimeframeProcessor>();

        public static ITimeframeProcessor GetProcessor(TimeframeUnit unitType)
        {
            var processor = processors.SingleOrDefault(p => p.GetTimeframeUnit() == unitType);
            if (processor == null)
            {
                processor = getNewProcessorInstance(unitType);
            }
            return processor;
        }

        private static ITimeframeProcessor getNewProcessorInstance(TimeframeUnit unitType)
        {
            switch (unitType)
            {
                case TimeframeUnit.Minutes: return new MinutesProcessor();
                case TimeframeUnit.Hours: return new HoursProcessor();
                case TimeframeUnit.Days: return new DaysProcessor();
                case TimeframeUnit.Weeks: return new WeeksProcessor();
                case TimeframeUnit.Months: return new MonthsProcessor();
            }

            throw new Exception("Unknown timeframe unit type: " + unitType.ToString());

        }


    }
}
