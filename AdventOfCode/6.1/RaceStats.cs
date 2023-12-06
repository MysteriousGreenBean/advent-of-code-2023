namespace _6._1
{
    internal class RaceStats
    {
        public int Time { get; init; }
        public int Distance { get; init; }

        // It devolves into a quadratic inequality
        // It's -x^2 + xy - z > 0
        // x being our button holding time/speed
        // y being time of the race
        // z being distance to beat
        public (int rangeStart, int rangeEnd) GetRangeOfValidButtonHoldingTimes()
        {
            double epsilon = 0.000001;
            double delta = Math.Pow(Time, 2) - 4 * -1 * -Distance;
            if (delta < 0)
                throw new InvalidOperationException("No valid button holding times");
            int x1 = (int)Math.Floor(((Time + Math.Sqrt(delta)) / 2) - epsilon);
            int x2 = (int)Math.Ceiling(((Time - Math.Sqrt(delta)) / 2) + epsilon);

            return (x2, x1);
        }
    }
}
