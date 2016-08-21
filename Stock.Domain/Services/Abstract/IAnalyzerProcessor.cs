using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalyzerProcessor
    {
        void runRightSide(IAnalyzer analyzer, int index, AssetTimeframe atf);
        void runRightSide(IAnalyzer analyzer, DataItem item, AssetTimeframe atf);
        void runFull(IAnalyzer analyzer, int index, AssetTimeframe atf);
        void runFull(IAnalyzer analyzer, DataItem item, AssetTimeframe atf);
    }
}
