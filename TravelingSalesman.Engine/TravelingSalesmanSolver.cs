using TravelingSalesman.Common;
using TravelingSalesman.Interfaces;
using Path = TravelingSalesman.Common.Path;

namespace TravelingSalesman.Engine;

public class TravelingSalesmanSolver : ITravelingSalesmanSolver
{
    public int GetMinimumTravel(int[,] connections, int start, int end)
    {
        return GetMinimumTravel(new Connections(connections), start, end);
    }

    public static int GetMinimumTravel(Connections connections, int start, int end)
    {
        var bestResult = int.MaxValue;
        var currentPath = GetPath(connections, start);

        FindPathsParallel(connections, currentPath, end, path =>
        {
            var length = path.GetTravel(connections);
            if (length < bestResult)
                bestResult = length;
        });
        return bestResult;
    }

    private static void FindPathsParallel(IConnections connections, IPath currentPath, int end, Action<IPath> pathFound)
    {
        Parallel.For(0, connections.Length, nextPosition =>
        {
            if (IsCanVisit(connections, currentPath, nextPosition))
            {
                var path = (IPath)currentPath.Clone();
                path.AddVisit(nextPosition);
                if (path.IsVisitedAll)
                {
                    if (nextPosition == end)
                        pathFound(path);
                }
                else
                    FindPaths(connections, path, end, pathFound);
            }
        });
    }

    private static void FindPaths(IConnections connections, IPath currentPath, int end, Action<IPath> pathFound)
    {
        for (int nextPosition = 0; nextPosition < connections.Length; nextPosition++)
        {
            if (IsCanVisit(connections, currentPath, nextPosition))
            {
                currentPath.AddVisit(nextPosition);
                if (currentPath.IsVisitedAll)
                {
                    if (nextPosition == end)
                        pathFound(currentPath);
                }
                else
                    FindPaths(connections, currentPath, end, pathFound);
                currentPath.RemoveLast();
            }
        }
    }

    private static bool IsCanVisit(IConnections connections, IPath currentPath, int nextPosition)
    {
        return !currentPath.IsVisited(nextPosition) &&
            connections.HasConnection(currentPath.LastVisited, nextPosition);
    }

    private static IPath GetPath(Connections connections, int start)
    {
        var currentPath = new Path(connections.Length);
        currentPath.AddVisit(start);
        return currentPath;
    }
}
