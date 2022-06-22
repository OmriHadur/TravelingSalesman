namespace TravelingSalesman.Interfaces;

public interface ITravelingSalesmanSolver
{
    ITravelPath GetMinimumTravel(ITravelConnections travelConnections, int start);
}