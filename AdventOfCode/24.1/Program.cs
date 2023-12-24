using _24._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var vectors = inputLines.Select(l => new HailVector(l)).ToArray();
decimal minX = 200000000000000;
decimal maxX = 400000000000000;
decimal minY = 200000000000000;
decimal maxY = 400000000000000;
int intersecting = 0;
for (int i = 0; i < vectors.Length; i++)
{
    for (int j = i + 1; j < vectors.Length; j++)
    {
        var vector1 = vectors[i];
        var vector2 = vectors[j];
        Point? intersection = vector1.IntersectionPoint(vector2);
        if (intersection != null)
        {
            if (intersection.X >= minX && intersection.X <= maxX 
                && intersection.Y >= minY && intersection.Y <= maxY)
            {
                intersecting++;
            }
        }
    }
}
Console.WriteLine(intersecting);