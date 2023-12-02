namespace _2._1
{
    internal class Game
    {
        public required int ID { get; init; }
        public required Draw[] Draws { get; init; }

        public bool IsPossible(int availableRed, int availableGreen, int availableBlue)
            => Draws.All(draw => draw.IsPossible(availableRed, availableGreen, availableBlue));
    }
}
