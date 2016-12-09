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
using Stock.Domain.Enums;
using System.Diagnostics;

namespace Stock.Domain.Services
{
    public class SimulationService : ISimulationService, IAnalysisDataService
    {

        private readonly IDataService _dataService;
        private IPriceAnalyzer _priceAnalyzer;
        //private IMacdAnalyzer _macdAnalyzer;
        private ITrendlineAnalyzer _trendAnalyzer;
        public AnalysisType[] types { get; set; }
        public DataItem[] Data { get; set; }
        public DataItem[] CurrentDataSet { get; set; }
        public int LastAnalyzed { get; set; }
        public string Symbol { get; set; }
        public AssetTimeframe AssetTimeframe { get; set; }
        public IQuotationService quotationService;



        public SimulationService(IDataService dataService)
        {
            _dataService = dataService ?? DataServiceFactory.Instance().GetService();
            quotationService = new SimulationQuotationService(this);
            //_macdAnalyzer = new MacdAnalyzer(this);
        }

        public bool Start(string pair, string timeframe, AnalysisType[] types)
        {
            return Start(new AssetTimeframe(pair, timeframe), types);
        }

        public bool Start(AssetTimeframe atf, AnalysisType[] types)
        {

            this.Symbol = atf.Symbol();
            this.types = types;

            try
            {
                Data = _dataService.GetFxQuotations(this.Symbol, true).ToArray();
                Data.AppendIndexNumbers();

                Debug.WriteLine(string.Format("+;SimulationService.Start - Data loaded (Items: {0})", Data.Length));

                if (types.Contains(AnalysisType.Price)){
                    _priceAnalyzer = new PriceAnalyzer(atf);
                    ((Analyzer)_priceAnalyzer).injectQuotationService(quotationService);
                }

                if (types.Contains(AnalysisType.Trendline))
                {
                    _trendAnalyzer = new TrendlineAnalyzer(atf);
                    ((Analyzer)_trendAnalyzer).injectQuotationService(quotationService);
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        public int NextStep(int incrementation)
        {

            Debug.WriteLine("+;<SimulationService.NextStep>");
            LastAnalyzed++;

            Debug.WriteLine(string.Format("*;SimulationService.NextStep | LastAnalyzed: {0}", LastAnalyzed));

            CurrentDataSet = Data.Where(d => d.Index < LastAnalyzed).ToArray();

            Debug.WriteLine(string.Format("*;SimulationService.NextStep | CurrentDataSet length: {0}", 
                                CurrentDataSet.Length));
            Debug.WriteLine(string.Format("*;SimulationService.NextStep | CurrentDataSet lastItem: {0}", 
                                CurrentDataSet.Max(d => d.Date)));
             
            

            //Napraw numerację (mogła zostać zepsuta przez obiekt PriceAnalyzer).
            Data.AppendIndexNumbers();


            //Prices.
            if (_priceAnalyzer != null) _priceAnalyzer.Analyze(CurrentDataSet);


            //MACD.
            //_macdAnalyzer.Analyze(this.Symbol, false);


            //Trendlines.
            if (_trendAnalyzer != null) _trendAnalyzer.Analyze(CurrentDataSet);

            Debug.WriteLine("+;<///SimulationService.NextStep>");
            return LastAnalyzed;

        }


        public DateTime? getLastCalculationDate(AnalysisType type)
        {
            if (LastAnalyzed > 2) return Data[LastAnalyzed - 2].Date;
            return null;
        }

        public DateTime? getLastCalculationDate(string symbol, string analysisSymbol)
        {
            if (LastAnalyzed > 2) return Data[LastAnalyzed - 2].Date;
            return null;
        }

        public DateTime? getLastCalculationDate(AssetTimeframe atf, AnalysisType analysisType)
        {
            if (LastAnalyzed > 2) return Data[LastAnalyzed - 2].Date;
            return null;
        }

        public DataItem[] fetchData(Dictionary<AnalysisType, IAnalyzer> analyzers)
        {
            var data = Data.Where(d => d.Index < LastAnalyzed).ToArray();
            return data;
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


        public IEnumerable<DataItem> GetQuotations(DateTime startDate, DateTime endDate)
        {
            IEnumerable<DataItem> items = CurrentDataSet.Where(di => di.Date >= startDate && di.Date <= endDate).OrderBy(di => di.Index);
            return items;
        }

        public IEnumerable<Trendline> GetTrendlines(DateTime startDateTime, DateTime endDateTime)
        {

            if (_trendAnalyzer != null)
            {
                return _trendAnalyzer.GetTrendlines();
            }

            return new List<Trendline>();

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
