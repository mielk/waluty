using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services;
using Stock.Domain.Enums;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities.MarketObjects.TimeframeProcessors;

namespace Stock.Domain.Entities
{

    public class Timeframe
    {

        //Static properties.
        private static ITimeframeService service = ServiceFactory.GetTimeframeService();

        //Instance properties.
        private int id;
        private string name;
        private TimeframeUnit unitType;
        private int unitsCounter;
        private ITimeframeProcessor processor;



        #region STATIC_METHODS

        public static void injectService(ITimeframeService _service)
        {
            service = _service;
        }

        public static void restoreDefaultService()
        {
            service = ServiceFactory.GetTimeframeService();
        }

        public static IEnumerable<Timeframe> GetAllTimeframes()
        {
            return service.GetAllTimeframes();
        }

        public static Timeframe ById(int id)
        {
            return service.GetTimeframeById(id);
        }

        public static Timeframe ByName(string name)
        {
            return service.GetTimeframeByName(name);
        }

        #endregion STATIC_METHODS


        #region CONSTRUCTORS

        public Timeframe(int id, string name, TimeframeUnit unitType, int unitsCounter)
        {
            this.id = id;
            this.name = name;
            this.unitType = unitType;
            this.unitsCounter = unitsCounter;
        }

        public static Timeframe FromDto(TimeframeDto dto)
        {
            return new Timeframe(dto.Id, dto.Symbol, dto.PeriodUnit.ToTimeframeUnit(), dto.PeriodCounter);
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public TimeframeUnit GetUnitType()
        {
            return unitType;
        }

        public int GetUnitsCounter()
        {
            return unitsCounter;
        }

        #endregion GETTERS


        #region DATA_PROCESSING

        private ITimeframeProcessor GetTimeframeProcessor()
        {
            if (processor == null)
            {
                processor = TimeframeProcessorFactory.GetProcessor(unitType);
            }
            return processor;
        }

        public void InjectTimeframeProcessor(ITimeframeProcessor processor)
        {
            this.processor = processor;
        }

        public int GetDifferenceBetweenDates(DateTime baseDate, DateTime comparedDate)
        {
            return GetTimeframeProcessor().CountTimeUnits(baseDate, comparedDate, unitsCounter);
        }

        public DateTime AddTimeUnits(DateTime baseDate, int units)
        {
            return GetTimeframeProcessor().AddTimeUnits(baseDate, unitsCounter, units);
        }

        public DateTime GetProperDateTime(DateTime datetime)
        {
            return GetTimeframeProcessor().GetProperDateTime(datetime, unitsCounter);
        }

        #endregion DATA_PROCESSING


        #region SYSTEM

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Timeframe)) return false;

            Timeframe compared = (Timeframe)obj;
            if ((compared.GetId()) != id) return false;
            if (!compared.GetName().Equals(name)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion SYSTEM



    }

}
