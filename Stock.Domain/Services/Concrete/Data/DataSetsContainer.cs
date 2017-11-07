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


    }
}
