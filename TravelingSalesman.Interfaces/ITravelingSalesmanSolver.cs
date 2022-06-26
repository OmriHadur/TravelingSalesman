namespace TravelingSalesman.Interfaces;

public interface ITravelingSalesmanSolver
{
    ITravelPath GetMinimumTravel(ITravelConnections connections, int start);
}