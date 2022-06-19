namespace TravelingSalesman.Interfaces;

public interface ITravelConnections
{
    int Length { get; }

    int GetConnection(int from, int to);

    bool HasConnection(int from, int to);
}