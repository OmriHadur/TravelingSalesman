namespace TravelingSalesman.Common
{
    public interface ITravelingSalesmanSolver
    {
        int GetMinLength(int[,] connections, int start, int end);
    }
}