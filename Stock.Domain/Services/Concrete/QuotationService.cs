using Stock.DAL.Repositories;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Concrete
{
    public class QuotationService : IQuotationService
    {

        private AssetTimeframe assetTimeframe;
        private static IDataRepository dataRepository;
        private Dictionary<AnalysisType, IAnalyzer> analyzers;

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

        public  void Setup(Asset asset, Timeframe timeframe, Dictionary<AnalysisType, IAnalyzer> dictAnalyzers)
        {

            if (asset == null) throw new ArgumentNullException("Asset is empty");
            if (timeframe == null) throw new ArgumentNullException("Timeframe is empty");
            assetTimeframe = new AssetTimeframe(asset, timeframe);

            analyzers = dictAnalyzers;

        }

        public void Setup(AssetTimeframe atf, Dictionary<AnalysisType, IAnalyzer> dictAnalyzers)
        {

            if (atf == null) throw new ArgumentNullException("AssetTimeframe is empty");
            if (atf.asset == null) throw new ArgumentNullException("Asset is empty");
            if (atf.timeframe == null) throw new ArgumentNullException("Timeframe is empty");
            assetTimeframe = atf;

            analyzers = dictAnalyzers;

        }

        public Dictionary<AnalysisType, DateTime> getLastDates()
        {
            var dict = new Dictionary<AnalysisType, DateTime>();
            return dict;
        }

        public DataItem[] fetchData()
        {
            return new DataItem[] { };
        }



    }
}