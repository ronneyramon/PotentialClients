using System;
using System.Collections.Generic;
using System.Text;

namespace PotentialClients.Domain.PotentialClients.Dtos
{
    public class PotentialClientDto
    {
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentRole { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public int? NumberOfRecommendations { get; set; }
        public int? NumberOfConnections { get; set; }
    }
}
