using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PotentialClients.Common;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.PotentialClients.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialClients.EFCore.Repositories
{
    class PotentialClientRepository : IPotentialClientRepository
    {
        private readonly PotentialClientsContext _context;

        public PotentialClientRepository(PotentialClientsContext context)
        {
            this._context = context;
        }

        public async Task BulkInsert(IEnumerable<PotentialClient> potentialClients)
        {
            await _context.BulkInsertAsync(potentialClients.ToList(), new BulkConfig
            {
                BatchSize = 1000
            }, p =>
            {
                Trace.WriteLine($"Importing percentage: {p}");
            });
        }

        public async Task<IEnumerable<PotentialClientIdDto>> GetTopClientIds(int top)
        {
            return await _context.PotentialClients
                .OrderByDescending(p => p.Score)
                .Take(top)
                .Select(p => new PotentialClientIdDto
                {
                    PersonId = p.PersonId
                }).ToListAsync();
        }

        public Task<PotentialClientPositionDto> GetClientPosition(long personId)
        {
            return _context.PotentialClientPositions.Where(p => p.PersonId == personId)
                .Select(p => new PotentialClientPositionDto { Postion = p.Postion } )
                .SingleOrDefaultAsync();           
        }

        public async Task Add(PotentialClient potentialClient)
        {
            try
            {
                await _context.AddAsync(potentialClient);

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException as SqlException;

                if (innerException?.Number == 2627 || innerException?.Number == 2601)
                {
                    throw new UserFriendlyException("PersonId already exists.");
                }
                else
                    throw;

            }
        }
    }
}
