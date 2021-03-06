﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ISimulationService
    {
        IEnumerable<Simulation> GetSimulations();
        Simulation GetSimulationById(int id);
        void Update(Simulation simulation);
    }
}
