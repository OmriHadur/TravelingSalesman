using Newtonsoft.Json;

namespace TravelingSalesman;

public class InputFactory
{
    public static int[,] GetFromFile(string fileName)
    {
        var fileContent = File.ReadAllText(@".\Inputs\" + fileName + ".json");
        return JsonConvert.DeserializeObject<int[,]>(fileContent);
    }

    public static int[,] GetRandom(int size)
    {
        var random = new Random();
        var connections = new int[size, size];
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                connections[x, y] = random.Next(size) + 1;
        return connections;
    }
}
