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
        private IEnumerable<AnalysisType> analysisTypes;
        private Dictionary<AnalysisType, IAnalyzer> analyzers;
        private DataItem[] dataItems;


        // Getter methods. //
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
        public Dictionary<AnalysisType, IAnalyzer> getAnalyzers() { 
            return analyzers; 
        }



        public void Setup(Asset asset, Timeframe timeframe, AnalysisType[] types)
        {

            if (asset == null) throw new ArgumentNullException("Asset is empty");
            if (timeframe == null) throw new ArgumentNullException("Timeframe is empty");
            assetTimeframe = new AssetTimeframe(asset, timeframe);

            analyzers = AnalyzerFactory.Instance().getAnalyzers(assetTimeframe, new List<AnalysisType>(types));

        }



        public bool Run(bool fromScratch)
        {

            //Check if all necessary properties are properly loaded.
            if (assetTimeframe == null) throw new ArgumentNullException("AssetTimeframe is empty");
            if (assetTimeframe.asset == null) throw new ArgumentNullException("Asset is empty");
            if (assetTimeframe.timeframe == null) throw new ArgumentNullException("Timeframe is empty");
            if (analyzers.Count == 0) throw new ArgumentNullException("Analyzers are not set");


            //Get last date for each analysis and then find the earliest quotation date required
            //to calculate each analysis types.
            IQuotationService qService = ProcessServiceFactory.Instance().GetQuotationService();
            qService.Setup(assetTimeframe, analyzers);
            dataItems = qService.fetchData();
            if (dataItems.Length == 0) return false;
            

            //If any data have been loaded, process them by all of assigned analyzers.
            foreach (var analyzer in analyzers.Values)
            {
                analyzer.Analyze();
            }


            return true;


        }


    }

}