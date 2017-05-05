using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public class EFDataRepository : IDataRepository
    {

        public IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId)
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate)
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(int assetId, int timeframeId, DateTime startDate, int limit)
        {
            return null;
        }

        public void Update(IEnumerable<DataItemDto> items)
        {

        }

    }
}
