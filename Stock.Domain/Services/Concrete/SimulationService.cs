using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.Domain.Services.Factories;

namespace Stock.Domain.Services
{
    public class SimulationService : ISimulationService, IAnalysisDataService
    {

        private readonly IDataService _dataService;
        private readonly IPriceAnalyzer _priceAnalyzer;
        private readonly IMacdAnalyzer _macdAnalyzer;
        public DataItem[] Data { get; set; }
        public DataItem[] CurrentDataSet { get; set; }
        public int LastAnalyzed { get; set; }
        public string Symbol { get; set; }
        public string Pair { get; set; }
        public string Timeframe { get; set; }


        public SimulationService(IDataService dataService)
        {
            _dataService = dataService ?? DataServiceFactory.Instance().GetService();
            _priceAnalyzer = new PriceAnalyzer(this);
            _macdAnalyzer = new MacdAnalyzer(this);
        }

        public bool Start(string pair, string timeframe)
        {
            this.Pair = pair;
            this.Timeframe = timeframe;
            this.Symbol = pair + '_' + timeframe;

            try
            {
                Data = _dataService.GetFxQuotations(this.Symbol, true).ToArray();
                Data.AppendIndexNumbers();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int NextStep(int incrementation)
        {
            LastAnalyzed++;

            CurrentDataSet = Data.Where(d => d.Index < LastAnalyzed).ToArray();
            

            //Napraw numerację (mogła zostać zepsuta przez obiekt PriceAnalyzer).
            Data.AppendIndexNumbers();

            _priceAnalyzer.Analyze(this.Symbol, false);
            _macdAnalyzer.Analyze(this.Symbol, false);

            return LastAnalyzed;

        }


        public object GetDataSetProperties()
        {
            return new 
            {
                counter = CurrentDataSet.Length,
                firstDate = CurrentDataSet.Min(d => d.Date),
                lastDate = CurrentDataSet.Max(d => d.Date),
                minPrice = CurrentDataSet.Min(d => d.Quotation.Low),
                maxPrice = CurrentDataSet.Max(d => d.Quotation.High),
            };
        }


        public IEnumerable<DataItem> GetQuotations(DateTime startDate, DateTime endDate)
        {
            IEnumerable<DataItem> items = CurrentDataSet.Where(di => di.Date >= startDate && di.Date <= endDate).OrderBy(di => di.Index);
            return items;
        }


        //Implementation of [IAnalysisDataService]//
        public IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName)
        {
            return CurrentDataSet;
        }

        public IEnumerable<DataItem> GetFxQuotationsForAnalysis(string symbol, string tableName, DateTime lastDate, int counter)
        {
            return CurrentDataSet;
        }

        public LastDates GetSymbolLastItems(string symbol, string tableName)
        {
            DateTime lastQuotation = CurrentDataSet[CurrentDataSet.Length - 1].Date;
            DateTime lastAnalyzed = CurrentDataSet.Length < 2 ? new DateTime(1900, 1, 1) : CurrentDataSet[CurrentDataSet.Length - 2].Date;

            return new LastDates
            {
                LastQuotation = lastQuotation,
                LastAnalysisItem = lastAnalyzed
            };

        }

        

        //Fake methods to met [IAnalysisDataService] interface requirements.//
        public void AddAnalysisInfo(Analysis analysis) { }
        public void AddPrice(Price price, string symbol) { }
        public void UpdatePrice(Price price, string symbol) { }
        public void AddMacd(Macd macd, string symbol) { }
        public void UpdateMacd(Macd macd, string symbol) { }
        public void AddAdx(Adx adx, string symbol) { }
        public void UpdateAdx(Adx adx, string symbol) { }
        public void UpdateQuotation(Quotation quotation, string symbol) { }


    }
}
