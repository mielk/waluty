using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalyzer
    {
        //Basic.
        //void injectAsset(Asset asset);
        //void injectTimeframe(Timeframe timeframe);
        //void injectAssetTimeframe(AssetTimeframe assetTimeframe);
        Asset getAsset();
        Timeframe getTimeframe();
        AssetTimeframe getAssetTimeframe();
        AnalysisType getAnalysisType();



        DateTime? getLastCalculationDate();
        DateTime? getFirstRequiredDate();
        void setLastCalculationDate(DateTime? date);
        void Analyze(DataItem[] items);
        void injectDaysForAnalysis(int value);
        DataItem getDataItem(int index);
        int getDataItemsLength();
        IEnumerable<DataItem> getDataItems();
    }
}