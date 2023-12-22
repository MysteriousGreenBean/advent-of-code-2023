using System.Diagnostics.CodeAnalysis;

namespace _22._2
{
    internal class Brick
    {
        private static int idCounter = 1;
        private readonly int id = idCounter++;
        public Coords Start { get; private set; }
        public Coords End { get; private set; }

        public Brick(Coords start, Coords end)
        {
            Start = start;
            End = end;
        }

        public void MoveDown()
        {
            Start = Start.MoveDown();
            End = End.MoveDown();
        }

        public bool IsAnythingBeneathMe(Brick[] otherBricks)
        {
            if (StandsOnGround())
            {
                return true;
            }

            foreach (var otherBrick in otherBricks)
            {
                if (otherBrick == this)
                {
                    continue;
                }

                if (IsBeneath(otherBrick))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsBeneath(Brick otherBrick)
        {
            if (otherBrick == this)
                return false;

            if (otherBrick.Start.Z + 1 == End.Z || otherBrick.End.Z + 1 == End.Z || otherBrick.Start.Z + 1 == Start.Z || otherBrick.End.Z + 1 == Start.Z)
                return RangesCross(otherBrick.Start.X, otherBrick.End.X, Start.X, End.X)
                       && RangesCross(otherBrick.Start.Y, otherBrick.End.Y, Start.Y, End.Y);

            return false;
        }

        private bool StandsOnGround()
            => Start.Z == 1 || End.Z == 1;

        private bool RangesCross(int start1, int end1, int start2, int end2)
            => Enumerable.Range(start1, end1 - start1 + 1).Intersect(Enumerable.Range(start2, end2 - start2 + 1)).Any();

        public override string ToString()
            => $"{(char)(id+64)}:({Start}~{End})";
    }

    internal class BrickComparer : IEqualityComparer<Brick>
    {
        public bool Equals(Brick? x, Brick? y)
        {
            if (x is null && y is not null)
                return false;
            if (x is not null && y is null)
                return false;
            if (x is null && y is null)
                return true;

            if (x!.Start == y!.Start && x!.End == y!.End)
                return true;

            if (x!.Start == y!.End && x!.End == y!.Start)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Brick obj)
        {
            return obj.GetHashCode();
        }
    }

    internal record Coords(int X, int Y, int Z)
    {
        public Coords MoveDown()
            => this with { Z = Z - 1 };

        public override string ToString()
            => $"({X}, {Y}, {Z})";
    }

    internal enum Axis
    {
        X,
        Y,
        Z
    }
}
