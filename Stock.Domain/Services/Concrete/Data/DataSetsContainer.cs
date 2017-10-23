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
    public class DataSetsContainer
    {

        public int AssetId { get; set; }
        public int TimeframeId { get; set; }
        public int SimulationId { get; set; }
        private List<DataSet> items = new List<DataSet>();


        #region CONSTRUCTOR

        public DataSetsContainer(int assetId, int timeframeId, int simulationId)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.SimulationId = simulationId;
        }

        #endregion CONSTRUCTOR



        #region API

        public IEnumerable<DataSet> GetDataSets()
        {
            return items.ToList();
        }

        public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef)
        {
            if (queryDef.StartIndex != null)
            {
                if (queryDef.EndIndex != null)
                {
                    return GetDataSetsBetween((int)queryDef.StartIndex, (int)queryDef.EndIndex);
                }
                else
                {
                    return GetDataSetsLaterThan((int)queryDef.StartIndex);
                }
            }
            else
            {
                if (queryDef.EndIndex != null)
                {
                    return GetDataSetsEarlierThan((int)queryDef.EndIndex);
                }
                else
                {
                    return GetDataSets();
                }
            }
        }

        private IEnumerable<DataSet> GetDataSetsLaterThan(int startIndex)
        {
            return items.Where(ds => ds.IndexNumber >= startIndex).ToList();
        }

        private IEnumerable<DataSet> GetDataSetsEarlierThan(int endIndex)
        {
            return items.Where(ds => ds.IndexNumber <= endIndex).ToList();
        }

        private IEnumerable<DataSet> GetDataSetsBetween(int startIndex, int endIndex)
        {
            return items.Where(ds => ds.IndexNumber >= startIndex && ds.IndexNumber <= endIndex).ToList();
        }

        #endregion API



        #region LOADING DATA

        public void LoadQuotations(IEnumerable<QuotationDto> dtos)
        {
            foreach(var dto in dtos)
            {
                DataSet ds = getOrCreateDataSet(dto.IndexNumber, dto.PriceDate);
                if (ds.quotation == null)
                {
                    Quotation q = Quotation.FromDto(ds, dto);
                    ds.SetQuotation(q);
                }
            }
        }

        private DataSet getOrCreateDataSet(int indexNumber, DateTime datetime)
        {
            DataSet ds = items.SingleOrDefault(i => i.IndexNumber == indexNumber);
            if (ds == null)
            {
                ds = new DataSet(AssetId, TimeframeId, datetime, indexNumber);
                items.Add(ds);
            }
            return ds;
        }

        public void LoadPrices(IEnumerable<PriceDto> dtos)
        {
            foreach (var dto in dtos)
            {
                DataSet ds = items.SingleOrDefault(i => i.IndexNumber == dto.IndexNumber);
                if (ds != null && ds.price == null)
                {
                    Price p = Price.FromDto(ds, dto);
                    ds.SetPrice(p);
                }
            }
        }

        #endregion LOADING DATA


        //private class AssetItemsContainer
        //{
        //    private int assetId;
        //    private Dictionary<int, TimeframeItemsContainer> dataSetsByTimeframes;

        //    public AssetItemsContainer(int assetId)
        //    {
        //        this.assetId = assetId;
        //        this.dataSetsByTimeframes = new Dictionary<int, TimeframeItemsContainer>();
        //    }

        //    public int GetId()
        //    {
        //        return assetId;
        //    }

        //    public TimeframeItemsContainer GetOrCreateTimeframeContainer(int timeframeId)
        //    {
        //        TimeframeItemsContainer timeframeContainer = null;
        //        try
        //        {
        //            dataSetsByTimeframes.TryGetValue(timeframeId, out timeframeContainer);
        //        }
        //        catch (Exception ex) { }

        //        if (timeframeContainer == null)
        //        {
        //            timeframeContainer = new TimeframeItemsContainer(timeframeId);
        //            dataSetsByTimeframes.Add(timeframeId, timeframeContainer);
        //        }
        //        return timeframeContainer;
        //    }

        //}


        //private class TimeframeItemsContainer
        //{
        //    private int timeframeId;
        //    private List<DataSet> items;

        //    public TimeframeItemsContainer(int timeframeId)
        //    {
        //        this.timeframeId = timeframeId;
        //        this.items = new List<DataSet>();
        //    }

        //    public int GetId()
        //    {
        //        return timeframeId;
        //    }

        //    private IEnumerable<AnalysisType> getDefaultAnalysisTypesCollection()
        //    {
        //        return new AnalysisType[] { AnalysisType.Quotations };
        //    }

        //    private IEnumerable<AnalysisType> getGivenCollectionIfNotEmptyOrDefaultCollectionOtherwise(IEnumerable<AnalysisType> types)
        //    {
        //        if (types == null || types.Count() == 0)
        //        {
        //            types = getDefaultAnalysisTypesCollection();
        //        }
        //        return types;
        //    }

        //    public IEnumerable<DataSet> GetDataSets(AnalysisDataQueryDefinition queryDef, Dictionary<AnalysisType, IDataAccessService> services)
        //    {
        //        List<DataSet> result = new List<DataSet>();
        //        IEnumerable<AnalysisType> analysisTypes = getGivenCollectionIfNotEmptyOrDefaultCollectionOtherwise(queryDef.AnalysisTypes);
                
        //        //Quotations.
        //        IDataAccessService quotationService = getDataAccessService(services, AnalysisType.Quotations);
        //        IEnumerable<Quotation> quotations = quotationService.GetUnits(queryDef).Select(q => (Quotation)q).ToList();

        //        foreach (var quotation in quotations)
        //        {
        //            DataSet dataSet = items.SingleOrDefault(price => price.IndexNumber == quotation.IndexNumber);
        //            if (dataSet == null)
        //            {
        //                dataSet = new DataSet(quotation);
        //                items.Add(dataSet);
        //            }
        //            else
        //            {
        //                dataSet.SetQuotation(quotation);
        //            }

        //            result.Add(dataSet);

        //        }


        //        foreach (var analysisType in analysisTypes)
        //        {
        //            if (analysisType != AnalysisType.Quotations)
        //            {
        //                IDataAccessService service = getDataAccessService(services, analysisType);
        //                appendDataForSpecificAnalysisType(result, service, analysisType, queryDef);
        //            }
        //        }

        //        return result;
            
        //    }

        //    private void appendDataForSpecificAnalysisType(IEnumerable<DataSet> dataSets, IDataAccessService service, AnalysisType analysisType, AnalysisDataQueryDefinition queryDef)
        //    {
        //        if (service == null)
        //        {
        //            //Niektóre serwisy nie mają danych wg notowań. np. TrendlineService.
        //            return;
        //            //throw new Exception(string.Format("Service for analysis type {0} not found", analysisType));
        //        }

        //        IEnumerable<IDataUnit> units = service.GetUnits(queryDef).ToList();
        //        foreach (var unit in units)
        //        {
        //            DataSet dataSet = dataSets.SingleOrDefault(price => price.IndexNumber == unit.GetIndexNumber());
        //            if (dataSet != null)
        //            {
        //                dataSet.SetObject(analysisType, unit);
        //            }
        //        }
        //    }

        //    private IDataAccessService getDataAccessService(Dictionary<AnalysisType, IDataAccessService> dict, AnalysisType type)
        //    {
        //        IDataAccessService service;
        //        try
        //        {
        //            dict.TryGetValue(type, out service);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Missing service for analysis type: " + type.ToString());
        //        }
        //        return service;
        //    }

        //}


    }
}
