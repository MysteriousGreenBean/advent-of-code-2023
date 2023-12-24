namespace _24._2
{
    internal record Point(decimal X, decimal Y, decimal Z)
    {
        public override string ToString()
            => $"({X},{Y},{Z})";
    }
}
