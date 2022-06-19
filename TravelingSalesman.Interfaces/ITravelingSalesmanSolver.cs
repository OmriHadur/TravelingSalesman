namespace TravelingSalesman.Interfaces;

public interface ITravelingSalesmanSolver
{
    int GetMinimumTravel(int[,] connections, int start, int end);
}