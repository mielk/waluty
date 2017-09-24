using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class AnalysisItemsContainer<T>
    {

        private Dictionary<int, AssetItemsContainer> quotationsByAssets = new Dictionary<int, AssetItemsContainer>();
        protected IBasicAnalysisService service;

        public AnalysisItemsContainer(IBasicAnalysisService service){
            this.service = service;
        }

        public IEnumerable<T> GetItems(AnalysisDataQueryDefinition queryDef)
        {
            AssetItemsContainer assetItems;
            TimeframeItemsContainer timeframeItems;
            if (quotationsByAssets.ContainsKey(queryDef.AssetId))
            {
                quotationsByAssets.TryGetValue(queryDef.AssetId, out assetItems);
            }
            else
            {
                assetItems = new AssetItemsContainer(queryDef.AssetId);
            }

            if (assetItems != null)
            {
                timeframeItems = assetItems.GetOrCreateTimeframeContainer(queryDef.TimeframeId);
            }

            return null;
        }

        public IEnumerable<T> ProcessDtoToItems(IEnumerable<IDataUnitDto> items, int assetId, int timeframeId)
        {

            if (items.Count() == 0) return new List<T>();
            AssetItemsContainer assetContainer = getOrCreateAssetContainer(assetId);
            TimeframeItemsContainer timeframeContainer = assetContainer.GetOrCreateTimeframeContainer(timeframeId);
            return timeframeContainer.ProcessDtoToItems(items, service);
        }

        private AssetItemsContainer getOrCreateAssetContainer(int assetId)
        {
            AssetItemsContainer assetContainer = null;
            try
            {
                quotationsByAssets.TryGetValue(assetId, out assetContainer);
            }
            catch (Exception ex) { }

            if (assetContainer == null)
            {
                assetContainer = new AssetItemsContainer(assetId);
                quotationsByAssets.Add(assetId, assetContainer);
            }
            return assetContainer;
        }


        private class AssetItemsContainer
        {
            private int assetId;
            private Dictionary<int, TimeframeItemsContainer> quotationsByTimeframes;

            public AssetItemsContainer(int assetId)
            {
                this.assetId = assetId;
                this.quotationsByTimeframes = new Dictionary<int, TimeframeItemsContainer>();
            }

            public int GetId()
            {
                return assetId;
            }

            public TimeframeItemsContainer GetOrCreateTimeframeContainer(int timeframeId)
            {
                TimeframeItemsContainer timeframeContainer = null;
                try
                {
                    quotationsByTimeframes.TryGetValue(timeframeId, out timeframeContainer);
                }
                catch (Exception ex) { }

                if (timeframeContainer == null)
                {
                    timeframeContainer = new TimeframeItemsContainer(timeframeId);
                    quotationsByTimeframes.Add(timeframeId, timeframeContainer);
                }
                return timeframeContainer;
            }

        }

        private class TimeframeItemsContainer
        {
            private int timeframeId;
            private List<IDataUnit> items;
            private bool isLoaded;

            public TimeframeItemsContainer(int timeframeId)
            {
                this.timeframeId = timeframeId;
                this.items = new List<IDataUnit>();
            }

            public int GetId()
            {
                return timeframeId;
            }

            public IEnumerable<IDataUnit> GetItems(AnalysisDataQueryDefinition queryDef)
            {
                if (!isLoaded)
                {
                    //loadItems();
                    isLoaded = true;
                }
                return null;
            }

            private void loadItems(int assetId, int timeframeId)
            {

            }

            public IEnumerable<T> ProcessDtoToItems(IEnumerable<IDataUnitDto> dtos, IBasicAnalysisService service)
            {
                List<T> result = new List<T>();
                foreach (var dto in dtos.ToList())
                {
                    var dataItem = items.SingleOrDefault(i => i.GetIndexNumber() == dto.GetIndexNumber());
                    if (dataItem == null)
                    {
                        dataItem = service.FromDto(dto);
                        items.Add(dataItem);
                    }
                    result.Add((T)dataItem);
                }

                return result;

            }

            public IEnumerable<T> ProcessEntitiesToItems(IEnumerable<IDataUnit> entities, IBasicAnalysisService service)
            {
                List<T> result = new List<T>();
                foreach (var entity in entities)
                {
                    var dataItem = items.SingleOrDefault(i => i.GetIndexNumber() == entity.GetIndexNumber());
                    if (dataItem == null)
                    {
                        dataItem = entity;
                        items.Add(dataItem);
                    }
                    result.Add((T)dataItem);
                }

                return result;

            }

        }

    }
}
