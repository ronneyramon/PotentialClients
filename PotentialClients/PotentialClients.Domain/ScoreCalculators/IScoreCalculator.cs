using PotentialClients.Domain.PotentialClients;
using System.Threading.Tasks;

namespace PotentialClients.Domain.ScoreCalculators
{
    public interface IScoreCalculator
    {
        Task<int> CalculateScore(PotentialClient potentialClient);
    }
}
