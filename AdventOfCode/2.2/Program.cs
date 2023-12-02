using _2._1;

namespace _2._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Provide path to input file:");
            string? inputFilePath = Console.ReadLine();
            if (!File.Exists(inputFilePath))
                throw new InvalidDataException("Provided file does not exist");

            string[] inputLines = File.ReadAllLines(inputFilePath);
            var games = new List<Game>(inputLines.Length);

            foreach (string line in inputLines)
                games.Add(new Game(line));

            int powerSum = 0;
            foreach (Game game in games)
            {
                (int red, int green, int blue) = game.CalculateMinimumColorSet();
                powerSum += red * green * blue;
            }
            Console.WriteLine(powerSum);
        }
    }
}