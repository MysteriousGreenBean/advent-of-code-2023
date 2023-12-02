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
            {
                string[] splitLines = line.Split(':', ';');
                int id = int.Parse(splitLines[0].Replace("Game ", string.Empty));

                var draws = new Draw[splitLines.Length - 1];
                for (int i = 1; i < splitLines.Length; i++)
                {
                    string[] splitDraws = splitLines[i].Split(", ");
                    int red = 0,  green = 0, blue = 0;
                    foreach (string drawPart in splitDraws)
                    {
                        if (drawPart.Contains("red"))
                            red = int.Parse(drawPart.Replace(" red", string.Empty));
                        else if (drawPart.Contains("green"))
                            green = int.Parse(drawPart.Replace(" green", string.Empty));
                        else if (drawPart.Contains("blue"))
                            blue = int.Parse(drawPart.Replace(" blue", string.Empty));
                    }
                    var draw = new Draw { Red = red, Green = green, Blue = blue };
                    draws[i - 1] = draw;
                }

                games.Add(new Game { ID = id, Draws = draws });
            }

            int idSum = games.Where(game => game.IsPossible(12, 13, 14)).Sum(game => game.ID);
            Console.WriteLine(idSum);
        }
    }
}