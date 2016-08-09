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
        private static Dictionary<string, AnalysisType> byNames;
        private static Dictionary<AnalysisType, string> byTypes;

        public static AnalysisType Type(string symbol)
        {

            if (byNames == null) LoadTypes();

            var type = AnalysisType.Unknown;
            byNames.TryGetValue(symbol, out type);

            return type;

        }

        public static string getTypeString(AnalysisType type)
        {
            string result = string.Empty;
            try
            {
                byTypes.TryGetValue(type, out result);
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static void LoadTypes()
        {
            byNames = new Dictionary<string, AnalysisType>();
            byTypes = new Dictionary<AnalysisType, string>();

            byNames.Add("prices", AnalysisType.Price);
            byTypes.Add(AnalysisType.Price, "prices");
            byNames.Add("trendline", AnalysisType.Trendline);
            byTypes.Add(AnalysisType.Trendline, "trendline");
            byNames.Add("macd", AnalysisType.MACD);
            byTypes.Add(AnalysisType.MACD, "macd");
            byNames.Add("adx", AnalysisType.ADX);
            byTypes.Add(AnalysisType.ADX, "adx");
            byNames.Add("candlestick", AnalysisType.Candlestick);
            byTypes.Add(AnalysisType.Candlestick, "candlestick");

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