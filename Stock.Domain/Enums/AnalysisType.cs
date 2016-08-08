using Stock.Domain.Entities;
using Stock.Domain.Services;
using Stock.Domain.Services.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Enums
{

    public enum AnalysisType{
    
        Unknown = 0,
        Price = 1,
        Trendline = 2,
        MACD = 3,
        ADX = 4,
        Candlestick = 5
    }


    public class AnalysisTypeHelper
    {
        private static Dictionary<string, AnalysisType> types;

        public static AnalysisType Type(string symbol)
        {

            if (types == null) LoadTypes();

            var type = AnalysisType.Unknown;
            types.TryGetValue(symbol, out type);

            return type;

        }

        private static void LoadTypes()
        {
            types = new Dictionary<string, AnalysisType>();
            types.Add("prices", AnalysisType.Price);
            types.Add("trendline", AnalysisType.Trendline);
            types.Add("macd", AnalysisType.MACD);
            types.Add("adx", AnalysisType.ADX);
            types.Add("candlestick", AnalysisType.Candlestick);

        }


        public static AnalysisType[] StringToTypesList(string types)
        {
            return StringToTypesList(types, ',');
        }

        public static AnalysisType[] StringToTypesList(string types, char separator)
        {
            var strTypes = types.Split(separator);
            List<AnalysisType> list = new List<AnalysisType>();
            for (var i = 0; i < strTypes.Length; i++)
            {
                var type = Type(strTypes[i]);
                if (type != AnalysisType.Unknown)
                {
                    list.Add(type);
                }
            }

            return list.ToArray();

        }

    }


}