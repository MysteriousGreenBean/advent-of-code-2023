using System.Diagnostics.CodeAnalysis;

namespace _23._2
{
    internal record Path
    {
        private static int idCounter = 0;
        public int ID { get; } = idCounter++;
        public List<Junction> Junctions { get; } = new List<Junction>();

        public Path Split()
        {
            var path = new Path();
            foreach (var field in Junctions) 
                path.Junctions.Add(field);
            return path;
        }

        public int GetLength()
        {
            int pathLength = 0;
            for (int i = 1; i < Junctions.Count; i++)
            {
                Junction previousJunction = Junctions[i - 1];
                Junction junction = Junctions[i];
                (_, int length) = junction.ConnectedJunctions.First(cj => cj.junction == previousJunction);
                pathLength += length;
            }
            return pathLength;
        }

        public override string ToString()
            => string.Join("->", Junctions);
    }

    internal class PathComparer : IEqualityComparer<Path>
    {
        public bool Equals(Path? x, Path? y)
            => x?.ID== y?.ID;

        public int GetHashCode([DisallowNull] Path obj)
            => obj.ID;
    }
}
