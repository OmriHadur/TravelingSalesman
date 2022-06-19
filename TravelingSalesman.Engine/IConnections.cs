namespace TravelingSalesman
{
    public interface IConnections
    {
        int Length { get; }

        int GetConnection(int from, int to);

        bool HasConnection(int from, int to);
    }
}