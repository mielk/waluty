using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IDataRepository
    {
        IEnumerable<DataItemDto> GetQuotations(int assetId, int timeband);
        IEnumerable<DataItemDto> GetQuotations(int assetId, int timeband, int count);
        IEnumerable<DataItemDto> GetQuotations(int assetId, int timeband, DateTime start);
        IEnumerable<DataItemDto> GetQuotations(int assetId, int timeband, DateTime start, DateTime end);

        IEnumerable<DataItemDto> GetFxQuotations(string symbol);
        IEnumerable<DataItemDto> GetFxQuotations(string symbol, int count);
        IEnumerable<DataItemDto> GetFxQuotations(string symbol, DateTime start);
        IEnumerable<DataItemDto> GetFxQuotations(string symbol, DateTime start, DateTime end);

        IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType, DateTime lastAnalysisItem, int counter);
        IEnumerable<DataItemDto> GetFxQuotationsForAnalysis(string symbol, string analysisType);

        IEnumerable<PriceDto> GetPrices(string tableName);
        IEnumerable<String> GetStats();

        void UpdateQuotation(QuotationDto quotation, string symbol);
        void AddPrice(PriceDto price, string symbol);
        void UpdatePrice(PriceDto price, string symbol);
        void AddMacd(MacdDto macd, string symbol);
        void UpdateMacd(MacdDto macd, string symbol);
        void AddAdx(AdxDto adx, string symbol);
        void UpdateAdx(AdxDto adx, string symbol);
        void AddAnalysisInfo(AnalysisDto analysis);
        

        bool CheckIfTableExists(string tableName);
        bool CreateTable(string tableName, string template);

        object GetDataSetProperties(string symbol);
        LastDates GetSymbolLastItems(string symbol, string analysisType);

    }
}
