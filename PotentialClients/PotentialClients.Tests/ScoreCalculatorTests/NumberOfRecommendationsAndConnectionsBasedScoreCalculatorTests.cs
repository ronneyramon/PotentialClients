using NUnit.Framework;
using PotentialClients.Domain.PotentialClients;
using PotentialClients.Domain.ScoreCalculators;

namespace PotentialClients.Tests
{
    public class NumberOfRecommendationsAndConnectionsBasedScoreCalculatorTests
    {
        private NumberOfRecommendationsAndConnectionsBasedScoreCalculator _calculator;
        static object[] TestCases => new object[]{
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",0,0), 0},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",1,0), 5},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",1,100), 15},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",0,100), 10},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",10,0), 50},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",11,500), 100},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",20,1000), 100},
            new object[] { new PotentialClient(1,"Test","Test","Test","Test","Test",0,500), 50},

        };

        [SetUp]
        public void Setup()
        {
            _calculator = new NumberOfRecommendationsAndConnectionsBasedScoreCalculator();
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async System.Threading.Tasks.Task Test(PotentialClient potentialClient, int expectedScore)
        {
            var score = await _calculator.CalculateScore(potentialClient);

            Assert.AreEqual(expectedScore, score);
        }
    }
}