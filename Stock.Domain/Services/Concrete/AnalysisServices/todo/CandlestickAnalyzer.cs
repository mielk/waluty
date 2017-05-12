﻿using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class CandlestickAnalyzer : Analyzer, ICandlestickAnalyzer
    {
        public override AnalysisType Type
        {
            get { return AnalysisType.Candlesticks; }
        }

        private IAnalysisDataService _dataService;





        public CandlestickAnalyzer(AssetTimeframe atf)
            : base(atf)
        {
        }


        public CandlestickAnalyzer(IAnalysisDataService dataService, AssetTimeframe atf)
            : base(atf)
        {
            this._dataService = dataService;
        }

        public void injectDataService(IAnalysisDataService dataService)
        {
            _dataService = dataService;
        }


        protected override IAnalyzerProcessor getProcessor()
        {
            //if (processor == null) processor = new PriceProcessor(this);
            //return processor;
            return null;
        }

        protected override void initialize_specific()
        {
            ItemsForAnalysis = 300;
        }






        public override void Analyze(DataItem[] items)
        {
            
        }


    }

}