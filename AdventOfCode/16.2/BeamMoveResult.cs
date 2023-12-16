namespace _16._2
{
    internal struct BeamMoveResult
    {
        public int NewRow { get; }
        public int NewColumn { get; }
        public BeamDirection NewDirection { get; }

        public BeamMoveResult(int newRow, int newColumn, BeamDirection newDirection)
        {
            NewRow = newRow;
            NewColumn = newColumn;
            NewDirection = newDirection;
        }
    }
}
