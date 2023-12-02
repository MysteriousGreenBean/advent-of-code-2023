namespace _2._1
{
    internal class Draw
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public Draw(string drawLine)
        {
            string[] splitDraws = drawLine.Split(", ");
            foreach (string drawPart in splitDraws)
            {
                if (drawPart.Contains("red"))
                    Red = int.Parse(drawPart.Replace(" red", string.Empty));
                else if (drawPart.Contains("green"))
                    Green = int.Parse(drawPart.Replace(" green", string.Empty));
                else if (drawPart.Contains("blue"))
                    Blue = int.Parse(drawPart.Replace(" blue", string.Empty));
            }
        }

        public bool IsPossible(int availableRed, int availableGreen, int availableBlue)
        {
            if (availableRed < Red || availableGreen < Green || availableBlue < Blue)
                return false;
            return true;
        }
    }
}
