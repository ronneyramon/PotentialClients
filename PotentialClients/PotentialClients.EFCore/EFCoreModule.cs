using Microsoft.Extensions.DependencyInjection;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PotentialClients.EFCore
{
    public static class EFCoreModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPotentialClientRepository, PotentialClientRepository>();           
        }
    }
}
