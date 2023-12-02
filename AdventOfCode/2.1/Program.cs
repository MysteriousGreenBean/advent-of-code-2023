namespace _2._1
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

            int idSum = games.Where(game => game.IsPossible(12, 13, 14)).Sum(game => game.ID);
            Console.WriteLine(idSum);
        }
    }
}