using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ISimulationService
    {
        bool Start(string pair, string timeframe);
        int NextStep(int incrementation);
        object GetDataSetProperties();
        IEnumerable<DataItem> GetQuotations(DateTime startDateTime, DateTime endDateTime);
    }
}
