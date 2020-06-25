using PotentialClients.Domain.PotentialClients.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PotentialClients.Domain.PotentialClients
{
    public interface IPotentialClientRepository
    {
        Task BulkInsert(IEnumerable<PotentialClient> potentialClients);
        Task<IEnumerable<PotentialClientIdDto>> GetTopClientIds(int top);
        Task<PotentialClientPositionDto> GetClientPosition(long personId);
        Task Add(PotentialClient potentialClient);
    }
}
