namespace _16._2
{
    internal class FieldMap
    {
        public int Rows { get; }
        public int Columns { get; }
        public Field[,] Fields { get; }

        public FieldMap(string[] inputLines) : this(inputLines.Length, inputLines[0].Length)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                    Fields[row, column] = new Field(row, column, inputLines[row][column]);
            }
        }

        private FieldMap(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Fields = new Field[rows, columns];
        }

        private FieldMap(Field[,] fields)
        {
            Rows = fields.GetLength(0);
            Columns = fields.GetLength(1);
            Fields = fields;
        }


        public void SimulateBeam(Beam startingBeam)
        {
            var beams = new List<Beam>() { startingBeam };
            while (beams.Count > 0)
            {
                var beamsToAdd = new List<Beam>();
                foreach (Beam beam in beams)
                {
                    Field field = Fields[beam.Row, beam.Column];
                    BeamMoveResult[] results = field.MoveThroughField(beam);
                    if (results.Length > 0)
                    {
                        beam.Move(results[0]);
                        for (int i = 1; i < results.Length; i++)
                            beamsToAdd.Add(new Beam(results[i].NewRow, results[i].NewColumn, results[i].NewDirection));
                    }
                }
                beams.AddRange(beamsToAdd);
                beamsToAdd.Clear();
                beams.RemoveAll(IsBeamOutOfBounds);
                beams.RemoveAll(beam => beam.IsLooped);
            }

        }

        public int GetEnergizedFieldsCount()
        {
            int energizedFields = 0;
            foreach (Field field in Fields)
                if (field.IsEnergized)
                    energizedFields++;
            return energizedFields;
        }

        private bool IsBeamOutOfBounds(Beam beam) => beam.Row < 0 || beam.Row >= Rows || beam.Column < 0 || beam.Column >= Columns;

        public FieldMap Copy()
        {
            var copy = new FieldMap(Rows, Columns);
            for (int row = 0; row < Fields.GetLength(0); row++)
            {
                for (int column = 0; column < Fields.GetLength(1); column++)
                    copy.Fields[row, column] = Fields[row, column].Copy();
            }
            return copy;
        }
    }
}
