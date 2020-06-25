using PotentialClients.Domain.PotentialClients;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("PotentialClients.Tests")]
namespace PotentialClients.Domain.ScoreCalculators
{
    /// <summary>
    /// Calculates the score of a potential client based on the NumberOfRecommendations and NumberOfConnections.
    /// The result is the sum of these two properties normalized in a range of 0 to 100, being:
    /// 500 considered the "best scenario" for NumberOfConnections, corresponding for 50% of the score
    /// 10 considered the "best scenario" for NumberOfRecommendations, corresponding for 50% of the score
    /// </summary>
    class NumberOfRecommendationsAndConnectionsBasedScoreCalculator : IScoreCalculator
    {
        const double MAX_NUMBER_OF_CONNECTIONS = 500;
        const double MAX_NUMBER_OF_RECOMENDATIONS = 10;
        public Task<int> CalculateScore(PotentialClient potentialClient)
        {
            int score = 0;

            double percentageConnections = (Math.Min(potentialClient.NumberOfConnections.GetValueOrDefault(), MAX_NUMBER_OF_CONNECTIONS) / MAX_NUMBER_OF_CONNECTIONS);

            score += (int)(percentageConnections * 50);

            double percentageRecommendations = (Math.Min(potentialClient.NumberOfRecommendations.GetValueOrDefault(), MAX_NUMBER_OF_RECOMENDATIONS) / MAX_NUMBER_OF_RECOMENDATIONS);
            score += (int)(percentageRecommendations * 50);

            return Task.FromResult(score);
        }
    }
}
