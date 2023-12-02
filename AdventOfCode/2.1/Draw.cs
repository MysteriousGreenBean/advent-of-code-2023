namespace _2._1
{
    internal class Draw
    {
        public required int Red { get; init; }
        public required int Green { get; init; }
        public required int Blue { get; init; }

        public bool IsPossible(int availableRed, int availableGreen, int availableBlue)
        {
            if (availableRed < Red || availableGreen < Green || availableBlue < Blue)
                return false;
            return true;
        }
    }
}
