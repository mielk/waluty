using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IDataRepository2
    {

        IEnumerable<DataItemDto> GetDataItems(string symbol, DateTime? startDate, DateTime? endDate, IEnumerable<string> analysisType);
        IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType, DateTime lastAnalysisItem, int counter);
        IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType);

        IEnumerable<PriceDto> GetPrices(string tableName);
        IEnumerable<String> GetStats();
        IEnumerable<ExtremumDto> GetExtrema(string symbol, DateTime startDate, DateTime endDate);

       
        void AddMacd(MacdDto macd, string symbol);
        void UpdateMacd(MacdDto macd, string symbol);
        void AddAdx(AdxDto adx, string symbol);
        void UpdateAdx(AdxDto adx, string symbol);
        void AddAnalysisInfo(AnalysisDto analysis);

        object GetDataSetProperties(string symbol);
        LastDates GetSymbolLastItems(string symbol, string analysisType);
        DateTime? GetAnalysisLastCalculation(string symbol, string analysisType);
        DateTime? GetLastQuotationDate(string symbol);

    }
}
