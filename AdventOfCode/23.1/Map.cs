namespace _23._1
{
    internal class Map
    {
        private readonly int rows;
        private readonly int columns;
        private readonly Dictionary<Coords, Field> map;
        private readonly Coords start;
        private readonly Coords end;

        public Map(string[] lines)
        {
            map = new Dictionary<Coords, Field>();
            rows = lines.Length;
            columns = lines[0].Length;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    var fieldType = (FieldType)lines[row][col];
                    if (row == 0 && fieldType == FieldType.Path)
                        start = (row, col);
                    if (row == lines.Length - 1 && fieldType == FieldType.Path)
                        end = (row, col);
                    map[(row, col)] = new Field((row, col), fieldType);
                }
            }
            if (start == null || end == null)
                throw new ArgumentException("Lack of start and end point");
        }

        public int FindLongestPath()
        {
            Field currentField = map[start];
            var firstPath = new Path();
            var paths = ExploreField(currentField, firstPath);
            return paths.Where(p => p.Fields.Last().Coords == end).Max(p => p.Fields.Count) - 1;
        }

        private HashSet<Path> ExploreField(Field field, Path path)
        {
            var uncoveredPaths = new HashSet<Path>(new PathComparer())
            {
                path
            };

            if (path.Fields.Contains(field))
                return uncoveredPaths;

            if (field.Type == FieldType.Forest)
                return uncoveredPaths;

            path.Fields.Add(field);
            if (field.Coords == end)
                return uncoveredPaths;

            if (field.Type == FieldType.SlopeLeft)
            {
                Coords nextCoords = field.Coords.Move(Direction.Left);
                WalkIn(nextCoords, uncoveredPaths);
                return uncoveredPaths;
            }

            if (field.Type == FieldType.SlopeRight)
            {
                Coords nextCoords = field.Coords.Move(Direction.Right);
                WalkIn(nextCoords, uncoveredPaths);
                return uncoveredPaths;
            }

            if (field.Type == FieldType.SlopeDown)
            {
                Coords nextCoords = field.Coords.Move(Direction.Down);
                WalkIn(nextCoords, uncoveredPaths);
                return uncoveredPaths;
            }
            if (field.Type == FieldType.SlopeUp)
            {
                Coords nextCoords = field.Coords.Move(Direction.Up);
                WalkIn(nextCoords, uncoveredPaths);
                return uncoveredPaths;
            }

            if (field.Type == FieldType.Path)
            {
                Coords nextCoords = field.Coords.Move(Direction.Down);
                WalkIn(nextCoords, uncoveredPaths);

                nextCoords = field.Coords.Move(Direction.Right);
                WalkIn(nextCoords, uncoveredPaths);

                nextCoords = field.Coords.Move(Direction.Up);
                WalkIn(nextCoords, uncoveredPaths);

                nextCoords = field.Coords.Move(Direction.Left);
                WalkIn(nextCoords, uncoveredPaths);
            }

            return uncoveredPaths.ToHashSet();

            void WalkIn(Coords nextCoords, HashSet<Path> uncoveredPaths)
            {
                if (InBounds(nextCoords))
                {
                    Field nextField = map[nextCoords];
                    if (CanWalkInto(field, nextField))
                    {
                        Path newPath = path.Split();
                        HashSet<Path> exploredPaths = ExploreField(nextField, newPath);
                        uncoveredPaths.UnionWith(exploredPaths);
                    }
                }
            }
        }

        private bool CanWalkInto(Field currentField, Field targetField)
        {
            switch (targetField.Type)
            {
                case FieldType.Path:
                    return true;
                case FieldType.Forest:
                    return false;
                case FieldType.SlopeRight:
                    return currentField.Coords.Move(Direction.Right) == targetField.Coords;
                case FieldType.SlopeLeft:
                    return currentField.Coords.Move(Direction.Left) == targetField.Coords;
                case FieldType.SlopeDown:
                    return currentField.Coords.Move(Direction.Down) == targetField.Coords;
                case FieldType.SlopeUp:
                    return currentField.Coords.Move(Direction.Up) == targetField.Coords;
                default:
                    throw new InvalidDataException();
            }
        }

        public bool InBounds(Coords coords)
        {
            if (map == null) return false;
            if (coords.Row < 0 || coords.Col < 0) return false;
            if (coords.Row >= rows) return false;
            if (coords.Col >= columns) return false;
            return true;
        }
    }

    internal record Field(Coords Coords, FieldType Type)
    {
        public override string ToString()
            => $"{Coords}:{Type}";
    }

    internal record Coords(int Row, int Col)
    {
        public Coords Move(Direction direction)
            => direction switch
            {
                Direction.Up => new Coords(Row - 1, Col),
                Direction.Down => new Coords(Row + 1, Col),
                Direction.Left => new Coords(Row, Col - 1),
                Direction.Right => new Coords(Row, Col + 1),
                    _ => throw new NotSupportedException()
            };

        public override string ToString()
            => $"({Row}, {Col})";

        public static implicit operator Coords((int row, int col) coords) => new (coords.row, coords.col);
    }

    internal enum Direction
    {
        Up, 
        Down,
        Left, 
        Right
    }

    internal enum FieldType
    {
        Path = '.',
        Forest = '#',
        SlopeUp = '^',
        SlopeLeft = '<',
        SlopeRight = '>',
        SlopeDown = 'v'
    }
}
