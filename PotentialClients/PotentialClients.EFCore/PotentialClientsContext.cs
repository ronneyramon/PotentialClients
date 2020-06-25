using Microsoft.EntityFrameworkCore;
using PotentialClients.Domain.PotentialClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace PotentialClients.EFCore
{
    public class PotentialClientsContext : DbContext
    {
        public DbSet<PotentialClient> PotentialClients { get; set; }
        public DbSet<PotentialClientPosition> PotentialClientPositions { get; set; }

        public PotentialClientsContext(DbContextOptions<PotentialClientsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PotentialClientPosition>(
                e =>
                {
                    e.HasNoKey();
                    e.ToView("View_PotentialClientPositions");
                });
        }

    }
}
