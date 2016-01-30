using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IPriceAnalyzer
    {
        void Analyze(string symbol);
        void Analyze(string symbol, bool fromScratch);
    }
}
