using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.PotentialClients.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PotentialClients.EFCore
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(PotentialClientsContext context, IPotentialClientService potentialClientService)
        {

            //TODO: Use migrations
            context.Database.EnsureCreated();

            if (context.PotentialClients.Any())
            {
                return;
            }            

            var assembly = typeof(DatabaseInitializer).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("PotentialClients.EFCore.people.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JArray jarray = serializer.Deserialize(reader) as JArray;

                    var potentialClients = jarray.Select(x => new PotentialClientDto
                    {
                        PersonId = (long)x["PersonId"],
                        FirstName = (string)x["FirstName"],
                        LastName = (string)x["LastName"],
                        CurrentRole = (string)x["CurrentRole"],
                        Country = (string)x["Country"],
                        Industry = (string)x["Industry"],
                        NumberOfRecommendations = (int?)x["NumberOfRecommendations"],
                        NumberOfConnections = (int?)x["NumberOfConnections"]
                    });

                    await potentialClientService.ImportPotentialClients(potentialClients);

                }
            }

            context.Database.ExecuteSqlRaw(
            @"
            IF OBJECT_ID('dbo.View_PotentialClientPositions', 'V') IS NOT NULL
                DROP VIEW dbo.View_PotentialClientPositions
            ");

            context.Database.ExecuteSqlRaw(
            @"
            CREATE VIEW View_PotentialClientPositions AS 
                SELECT P.PersonId, ROW_NUMBER() OVER   
	                (ORDER BY P.Score DESC) AS Postion  
                FROM [dbo].[PotentialClients] AS P");

        }
    }
}
