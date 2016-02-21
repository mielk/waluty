using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalysisDataService
    {

        IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName);
        IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName, DateTime lastDate, int counter);
        LastDates GetSymbolLastItems(string symbol, string tableName);

        void AddAnalysisInfo(Analysis analysis);
        void AddPrice(Price price, string symbol);
        void UpdatePrice(Price price, string symbol);
        void AddMacd(Macd macd, string symbol);
        void UpdateMacd(Macd macd, string symbol);
        void AddAdx(Adx adx, string symbol);
        void UpdateAdx(Adx adx, string symbol);
        void UpdateQuotation(Quotation quotation, string symbol);

    }
}
