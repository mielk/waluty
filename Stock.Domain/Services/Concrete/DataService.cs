using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{
    public class DataService : IDataService
    {

        private readonly IDataRepository _repository;

        public DataService(IDataRepository repository)
        {
            _repository = repository ?? RepositoryFactory.GetDataRepository();
        }


        private IEnumerable<Quotation> DataItemDtosToQuotationList(IEnumerable<DataItemDto> items)
        {
            var quotations = new List<Quotation>();

            foreach (var item in items)
            {
                var quotation = new Quotation
                {
                      Id = item.QuotationId
                    , Date = item.ItemDate
                    , AssetId = item.AssetId
                    , Open = item.OpenPrice
                    , High = item.HighPrice
                    , Low = item.LowPrice
                    , Close = item.ClosePrice
                    , Volume = item.Volume
                };
                quotations.Add(quotation);
            }

            return quotations;
        }


        public IEnumerable<Quotation> GetQuotations(int assetId, int timeband, int count)
        {
            var dtos = _repository.GetQuotations(assetId, timeband, count);
            return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeband, DateTime start)
        {
            var dtos = _repository.GetQuotations(assetId, timeband, start);
            return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeband, DateTime start, DateTime end)
        {
            var dtos = _repository.GetQuotations(assetId, timeband, start, end);
            return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeband)
        {
            var dtos = _repository.GetQuotations(assetId, timeband);
            return DataItemDtosToQuotationList(dtos);
        }



        public IEnumerable<DataItem> GetFxQuotations(string symbol, int count)
        {
            var dtos = _repository.GetFxQuotations(symbol, count);
            return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol, DateTime start)
        {
            var dtos = _repository.GetFxQuotations(symbol, start);
            return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol, DateTime start, DateTime end)
        {
            var dtos = _repository.GetFxQuotations(symbol, start, end);
            return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol)
        {
            var dtos = _repository.GetFxQuotations(symbol);
            return dtos.Select(DataItem.FromDto).ToList();
        }

        public object GetDataSetProperties(string symbol)
        {
            var properties = _repository.GetDataSetProperties(symbol);
            return properties;
        }

    }
}
