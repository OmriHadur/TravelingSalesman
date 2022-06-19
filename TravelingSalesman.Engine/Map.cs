namespace TravelingSalesman
{
    public class Connections : IConnections
    {
        private readonly int[,] _connections;

        public Connections(int[,] connections)
        {
            _connections = connections;
        }

        public int Length => _connections.GetLength(0);

        public bool HasConnection(int from, int to) => _connections[from, to] != 0;

        public int GetConnection(int from, int to) => _connections[from, to];
    }
}
