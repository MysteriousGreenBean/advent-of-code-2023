using System.Text.RegularExpressions;

namespace _18._1
{
    internal class Field
    {
        public int Rows { get; private set; } = 0;
        public int Columns { get; private set; } = 0;
        private int currentRow = 0;
        private int currentColumn = 0;


        public List<PointRow> Map { get; } = new List<PointRow>() { 
            new PointRow()
        };


        public void PerformDigInstruction(DigInstruction dig)
        {
            switch (dig.Direction)
            {
                case DigDirection.Up:
                    DigUp(dig.Meters);
                    break;
                case DigDirection.Down:
                    DigDown(dig.Meters);
                    break;
                case DigDirection.Left:
                    DigLeft(dig.Meters);
                    break;
                case DigDirection.Right:
                    DigRight(dig.Meters);
                    break;
            }
        }

        private void DigUp(int meters)
        {
            while (currentRow - meters < 1)
            {
                AddRow(true);
                currentRow++;
            }

            Map[currentRow][currentColumn].Symbol = '^';
            Map[currentRow][currentColumn].Direction = DigDirection.Up;
            for (int i = 0; i < meters; i++)
            {
                currentRow--;
                Map[currentRow][currentColumn].Symbol = '^';
                Map[currentRow][currentColumn].Direction = DigDirection.Up;
            }
        }

        private void DigDown(int meters)
        {
            while (currentRow + meters >= Rows - 1)
            {
                AddRow(false);
            }

            Map[currentRow][currentColumn].Symbol = 'v';
            Map[currentRow][currentColumn].Direction = DigDirection.Down;
            for (int i = 0; i < meters; i++)
            {
                currentRow++;
                Map[currentRow][currentColumn].Symbol = 'v';
                Map[currentRow][currentColumn].Direction = DigDirection.Down;
            }
        }

        private void DigLeft(int meters)
        {
            if (currentColumn == 0)
                AddColumn(true);

            while (currentColumn - meters < 1)
            {
                AddColumn(true);
                currentColumn++;
            }

            if (Map[currentRow][currentColumn].Direction.HasValue)
            {
                meters--;
                currentColumn--;
            }

            for (int i = 0; i < meters; i++)
            {
                Map[currentRow][currentColumn].Symbol = '<';
                Map[currentRow][currentColumn].Direction = DigDirection.Left;
                currentColumn--;
            }
        }

        private void DigRight(int meters)
        {
            while (currentColumn + meters >= Columns - 1)
            {
                AddColumn(false);
            }

            if (Map[currentRow][currentColumn].Direction.HasValue)
            {
                meters--;
                currentColumn++;
            }

            for (int i = 0; i < meters; i++)
            {
                Map[currentRow][currentColumn].Symbol = '>';
                Map[currentRow][currentColumn].Direction = DigDirection.Right;
                currentColumn++;
            }
        }


        private void AddRow(bool shouldBeAddedFirst)
        {
            var row = new PointRow();
            for (int i = 0; i < Columns; i++)
                row.Points.Add(new Point() { Symbol = '.', Direction = null });
            if (shouldBeAddedFirst)
                Map.Insert(0, row);
            else
                Map.Add(row);
            Rows++;
        }

        private void AddColumn(bool shouldBeAddedFirst)
        {
            if (shouldBeAddedFirst)
            {
                foreach (var row in Map)
                    row.Insert(0, new Point() { Symbol = '.', Direction = null });
            }
            else
            {
                foreach (var row in Map)
                    row.Add(new Point() { Symbol = '.', Direction = null });
            }
            Columns++;
        }

        public void DigInside()
        {
            var insideRegex = new Regex(@"((\^|\^[<>]+?\^).+?(v|(v[<>]+?v))\.)|((v|v[<>]+?v).+?(\^|(\^[<>]+?\^))\.)|(\^.+?v\.)|(v.+?\^\.)");
            foreach (PointRow pointRow in Map)
            {
                string row = pointRow.ToString();
                var matches = insideRegex.Matches(row);
                foreach (Match match in matches)
                {
                    int insideStartIndex = match.Index;
                    int insideEndIndex = insideStartIndex + match.Length - 1;
                    for (int i = insideStartIndex + 1; i < insideEndIndex; i++)
                    {
                        pointRow[i].Symbol = '#';
                    }
                }
                for (int i = 0; i< row.Length;i++)
                {
                    if (row[i] == '^' || row[i] == 'v' || row[i] == '<' || row[i] == '>')
                        pointRow[i].Symbol = '#';
                }
            }
        }



        public int CalculateCubicMeters()
        {
            int cubicMeters = 0;
            foreach (string row in Map.Select(l => l.ToString()))
            {
                cubicMeters += row.Count(c => c == '#');
            }
            return cubicMeters;
        }

        public void Print(string printPath)
        {
            File.WriteAllLines(printPath, Map.Select(row => row.ToString()).ToArray());
        }
    }
}
