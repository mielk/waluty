using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Repositories;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class SimulationService : ISimulationService
    {

        private ISimulationRepository _repository;
        private IEnumerable<Simulation> simulations = new List<Simulation>();


        #region INFRASTRUCTURE

        public SimulationService(ISimulationRepository repository)
        {
            _repository = repository;
        }

        public void InjectRepository(ISimulationRepository repository)
        {
            _repository = repository;
        }

        #endregion INFRASTRUCTURE


        #region API


        public IEnumerable<Simulation> GetSimulations()
        {
            var dtos = _repository.GetSimulations();
            return GetSimulations(dtos);
        }

        private IEnumerable<Simulation> GetSimulations(IEnumerable<SimulationDto> dtos)
        {
            List<Simulation> result = new List<Simulation>();
            foreach (var dto in dtos)
            {
                Simulation simulation = simulations.SingleOrDefault(s => s.Id == dto.Id);
                if (simulation == null)
                {
                    simulation = Simulation.FromDto(dto);
                    appendLastUpdates(simulation);
                    appendSimulation(simulation);
                }
                result.Add(simulation);
            }
            return result;
        }

        public void Update(Simulation simulation)
        {
            SimulationDto dto = simulation.ToDto();
            IEnumerable<AnalysisTimestampDto> timestampDtos = simulation.GetAnalysisTimestampDtos();
            _repository.UpdateSimulations(new SimulationDto[] { dto });
            _repository.UpdateAnalysisTimestamps(timestampDtos);
        }

        private void appendLastUpdates(Simulation simulation)
        {
            var dtos = _repository.GetAnalysisTimestampsForSimulation(simulation.Id);
            foreach (var dto in dtos)
            {
                simulation.AddLastUpdate(dto);
            }
        }

        public Simulation GetSimulationById(int id){
            var simulation = simulations.SingleOrDefault(m => m.Id == id);
            if (simulation == null)
            {
                var dto = _repository.GetSimulationById(id);
                if (dto != null)
                {
                    simulation = Simulation.FromDto(dto);
                    appendLastUpdates(simulation);
                    appendSimulation(simulation);
                }
            }
            return simulation;
        }

        private void appendSimulation(Simulation simulation)
        {
            simulations = simulations.Concat(new[] { simulation });
        }

        
        #endregion API


    }
}
