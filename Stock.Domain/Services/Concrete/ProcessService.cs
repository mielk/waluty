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
using Stock.Domain.Services.Factories;
using Stock.Core;

namespace Stock.Domain.Services
{

    public class ProcessService : IProcessService
    {

        private AssetTimeframe assetTimeframe;
        private Dictionary<AnalysisType, IAnalyzer> analyzers;
        private IQuotationService3 quotationService = ProcessServiceFactory.Instance().GetQuotationService();
        private DataItem[] dataItems;


        // Getter methods. //
        public Asset getAsset()
        {
            if (assetTimeframe != null)
            {
                return assetTimeframe.GetAsset();
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
                return assetTimeframe.GetTimeframe();
            }
            else
            {
                return null;
            }
        }
        public Dictionary<AnalysisType, IAnalyzer> getAnalyzers() { 
            return analyzers; 
        }


        public ProcessService(Asset asset, Timeframe timeframe)
        {
            this.assetTimeframe = new AssetTimeframe(asset, timeframe);
        }
        public ProcessService(AssetTimeframe atf)
        {
            this.assetTimeframe = atf;
        }


        #region Setup

            public void Setup(AnalysisType[] types)
            {
                checkProperties();
                analyzers = AnalyzerFactory.Instance().getAnalyzers(assetTimeframe, new List<AnalysisType>(types));
            }

            public void Setup(IEnumerable<AnalysisType> types)
            {
                checkProperties();
                analyzers = AnalyzerFactory.Instance().getAnalyzers(assetTimeframe, new List<AnalysisType>(types));
            }

            public void loadAnalyzers(Dictionary<AnalysisType, IAnalyzer> analyzers)
            {
                this.analyzers = analyzers;
            }

            public void injectQuotationService(IQuotationService3 instance)
            {
                quotationService = instance;
            }

            private void checkProperties(){
                if (assetTimeframe == null) throw new ArgumentNullException("AssetTimeframe is empty");
                if (assetTimeframe.GetAsset() == null) throw new ArgumentNullException("Asset is empty");
                if (assetTimeframe.GetTimeframe() == null) throw new ArgumentNullException("Timeframe is empty");
            }

        #endregion Setup


        public bool Run(bool fromScratch)
        {

            //Check if all necessary properties are properly loaded.
            checkProperties();
            if (analyzers.Count == 0) throw new ArgumentNullException("Analyzers are not set");


            //Get last date for each analysis and then find the earliest quotation date required
            //to calculate each analysis types.
            dataItems = quotationService.fetchData(analyzers);
            if (dataItems.Length == 0) return false;
            

            //If any data have been loaded, process them by all of assigned analyzers.
            foreach (var analyzer in analyzers.Values)
            {
                analyzer.Analyze(dataItems);
            }


            return true;


        }


    }

}