﻿using System;
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
    public class PriceAnalyzer : Analyzer, IPriceAnalyzer
    {
        
        public override AnalysisType Type
        {
            get { return AnalysisType.Prices; }
        }

        /* CONSTRUCTORS */
        public PriceAnalyzer(AssetTimeframe atf) : base(atf)
        {
        }





        protected override void initialize_specific()
        {
            ItemsForAnalysis = 240;
        }

        public void InjectProcessor(IPriceProcessor obj)
        {
            //this.processor = obj;
        }

        protected override IAnalyzerProcessor getProcessor()
        {
            //if (processor == null) processor = new PriceProcessor(this);
            return processor;
        }


    }

}