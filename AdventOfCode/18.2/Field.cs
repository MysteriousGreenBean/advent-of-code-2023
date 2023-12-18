
namespace _18._2
{
    internal class Field
    {
        private List<DigVector> digVectors = new List<DigVector>();

        public void PerformDigInstructions(IEnumerable<DigInstruction> instructions)
        {
            foreach (var instruction in instructions)
                PerformDigInstruction(instruction);

            digVectors[^1].EndVector = digVectors[0];
            digVectors[0].StartVector = digVectors[^1];
        }

        public void PerformDigInstruction(DigInstruction dig)
        {
            var startingPoint = digVectors.Count == 0 ? new Point(0, 0) : digVectors[^1].End;
            var startingVector = digVectors.Count == 0 ? null : digVectors[^1];

            var newVector = new DigVector(startingPoint, Dig(startingPoint, dig.Meters, dig.Direction), dig.Direction);
            if (digVectors.Count > 0)
                digVectors[^1].EndVector = newVector;
            newVector.StartVector = startingVector;
            digVectors.Add(newVector);
        }

        private Point Dig(Point startingPoint, long meters, DigDirection direction)
        {
            switch (direction)
            {
                case DigDirection.Up:
                    return DigUp(startingPoint, meters);
                case DigDirection.Down:
                    return DigDown(startingPoint, meters);
                case DigDirection.Left:
                    return DigLeft(startingPoint, meters);
                case DigDirection.Right:
                    return DigRight(startingPoint, meters);
                default:
                    throw new ArgumentException("Invalid direction");
            }
        }
        private Point DigUp(Point startingPoint, long meters)
        => new (startingPoint.X, startingPoint.Y - meters);

        private Point DigDown(Point startingPoint, long meters)
            => new (startingPoint.X, startingPoint.Y + meters);

        private Point DigLeft(Point startingPoint, long meters)
            => new (startingPoint.X - meters, startingPoint.Y);
        private Point DigRight(Point startingPoint, long meters)
            => new (startingPoint.X + meters, startingPoint.Y);

        public double CalculateLagoonArea()
        {
            List<Point> vertices = digVectors.Select(v => v.Start).ToList();
            vertices.Add(digVectors.First().Start); 

            double area = 0;
            int n = vertices.Count();
            for (int i = 0; i < vertices.Count - 1; i++)
                area += vertices[i].X * vertices[(i + 1) % n].Y - vertices[(i + 1) % n].X * vertices[i].Y;

            area = Math.Abs(area) / 2L;

            return area + CalculateLagoonPerimeter()/2 + 1;
        }

        public double CalculateLagoonPerimeter()
        {
            double perimeter = 0.0;

            foreach (var vector in digVectors)
            {
                perimeter += CalculateDistance(vector.Start, vector.End);
            }

            return perimeter;
        }

        private double CalculateDistance(Point a, Point b)
        {
            double deltaX = b.X - a.X;
            double deltaY = b.Y - a.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
