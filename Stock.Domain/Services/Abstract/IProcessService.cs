using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface IProcessService
    {
        void LoadParams(Asset asset, Timeframe timeframe);
        void LoadParams(string asset, string timeframe);
        void LoadParams(string symbol);
        bool Run(bool fromScratch);
    }
}
