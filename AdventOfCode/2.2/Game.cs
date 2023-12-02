namespace _2._1
{
    internal class Game
    {
        public int ID { get; }
        public Draw[] Draws { get; }

        public Game(string line)
        {
            string[] splitLines = line.Split(':', ';');
            ID = int.Parse(splitLines[0].Replace("Game ", string.Empty));

            var draws = new Draw[splitLines.Length - 1];
            for (int i = 1; i < splitLines.Length; i++)
            {
                var draw = new Draw(splitLines[i]);
                draws[i - 1] = draw;
            }
            Draws = draws;
        }

        public bool IsPossible(int availableRed, int availableGreen, int availableBlue)
            => Draws.All(draw => draw.IsPossible(availableRed, availableGreen, availableBlue));

        public (int red, int green, int blue) CalculateMinimumColorSet()
        {
            int red = Draws.Max(draw => draw.Red);
            int green = Draws.Max(draw => draw.Green);
            int blue = Draws.Max(draw => draw.Blue);
            return (red, green, blue);
        }
    }
}
