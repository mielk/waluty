using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Repositories
{
    public interface IDataRepository
    {
        IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId);
        IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate);
        IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate, DateTime endDate);
        IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate, int limit);
        void Update(IEnumerable<DataItemDto> items);
    }
}