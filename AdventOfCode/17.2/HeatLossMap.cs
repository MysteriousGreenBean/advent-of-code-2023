namespace _17._2
{
    internal class HeatLossMap
    {
        private static readonly (int, int) Up = (0, -1);
        private static readonly (int, int) Down = (0, 1);
        private static readonly (int, int) Right = (1, 0);
        private static readonly (int, int) Left = (-1, 0);

        private readonly int[,] map;
        private readonly int columns;
        private readonly int rows;


        public HeatLossMap(string[] mapInput)
        {
            columns = mapInput[0].Length;
            rows = mapInput.Length;
            map = new int[columns, rows];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    map[column, row] = int.Parse(mapInput[row][column].ToString());
                }
            }

        }

        public int DijkstraIt()
        {
            var queue = new PriorityQueue<(int X, int Y, (int Dx, int Dy) Direction, int Steps), int>();

            var visited = new HashSet<string>();

            queue.Enqueue((0, 0, Right, 1), 0);
            queue.Enqueue((0, 0, Down, 1), 0);

            var directions = new List<(int Dx, int Dy)>();

            while (queue.TryDequeue(out var item, out var cost))
            {
                if (item.X == columns - 1 && item.Y == rows - 1 && item.Steps >= 4)
                {
                    return cost;
                }

                directions.Clear();

                if (item.Steps < 3)
                {
                    directions.Add(item.Direction);
                }
                else
                {
                    GetDirections(directions, item.Direction);
                }

                foreach (var direction in directions)
                {
                    var newSteps = direction == item.Direction ? item.Steps + 1 : 0;

                    if (newSteps == 10)
                    {
                        continue;
                    }

                    var (x, y) = (item.X + direction.Dx, item.Y + direction.Dy);

                    if (x < 0 || x == columns || y < 0 || y == rows)
                    {
                        continue;
                    }

                    var key = $"{item.X},{item.Y},{x},{y},{newSteps}";

                    if (visited.Add(key))
                    {
                        queue.Enqueue((x, y, direction, newSteps), cost + map[x, y]);
                    }
                }
            }

            return 0;
        }

        private static void GetDirections(List<(int, int)> directions, (int, int) direction)
        {
            if (direction != Down)
            {
                directions.Add(Up);
            }

            if (direction != Left)
            {
                directions.Add(Right);
            }

            if (direction != Up)
            {
                directions.Add(Down);
            }

            if (direction != Right)
            {
                directions.Add(Left);
            }
        }
    }
}
