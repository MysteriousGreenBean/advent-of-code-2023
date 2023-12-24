namespace _24._1
{
    internal record Point(decimal X, decimal Y, decimal Z)
    {
        public override string ToString()
            => $"({X},{Y},{Z})";
    }
}
