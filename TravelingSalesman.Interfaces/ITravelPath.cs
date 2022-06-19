namespace TravelingSalesman.Interfaces;

public interface ITravelPath : ICloneable
{
    int Start { get; }

    bool IsVisitedAll { get; }

    int LastVisited { get; }

    void AddVisit(int position);

    int GetTravel(ITravelConnections connections);

    bool IsVisited(int position);

    void RemoveLast();
}