using _24._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var vectors = new HailVectors(inputLines);
Point? startingPoint = vectors.FindGoodPlaceForAThrow();
if (startingPoint == null)
    Console.WriteLine("No good starting point found");
else
{
    Console.WriteLine($"Starting point: {startingPoint}");
    Console.WriteLine($"Coord sum: {startingPoint.X + startingPoint.Y + startingPoint.Z}");
}