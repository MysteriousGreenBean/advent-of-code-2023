namespace _6._2
{
    internal class RaceStats
    {
        public long Time { get; init; }
        public long Distance { get; init; }

        // It devolves into a quadratic inequality
        // It's -x^2 + xy - z > 0
        // x being our button holding time/speed
        // y being time of the race
        // z being distance to beat
        public (long rangeStart, long rangeEnd) GetRangeOfValidButtonHoldingTimes()
        {
            double epsilon = 0.000001;
            double delta = Math.Pow(Time, 2) - 4 * -1 * -Distance;
            if (delta < 0)
                throw new InvalidOperationException("No valid button holding times");
            long x1 = (long)Math.Floor(((Time + Math.Sqrt(delta)) / 2) - epsilon);
            long x2 = (long)Math.Ceiling(((Time - Math.Sqrt(delta)) / 2) + epsilon);

            return (x2, x1);
        }
    }
}
