namespace _24._2
{
    internal class HailVector
    {
        private readonly string unparsed;
        public Point Start { get; }
        public decimal[] Velocities { get; }

        public HailVector(string line)
        {
            unparsed = line;
            string[] strings = line.Split(" @ ");
            string[] start = strings[0].Split(", ");
            Start = new Point(decimal.Parse(start[0]), decimal.Parse(start[1]), decimal.Parse(start[2]));

            string[] velocities = strings[1].Split(", ");
            Velocities = new decimal[]
            {
                decimal.Parse(velocities[0]),
                decimal.Parse(velocities[1]),
                decimal.Parse(velocities[2]),
            };
        }

        public override string ToString()
            => $"Hailstone: {unparsed}";
    }
}
