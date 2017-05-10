using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services.Abstract.DataServices;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Domain.Entities;

namespace Stock.Domain.Services.Concrete.DataServices
{
    public class SimulationDataService : IDataService
    {

        private IDataRepository2 repository;
        private Asset asset;
        private Timeframe timeframe;
        private IEnumerable<DataItem> items = new List<DataItem>();


        #region INFRASTRUCTURE

        public void InjectRepository(IDataRepository2 repository)
        {
            this.repository = repository;
        }

        public void SetParameters(int assetId, int timeframeId)
        {
            this.asset = Asset.ById(assetId);
            this.timeframe = Timeframe.ById(timeframeId);
        }

        public void SetParameters(Asset asset, Timeframe timeframe)
        {
            this.asset = asset;
            this.timeframe = timeframe;
        }

        #endregion INFRASTRUCTURE


        public IEnumerable<DataItemDto> GetDataItems()
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(DateTime startDate)
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public IEnumerable<DataItemDto> GetDataItems(DateTime startDate, int limit)
        {
            return null;
        }

        public IEnumerable<DataItemDto> AppendDataItems(int counter)
        {
            return null;   
        }

    }
}
