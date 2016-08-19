using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IPriceProcessor
    {
        void Process(IPriceAnalyzer analyzer, DataItem item);
    }
}
