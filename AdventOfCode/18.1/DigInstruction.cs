namespace _18._1
{
    internal class DigInstruction
    {
        public DigDirection Direction { get; }
        public int Meters { get; }

        public DigInstruction(string line)
        {
            string[] lineElements = line.Split(' ');
            Direction = (DigDirection)lineElements[0][0];
            Meters = int.Parse(lineElements[1]);
        }
    }

    internal enum DigDirection
    {
        Up = 'U',
        Down = 'D',
        Left = 'L',
        Right = 'R'
    }
}
