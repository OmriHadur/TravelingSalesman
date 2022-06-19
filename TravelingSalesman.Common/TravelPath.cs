using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Common;

public class TravelPath : ITravelPath
{
    private readonly int[] _path;

    private readonly bool[] _visited;

    private int _currentIndex;

    public TravelPath(int size, int start)
    {
        _path = new int[size];
        _visited = new bool[size];
        AddVisit(start);
    }

    private TravelPath(int[] path, bool[] visited, int currentIndex)
    {
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

    public int GetTravel(ITravelConnections connections)
    {
        var sum = 0;
        for (int i = 0; i < _path.Length - 1; i++)
            sum += connections.GetConnection(_path[i], _path[i + 1]);
        sum += connections.GetConnection(_path[^1], _path[0]);
        return sum;
    }

    public object Clone()
    {
        return new TravelPath(_path, _visited, _currentIndex);
    }
}
