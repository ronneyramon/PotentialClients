using PotentialClients.Domain.ScoreCalculators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace PotentialClients.Domain.PotentialClients
{
    public class PotentialClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string CurrentRole { get; protected set; }
        public string Country { get; protected set; }
        public string Industry { get; protected set; }
        public int? NumberOfRecommendations { get; protected set; }
        public int? NumberOfConnections { get; protected set; }

        [Range(0,100)]
        public int Score { get; protected set; }

        public PotentialClient(long personId, string firstName, string lastName, string currentRole, string country, string industry, int? numberOfRecommendations, int? numberOfConnections)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            CurrentRole = currentRole;
            Country = country;
            Industry = industry;
            NumberOfRecommendations = numberOfRecommendations;
            NumberOfConnections = numberOfConnections;
        }

        public async Task CalculateScore(IScoreCalculator scoreCalculator) {
            this.Score = await scoreCalculator.CalculateScore(this);
        }
    }
}
