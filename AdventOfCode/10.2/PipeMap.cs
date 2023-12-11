namespace _10._2
{
    internal class PipeMap
    {
        public PipeField[,] PipeFields { get; }

        public PipeField StartingPipeField { get; }

        public PipeMap(string[] pipeMapLines)
        {
            this.PipeFields = new PipeField[pipeMapLines.Length, pipeMapLines[0].Length];
            PipeField? startingPipeField = null;
            for (int row = 0; row < pipeMapLines.Length; row++)
            {
                for (int column = 0; column < pipeMapLines[row].Length; column++)
                {
                    this.PipeFields[row, column] = new PipeField(pipeMapLines[row][column], row, column);
                    if (pipeMapLines[row][column] == 'S')
                        startingPipeField = this.PipeFields[row, column];
                }
            }

            PipeField leftField = PipeFields[startingPipeField!.Row, startingPipeField!.Column - 1];
            bool leftFieldConnects = leftField.Symbol == '-' || leftField.Symbol == 'L' || leftField.Symbol == 'F';
            PipeField rightField = PipeFields[startingPipeField!.Row, startingPipeField!.Column + 1];
            bool rightFieldConnects = rightField.Symbol == '-' || rightField.Symbol == 'J' || rightField.Symbol == '7';
            PipeField? topField = startingPipeField.Row > 0 ? PipeFields[startingPipeField!.Row - 1, startingPipeField!.Column] : null;
            bool topFieldConnects = topField == null ? false : topField.Symbol == '|' || topField.Symbol == 'F' || topField.Symbol == '7';
            PipeField bottomField = PipeFields[startingPipeField!.Row + 1, startingPipeField!.Column];
            bool bottomFieldConnects = bottomField.Symbol == '|' || bottomField.Symbol == 'J' || bottomField.Symbol == 'L';

            this.StartingPipeField = new PipeField(DeduceStartReplacement(leftFieldConnects, rightFieldConnects, topFieldConnects, bottomFieldConnects), startingPipeField!.Row, startingPipeField!.Column);
            this.PipeFields[startingPipeField!.Row, startingPipeField!.Column] = this.StartingPipeField;
        }

        public PipeField MoveByPipe(PipeField fieldToMoveBy, PipeField entryField)
        {
            (int row, int column) = fieldToMoveBy.MoveByPipe(entryField.Row, entryField.Column);
            return this.PipeFields[row, column];
        }

        public PipeField MoveByStartingPipe()
        {
            (int row, int column) = (0, 0);
            switch (this.StartingPipeField.Symbol)
            {
                case '-':
                    (row, column) = this.StartingPipeField.MoveByPipe(this.StartingPipeField.Row, this.StartingPipeField.Column - 1);
                    break;
                case '|':
                case 'L':
                case 'J':
                    (row, column) = this.StartingPipeField.MoveByPipe(this.StartingPipeField.Row - 1, this.StartingPipeField.Column);
                    break;
                case '7':
                case 'F':
                    (row, column) = this.StartingPipeField.MoveByPipe(this.StartingPipeField.Row + 1, this.StartingPipeField.Column);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid starting pipe field symbol: {this.StartingPipeField.Symbol}");
            }
            return this.PipeFields[row, column];
        }

        private char DeduceStartReplacement(bool leftFieldConnects, bool rightFieldConnects, bool topFieldConnects, bool bottomFieldConnects)
        {
            int numberOfConnections = (new bool[] { leftFieldConnects, rightFieldConnects, topFieldConnects, bottomFieldConnects }).Where(v => v == true).Count();
            if (numberOfConnections != 2)
                throw new InvalidOperationException($"Invalid number of connections: {numberOfConnections}");

            if (leftFieldConnects && rightFieldConnects)
                return '-';
            if (topFieldConnects && bottomFieldConnects)
                return '|';
            if (leftFieldConnects && topFieldConnects)
                return 'J';
            if (leftFieldConnects && bottomFieldConnects)
                return '7';
            if (rightFieldConnects && topFieldConnects)
                return 'L';
            if (rightFieldConnects && bottomFieldConnects)
                return 'F';

            throw new InvalidOperationException($"Invalid connections: {leftFieldConnects}, {rightFieldConnects}, {topFieldConnects}, {bottomFieldConnects}");
        }

        public PipeField[] GetNeighbours(PipeField pipeField)
        {
            var neighbours = new List<PipeField>();
            if (pipeField.Row > 0)
                neighbours.Add(this.PipeFields[pipeField.Row - 1, pipeField.Column]);
            if (pipeField.Row < this.PipeFields.GetLength(0) - 1)
                neighbours.Add(this.PipeFields[pipeField.Row + 1, pipeField.Column]);
            if (pipeField.Column > 0)
                neighbours.Add(this.PipeFields[pipeField.Row, pipeField.Column - 1]);
            if (pipeField.Column < this.PipeFields.GetLength(1) - 1)
                neighbours.Add(this.PipeFields[pipeField.Row, pipeField.Column + 1]);

            return neighbours.ToArray();
        }
    }
}
