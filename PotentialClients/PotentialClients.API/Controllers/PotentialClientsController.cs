using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.PotentialClients.Dtos;

namespace PotentialClients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotentialClientsController : ControllerBase
    {
        private readonly IPotentialClientRepository _potentialClientRepository;
        private readonly IPotentialClientService _potentialClientService;

        public PotentialClientsController(IPotentialClientRepository potentialClientRepository, IPotentialClientService potentialClientService)
        {
            this._potentialClientRepository = potentialClientRepository;
            this._potentialClientService = potentialClientService;
        }

        // GET: api/potentialClients/topclients/{top}
        [HttpGet("topclients/{top}")]
        public async Task<IEnumerable<PotentialClientIdDto>> GetTopClients(int top)
        {
            var potentialClients = await _potentialClientRepository.GetTopClientIds(top);

            return potentialClients;
        }

        // GET: api/potentialClients/clientposition/{personId}
        [HttpGet("clientposition/{personId}")]
        public async Task<PotentialClientPositionDto> GetClientPosition(long personId)
        {
            PotentialClientPositionDto position = await _potentialClientRepository.GetClientPosition(personId);

            return position;
        }

        //POST: api/potentialClients
        [HttpPost("")]
        public async Task Post(PotentialClientDto potentialClientDto)
        {
            await _potentialClientService.AddPotentialClient(potentialClientDto);
        }


    }
}
