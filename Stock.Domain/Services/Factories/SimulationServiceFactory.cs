using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class SimulationServiceFactory : ISimulationServiceFactory{

        //private static SimulationServiceFactory _instance;
        private static ISimulationService _service;


        public ISimulationService GetService()
        {
            if (_service == null)
            {
                _service = new SimulationService(null);
            }

            return _service;

        }


    }
}