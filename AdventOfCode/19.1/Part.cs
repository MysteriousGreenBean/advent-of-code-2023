namespace _19._1
{
    internal struct Part
    {
        public int X { get; }
        public int M { get; }
        public int A { get; }
        public int S { get; }

        public Part(string line)
        {
            string[] rates = line.Trim('{', '}').Split(',');
            X = int.Parse(rates[0][2..]);
            M = int.Parse(rates[1][2..]);
            A = int.Parse(rates[2][2..]);
            S = int.Parse(rates[3][2..]);
        }

        public int GetSumOfRates()
            => X + M + A + S;

        public override string ToString()
        {
            return $"{{ x: {X}, m: {M}, a: {A}, s: {S} }}";
        }
    }
}
