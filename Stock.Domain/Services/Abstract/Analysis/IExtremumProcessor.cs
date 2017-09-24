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
        void InjectProcessManager(IProcessManager manager);
        bool IsExtremum(DataSet dataSet, ExtremumType type);
        double CalculateEarlierAmplitude(Extremum extremum);
        int CalculateEarlierCounter(Extremum extremum);
        double CalculateEarlierChange(Extremum extremum, int units);
        double CalculateLaterAmplitude(Extremum extremum);
        int CalculateLaterCounter(Extremum extremum);
        double CalculateLaterChange(Extremum extremum, int units);
        double CalculateValue(Extremum extremum);
    }
}
