namespace _24._2
{
    internal class HailVectors
    {
        private readonly HailVector[] vectors;

        public HailVectors(string[] inputLines)
        {
            vectors = inputLines.Select(l => new HailVector(l)).ToArray();
        }

        public Point? FindGoodPlaceForAThrow()
        {
            for (int x = -vectors.Length + 1; x < vectors.Length; x++)
            {
                for (int y = -vectors.Length + 1; y < vectors.Length; y++)
                {
                    var intersect1 = TryIntersectPos(vectors[1], vectors[0], (x, y));
                    var intersect2 = TryIntersectPos(vectors[2], vectors[0], (x, y));
                    var intersect3 = TryIntersectPos(vectors[3], vectors[0], (x, y));

                    if (!intersect1.IsIntersecting
                         || intersect1.Coords != intersect2.Coords 
                         || intersect1.Coords != intersect3.Coords)
                        continue;

                    for (int z = -vectors.Length + 1; y < vectors.Length; z++)
                    {
                        var intersectZ = vectors[1].Start.Z + intersect1.Time * (vectors[1].Velocities[2] + z);
                        var intersectZ2 = vectors[2].Start.Z + intersect2.Time * (vectors[2].Velocities[2] + z);
                        var intersectZ3 = vectors[3].Start.Z + intersect3.Time * (vectors[3].Velocities[2] + z);
                        
                        if (intersectZ != intersectZ2 || intersectZ != intersectZ3)
                            continue;

                        return new(intersect1.Coords.X, intersect1.Coords.Y, intersectZ);
                    }
                }
            }

            return null;
        }

        private TryIntersectionResult TryIntersectPos(HailVector one, HailVector two, (int x, int y) offset)
        {
            decimal vx1 = one.Velocities[0] + offset.x;
            decimal vy1 = one.Velocities[1] + offset.y;
            decimal vx2 = two.Velocities[0] + offset.x;
            decimal vy2 = two.Velocities[1] + offset.y;

            decimal determinant = (vx1 * -1 * vy2) - (vy1 * -1 * vx2);

            if (determinant == 0) return new(false, new(-1, -1), -1);

            var Qx = (-1 * vy2 * (two.Start.X - one.Start.X)) - (-1 * vx2 * (two.Start.Y - one.Start.Y));
            var Qy = (vx1 * (two.Start.Y - one.Start.Y)) - (vy1 * (two.Start.X - one.Start.X));

            var t = Qx / determinant;

            var Px = (one.Start.X + t * vx1);
            var Py = (one.Start.Y + t * vy1);

            return new(true, new(Px, Py), t);
        }

        internal record TryIntersectionResult(bool IsIntersecting, Coords Coords, decimal Time);

        internal record Coords(decimal X, decimal Y)
        {
            public override string ToString()
                => $"({X}, {Y})";

            public static implicit operator Coords((decimal row, decimal col) coords) => new(coords.row, coords.col);
        }
    }
}
