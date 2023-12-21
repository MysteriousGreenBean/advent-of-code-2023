namespace _21._1
{
    internal class Map
    {
        public bool[,] Walkable { get; init; }
        public bool[,] InReach { get; init; }
        public (int X, int Y) Start { get; init; }

        public Map(string[] lines)
        {
            Walkable = new bool[lines.Length, lines[0].Length];
            InReach = new bool[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Walkable[i, j] = lines[i][j] == '.';
                    if (lines[i][j] == 'S')
                    {
                        Walkable[i, j] = true;
                        Start = (i, j);
                    }
                }
            }
        }

        public int PossibleTargets(int steps)
        {
            FieldCache[,] visited = new FieldCache[Walkable.GetLength(0), Walkable.GetLength(1)];
            FillfieldCache(visited);
            var currentPosition = Start;
            ExploreField(currentPosition, steps, visited);
            return visited.Cast<FieldCache>().Count(f => f.InReach);
        }

        void FillfieldCache(FieldCache[,] visited)
        {
            int rows = visited.GetLength(0);
            int cols = visited.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    visited[i, j] = new FieldCache();
                }
            }
        }

        public void ExploreField((int X, int Y) field, int stepsLeft, FieldCache[,] visited)
        {
            if (stepsLeft == -1)
                return;

            if (field.X < 0 || field.X >= Walkable.GetLength(0) || field.Y < 0 || field.Y >= Walkable.GetLength(1))
                return;

            if (visited[field.X, field.Y].Visited && visited[field.X, field.Y].VisitedWithSteps.Contains(stepsLeft))
                return;

            bool canWalk = Walkable[field.X, field.Y];
            if (!canWalk)
                return;

            visited[field.X, field.Y].VisitedWithSteps.Add(stepsLeft);

            if (stepsLeft % 2 == 0)
            {
                visited[field.X, field.Y].InReach = true;
            }

            ExploreField((field.X - 1, field.Y), stepsLeft - 1, visited);
            ExploreField((field.X + 1, field.Y), stepsLeft - 1, visited);
            ExploreField((field.X, field.Y - 1), stepsLeft - 1, visited);
            ExploreField((field.X, field.Y + 1), stepsLeft - 1, visited);
        }

        internal class FieldCache
        {
            public bool Visited => VisitedWithSteps.Count > 0;
            public List<int> VisitedWithSteps { get; set; } = new();
            public bool InReach { get; set; } = false;

            public override string ToString()
            {
                return $"V:{Visited} {string.Join(',', VisitedWithSteps)} In reach:{InReach}";
            }
        }
    }
}
