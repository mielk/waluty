using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IDataService
    {
        IEnumerable<Quotation> GetQuotations(int companyId, int timeband);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeband, int count);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeband, DateTime start);
        IEnumerable<Quotation> GetQuotations(int companyId, int timeband, DateTime start, DateTime end);

        IEnumerable<DataItem> GetFxQuotations(string tableName);
        IEnumerable<DataItem> GetFxQuotations(string tableName, bool isSimulation);
        IEnumerable<DataItem> GetFxQuotations(string tableName, int count);
        IEnumerable<DataItem> GetFxQuotations(string tableName, DateTime start);
        IEnumerable<DataItem> GetFxQuotations(string tableName, DateTime start, DateTime end);

        object GetDataSetProperties(string symbol);
    }
}
