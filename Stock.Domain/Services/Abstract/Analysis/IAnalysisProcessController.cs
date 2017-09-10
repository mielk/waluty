using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public interface IAnalysisProcessController
    {
        void Run(IProcessManager manager);
    }
}
