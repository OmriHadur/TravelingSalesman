using System.Diagnostics;
using TravelingSalesman.Common;

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
    public void TestCurrect()
    {
        var result = _travelingSalesman.GetMinLength(InputFactory.GetFixed(), 0, 3);
        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(10)]
    public void TestRandom(int size)
    {
        var connections = InputFactory.GetRandom(size);
        var result = _travelingSalesman.GetMinLength(connections, 0, size - 1);
        Assert.That(result, Is.LessThan(int.MaxValue));
    }

    [Test]
    [TestCase(5, 1_000, 25)]
    [TestCase(10, 1, 50)]
    public void TestPerformance(int size, int repetitions, int elapsedMilliseconds)
    {
        var connections = InputFactory.GetRandom(size);
        _travelingSalesman.GetMinLength(connections, 0, size - 1);

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < repetitions; i++)
            _travelingSalesman.GetMinLength(connections, 0, size - 1);

        sw.Stop();
        Assert.That(sw.ElapsedMilliseconds, Is.LessThan(elapsedMilliseconds));
    }
}