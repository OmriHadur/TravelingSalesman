using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Common;

public class TravelPath : ITravelPath
{
    private readonly ITravelConnections _travelConnections;

    private readonly int[] _path;

    private readonly bool[] _visited;

    private int _currentIndex;

    private int _distace;

    public TravelPath(ITravelConnections travelConnections, int start)
    {
        _travelConnections = travelConnections;
        _path = new int[travelConnections.Length];
        _visited = new bool[travelConnections.Length];
        AddVisit(start);
    }

    private TravelPath( ITravelConnections travelConnections, 
                        int[] path, 
                        bool[] visited, 
                        int currentIndex,
                        int distace)
    {
        _travelConnections = travelConnections;
        _path = (int[])path.Clone();
        _visited = (bool[])visited.Clone();
        _currentIndex = currentIndex;
        _distace = distace;
    }

    public bool IsVisited(int position) => _visited[position];

    public void AddVisit(int position)
    {
        if (_currentIndex > 0)
            _distace += _travelConnections.GetConnection(LastVisited, position);
        _path[_currentIndex++] = position;
        _visited[position] = true;
        if (IsVisitedAll)
            _distace += _travelConnections.GetConnection(LastVisited, Start);
    }

    public void RemoveLast()
    {
        _distace -= _travelConnections.GetConnection(_path[_currentIndex - 2], LastVisited);
        _visited[_path[_currentIndex - 1]] = false;
        _currentIndex--;
    }

    public bool IsVisitedAll => _currentIndex == _path.Length;

    public int LastVisited => _path[_currentIndex - 1];

    public int Start => _path[0];

    public int Distace => _distace;

    public IEnumerable<int> Path => _path;

    public object Clone()
    {
        return new TravelPath(_travelConnections, _path, _visited, _currentIndex, _distace);
    }
}
