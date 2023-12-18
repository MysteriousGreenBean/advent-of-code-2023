namespace _18._2
{
    internal class DigVector
    {
        public DigVector? StartVector { get; set; }
        public Point Start { get; }
        public DigVector? EndVector { get; set; }
        public Point End { get; }

        public bool IsHorizontal => Start.Y == End.Y;
        public bool IsEdge => StartVector!.Direction == EndVector!.Direction;
        public DigDirection Direction { get; }

        public double Length => IsHorizontal ? Math.Abs(Start.X - End.X) : Math.Abs(Start.Y - End.Y);

        public DigVector(Point start, Point end, DigDirection direction) => (Start, End, Direction) = (start, end, direction);

        public override string ToString() => $"{Start} -> {End}";
    }

    internal enum DigDirection
    {
        Up = '3',
        Down = '1',
        Left = '2',
        Right = '0'
    }
}
