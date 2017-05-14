using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public interface IExtremumProcessor
    {
        bool IsExtremum(DataSet dataSet, ExtremumType type);
        void InjectProcessManager(IProcessManager manager);
        double CalculateEarlierAmplitude(Extremum extremum);
        int CalculateEarlierCounter(Extremum extremum);
        double CalculateEarlierChange(Extremum extremum, int units);
    }
}
