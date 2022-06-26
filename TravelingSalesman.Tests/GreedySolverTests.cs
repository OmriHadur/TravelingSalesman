using TravelingSalesman.Engine;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Tests;

public class GreedySolverTests : BaseTravelingSalesmanSolverUnitTests
{
    protected override ITravelingSalesmanSolver GetTravelingSalesmanSolver()
    {
        return new GreedySolver(2);
    }
}