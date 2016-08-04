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
using Stock.Domain.Services.Abstract;
using Stock.Domain.Services.Factories;

namespace Stock.Domain.Services
{
    public class ProcessService : IProcessService
    {

        private AssetTimeframe assetTimeframe;
        private AnalysisType[] analysisTypes;
        //private static IDataRepository dataRepository;
        private Dictionary<AnalysisType, IAnalyzer> _analyzers;
        private DataItem[] dataItems;

        public Asset getAsset()
        {
            if (assetTimeframe != null)
            {
                return assetTimeframe.asset;
            }
            else
            {
                return null;
            }
        }
        public Timeframe getTimeframe()
        {
            if (assetTimeframe != null)
            {
                return assetTimeframe.timeframe;
            }
            else
            {
                return null;
            }
        }

        //public static void injectService(IDataRepository _repository)
        //{
        //    dataRepository = _repository;
        //}

        //public ProcessService(IDataRepository _dataRepository)
        //{
        //    dataRepository = _dataRepository ?? RepositoryFactory.GetDataRepository();
        //}



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
                    _analyzers.Add(type, AnalysisTypeHelper.GetAnalyzer(getAsset(), getTimeframe(), type));
                }
            }

        }

        public void Setup(Asset asset, Timeframe timeframe, AnalysisType[] types)
        {

            if (asset == null) throw new ArgumentNullException("Asset is empty");
            if (timeframe == null) throw new ArgumentNullException("Timeframe is empty");

            assetTimeframe = new AssetTimeframe(asset, timeframe);
            analysisTypes = types;

        }


        public bool Run(bool fromScratch)
        {

            if (assetTimeframe == null) throw new ArgumentNullException("AssetTimeframe is empty");
            if (assetTimeframe.asset == null) throw new ArgumentNullException("Asset is empty");
            if (assetTimeframe.timeframe == null) throw new ArgumentNullException("Timeframe is empty");

            loadAnalyzers();

            IQuotationService qService = ProcessServiceFactory.Instance().GetQuotationService();
            DateTime earliestRequired = qService.findEarliestRequiredDate(fromScratch);
            dataItems = qService.loadData(earliestRequired);



            if (dataItems.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

    }

}
