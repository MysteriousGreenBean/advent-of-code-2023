namespace _16._1
{
    internal class Beam
    {
        public bool IsLooped { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public BeamDirection Direction { get; private set; }

        public Beam(int row, int column, BeamDirection direction)
        {
            Row = row;
            Column = column;
            Direction = direction;
        }

        public void Move(BeamMoveResult moveResult)
        {
            Row = moveResult.NewRow;
            Column = moveResult.NewColumn;
            Direction = moveResult.NewDirection;
        }

        public void MarkAsLooped()
        {
            IsLooped = true;
        }
    }
}
