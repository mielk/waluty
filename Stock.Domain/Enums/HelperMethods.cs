using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Enums
{
    public static class HelperMethods
    {

        public static TimeframeUnit ToTimeframeUnit(this string value)
        {

            foreach(TimeframeUnit unit in TimeframeUnit.GetValues(typeof(TimeframeUnit))){
                if (value.Equals(unit.ToString(), StringComparison.CurrentCultureIgnoreCase)){
                    return unit;
                }
            }

            throw new Exception("Unknown timeframe unit: " + value);

        }

        //public static DbAnalysisDataTypeEnum ToDbAnalysisDataTypeEnum(this AnalysisType value)
        //{
        //    switch (value)
        //    {
        //        case AnalysisType.Quotation: return DbAnalysisDataTypeEnum.Quotation;
        //        case AnalysisType.Price: return DbAnalysisDataTypeEnum.Price;
        //        case AnalysisType.MACD: return DbAnalysisDataTypeEnum.Macd;
        //        case AnalysisType.ADX: return DbAnalysisDataTypeEnum.Adx;
        //        case AnalysisType.Candlestick: return DbAnalysisDataTypeEnum.Candlestick;
        //        case AnalysisType.Trendline: return DbAnalysisDataTypeEnum.Trendline;
        //        default:
        //            throw new Exception("Unknown AnalysisType enum");
        //    }
        //}

        //public static AnalysisType ToAnalysisTypeEnum(this DbAnalysisDataTypeEnum value)
        //{
        //    switch (value)
        //    {
        //        case DbAnalysisDataTypeEnum.Quotation: return AnalysisType.Quotation;
        //        case DbAnalysisDataTypeEnum.Price: return AnalysisType.Price;
        //        case DbAnalysisDataTypeEnum.Macd: return AnalysisType.MACD;
        //        case DbAnalysisDataTypeEnum.Adx: return AnalysisType.ADX;
        //        case DbAnalysisDataTypeEnum.Candlestick: return AnalysisType.Candlestick;
        //        case DbAnalysisDataTypeEnum.Trendline: return AnalysisType.Trendline;
        //        default:
        //            throw new Exception("Unknown DbAnalysisDataTypeEnum");
        //    }
        //}

    }
}
