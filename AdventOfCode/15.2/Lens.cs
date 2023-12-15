namespace _15._2
{
    internal readonly struct Lens
    {
        public string Label { get; }
        public int FocalLength { get; }

        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;
        }
    }
}
