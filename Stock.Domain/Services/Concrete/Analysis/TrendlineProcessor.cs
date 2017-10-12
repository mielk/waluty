using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public class TrendlineProcessor : ITrendlineProcessor
    {
        private IProcessManager manager;


        #region CONSTRUCTOR

        public TrendlineProcessor(IProcessManager manager)
        {
            this.manager = manager;
        }

        #endregion CONSTRUCTOR


        //#region INFRASTRUCTURE

        //public void InjectExtremumProcessor(IExtremumProcessor processor)
        //{
        //    this.extremumProcessor = processor;
        //    processor.InjectProcessManager(manager);
        //}

        //private IExtremumProcessor getExtremumProcessor()
        //{
        //    if (extremumProcessor == null)
        //    {
        //        extremumProcessor = new ExtremumProcessor(manager);
        //    }
        //    return extremumProcessor;
        //}

        //#endregion INFRASTRUCTURE

        
    }

}