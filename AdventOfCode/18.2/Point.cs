namespace _18._2
{
    internal class Point
    {

        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y) => (X, Y) = (x, y);

        public override string ToString() => $"({X}, {Y})";

        public static implicit operator (double, double)(Point point) => (point.X, point.Y);
        public static implicit operator Point((double x, double y) point) => new(point.x, point.y);

        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    }
}
