using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ISimulationService
    {
        void Increment();
        int GetValue();
        //bool Run(bool fromScratch);
    }
}
