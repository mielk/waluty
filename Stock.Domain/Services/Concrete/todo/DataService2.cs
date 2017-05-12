using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;
using Stock.Core;
using Stock.Domain.Entities.Old;

namespace Stock.Domain.Services
{
    public class DataService2 : IDataService2, IAnalysisDataService
    {

        private readonly IDataRepository2 _repository;

        public DataService2(IDataRepository2 repository)
        {
            _repository = repository ?? RepositoryFactory.GetDataRepository2();
        }

        public IEnumerable<DataItem> GetDataItems(AssetTimeframe atf, DateTime? startDate, DateTime? endDate, 
                                                    IEnumerable<AnalysisType> analysisTypes)
        {

            //List<string> list = new List<string>();
            //foreach (var at in analysisTypes)
            //{
            //    list.Add(at.TableName());
            //}

            //IEnumerable<DataItemDto> dataItemsDto = _repository.GetDataItems(atf.GetSymbol(), startDate, endDate, list);
            //List<DataItem> dataItems = new List<DataItem>();
            //foreach (var dto in dataItemsDto)
            //{
            //    dataItems.Add(DataItem.FromDto(dto));
            //}

            //return dataItems;
            return null;
        }


        private IEnumerable<Quotation> DataItemDtosToQuotationList(IEnumerable<DataItemDto> items)
        {
            var quotations = new List<Quotation>();

            foreach (var item in items)
            {
                var quotation = new Quotation
                {
                      Id = item.QuotationId
                    , Date = item.PriceDate
                    , AssetId = item.AssetId
                    , Open = item.OpenPrice
                    , High = item.HighPrice
                    , Low = item.LowPrice
                    , Close = item.ClosePrice
                    , Volume = item.Volume ?? 0
                };
                quotations.Add(quotation);
            }

            return quotations;
        }


        public IEnumerable<Quotation> GetQuotations(int assetId, int timeframe, int count)
        {
            return null;
            //var dtos = _repository.GetQuotations(assetId, timeframe, count);
            //return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeframe, DateTime start)
        {
            return null;
            //var dtos = _repository.GetQuotations(assetId, timeframe, start);
            //return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeframe, DateTime start, DateTime end)
        {
            return null;
            //var dtos = _repository.GetQuotations(assetId, timeframe, start, end);
            //return DataItemDtosToQuotationList(dtos);
        }

        public IEnumerable<Quotation> GetQuotations(int assetId, int timeframe)
        {
            return null;
            //var dtos = _repository.GetQuotations(assetId, timeframe);
            //return DataItemDtosToQuotationList(dtos);
        }



        public IEnumerable<DataItem> GetFxQuotations(string symbol, int count)
        {
            return null;
            //var dtos = _repository.GetFxQuotations(symbol, count);
            //return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol, DateTime start)
        {
            return null;
            //var dtos = _repository.GetFxQuotations(symbol, start);
            //return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol, DateTime start, DateTime end)
        {
            return null;
            //var dtos = _repository.GetFxQuotations(symbol, start, end);
            //return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol)
        {
            return null;
            //var dtos = _repository.GetFxQuotations(symbol);
            //return dtos.Select(DataItem.FromDto).ToList();
        }

        public IEnumerable<DataItem> GetFxQuotations(string symbol, bool isSimulation)
        {
            return null;
            //var dtos = _repository.GetFxQuotations(symbol, true);
            //return dtos.Select(DataItem.FromDto).ToList();
        }

        public object GetDataSetProperties(string symbol)
        {
            //var properties = _repository.GetDataSetProperties(symbol);
            //return properties;
            return null;
        }

        public void AddAnalysisInfo(Analysis analysis){
            //_repository.AddAnalysisInfo(analysis.ToDto());
        }

        public LastDates GetSymbolLastItems(string symbol, string analysisType)
        {
            //return _repository.GetSymbolLastItems(symbol, analysisType);
            return null;
        }

        public IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName)
        {
            //return _repository.GetFxQuotationsForAnalysis(symbol, tableName).Select(DataItem.FromDto);
            return null;
        }

        public IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName, DateTime lastDate, int counter)
        {
            //var items = _repository.GetFxQuotationsForAnalysis(symbol, tableName, lastDate, counter).Select(DataItem.FromDto);
            //LoadExtrema(items, symbol);
            //return items;
            return null;
        }


        private void LoadExtrema(IEnumerable<DataItem> items, string symbol)
        {

            //var firstQuotationDate = items.Min(q => q.Date);
            //var lastQuotationDate = items.Max(q => q.Date);
            //var extrema = _repository.GetExtrema(symbol, firstQuotationDate, lastQuotationDate);

            //foreach (var extremumDto in extrema)
            //{
            //    var item = items.SingleOrDefault(q => q.Date.Equals(extremumDto.PriceDate));
            //    if (item != null && item.Price != null)
            //    {
            //        item.Price.ApplyExtremumValue(Extremum.FromDto(extremumDto));
            //    }
            //}

        }

        public void AddPrice(Price price, string symbol)
        {
            //_repository.AddPrice(price.ToDto(), symbol);
        }

        public void UpdatePrice(Price price, string symbol)
        {
            //_repository.UpdatePrice(price.ToDto(), symbol);
        }


        public void AddMacd(Macd macd, string symbol)
        {
            //_repository.AddMacd(macd.ToDto(), symbol);
        }

        public void UpdateMacd(Macd macd, string symbol)
        {
            //_repository.UpdateMacd(macd.ToDto(), symbol);
        }


        public void AddAdx(Adx adx, string symbol)
        {
            //_repository.AddAdx(adx.ToDto(), symbol);
        }

        public void UpdateAdx(Adx adx, string symbol)
        {
            //_repository.UpdateAdx(adx.ToDto(), symbol);
        }


        public void UpdateQuotation(Quotation quotation, string symbol)
        {
            //_repository.UpdateQuotation(quotation.ToDto(), symbol);
        }



        public DateTime? GetAnalysisLastCalculation(string symbol, string analysisType){
            //return _repository.GetAnalysisLastCalculation(symbol, analysisType);
            return null;
        }

        public DateTime? GetLastQuotationDate(string symbol)
        {
            //return _repository.GetLastQuotationDate(symbol);
            return null;
        }

        public DateTime? GetFirstQuotationDate(string symbol)
        {
            return null;
        }

    }
}
