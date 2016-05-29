using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Enums
{

    public enum AnalysisType
    {
        Unknown = -1,
        Price = 0,
        Trendline = 1,
        MACD = 2,
        ADX = 3,
        Candlestick = 4
    }


    public class AnalysisTypeHelper
    {
        private static Dictionary<string, AnalysisType> types;

        public static AnalysisType GetAnalysisType(string symbol)
        {

            if (types == null) LoadTypes();

            var type = AnalysisType.Unknown;
            types.TryGetValue(symbol, out type);

            return type;

        }

        private static void LoadTypes()
        {
            types = new Dictionary<string, AnalysisType>();
            types.Add("price", AnalysisType.Price);
            types.Add("trendline", AnalysisType.Trendline);
            types.Add("macd", AnalysisType.MACD);
            types.Add("adx", AnalysisType.ADX);
            types.Add("candlestick", AnalysisType.Candlestick);

        }

        public static AnalysisType[] FromString(string types, char separator)
        {
            var strTypes = types.Split(separator);
            List<AnalysisType> list = new List<AnalysisType>();
            for (var i = 0; i < strTypes.Length; i++)
            {
                var type = GetAnalysisType(strTypes[i]);
                if (type != AnalysisType.Unknown)
                {
                    list.Add(type);
                }
            }

            return list.ToArray();

        }

    }


}