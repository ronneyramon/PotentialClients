using Microsoft.Extensions.DependencyInjection;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.ScoreCalculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace PotentialClients.Domain
{
    public static class DomainModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPotentialClientService, PotentialClientService>();
            services.AddScoped<IScoreCalculator, NumberOfRecommendationsAndConnectionsBasedScoreCalculator>();
        }
    }
}
