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

namespace Stock.Domain.Services
{
    public class ProcessService : IProcessService
    {

        private AssetTimeframe assetTimeframe;
        private AnalysisType[] analysisTypes;
        private readonly IDataRepository _dataRepository;
        private Dictionary<AnalysisType, IAnalyzer> _analyzers;



        public ProcessService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? RepositoryFactory.GetDataRepository();
        }


        public void LoadAssetTimeframe(Asset asset, Timeframe timeframe)
        {
            assetTimeframe = new AssetTimeframe(asset, timeframe);
        }

        public void LoadAssetTimeframe(string asset, string timeframe)
        {
            assetTimeframe = new AssetTimeframe(asset, timeframe);
        }

        public void LoadAssetTimeframe(string symbol)
        {
            assetTimeframe = new AssetTimeframe(symbol);
        }


        public void LoadAnalysisTypes(AnalysisType[] types)
        {
            analysisTypes = types;
            loadAnalyzers();
        }


        private void loadAnalyzers()
        {

            if (_analyzers == null)
            {
                _analyzers = new Dictionary<AnalysisType, IAnalyzer>();
            }


            IAnalyzer analyzer;
            foreach (var type in analysisTypes)
            {
                analyzer = null;

                _analyzers.TryGetValue(type, out analyzer);
                if (analyzer == null)
                {
                    _analyzers.Add(type, AnalysisTypeHelper.GetAnalyzer(type));
                }
            }

        }


        private void LoadLastEntries(){

            foreach (var type in analysisTypes)
            {
                var date = getLastEntry(type);
                assetTimeframe.AddLastDbEntry(type, date);
            }

        }

        private DateTime? getLastEntry(AnalysisType type)
        {
            return new DateTime();
        }

        private DateTime findEarliestRequiredQuotation()
        {

            LoadLastEntries();

            return new DateTime();
        }

        
        public bool Run(bool fromScratch)
        {

            if (assetTimeframe == null || !assetTimeframe.isValid()) throw new Exception("Asset is required");

            assetTimeframe.LoadRequiredQuotations();

            //_priceAnalyzer.Analyze(asset.Name, true);
            //_macdAnalyzer.Analyze(symbol, false);
            return false;

        }

    }

}
