using System.Diagnostics;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Tests;

public abstract class BaseTravelingSalesmanSolverUnitTests
{
    private ITravelingSalesmanSolver _travelingSalesman;

    [SetUp]
    public void Setup()
    {
        _travelingSalesman = GetTravelingSalesmanSolver();
    }

    [Test]
    [TestCase("Input1", 0, 15)]
    [TestCase("Input2", 0, 64)]
    [TestCase("Input3", 5, 76)]
    public void SolutionIsCurrectTest(string fileName, int startPosition, int expectedMinimumTravel)
    {
        var connections = InputFactory.GetFromFile(fileName);
        var bestPath = _travelingSalesman.GetMinimumTravel(connections, startPosition);
        Assert.That(bestPath.Distace, Is.EqualTo(expectedMinimumTravel));
    }

    [Test]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(10)]
    public void RandomConnectionsTest(int size)
    {
        var connections = InputFactory.GetRandom(size);
        var bestPath = _travelingSalesman.GetMinimumTravel(connections, 0);
        Console.WriteLine(bestPath.Distace);
        Assert.That(bestPath.Distace, Is.LessThan(int.MaxValue));
    }

    [Test]
    [TestCase(5, 1000, 30)]
    [TestCase(10, 1, 70)]
    public void PerformanceTest(int size, int repetitions, int elapsedMilliseconds)
    {
        var connections = InputFactory.GetRandom(size);
        _travelingSalesman.GetMinimumTravel(connections, 0);

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < repetitions; i++)
            _travelingSalesman.GetMinimumTravel(connections, 0);

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        Assert.That(sw.ElapsedMilliseconds, Is.LessThan(elapsedMilliseconds));
    }

    protected abstract ITravelingSalesmanSolver GetTravelingSalesmanSolver();
}