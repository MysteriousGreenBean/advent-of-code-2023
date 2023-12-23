namespace _23._2
{
    internal record Junction(Coords Coords) : Field(Coords, FieldType.Path)
    {
        public HashSet<(Junction junction, int length)> ConnectedJunctions { get; } = new HashSet<(Junction junction, int length)>();
    }
}
