using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class AdxAnalyzer : Analyzer, IAdxAnalyzer
    {


        public override AnalysisType Type
        {
            get { return AnalysisType.Adx; }
        }


        private IAnalysisDataService _dataService;
        


        public AdxAnalyzer(AssetTimeframe atf) : base(atf)
        {
            initialize_specific();
        }

        public AdxAnalyzer(IAnalysisDataService dataService, AssetTimeframe atf)
            : base(atf)
        {
            initialize_specific();
            this._dataService = dataService;
        }


        protected override void initialize_specific()
        {
            ItemsForAnalysis = 300;
        }


        protected override IAnalyzerProcessor getProcessor()
        {
            //if (processor == null) processor = new adxpro(this);
            //return processor;
            return null;
        }



        public override void Analyze(DataItem[] items)
        {
            ;
        }


    }


}
