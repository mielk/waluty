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

    public enum AnalysisType
    {
        Unknown = 0,
        Quotation = 1,
        Price = 2,
        MACD = 3,
        ADX = 4,
        Candlestick = 5,
        Trendline = 6
    }


    public class AnalysisTypeHelper
    {
        private static Dictionary<string, AnalysisType> byNames;
        private static Dictionary<AnalysisType, string> byTypes;

        public static AnalysisType Type(string symbol)
        {
            if (byNames == null) loadTypes();
            var type = AnalysisType.Unknown;
            byNames.TryGetValue(symbol, out type);
            return type;
        }

        public static string getTypeString(AnalysisType type)
        {
            if (byTypes == null) loadTypes();
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

        private static void loadTypes()
        {
            byNames = new Dictionary<string, AnalysisType>();
            byTypes = new Dictionary<AnalysisType, string>();

            loadType(AnalysisType.Price, new string[] { "prices", "price", "Prices", "Price" });
            loadType(AnalysisType.Trendline, new string[] { "trendlines", "trendline", "Trendlines", "Trendline" });
            loadType(AnalysisType.MACD, new string[] { "macd", "MACD", "Macd" });
            loadType(AnalysisType.ADX, new string[] { "adx", "ADX", "Adx" });
            loadType(AnalysisType.Candlestick, new string[] { "candlestick", "Candlestick" });

        }

        private static void loadType(AnalysisType type, string[] names)
        {

            foreach (string name in names)
            {
                byNames.Add(name, type);
                if (!byTypes.ContainsKey(type))
                {
                    byTypes.Add(type, name);
                }
            }
        }

        public static AnalysisType[] FromStringListToTypesList(string types)
        {
            return FromStringListToTypesList(types, ',');
        }

        public static AnalysisType[] FromStringListToTypesList(string types, char separator)
        {
            var strTypes = types.Split(separator);
            List<AnalysisType> list = new List<AnalysisType>();
            for (var i = 0; i < strTypes.Length; i++)
            {
                var key = strTypes[i].Trim();
                var type = Type(key);
                if (type != AnalysisType.Unknown)
                {
                    list.Add(type);
                }
            }

            return list.ToArray();

        }

    }


}