using System.Diagnostics.CodeAnalysis;

namespace _23._1
{
    internal record Path
    {
        private static int idCounter = 0;
        public int ID { get; } = idCounter++;
        public List<Field> Fields { get; } = new List<Field>();

        public Path Split()
        {
            var path = new Path();
            foreach (var field in Fields) 
                path.Fields.Add(field);
            return path;
        }

        public override string ToString()
            => string.Join("->", Fields);
    }

    internal class PathComparer : IEqualityComparer<Path>
    {
        public bool Equals(Path? x, Path? y)
            => x?.ID== y?.ID;

        public int GetHashCode([DisallowNull] Path obj)
            => obj.ID;
    }
}
