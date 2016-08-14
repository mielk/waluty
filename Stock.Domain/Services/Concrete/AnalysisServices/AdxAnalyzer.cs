using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class AdxAnalyzer : Analyzer, IAdxAnalyzer
    {

        public override AnalysisType Type
        {
            get { return AnalysisType.ADX; }
        }


        private IAnalysisDataService _dataService;
        


        public AdxAnalyzer(AssetTimeframe atf) : base(atf)
        {
            initialize();
        }

        public AdxAnalyzer(IAnalysisDataService dataService, AssetTimeframe atf)
            : base(atf)
        {
            initialize();
            this._dataService = dataService;
        }


        protected override void initialize()
        {
            DaysForAnalysis = 300;
        }




        public override void Analyze(DataItem[] items)
        {
            ;
        }


    }


}
