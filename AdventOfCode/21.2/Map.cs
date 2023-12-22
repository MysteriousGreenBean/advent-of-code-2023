namespace _21._2
{
    internal class Map
    {
        public bool[,] Walkable { get; init; }
        public Coords Start { get; init; }
        public Dictionary<Coords, FieldCache> Fields { get; init; } = new();

        public Map(string[] lines)
        {
            Walkable = new bool[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Walkable[i, j] = lines[i][j] == '.';
                    if (lines[i][j] == 'S')
                    {
                        Walkable[i, j] = true;
                        Start = new Coords() { X = i, Y = j, BoardX = 0, BoardY = 0 };
                    }
                }
            }
        }

        public int PossibleTargets(int steps)
        {
            var currentPosition = Start;
            int count = ExploreField(currentPosition, steps);

            return count;
        }


        public int ExploreField(Coords field, int stepsLeft)
        {
            if (stepsLeft == -1)
                return 0;

            if (field.X < 0)
            {
                field.X = Walkable.GetLength(0) - 1;
                field.BoardX--;
            }
            if (field.X >= Walkable.GetLength(0))
            {
                field.X = 0;
                field.BoardX++;
            }
            if (field.Y < 0)
            {
                field.Y = Walkable.GetLength(1) - 1;
                field.BoardY--;
            }
            if (field.Y >= Walkable.GetLength(1))
            {
                field.Y = 0;
                field.BoardY++;
            }

            bool canWalk = Walkable[field.X, field.Y];
            if (!canWalk)
                return 0;

            if (!Fields.ContainsKey(field))
                Fields[field] = new FieldCache()
                {
                    VisitedWithSteps = new List<int>(),
                    Walkable = Walkable[field.X, field.Y],
                };

            if (Fields[field].Visited && Fields[field].VisitedWithSteps.Contains(stepsLeft))
                return 0;


            int inReachConunt = 0;

            if (stepsLeft % 2 == 0)
            {
                Fields[field].InReach = true;
                if (!Fields[field].Visited)
                    inReachConunt++;
            }

            Fields[field].VisitedWithSteps.Add(stepsLeft);
            inReachConunt += ExploreField(new Coords() { BoardX = field.BoardX, BoardY = field.BoardY, X = field.X - 1, Y = field.Y }, stepsLeft - 1);
            inReachConunt += ExploreField(new Coords() { BoardX = field.BoardX, BoardY = field.BoardY, X = field.X + 1, Y = field.Y }, stepsLeft - 1);
            inReachConunt += ExploreField(new Coords() { BoardX = field.BoardX, BoardY = field.BoardY, X = field.X, Y = field.Y - 1 }, stepsLeft - 1);
            inReachConunt += ExploreField(new Coords() { BoardX = field.BoardX, BoardY = field.BoardY, X = field.X, Y = field.Y + 1 }, stepsLeft - 1);

            return inReachConunt;
        }

        internal struct Coords
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int BoardX { get; set; }
            public int BoardY { get; set; }

            public override string ToString()
            {
                return $"({X},{Y}) B: ({BoardX}, {BoardY})";
            }
        }

        internal class FieldCache
        {
            public bool Visited => VisitedWithSteps.Count > 0;
            public List<int> VisitedWithSteps { get; set; } = new();
            public bool InReach { get; set; } = false;
            public bool Walkable { get; set; } = true;

            public override string ToString()
            {
                return $"Walkable: {Walkable} V:{Visited} {string.Join(',', VisitedWithSteps)} In reach:{InReach}";
            }
        }
    }
}
