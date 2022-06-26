using TravelingSalesman.Common;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman.Engine
{
    public class GreedySolver : ITravelingSalesmanSolver
    {
        private readonly int _greedyPaths;

        public GreedySolver(int greedyPaths)
        {
            _greedyPaths = greedyPaths;
        }
        public ITravelPath GetMinimumTravel(ITravelConnections connections, int start)
        {
            var path = new TravelPath(connections, start);
            return GetBestPath(connections, path)!;
        }

        private ITravelPath? GetBestPath(ITravelConnections connections, ITravelPath path)
        {
            if (path.IsVisitedAll)
                return connections.HasConnection(path.LastVisited, path.Start) ? path : null;

            var shortestConnections = GetShortestPaths(connections, path)
                .OrderBy(t => t.Item2)
                .Take(_greedyPaths);

            ITravelPath? bestResult = null;
            var bestResultDistance = int.MaxValue;

            foreach (var shortestConnection in shortestConnections)
            {
                path.AddVisit(shortestConnection.Item1);
                var pathOfPosition = GetBestPath(connections, path);
                if (pathOfPosition != null && pathOfPosition.Distace < bestResultDistance)
                {
                    bestResult = (ITravelPath)pathOfPosition.Clone();
                    bestResultDistance = bestResult.Distace;
                }
                path.RemoveLast();
            }
            return bestResult;
        }

        private static IEnumerable<Tuple<int, int>> GetShortestPaths(ITravelConnections connections, ITravelPath path)
        {
            for (int i = 0; i < connections.Length; i++)
                if (!path.IsVisited(i) && connections.HasConnection(path.LastVisited, i))
                    yield return new Tuple<int, int>(i, connections.GetConnection(path.LastVisited, i));
        }
    }
}
