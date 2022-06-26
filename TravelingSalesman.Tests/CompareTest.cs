using System.Diagnostics;
using TravelingSalesman.Engine;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Tests
{
    public class CompareTest
    {
        private ITravelingSalesmanSolver[] _solvers;

        [SetUp]
        public void Setup()
        {
            _solvers = new ITravelingSalesmanSolver[]
            {
                new GreedySolver(2),
                new BruteForceSolver()
            };
        }

        [Test]
        [TestCase(10,100)]
        [TestCase(12,50)]
        [TestCase(12,100)]
        public void CompareolverTest(int size,int connectionOdds)
        {
            var connections = InputFactory.GetRandom(size, connectionOdds);
            foreach (var sovler in _solvers)
            {
                var sw = Stopwatch.StartNew();
                var bestResult = sovler.GetMinimumTravel(connections, 0);
                sw.Stop();
                Console.WriteLine(
                    $"{sovler.GetType().Name} " +
                    $"Result: {bestResult.Distace} " +
                    $"Time: {sw.ElapsedMilliseconds}");
            }
        }
    }
}
