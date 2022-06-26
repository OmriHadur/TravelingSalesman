using TravelingSalesman.Common;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Engine;

public class BruteForceSolver : ITravelingSalesmanSolver
{
    public ITravelPath? GetMinimumTravel(ITravelConnections connections, int start)
    {
        object locky = new();
        var startPath = GetPath(connections, start);
        ITravelPath? bestPath = null; ;

        FindPathsParallel(connections, startPath, path =>
        {
            if (IsBetter(path, bestPath))
                lock (locky)
                    if (IsBetter(path, bestPath))
                        bestPath = (ITravelPath)path.Clone();
        });
        return bestPath;
    }

    private static bool IsBetter(ITravelPath path, ITravelPath? bestPath)
    {
        return bestPath == null || path.Distace < bestPath.Distace;
    }

    private static void FindPathsParallel(ITravelConnections connections, ITravelPath currentPath, Action<ITravelPath> pathFound)
    {
        Parallel.For(0, connections.Length, nextPosition =>
        {
            if (IsCanVisit(connections, currentPath, nextPosition))
            {
                var path = (ITravelPath)currentPath.Clone();
                Visit(connections, path, pathFound, nextPosition);
            }
        });
    }

    private static void FindPaths(ITravelConnections connections, ITravelPath path, Action<ITravelPath> pathFound)
    {
        for (int nextPosition = 0; nextPosition < connections.Length; nextPosition++)
            if (IsCanVisit(connections, path, nextPosition))
                Visit(connections, path, pathFound, nextPosition);
    }

    private static void Visit(ITravelConnections connections, ITravelPath path, Action<ITravelPath> pathFound, int nextPosition)
    {
        path.AddVisit(nextPosition);
        FindPaths(connections, path, pathFound, nextPosition);
        path.RemoveLast();
    }

    private static void FindPaths(ITravelConnections connections, ITravelPath path, Action<ITravelPath> pathFound, int nextPosition)
    {
        if (path.IsVisitedAll)
        {
            if (connections.HasConnection(nextPosition, path.Start))
                pathFound(path);
        }
        else
            FindPaths(connections, path, pathFound);
    }

    private static bool IsCanVisit(ITravelConnections connections, ITravelPath currentPath, int nextPosition)
    {
        return !currentPath.IsVisited(nextPosition) &&
            connections.HasConnection(currentPath.LastVisited, nextPosition);
    }

    private static ITravelPath GetPath(ITravelConnections connections, int start)
    {
        return new TravelPath(connections, start);
    }
}
