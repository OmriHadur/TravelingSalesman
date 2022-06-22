using Newtonsoft.Json;
using TravelingSalesman.Common;
using TravelingSalesman.Interfaces;

namespace TravelingSalesman;

public class InputFactory
{
    public static ITravelConnections GetFromFile(string fileName)
    {
        var fileContent = File.ReadAllText(@".\Inputs\" + fileName + ".json");
        var connections = JsonConvert.DeserializeObject<int[,]>(fileContent);
        return new TravelConnections(connections);
    }

    public static ITravelConnections GetRandom(int size)
    {
        var random = new Random();
        var connections = new int[size, size];
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                connections[x, y] = random.Next(size) + 1;
        return new TravelConnections(connections);
    }
}
