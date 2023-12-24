using Microsoft.VisualBasic;

namespace _24._1
{
    internal class HailVector
    {
        private readonly string unparsed;
        public Point Start { get; }
        public decimal[] Velocities { get; }

        public HailVector(string line)
        {
            unparsed = line;
            string[] strings = line.Split(" @ ");
            string[] start = strings[0].Split(", ");
            Start = new Point(long.Parse(start[0]), long.Parse(start[1]), long.Parse(start[2]));

            string[] velocities = strings[1].Split(", ");
            Velocities = new decimal[]
            {
                int.Parse(velocities[0]),
                int.Parse(velocities[1]),
                int.Parse(velocities[2]),
            };
        }

        // n - nanosecond
        // Px1(n) = x1 + n*v1
        // Py1(n) = y1 + n*v2
        // Px2(n) = x2 + n*v3
        // Py2(n) = y2 + n*v4
        // Find n that Px1(n1) == Px2(n2) && Py1(n1) == Py2(n2)
        // x1 + n1*v1 == x2 + n2*v3
        // y1 + n1*v2 == y2 + n2*v4
        // n1*v1 = x2 - x1 + n2*v3 | n1 = (x2 - x1 + n2*v3)/v1
        // y1 + ((x2 - x1 + n2*v3)*v2)/v1 = y2 + n2*v4
        // VYX1 = v2/v1
        // y1 + x2*VYX1 - x1*VYX1 + n2*v3*VYX1 = y2 + n2*v4
        // y1 - y2 + x2*VYX1 - x1*VYX1 = n2*v4  - n2*v3*VYX1
        // y1 - y2 + VYX1(x2-x1) = n2*(v4 - v3*VYX1)
        // n2 = (y1 - y2 + VYX1(x2-x1))/(v4 - v3*VYX1)

        public Point? IntersectionPoint(HailVector other)
        {
            decimal x1 = Start.X;
            decimal y1 = Start.Y;
            decimal x2 = other.Start.X;
            decimal y2 = other.Start.Y;
            decimal v1 = Velocities[0];
            decimal v2 = Velocities[1];
            decimal v3 = other.Velocities[0];
            decimal v4 = other.Velocities[1];

            decimal n1 = decimal.MinValue;
            decimal n2 = decimal.MinValue;
            decimal down = v2*v3 - v1*v4;
            if (down != 0 && v3 != 0)
            {
                n1 = (-v3 * y1 + v3 * y2 + v4 * x1 - v4 * x2) / down;
                n2 = (-v1 * y1 + v1 * y2 + v2 * x1 - v2 * x2) / down;
            }
            else if (v3 == 0 && v1 != 0 && v4 != 0)
            {
                n1 = (x2 - x1) / v1;
                n2 = (v1 * (y1 - y2) + v2 * (x2 - x1)) / (v1 * v4);
            }
            
            if (n1 == decimal.MinValue && v4 == 0 && v3 == 0 && y1 != y2 && v2*x1 != v2*x2 && v1 == (v2 * (x1 - x2) / y1 - y2))
            {
                n1 = (y2 - y1) / v2;
            }

            if (n2 == decimal.MinValue && x1 == x2 && v3 == 0 && v1 == 0 && v4 != 0 && y1 != y2)
            {
                n2 = (n1 * v2 + y1 - y2) / v4;
            }

            if (n2 == decimal.MinValue && y1 == y2 && v4==0 && v2==0 && v3 != 0 && x1 != x2)
            {
                n2 = (n1*v1 + x1 -x2)/v3;
            }

            if (n1 < 0 || n2 < 0)
                return null;

            decimal x = Px(n1);
            //if (other.Px(n2) != x)
            //    throw new Exception("Something went wrong in calculation");

            decimal y = Py(n1);
            //if (other.Py(n2) != y)
            //    throw new Exception("Something went wrong in calculation");

            return new Point(x, y, 0);
        }

        private decimal Px(decimal n)
            => Start.X + Velocities[0] * n;

        private decimal Py(decimal n)
            => Start.Y + Velocities[1] * n;


        public override string ToString()
            => $"Hailstone: {unparsed}";
    }
}
