namespace TravelingSalesman
{
    public interface IPath : ICloneable
    {
        bool IsVisitedAll { get; }
        int LastVisited { get; }
        void AddVisit(int position);
        int GetLength(IConnections connections);
        bool IsVisited(int position);
        void RemoveLast();
    }
}