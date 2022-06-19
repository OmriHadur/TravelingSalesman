namespace TravelingSalesman.Interfaces;

public interface IPath : ICloneable
{
    bool IsVisitedAll { get; }

    int LastVisited { get; }

    void AddVisit(int position);

    int GetTravel(IConnections connections);

    bool IsVisited(int position);

    void RemoveLast();
}