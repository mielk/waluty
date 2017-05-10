using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public interface IDataService2
    {

        IEnumerable<DataItem> GetDataItems(AssetTimeframe atf, DateTime? startDate, DateTime? endDate, IEnumerable<AnalysisType> analysisType);

        IEnumerable<Quotation> GetQuotations(int companyId, int timeframe);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeframe, int count);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeframe, DateTime start);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeframe, DateTime start, DateTime end);

        IEnumerable<DataItem> GetFxQuotations(string tableName);
        IEnumerable<DataItem> GetFxQuotations(string tableName, bool isSimulation);
        IEnumerable<DataItem> GetFxQuotations(string tableName, int count);
        IEnumerable<DataItem> GetFxQuotations(string tableName, DateTime start);
        IEnumerable<DataItem> GetFxQuotations(string tableName, DateTime start, DateTime end);

        object GetDataSetProperties(string symbol);
        LastDates GetSymbolLastItems(string symbol, string analysisType);
        DateTime? GetAnalysisLastCalculation(string symbol, string analysisType);
        DateTime? GetLastQuotationDate(string symbol);
        DateTime? GetFirstQuotationDate(string symbol);

    }
}
