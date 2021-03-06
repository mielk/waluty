﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ISimulationManager : IProcessManager
    {
        void RunByGivenSteps(int steps);
    }
}
