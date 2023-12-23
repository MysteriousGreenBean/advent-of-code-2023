namespace _23._2
{
    internal class Map
    {
        private readonly int rows;
        private readonly int columns;
        private readonly Dictionary<Coords, Field> map;
        private readonly Dictionary<Coords, Junction> junctions;
        private readonly Coords start;
        private readonly Coords end;

        public Map(string[] lines)
        {
            junctions= new Dictionary<Coords, Junction>();
            map = new Dictionary<Coords, Field>();
            rows = lines.Length;
            columns = lines[0].Length;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    var fieldType = (FieldType)lines[row][col];
                    fieldType = fieldType switch
                    {
                        FieldType.SlopeUp => FieldType.Path,
                        FieldType.SlopeDown => FieldType.Path,
                        FieldType.SlopeLeft => FieldType.Path,
                        FieldType.SlopeRight => FieldType.Path,
                        _ => fieldType
                    };
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
            var startJunction = new Junction(start);
            var endJunction = new Junction(end);
            junctions.Add(start, startJunction);
            junctions.Add(end, endJunction);

            Field currentField = map[start];
            CalculateJunctions(currentField, new HashSet<Field>(), startJunction, 0);

            
            var firstPath = new Path();
            var paths = ExploreJunction(startJunction, firstPath);

            
            return paths.Where(p => p.Junctions.Last().Coords == end).Max(p => p.GetLength());
        }

        private void CalculateJunctions(Field field, HashSet<Field> visitedFields, Junction sourceJunction, int stepsFromSource)
        {
            var fieldsToExplore = new Queue<Field>();
            fieldsToExplore.Enqueue(field);

            //while (fieldsToExplore.Count > 0)
            //{
            //    field = fieldsToExplore.Dequeue();
            //    if (field.Type == FieldType.Forest)
            //        continue
            //}

            //if (field.Type == FieldType.Forest)
            //    return;

            int allFieldsToTraverse = map.Values.Count(f => f.Type == FieldType.Path);

            var coordsCanMoveInto = new List<Coords>();
            Coords nextCoords = field.Coords.Move(Direction.Down);

            if (CanWalkInto(field, nextCoords))
                coordsCanMoveInto.Add(nextCoords);

            nextCoords = field.Coords.Move(Direction.Right);
            if (CanWalkInto(field, nextCoords))
                coordsCanMoveInto.Add(nextCoords);

            nextCoords = field.Coords.Move(Direction.Up);
            if (CanWalkInto(field, nextCoords))
                coordsCanMoveInto.Add(nextCoords);

            nextCoords = field.Coords.Move(Direction.Left);
            if (CanWalkInto(field, nextCoords))
                coordsCanMoveInto.Add(nextCoords);

            if (coordsCanMoveInto.Count > 2 || field.Coords == end)
            {
                if (!junctions.ContainsKey(field.Coords))
                    junctions.Add(field.Coords, new Junction(field.Coords));

                var junction = junctions[field.Coords];
                junction.ConnectedJunctions.Add((sourceJunction, stepsFromSource));
                sourceJunction.ConnectedJunctions.Add((junction, stepsFromSource));
            }

            if (visitedFields.Count == allFieldsToTraverse) 
                return;
            if (visitedFields.Contains(field))
                return;

            visitedFields.Add(field);
            if (field.Coords == end)
                return;

            foreach (var coord in coordsCanMoveInto)
                WalkIn(coord, visitedFields, coordsCanMoveInto.Count > 2);

            void WalkIn(Coords nextCoords, HashSet<Field> visitedFields, bool isJunction)
            {
                if (InBounds(nextCoords))
                {
                    Field nextField = map[nextCoords];
                    if (CanWalkInto(field, nextField))
                    {
                        Junction junction = isJunction ? junctions[field.Coords] : sourceJunction;
                        int stepsFromJunction = isJunction ? 1 : stepsFromSource + 1;
                        CalculateJunctions(nextField, visitedFields, junction, stepsFromJunction);
                    }
                }
            }
        }

        private HashSet<Path> ExploreJunction(Junction currentJunction, Path path)
        {
            var uncoveredPaths = new HashSet<Path>(new PathComparer())
            {
                path
            };

            if (path.Junctions.Contains(currentJunction))
                return uncoveredPaths;

            path.Junctions.Add(currentJunction);
            if (currentJunction.Coords == end)
                return uncoveredPaths;

            foreach (var nextJunction in currentJunction.ConnectedJunctions)
                WalkIn(nextJunction.junction, uncoveredPaths);

            return uncoveredPaths.ToHashSet();

            void WalkIn(Junction nextJunction, HashSet<Path> uncoveredPaths)
            {
                Path newPath = path.Split();
                HashSet<Path> exploredPaths = ExploreJunction(nextJunction, newPath);
                uncoveredPaths.UnionWith(exploredPaths);
            }
        }
        private bool CanWalkInto(Field currentField, Coords targetCoords)
        {
            if (!InBounds(targetCoords))
                return false;

            Field field = map[targetCoords];
            return CanWalkInto(currentField, field);
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

        public static implicit operator Coords((int row, int col) coords) => new(coords.row, coords.col);
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
