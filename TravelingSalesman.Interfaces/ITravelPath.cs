namespace TravelingSalesman.Interfaces;

public interface ITravelPath : ICloneable
{
    int Start { get; }

    bool IsVisitedAll { get; }

    int LastVisited { get; }

    void AddVisit(int position);

    int Distace { get; }

    bool IsVisited(int position);

    void RemoveLast();

    IEnumerable<int> Path { get; }
}