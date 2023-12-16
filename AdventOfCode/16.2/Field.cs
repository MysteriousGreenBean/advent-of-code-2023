namespace _16._2
{
    internal class Field
    {
        public int Row { get; }
        public int Column { get; }
        public bool IsEnergized { get; private set; }
        public FieldType Type { get; }
        public List<BeamDirection> PresentDirections { get; private set; }

        public Field(int row, int column, char symbol)
        {
            Row = row;
            Column = column;
            Type = (FieldType)symbol;
            PresentDirections = new List<BeamDirection>();
        }

        public BeamMoveResult[] MoveThroughField(Beam beam)
        {
            if (PresentDirections.Contains(beam.Direction))
            {
                beam.MarkAsLooped();
                return Array.Empty<BeamMoveResult>();
            }

            PresentDirections.Add(beam.Direction);
            IsEnergized = true;
            switch (Type)
            {
                case FieldType.Empty:
                    return new BeamMoveResult[] { GetNextFieldEmpty(Row, Column, beam.Direction) };
                case FieldType.MirrorRight:
                    return new BeamMoveResult[] { GetNextFieldMirrorRight(beam.Direction) };
                case FieldType.MirrorLeft:
                    return new BeamMoveResult[] { GetNextFieldMirrorLeft(beam.Direction) };
                case FieldType.SplitterHorizontal:
                    return GetNextFieldSplitterHorizontal(beam.Direction);
                case FieldType.SplitterVertical:
                    return GetNextFieldSplitterVertical(beam.Direction);
                default:
                    throw new Exception("Unknown field type");
            }
        }

        private BeamMoveResult GetNextFieldEmpty(int row, int column, BeamDirection direction)
        {
            switch (direction)
            {
                case BeamDirection.Left:
                    return new BeamMoveResult(row, column - 1, BeamDirection.Left);
                case BeamDirection.Right:
                    return new BeamMoveResult(row, column + 1, BeamDirection.Right);
                case BeamDirection.Up:
                    return new BeamMoveResult(row - 1, column, BeamDirection.Up);
                case BeamDirection.Down:
                    return new BeamMoveResult(row + 1, column, BeamDirection.Down);
                default:
                    throw new Exception("Unknown beam direction");
            }
        }

        private BeamMoveResult GetNextFieldMirrorRight(BeamDirection direction)
        {
            switch (direction)
            {
                case BeamDirection.Left:
                    return new BeamMoveResult(Row + 1, Column, BeamDirection.Down);
                case BeamDirection.Right:
                    return new BeamMoveResult(Row - 1, Column, BeamDirection.Up);
                case BeamDirection.Up:
                    return new BeamMoveResult(Row, Column + 1, BeamDirection.Right);
                case BeamDirection.Down:
                    return new BeamMoveResult(Row, Column - 1, BeamDirection.Left);
                default:
                    throw new Exception("Unknown beam direction");
            }
        }

        private BeamMoveResult GetNextFieldMirrorLeft(BeamDirection direction)
        {
            switch (direction)
            {
                case BeamDirection.Left:
                    return new BeamMoveResult(Row - 1, Column, BeamDirection.Up);
                case BeamDirection.Right:
                    return new BeamMoveResult(Row + 1, Column, BeamDirection.Down);
                case BeamDirection.Up:
                    return new BeamMoveResult(Row, Column - 1, BeamDirection.Left);
                case BeamDirection.Down:
                    return new BeamMoveResult(Row, Column + 1, BeamDirection.Right);
                default:
                    throw new Exception("Unknown beam direction");
            }
        }

        private BeamMoveResult[] GetNextFieldSplitterHorizontal(BeamDirection direction)
        {
            switch (direction)
            {
                case BeamDirection.Right:
                case BeamDirection.Left:
                    return new BeamMoveResult[]
                    {
                        GetNextFieldEmpty(Row, Column, direction),
                    };
                case BeamDirection.Down:
                case BeamDirection.Up:
                    return new BeamMoveResult[]
                    {
                        new BeamMoveResult(Row, Column - 1, BeamDirection.Left),
                        new BeamMoveResult(Row, Column + 1, BeamDirection.Right)
                    };
                default:
                    throw new Exception("Unknown beam direction");
            }
        }

        private BeamMoveResult[] GetNextFieldSplitterVertical(BeamDirection direction)
        {
            switch (direction)
            {
                case BeamDirection.Up:
                case BeamDirection.Down:
                    return new BeamMoveResult[]
                    {
                        GetNextFieldEmpty(Row, Column, direction),
                    };
                case BeamDirection.Left:
                case BeamDirection.Right:
                    return new BeamMoveResult[]
                    {
                        new BeamMoveResult(Row - 1, Column, BeamDirection.Up),
                        new BeamMoveResult(Row + 1, Column, BeamDirection.Down)
                    };
                default:
                    throw new Exception("Unknown beam direction");
            }
        }

        public Field Copy()
        {
            return new Field(Row, Column, (char)Type);
        }
    }

    public enum FieldType
    {
        Empty = '.',
        MirrorRight = '/',
        MirrorLeft = '\\',
        SplitterHorizontal = '-',
        SplitterVertical = '|',
    }
}
