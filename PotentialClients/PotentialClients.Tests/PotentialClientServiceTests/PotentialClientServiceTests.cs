using Moq;
using NUnit.Framework;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.PotentialClients.Dtos;
using PotentialClients.Domain.ScoreCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotentialClients.Tests.PotentialClientServiceTests
{
    public class PotentialClientServiceTests
    {
        static object[] TestCases => new object[]{
            new object[] { new List<PotentialClientDto>{
                new PotentialClientDto
                {
                    PersonId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Country = "Brazil",
                    CurrentRole = "CTO",
                    Industry = "Petrol",
                    NumberOfConnections = 100,
                    NumberOfRecommendations = 5
                },
                new PotentialClientDto
                {
                    PersonId = 2,
                    FirstName = "Mary",
                    LastName = "Jane",
                    Country = "USA",
                    CurrentRole = "CEO",
                    Industry = "IT",
                    NumberOfConnections = 10,
                    NumberOfRecommendations = 8
                }
            }
            },


        };

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async System.Threading.Tasks.Task ImportPotentialClients_Should_Call_CalculateScore_For_All_PotentialClients(IEnumerable<PotentialClientDto> potentialClientDtos)
        {
            //Arrange
            var repository = new Mock<IPotentialClientRepository>();

            IList<PotentialClient> potentialClientsWithMethodCalled = new List<PotentialClient>();
            var scoreCalculator = new Mock<IScoreCalculator>();
            scoreCalculator.Setup(c => c.CalculateScore(Capture.In(potentialClientsWithMethodCalled)));


            var service = new PotentialClientService(repository.Object, scoreCalculator.Object);

            //Act
            var ret = await service.ImportPotentialClients(potentialClientDtos);

            //Assert
            repository.VerifyAll();
            scoreCalculator.VerifyAll();
            CollectionAssert.AreEqual(potentialClientsWithMethodCalled, ret);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async System.Threading.Tasks.Task ImportPotentialClients_Should_Call_Repository_BulkInsert(IEnumerable<PotentialClientDto> potentialClientDtos)
        {
            //Arrange
            IList<PotentialClient> potentialClients = new List<PotentialClient>();

            var repository = new Mock<IPotentialClientRepository>();
            repository.Setup(r => r.BulkInsert(It.Is<IEnumerable<PotentialClient>>(s => s.All(i => potentialClients.Contains(i)))));

            var scoreCalculator = new Mock<IScoreCalculator>();
            scoreCalculator.Setup(s => s.CalculateScore(Capture.In(potentialClients)));

            var service = new PotentialClientService(repository.Object, scoreCalculator.Object);

            //Act

            var ret = await service.ImportPotentialClients(potentialClientDtos);


            //Assert
            repository.VerifyAll();
            scoreCalculator.VerifyAll();

        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async System.Threading.Tasks.Task ImportPotentialClients_Should_Return_PotentialClients_Entities(IEnumerable<PotentialClientDto> potentialClientDtos)
        {
            //Arrange

            IList<IList<PotentialClient>> potentialClients = new List<IList<PotentialClient>>();

            var repository = new Mock<IPotentialClientRepository>();
            repository.Setup(r => r.BulkInsert(Capture.In(potentialClients)));

            var scoreCalculator = new Mock<IScoreCalculator>();


            var service = new PotentialClientService(repository.Object, scoreCalculator.Object);

            //Act

            var ret = await service.ImportPotentialClients(potentialClientDtos);

            //Assert

            CollectionAssert.AreEqual(potentialClients.First(), ret);

        }

    }
}
