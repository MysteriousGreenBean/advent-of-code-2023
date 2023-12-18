namespace _18._2
{
    internal class DigInstruction
    {
        public DigDirection Direction { get; }
        public int Meters { get; }

        public DigInstruction(string line)
        {
            string[] lineElements = line.Split(' ');
            Direction = (DigDirection)lineElements[2][lineElements[2].Length - 2];
            Meters = int.Parse(lineElements[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
        }
    }
}
