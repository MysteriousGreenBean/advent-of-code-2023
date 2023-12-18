namespace _18._1
{
    internal class PointRow
    {
        public List<Point> Points { get; init; } = new List<Point>();

        public int Length => Points.Count;

        public Point this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public void Add(Point point)
        {
            Points.Add(point);
        }

        public void Insert(int index, Point point)
        {
            Points.Insert(index, point);
        }

        public override string ToString()
        {
            return string.Join("", Points.Select(p => p.Symbol));
        }
    }
}
