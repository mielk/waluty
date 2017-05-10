using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;

namespace Stock.Domain.Services.Abstract.DataServices
{
    public interface IDataService
    {
        void InjectRepository(IDataRepository2 repository);
        void SetParameters(int assedId, int timeframeId);
        void SetParameters(Asset asset, Timeframe timeframe);
        IEnumerable<DataItemDto> GetDataItems();
        IEnumerable<DataItemDto> GetDataItems(DateTime startDate);
        IEnumerable<DataItemDto> GetDataItems(DateTime startDate, DateTime endDate);
        IEnumerable<DataItemDto> GetDataItems(DateTime startDate, int limit);
        IEnumerable<DataItemDto> AppendDataItems(int counter);
    }
}
