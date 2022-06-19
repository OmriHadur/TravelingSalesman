namespace TravelingSalesman
{
    public class InputFactory
    {
        public static int[,] GetFixed()
        {
            return new int[,]
            {
                { 0, 3, 2, 6, 0 },
                { 3, 0, 1, 0, 4 },
                { 2, 1, 0, 1, 2, },
                { 6, 0, 1, 0, 5 },
                { 0, 4, 2, 5, 0 }
            };
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
}
