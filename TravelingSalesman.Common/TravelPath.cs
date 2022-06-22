using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Common;

public class TravelPath : ITravelPath
{
    private readonly ITravelConnections _connections;

    private readonly int[] _path;

    private readonly bool[] _visited;

    private int _currentIndex;

    public TravelPath(ITravelConnections connections, int start)
    {
        _connections = connections;
        _path = new int[connections.Length];
        _visited = new bool[connections.Length];
        AddVisit(start);
    }

    private TravelPath(ITravelConnections connections,
                        int[] path,
                        bool[] visited,
                        int currentIndex)
    {
        _connections = connections;
        _path = (int[])path.Clone();
        _visited = (bool[])visited.Clone();
        _currentIndex = currentIndex;
    }

    public bool IsVisited(int position) => _visited[position];

    public void AddVisit(int position)
    {
        _path[_currentIndex++] = position;
        _visited[position] = true;
    }

    public void RemoveLast()
    {
        _visited[_path[_currentIndex - 1]] = false;
        _currentIndex--;
    }

    public bool IsVisitedAll => _currentIndex == _path.Length;

    public int LastVisited => _path[_currentIndex - 1];

    public int Start => _path[0];

    public int Distace
    {
        get
        {
            var sum = 0;
            for (int i = 0; i < _path.Length - 1; i++)
                sum += _connections.GetConnection(_path[i], _path[i + 1]);
            sum += _connections.GetConnection(_path[^1], _path[0]);
            return sum;
        }
    }

    public IEnumerable<int> Path => _path;

    public object Clone()
    {
        return new TravelPath(_connections, _path, _visited, _currentIndex);
    }
}
