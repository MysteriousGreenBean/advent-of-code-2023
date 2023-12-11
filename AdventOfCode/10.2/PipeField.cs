namespace _10._2
{
    internal class PipeField
    {
        public char Symbol { get; }
        public int Row { get; }
        public int Column { get; }

        public PipeField(char pipeFieldSymbol, int row, int column)
        {
            this.Symbol = pipeFieldSymbol;
            this.Row = row;
            this.Column = column;
        }   

        public (int row, int column) MoveByPipe(int rowEntry, int columnEntry)
        {
            switch (this.Symbol)
            {
                case '|':
                    if (columnEntry != this.Column)
                        throw new InvalidOperationException($"Invalid column entry: {columnEntry}");
                    if (rowEntry == this.Row + 1)
                        return (this.Row - 1, columnEntry);
                    else if (rowEntry == this.Row - 1)
                        return (this.Row + 1, columnEntry);
                    else
                        throw new InvalidOperationException($"Invalid row entry: {rowEntry}");
                case '-':
                    if (rowEntry != this.Row)
                        throw new InvalidOperationException($"Invalid row entry: {rowEntry}");
                    if (columnEntry == this.Column + 1)
                        return (rowEntry, this.Column - 1);
                    else if (columnEntry == this.Column - 1)
                        return (rowEntry, this.Column + 1);
                    else
                        throw new InvalidOperationException($"Invalid column entry: {columnEntry}");
                case 'L':
                    if (rowEntry == this.Row - 1 && columnEntry == this.Column)
                        return (this.Row, this.Column + 1);
                    else if (rowEntry == this.Row && columnEntry == this.Column + 1)
                        return (this.Row - 1, this.Column);
                    else
                        throw new InvalidOperationException($"Invalid entry: ({rowEntry}, {columnEntry})");
                case 'J':
                    if (rowEntry == this.Row - 1 && columnEntry == this.Column)
                        return (this.Row, this.Column - 1);
                    else if (rowEntry == this.Row && columnEntry == this.Column - 1)
                        return (this.Row - 1, this.Column);
                    else
                        throw new InvalidOperationException($"Invalid entry: ({rowEntry}, {columnEntry})");
                case '7':
                    if (rowEntry == this.Row && columnEntry == this.Column - 1)
                        return (this.Row + 1, this.Column);
                    else if (rowEntry == this.Row + 1 && columnEntry == this.Column)
                        return (this.Row, this.Column - 1);
                    else
                        throw new InvalidOperationException($"Invalid entry: ({rowEntry}, {columnEntry})");
                case 'F':
                    if (rowEntry == this.Row && columnEntry == this.Column + 1)
                        return (this.Row + 1, this.Column);
                    else if (rowEntry == this.Row + 1 && columnEntry == this.Column)
                        return (this.Row, this.Column + 1);
                    else
                        throw new InvalidOperationException($"Invalid entry: ({rowEntry}, {columnEntry})");
                case '.':
                    throw new InvalidOperationException("Cannot enter a ground");
                default:
                    throw new InvalidOperationException($"Invalid pipe field symbol: {this.Symbol}");
            }
        }
    }
}
