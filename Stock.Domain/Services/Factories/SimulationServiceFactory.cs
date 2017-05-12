using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services.Factories
{
    public class SimulationServiceFactory : ISimulationServiceFactory{

        //private static SimulationServiceFactory _instance;
        private static ISimulationService2 _service;


        public ISimulationService2 GetService()
        {
            if (_service == null)
            {
                _service = new SimulationService2(null);
            }

            return _service;

        }


    }
}