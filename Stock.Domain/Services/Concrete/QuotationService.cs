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
        private AnalysisType[] analysisTypes;
        private static IDataRepository dataRepository;
        private Dictionary<AnalysisType, IAnalyzer> _analyzers;

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

        public void Setup(Asset asset, Timeframe timeframe, AnalysisType[] types)
        {

            if (assetTimeframe == null) throw new ArgumentNullException("AssetTimeframe is empty");
            if (assetTimeframe.asset == null) throw new ArgumentNullException("Asset is empty");
            if (assetTimeframe.timeframe == null) throw new ArgumentNullException("Timeframe is empty");

            assetTimeframe = new AssetTimeframe(asset, timeframe);
            analysisTypes = types;

        }


        public DateTime findEarliestRequiredDate(bool fromScratch)
        {
            return new DateTime();
        }

        public DataItem[] loadData(DateTime initialTime)
        {
            return new DataItem[] { };
        }

        public void count(int x)
        {
        }


    }
}