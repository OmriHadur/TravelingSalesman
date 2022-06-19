using TravelingSalesman.Common;

namespace TravelingSalesman;

public class TravelingSalesmanSolver : ITravelingSalesmanSolver
{
    public int GetMinLength(int[,] connections, int start, int end)
    {
        return GetMinLength(new Connections(connections), start, end);
    }

    public static int GetMinLength(Connections connections, int start, int end)
    {
        var bestResult = int.MaxValue;
        var currentPath = GetPath(connections, start);

        GetPathsParallel(connections, currentPath, end, path =>
        {
            var length = path.GetLength(connections);
            if (length < bestResult)
                bestResult = length;
        });
        return bestResult;
    }

    private static void GetPathsParallel(IConnections connections, IPath currentPath, int end, Action<IPath> pathFound)
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
                    GetPaths(connections, path, end, pathFound);
            }
        });
    }

    private static void GetPaths(IConnections connections, IPath currentPath, int end, Action<IPath> pathFound)
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
                    GetPaths(connections, currentPath, end, pathFound);
                currentPath.RemoveLast();
            }
        }
    }

    private static bool IsCanVisit(IConnections connections, IPath currentPath, int nextPosition)
    {
        return !currentPath.IsVisited(nextPosition) &&
            connections.HasConnection(currentPath.LastVisited, nextPosition);
    }

    private static Path GetPath(Connections connections, int start)
    {
        var currentPath = new Path(connections.Length);
        currentPath.AddVisit(start);
        return currentPath;
    }
}
