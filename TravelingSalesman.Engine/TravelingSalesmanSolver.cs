using TravelingSalesman.Common;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Engine;

public class TravelingSalesmanSolver : ITravelingSalesmanSolver
{
    public int GetMinimumTravel(int[,] connections, int start)
    {
        return GetMinimumTravel(new TravelConnections(connections), start);
    }

    public static int GetMinimumTravel(TravelConnections connections, int start)
    {
        var bestResult = int.MaxValue;
        var currentPath = GetPath(connections, start);

        FindPathsParallel(connections, currentPath, path =>
        {
            bestResult = GetBestResult(connections, path, bestResult);
        });
        return bestResult;
    }

    private static int GetBestResult(TravelConnections connections, ITravelPath path, int bestResult)
    {
        var travelLength = path.GetTravel(connections);
        if (travelLength < bestResult)
            bestResult = travelLength;
        return bestResult;
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

    private static ITravelPath GetPath(TravelConnections connections, int start)
    {
        return new TravelPath(connections.Length, start);
    }
}
