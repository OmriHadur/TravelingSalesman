using TravelingSalesman.Engine;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Tests;

public class BruteForceSolverTests : BaseTravelingSalesmanSolverUnitTests
{
    protected override ITravelingSalesmanSolver GetTravelingSalesmanSolver()
    {
        return new BruteForceSolver();
    }
}