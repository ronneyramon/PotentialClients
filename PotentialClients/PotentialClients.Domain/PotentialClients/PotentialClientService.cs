using PotentialClients.Domain.PotentialClients.Dtos;
using PotentialClients.Domain.ScoreCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialClients.Domain.PotentialClients
{
    public interface IPotentialClientService
    {
        public Task<IEnumerable<PotentialClient>> ImportPotentialClients(IEnumerable<PotentialClientDto> potentialClientsDtos);
        Task AddPotentialClient(PotentialClientDto potentialClientDto);
    }

    class PotentialClientService : IPotentialClientService
    {
        private readonly IPotentialClientRepository _potentialClientRepository;
        private readonly IScoreCalculator _scoreCalculator;

        public PotentialClientService(IPotentialClientRepository potentialClientRepository, IScoreCalculator scoreCalculator)
        {
            this._potentialClientRepository = potentialClientRepository;
            this._scoreCalculator = scoreCalculator;
        }

        public async Task AddPotentialClient(PotentialClientDto potentialClientDto)
        {
            PotentialClient potentialClient = new PotentialClient(potentialClientDto.PersonId, potentialClientDto.FirstName, potentialClientDto.LastName, potentialClientDto.CurrentRole, potentialClientDto.Country, potentialClientDto.Industry, potentialClientDto.NumberOfRecommendations, potentialClientDto.NumberOfConnections);

            await potentialClient.CalculateScore(_scoreCalculator);

            await _potentialClientRepository.Add(potentialClient);

        }

        public async Task<IEnumerable<PotentialClient>> ImportPotentialClients(IEnumerable<PotentialClientDto> potentialClientsDtos)
        {
            var potentialClients = potentialClientsDtos.Select(p => new PotentialClient(
                p.PersonId,
                p.FirstName,
                p.LastName,
                p.CurrentRole,
                p.Country,
                p.Industry,
                p.NumberOfRecommendations,
                p.NumberOfConnections
                )).ToList();

            //TODO: Do in parallel, but carefully
            foreach (var potentialClient in potentialClients)
            {
                await potentialClient.CalculateScore(_scoreCalculator);
            }

            await _potentialClientRepository.BulkInsert(potentialClients);

            return potentialClients;

        }
    }
}
