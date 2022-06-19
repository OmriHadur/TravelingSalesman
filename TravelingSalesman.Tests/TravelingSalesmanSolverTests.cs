using System.Diagnostics;
using TravelingSalesman.Common;
using TravelingSalesman.Engine;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Tests;

public class TravelingSalesmanSolverUnitTests
{
    private ITravelingSalesmanSolver _travelingSalesman;

    [SetUp]
    public void Setup()
    {
        _travelingSalesman = new TravelingSalesmanSolver();
    }

    [Test]
    [TestCase("Input1",10)]
    public void SolutionIsCurrectTest(string fileName,int expectedMinimumTravel)
    {
        var connections = InputFactory.GetFromFile(fileName);
        var result = _travelingSalesman.GetMinimumTravel(connections, 0, 3);
        Assert.That(result, Is.EqualTo(expectedMinimumTravel));
    }

    [Test]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(10)]
    public void RandomConnectionsTest(int size)
    {
        var connections = InputFactory.GetRandom(size);
        var result = _travelingSalesman.GetMinimumTravel(connections, 0, size - 1);
        Assert.That(result, Is.LessThan(int.MaxValue));
    }

    [Test]
    [TestCase(5, 1000, 25)]
    [TestCase(10, 1, 50)]
    public void PerformanceTest(int size, int repetitions, int elapsedMilliseconds)
    {
        var connections = InputFactory.GetRandom(size);

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < repetitions; i++)
            _travelingSalesman.GetMinimumTravel(connections, 0, size - 1);

        sw.Stop();
        Assert.That(sw.ElapsedMilliseconds, Is.LessThan(elapsedMilliseconds));
    }
}